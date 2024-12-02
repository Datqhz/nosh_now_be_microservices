using System.Reflection;
using System.Security.Claims;
using AuthServer.Consumers;
using AuthServer.Core;
using AuthServer.Data.DbContext;
using AuthServer.Data.Models;
using AuthServer.Repositories;
using IdentityServer4;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Helpers;
using Shared.Validations;
using FluentValidation;
using MassTransit;
using Shared.MassTransits;
using Shared.MassTransits.Contracts;
using ExchangeType = RabbitMQ.Client.ExchangeType;

namespace AuthServer.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureIdentityServer(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddTransient(typeof(IdentityServer4.Services.ICache<>), typeof(IdentityServer4.Services.DefaultCache<>));
        services.AddIdentity<Account, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();
        services.AddIdentityServer()
            //.AddDeveloperSigningCredential()
            .AddSigningCredential(CryptographyHelper.CreateRsaKey(), IdentityServerConstants.RsaSigningAlgorithm.RS256)
            .AddClientStoreCache<ClientStore>()
            .AddResourceStoreCache<ResourceStore>()
            .AddAspNetIdentity<Account>()
            .AddProfileService<ProfileService>();
        
        
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
            });
        services.AddTransient<ClaimsPrincipal>(provider =>
            provider.GetService<IHttpContextAccessor>().HttpContext?.User);
        return services;
    }
    
    public static IServiceCollection AddAuthorizationSettings(this IServiceCollection services)
    {
        services.AddAuthorization(option =>
        {
            option.AddPolicy("Customer",
                policy => policy.RequireAssertion(context =>
                    context.User.HasClaim(claim => claim.Type == "scope" && claim.Value.Contains("Customer"))
                ));
            option.AddPolicy("Restaurant",
                policy => policy.RequireAssertion(context =>
                    context.User.HasClaim(claim => claim.Type == "scope" && claim.Value.Contains("Restaurant"))
                ));
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
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
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
        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.EnableSensitiveDataLogging(false);
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
            cfg.ReceiveEndpoint(new KebabCaseEndpointNameFormatter(false).SanitizeName(nameof(DeleteAccount)), e =>
            {
                e.ConfigureConsumeTopology = false;
                e.ConfigureConsumer <DeleteAccountConsumer>(ctx);
            });
        });
        return services;
    }
}