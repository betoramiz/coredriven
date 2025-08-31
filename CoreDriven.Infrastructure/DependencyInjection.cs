using System.Reflection;
using CoreDriven.Application.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreDriven.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddPersistence(configuration);
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("database");
        var thisAssembly = Assembly.GetExecutingAssembly().GetName().Name;

        services.AddDbContext<Database>((options) =>
        {
            options.UseNpgsql(connectionString, x => x.MigrationsAssembly(thisAssembly));
        });

        services.AddScoped<IDataBaseAccess>(provider => provider.GetService<Database>());
        
        return services;
    }
}