using Microsoft.EntityFrameworkCore;
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

var app = builder.Build();


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();





