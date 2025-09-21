using System.Reflection;
using System.Text;
using CoreDriven.Application.Common;
using CoreDriven.Application.Common.Storage;
using CoreDriven.Application.Common.Token;
using CoreDriven.Infrastructure.Authentication;
using CoreDriven.Infrastructure.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CoreDriven.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAuthentication(configuration)
            .AddPersistence(configuration)
            .AddStorage(configuration);
    }
    
    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.Section, jwtSettings);
    
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<ITokenGenerator, TokenGenerator>();
    
        services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => 
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ClockSkew = TimeSpan.Zero
                }
            );
        
        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .Build();
        });
        
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("database");
        var thisAssembly = Assembly.GetExecutingAssembly().GetName().Name;

        services.AddDbContext<Database>((options) =>
        {
            options.UseNpgsql(connectionString, x => x.MigrationsAssembly(thisAssembly));
        })
        .AddIdentity<IdentityUser, IdentityRole>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<Database>()
        .AddDefaultTokenProviders();

        services.AddScoped<IDataBaseAccess>(provider => provider.GetService<Database>());
        
        return services;
    }
    
    private static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        S3Configuration s3Config = new S3Configuration();
        configuration.GetSection(S3Configuration.Option).Bind(s3Config);
        services.AddTransient<IStorageService, MinioStorage>();
        
        return services;
    }
}