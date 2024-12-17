using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PropertiesMinimalAPI.Data;
using PropertiesMinimalAPI.Maps;
using PropertiesMinimalAPI.Models;
using PropertiesMinimalAPI.Models.DTOS;
using System.Collections;
using System.Net;
using static PropertiesMinimalAPI.Data.Data;

var builder = WebApplication.CreateBuilder(args);
//Config conexion a BD
builder.Services.AddDbContext<ApplicationDbContext>(opc => opc.UseNpgsql(builder.Configuration.GetConnectionString("ConexionSql")));

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

app.MapGet("/api/properties", async (ApplicationDbContext _bd, ILogger<Program> logger) =>
{
    ResponseAPI resp = new();

    logger.Log(LogLevel.Information, "Carga todas las propiedades");

    resp.Result = _bd.Properties;
    resp.Success = true;
    resp.StatusCode = HttpStatusCode.OK;

    return Results.Ok(resp);
}).WithName("GetProperties").Produces<ResponseAPI>(200);

app.MapGet("/api/properties/{id:int}", async (ApplicationDbContext _bd, int id) =>
{
    ResponseAPI resp = new();

    resp.Result = await _bd.Properties.FirstOrDefaultAsync(r => r.Id == id);
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

app.MapPut("/api/properties", async (IMapper _mapper, IValidator<UpdatePropertyDTO> _validation, [FromBody] UpdatePropertyDTO updatePropertyDTO) =>
{
    ResponseAPI resp = new() { Success = false, StatusCode = HttpStatusCode.BadRequest };

    var resultValidators = await _validation.ValidateAsync(updatePropertyDTO);
    //validar
    if (!resultValidators.IsValid)
    {
        resp.Errors.Add(resultValidators.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(resp);
    }

    //if (DataProperties.Properties.FirstOrDefault(p => p.Name.ToLower() == updatePropertyDTO.Name.ToLower()) != null)
    //{
    //    resp.Errors.Add("El nombre ya existe");
    //    return Results.BadRequest(resp);
    //}

    Properties propertyBD = DataProperties.Properties.FirstOrDefault(r => r.Id == updatePropertyDTO.Id);

    propertyBD.Name = updatePropertyDTO.Name;
    propertyBD.Description = updatePropertyDTO.Description;
    propertyBD.Location = updatePropertyDTO.Location;
    propertyBD.IsActive = updatePropertyDTO.IsActive;

    PropertyDTO propertyDTO = _mapper.Map<PropertyDTO>(propertyBD);

    resp.Result = propertyDTO;
    resp.Success = true;
    resp.StatusCode = HttpStatusCode.Created;

    return Results.Ok(resp);

}).WithName("UpdatePorperty").Accepts<UpdatePropertyDTO>("application/json").Produces<ResponseAPI>(200).Produces(400);

app.MapDelete("/api/properties/{id:int}", (int id) =>
{
    ResponseAPI resp = new() { Success = false, StatusCode = HttpStatusCode.BadRequest };
    Properties propertyBD = DataProperties.Properties.FirstOrDefault(r => r.Id == id);

    if (propertyBD != null)
    {
        DataProperties.Properties.Remove(propertyBD);
        resp.Success = true;
        resp.StatusCode = HttpStatusCode.NoContent;
        return Results.Ok(resp);
    }
    else
    {
        resp.Errors.Add("El ID de la propiedad es inválido");
        return Results.BadRequest(resp);
    }

});

app.UseHttpsRedirection();

app.Run();
