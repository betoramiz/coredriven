using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using CoreDriven.Application.Common.UseCases;

namespace CoreDriven.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddFluentValidation()
            .AddUseCases();
    }
    
    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        
        return services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
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