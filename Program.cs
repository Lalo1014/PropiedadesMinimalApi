using ApiPeliculas.Data;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropiedadesMinimalApi.Datos;
using PropiedadesMinimalApi.Mapas;
using PropiedadesMinimalApi.Modelo;
using PropiedadesMinimalApi.Modelo.Dtos;
using System.ComponentModel.Design;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AplicationDbContext>(opciones =>
                            opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(typeof(ConfiguracionMapas));


//anadir validaciones
builder.Services.AddValidatorsFromAssemblyContaining<Propiedad>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//primeros endpoint

//obtener todas datos
app.MapGet("/api/propiedades", (ILogger<Program> logger) =>
{
    RespuestaApi respuesta = new RespuestaApi();
    respuesta.Resultado = DatosPropiedad.listaPropiedades;
    respuesta.Success = true;
    respuesta.CodigoEstado = HttpStatusCode.OK;

    return Results.Ok(respuesta);

    //logger.Log(LogLevel.Information, "cargar propiedades");
    //return Results.Ok(DatosPropiedad.listaPropiedades);
});

app.MapGet("/api/propiedades/{id:int}", (int id) =>
{
    Results.Ok(DatosPropiedad.listaPropiedades.FirstOrDefault(u => u.IdPropiedad == id));
}).WithName("ObtenerPropiedad");

//app.MapGet("/saludo{id}", (int id) =>
//{
//    //return Results.BadRequest("Error de ejecucion");
//    return Results.Ok("todo bien con " + id);
//});
app.MapPost("/saludo2", () => "Bienvenidos");


//crear 
app.MapPost("/api/propiedades", async (IMapper mapper, IValidator<CrearPropiedadDto>_validcion, [FromBody] CrearPropiedadDto crearPropiedadDto) =>
{

    //var resultadoValidaciones =  await _validcion.ValidateAsync(crearPropiedadDto).GetAwaiter().GetResult();
    var resultadoValidaciones =  await _validcion.ValidateAsync(crearPropiedadDto); 

    if (!resultadoValidaciones.IsValid)
    {
        return Results.BadRequest(resultadoValidaciones.Errors.FirstOrDefault().ToString());
    }

    if(DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.Nombre.ToLower() == crearPropiedadDto.Nombre.ToLower()) != null)
    {
        return Results.BadRequest("el nombre de propiedad ya existe");
    }

    //Propiedad propiedad = new Propiedad
    //{
    //    Nombre = crearPropiedadDto.Nombre,
    //    Descripcion = crearPropiedadDto.Descripcion,
    //    Ubicacion = crearPropiedadDto.Ubicacion,
    //    Activa = crearPropiedadDto.Activa
    //};


    Propiedad propiedad = mapper.Map<Propiedad>(crearPropiedadDto);


    propiedad.IdPropiedad = DatosPropiedad.listaPropiedades.OrderByDescending(p => p.IdPropiedad).FirstOrDefault().IdPropiedad + 1;
    DatosPropiedad.listaPropiedades.Add(propiedad);

    //PropiedadDto propiedadDto = new PropiedadDto
    //{
    //    IdPropiedad = propiedad.IdPropiedad,
    //    Nombre = crearPropiedadDto.Nombre,
    //    Descripcion = crearPropiedadDto.Descripcion,
    //    Ubicacion = crearPropiedadDto.Ubicacion,
    //    Activa = crearPropiedadDto.Activa
    //};

    PropiedadDto propiedadDto = mapper.Map<PropiedadDto>(propiedad);
    //return Results.Ok(DatosPropiedad.listaPropiedades);
    //return Results.Created($"/api/propiedades/{propiedad.IdPropiedad}", propiedad);
    return Results.CreatedAtRoute("ObtenerPropiedad", new { id = propiedad.IdPropiedad }, propiedadDto);
}).WithName("CrearPropiedad").Accepts<CrearPropiedadDto>("application/json").Produces<PropiedadDto>(201).Produces(400);


