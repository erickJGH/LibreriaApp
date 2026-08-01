using Application.UseCases.Autores;
using Application.UseCases.Libros;
using Application.UseCases.Prestamos;
using Data;
using Data.Identity;
using LibreriaWebApi.EndPoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddData(builder.Configuration);



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi




builder.Services.AddData(builder.Configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,

        ValidateAudience = true,

        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,


        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        ValidAudience = builder.Configuration["Jwt:Audience"],


        IssuerSigningKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]!))
    };
});


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


//servicios
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
// Swagger con soporte JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Libreria API", Version = "v1" });

    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa el token JWT"
    });

    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("bearer", document)] = []
    });
});


//builder.Services.AddOpenApi();
builder.Services.AddAuthorization();

var app = builder.Build();

//agregar roles al iniciar la aplicacion    

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await IdentitySeeder.SeedRolesAsync(services);
    await IdentitySeeder.SeedAdminUserAsync(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   //app.MapOpenApi();
}
//middlewares

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


//Mapeo endpoints

app.MapAutoresEndpoints();
app.MaplibrosEndpoints();
app.MapPrestamosEndpoints();
app.MapAuthEndpoints();

app.Run();




