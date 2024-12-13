using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PropertiesMinimalAPI.Maps;
using PropertiesMinimalAPI.Models;
using PropertiesMinimalAPI.Models.DTOS;
using System.Collections;
using System.Net;
using static PropertiesMinimalAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//AutoMapper
builder.Services.AddAutoMapper(typeof(CustomMap));

//Validators
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/properties", (ILogger<Program> logger) =>
{
    ResponseAPI resp = new();

    logger.Log(LogLevel.Information, "Carga todas las propiedades");

    resp.Result = DataProperties.Properties;
    resp.Success = true;
    resp.StatusCode = HttpStatusCode.OK;

    return Results.Ok(resp);
}).WithName("GetProperties").Produces<ResponseAPI>(200);

app.MapGet("/api/properties/{id:int}", (int id) =>
{
    ResponseAPI resp = new();

    resp.Result = DataProperties.Properties.FirstOrDefault(p => p.Id == id);
    resp.Success = true;
    resp.StatusCode = HttpStatusCode.OK;

    return Results.Ok(resp);

}).WithName("GetProperty").Produces<ResponseAPI>(200);

app.MapPost("/api/properties", async (IMapper _mapper, IValidator<CreatePropertyDTO> _validation, [FromBody] CreatePropertyDTO createPropertyDTO) =>
{
    ResponseAPI resp = new() {Success = false, StatusCode = HttpStatusCode.BadRequest};

    var resultValidators = await _validation.ValidateAsync(createPropertyDTO);
    //validar
    if (!resultValidators.IsValid)
    {
        resp.Errors.Add(resultValidators.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(resp);
    }

    if (DataProperties.Properties.FirstOrDefault(p => p.Name.ToLower() == createPropertyDTO.Name.ToLower()) != null)
    {
        resp.Errors.Add("El nombre ya existe");
        return Results.BadRequest(resp);
    }

    Properties property = _mapper.Map<Properties>(createPropertyDTO);

    property.Id = DataProperties.Properties.OrderByDescending(p => p.Id).FirstOrDefault().Id + 1;
    DataProperties.Properties.Add(property);

    PropertyDTO propertyDTO = _mapper.Map<PropertyDTO>(property);

    //return Results.Ok(DataProperties.Properties);
    //return Results.Created($"api/properties/{property.Id}", property);
    //return Results.CreatedAtRoute("GetProperty",new { id = property.Id }, propertyDTO);

    resp.Result = propertyDTO;
    resp.Success = true;
    resp.StatusCode = HttpStatusCode.Created;

    return Results.Ok(resp);

}).WithName("CreatePorperty").Accepts<CreatePropertyDTO>("application/json").Produces<ResponseAPI>(201).Produces(400);

app.UseHttpsRedirection();

app.Run();