//actualizar
app.MapPut("/api/propiedades", async (IMapper mapper, IValidator<ActualizarPropiedadDto> _validcion, [FromBody] ActualizarPropiedadDto actualizarPropiedadDto) =>
{

    RespuestaApi respuesta = new RespuestaApi() { Success = false, CodigoEstado = HttpStatusCode.BadGateway};

    //var resultadoValidaciones =  await _validcion.ValidateAsync(crearPropiedadDto).GetAwaiter().GetResult();
    var resultadoValidaciones = await _validcion.ValidateAsync(actualizarPropiedadDto);

    if (!resultadoValidaciones.IsValid)
    {
        return Results.BadRequest(resultadoValidaciones.Errors.FirstOrDefault().ToString());
    }

    if (DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.Nombre.ToLower() == actualizarPropiedadDto.Nombre.ToLower()) != null)
    {
        return Results.BadRequest("el nombre de propiedad ya existe");
    }

    //Propiedad propiedad = new Propiedad
    //{
    //    Nombre = crearPropiedadDto.Nombre,
    //    Descripcion = crearPropiedadDto.Descripcion,
    //    Ubicacion = crearPropiedadDto.Ubicacion,
    //    Activa = crearPropiedadDto.Activa
    //};

    Propiedad propiedadDesdeBD = DatosPropiedad.listaPropiedades.FirstOrDefault
    (p => p.IdPropiedad == actualizarPropiedadDto.IdPropiedad);
    propiedadDesdeBD.Nombre = actualizarPropiedadDto.Nombre;
    propiedadDesdeBD.Descripcion = actualizarPropiedadDto.Descripcion;
    propiedadDesdeBD.Ubicacion = actualizarPropiedadDto.Ubicacion;
    propiedadDesdeBD.Activa = actualizarPropiedadDto.Activa;

    respuesta.Resultado = mapper.Map<PropiedadDto>(propiedadDesdeBD);
    respuesta.Success = true;
    respuesta.CodigoEstado = HttpStatusCode.Created;
    return Results.Ok(respuesta);

    //PropiedadDto propiedadDto = new PropiedadDto
    //{
    //    IdPropiedad = propiedad.IdPropiedad,
    //    Nombre = crearPropiedadDto.Nombre,
    //    Descripcion = crearPropiedadDto.Descripcion,
    //    Ubicacion = crearPropiedadDto.Ubicacion,
    //    Activa = crearPropiedadDto.Activa
    //};

    //PropiedadDto propiedadDto = mapper.Map<PropiedadDto>(propiedad);
    ////return Results.Ok(DatosPropiedad.listaPropiedades);
    ////return Results.Created($"/api/propiedades/{propiedad.IdPropiedad}", propiedad);
    //return Results.CreatedAtRoute("ObtenerPropiedad", new { id = propiedad.IdPropiedad }, propiedadDto);
}).WithName("ActualizarPropiedad").Accepts<ActualizarPropiedadDto>("application/json").Produces<RespuestaApi>(201).Produces(400);


app.MapDelete("/api/propiedades/{id:int}", (int id) =>
{
    RespuestaApi respuesta = new RespuestaApi() { Success = false, CodigoEstado = HttpStatusCode.BadGateway };

    Propiedad propiedadDesdeBD = DatosPropiedad.listaPropiedades.FirstOrDefault
    (p => p.IdPropiedad == id);

    if(propiedadDesdeBD != null)
    {
        DatosPropiedad.listaPropiedades.Remove(propiedadDesdeBD);
        respuesta.Success = true;
        respuesta.CodigoEstado = HttpStatusCode.NoContent;
        return Results.Ok(respuesta);
    }else
    {
        respuesta.Errores.Add("id invalido");
        return Results.BadRequest(respuesta);
    }
});

app.UseHttpsRedirection();
app.Run();