using System.Reflection;
using CommunicationService.Consumers;
using CommunicationService.Hubs;
using CommunicationService.Repositories;
using CommunicationService.Services;
using FluentValidation;
using MassTransit;
using MassTransit.Transports.Fabric;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Helpers;
using Shared.MassTransits;
using Shared.MassTransits.Contracts;
using Shared.Validations;
using ExchangeType = RabbitMQ.Client.ExchangeType;

namespace CommunicationService.Extensions;

public static class ServiceExtensions
{
    
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "http://localhost:5237";
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

    // public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    // {
    //     var connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
    //     services.AddDbContext<CoreDbContext>(options =>
    //     {
    //         options.UseNpgsql(connectionString);
    //         options.EnableSensitiveDataLogging();
    //     });
    //     return services;
    // }

    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfRepository, UnitOfRepository>();
        services.AddScoped<IEmailService, EmailService>();
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
            cfg.ReceiveEndpoint(new KebabCaseEndpointNameFormatter(false).SanitizeName(nameof(SendVerificationEmail)), e =>
            {
                e.ConfigureConsumeTopology = false;
                e.ConfigureConsumer<SendVerificationEmailConsumer>(ctx);
            });
        });
        return services;
    }
    
    public static IServiceCollection AddSignalRServer(this IServiceCollection services)
    {
        services.AddSignalR(options =>
            {
                options.KeepAliveInterval = TimeSpan.FromSeconds(30); 
                options.EnableDetailedErrors = true;
            })
            .AddMessagePackProtocol()
            ;
        return services;
    }
    public static void AddSignalREndpoints(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<NotificationHub>("/order-status");
        });
    }
}