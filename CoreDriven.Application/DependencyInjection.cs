using CoreDriven.Application.Common;
using CoreDriven.Application.Common.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace CoreDriven.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddUseCases();
        
        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssemblyOf<IUseCaseRepository>()
            .AddClasses(classes => classes.AssignableTo<IUseCaseRepository>())
            .AsSelfWithInterfaces()
            .WithScopedLifetime()
            .FromAssemblyOf<IBaseUseCase>()
            .AddClasses(classes => classes.AssignableTo(typeof(IUseCase<,>)))
            .AddClasses(classes => classes.AssignableTo(typeof(IUseCase<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime()
        );
    }
}