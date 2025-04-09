using CoreDriven.Application.Common;
using CoreDriven.Application.UseCases.Todos;
using CoreDriven.Application.UseCases.Todos.Create;
using CoreDriven.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// builder.Services
//     .AddScoped<TodoUseCases>()
//     .AddScoped<Create>();

builder.Services.Scan(scan => scan
    .FromAssemblyOf<IUseCases>()
    .AddClasses(classes => classes.AssignableTo<IUseCases>())
    .AsSelfWithInterfaces()
    .WithScopedLifetime()
    .FromAssemblyOf<IUseCase>()
    .AddClasses(classes => classes.AssignableTo<IUseCase>())
    .AsSelfWithInterfaces()
    .WithScopedLifetime()
);

builder.Services.AddScoped<IDataBase, Database>();

var app = builder.Build();

// app.UseHttpsRedirection();

app.MapControllers();
app.Run();