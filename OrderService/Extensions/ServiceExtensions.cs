using System.Reflection;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrderService.Consumers;
using OrderService.Data.DbContexts;
using OrderService.Repositories;
using OrderService.Services;
using RabbitMQ.Client;
using Shared.Helpers;
using Shared.MassTransits;
using Shared.MassTransits.Contracts;
using Shared.Validations;

namespace OrderService.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "http://localhost:5237";
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = CryptographyHelper.CreateRsaKey()
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated successfully.");
                        return Task.CompletedTask;
                    }
                };
            });
        return services;
    }
    
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );
        });
        return services;
    }
    public static IServiceCollection ConfigureMediator(this IServiceCollection services)
    {
        services.AddMediatR(configure =>
        {
            configure.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        return services;
    }
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        services.AddDbContext<OrderDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.EnableSensitiveDataLogging(false);
        });
        return services;
    }

    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IShipperSimulation, ShipperSimulation>();
        services.AddScoped<IUnitOfRepository, UnitOfRepository>();
        
        return services;
    }
    
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthApi", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
        return services;
    }
    public static IServiceCollection AddCustomMassTransitRegistration(this IServiceCollection services)
    {
        services.AddMassTransitRegistration(registrationConfigure: (ctx, cfg) =>
        {
            cfg.ReceiveEndpoint(new KebabCaseEndpointNameFormatter(false).SanitizeName(nameof(CreateSnapshotUser)), e =>
            {
                e.ConfigureConsumeTopology = false;
                e.ConfigureConsumer <CreateUserConsumer>(ctx);
            });
            cfg.ReceiveEndpoint(new KebabCaseEndpointNameFormatter(false).SanitizeName(nameof(UpdateSnapshotUser)), e =>
            {
                e.ConfigureConsumeTopology = false;
                e.ConfigureConsumer <UpdateUserConsumer>(ctx);
            });
        });
        return services;
    }
}