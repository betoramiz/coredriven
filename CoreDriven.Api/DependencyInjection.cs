using CoreDriven.Api.Services;
using CoreDriven.Application.Common.Authorization;

namespace CoreDriven.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, string corsPolicyName)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();
        
        services
            .AddCustomCors(corsPolicyName)
            .AddOpenApiDocs();
        
        
        services.AddTransient<ICurrentUserProvider, CurrentUserProvider>();
        return services;
    }
    
    private static IServiceCollection AddOpenApiDocs(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName.Replace("+", ""));
        });
        
        return services;
    }
    
    private static IServiceCollection AddCustomCors(this IServiceCollection services, string policyName)
    {
        services
            .AddCors(options =>
            {
                options.AddPolicy(name: policyName,
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });
        return services;
    }
}