using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PropertiesMinimalAPI.Models;
using static PropertiesMinimalAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/properties", (ILogger<Program> logger) =>
{
    logger.Log(LogLevel.Information, "Carga todas las propiedades");
    return Results.Ok(DataProperties.Properties);
}).WithName("GetProperties");

app.MapGet("/api/properties/{id:int}", (int id) =>
{
    return Results.Ok(DataProperties.Properties.FirstOrDefault(p => p.Id == id));
}).WithName("GetProperty");

app.MapPost("/api/properties", ([FromBody] Properties property) =>
{
    //validar
    if (property.Id != 0 || string.IsNullOrEmpty(property.Name))
    {
        return Results.BadRequest("Id incorrecto o el nombre esta vacio");
    }

    if (DataProperties.Properties.FirstOrDefault(p => p.Name.ToLower() == property.Name.ToLower()) != null)
    {
        return Results.BadRequest("El nombre ya existe");

    }

    property.Id = DataProperties.Properties.OrderByDescending(p => p.Id).FirstOrDefault().Id + 1;
    DataProperties.Properties.Add(property);

    //return Results.Ok(DataProperties.Properties);
    //return Results.Created($"api/properties/{property.Id}", property);
    return Results.CreatedAtRoute("GetProperty",new { id = property.Id }, property);
}).WithName("CreatePorperty");

app.UseHttpsRedirection();

app.Run();
