using CoreDriven.Application;
using CoreDriven.Application.Common;
using CoreDriven.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplication();

builder.Services.AddScoped<IDataBaseAccess, Database>();

var app = builder.Build();

// app.UseHttpsRedirection();

app.MapControllers();
app.Run();