using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Api.Middleware;
using ProductAPI.Application;
using ProductAPI.Infrastructure;
using ProductAPI.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddCoreDependencies();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => {
    options.AddPolicy("default", policy => {
        policy.WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseMiddleware<ErrorHandlerMiddleware>();


// Initialise and seed database
using (var scope = app.Services.CreateScope())
{
        var initialiser = scope.ServiceProvider.GetRequiredService<ProductPurchaseContextInitializer>();
        await initialiser.InitialiseAsync();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("default");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();





