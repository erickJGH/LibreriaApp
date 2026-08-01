using Application.UseCases.Autores;
using Application.UseCases.Libros;
using Application.UseCases.Prestamos;
using Data;
using Data.Identity;
using LibreriaWebApi.EndPoints;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddData(builder.Configuration);



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Te hace falta la conexion a la bd");

builder.Services.AddData(connectionString);

builder.Services.AddScoped<ActualizarActorUseCase>();
builder.Services.AddScoped<BorrarAutorUseCase>();
builder.Services.AddScoped<CrearAutorUseCase>();
builder.Services.AddScoped<ObtenerAutorPorIdUseCase>();
builder.Services.AddScoped<ObtenerTodosAutoresUseCase>();

builder.Services.AddScoped<ActualizarLibroUseCase>();
builder.Services.AddScoped<BorrarLibroUseCase>();
builder.Services.AddScoped<CrearLibroUseCase>();
builder.Services.AddScoped<ObtenerLibroPorIdUseCase>();
builder.Services.AddScoped<ObtenerLibrosAntesde2000UseCase>();
builder.Services.AddScoped<ObtenerTodosLosLibrosUseCase>();

builder.Services.AddScoped<BorrarPrestamoUseCase>();
builder.Services.AddScoped<CrearPrestamoUseCase>();
builder.Services.AddScoped<ObtenerPrestamoPorIdUseCase>();
builder.Services.AddScoped<ObtenerPrestamosNoDevueltosUseCase>();
builder.Services.AddScoped<ObtenerTodosLosPrestamosUseCase>();
builder.Services.AddScoped<RegistrarDevolucionPrestamoUseCase>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   //app.MapOpenApi();
}

app.UseHttpsRedirection();


//Mapeo endpoints

app.MapAutoresEndpoints();
app.MaplibrosEndpoints();
app.MapPrestamosEndpoints();

app.Run();


