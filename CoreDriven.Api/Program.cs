using CoreDriven.Application.Common;
using CoreDriven.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Scan(scan => scan
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

builder.Services.AddScoped<IDataBase, Database>();

var app = builder.Build();

// app.UseHttpsRedirection();

app.MapControllers();
app.Run();