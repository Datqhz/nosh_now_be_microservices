using System.Reflection;
using CoreService.Consumers;
using CoreService.Data.DbContexts;
using CoreService.Repositories;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Helpers;
using Shared.MassTransits;
using Shared.MassTransits.Contracts;
using Shared.Validations;
using ExchangeType = RabbitMQ.Client.ExchangeType;

namespace CoreService.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "http://localhost:5092";
                options.RequireHttpsMetadata = false;
                options.Audience = "Mobile";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = CryptographyHelper.CreateRsaKey()
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
        services.AddDbContext<CoreDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.EnableSensitiveDataLogging();
        });
        return services;
    }

    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
    {
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
            cfg.ReceiveEndpoint($"core-service-{new KebabCaseEndpointNameFormatter(false).SanitizeName(nameof(CreateUser))}", e =>
            {
                e.ConfigureConsumeTopology = false;
                var exchangeName = new KebabCaseEndpointNameFormatter(false).SanitizeName(nameof(CreateUser));
                e.Bind(exchangeName, x =>
                {
                    x.ExchangeType = ExchangeType.Topic;
                });
                e.ConfigureConsumer<CreateUserConsumer>(ctx);
            });
            cfg.ReceiveEndpoint($"core-service-{new KebabCaseEndpointNameFormatter(false).SanitizeName(nameof(UpdateUser))}", e =>
            {
                e.ConfigureConsumeTopology = false;
                var exchangeName = new KebabCaseEndpointNameFormatter(false).SanitizeName(nameof(UpdateUser));
                e.Bind(exchangeName, x =>
                {
                    x.ExchangeType = ExchangeType.Topic;
                });
                e.ConfigureConsumer<UpdateUserConsumer>(ctx);
            });
        });
        return services;
    }
}