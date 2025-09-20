using CoreDriven.Api.Services;
using CoreDriven.Application;
using CoreDriven.Application.Common;
using CoreDriven.Application.Common.Authorization;
using CoreDriven.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplication();

builder.Services.AddScoped<IDataBaseAccess, Database>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ICurrentUserProvider, CurrentUserProvider>();

var app = builder.Build();

// app.UseHttpsRedirection();

app.MapControllers();
app.Run();