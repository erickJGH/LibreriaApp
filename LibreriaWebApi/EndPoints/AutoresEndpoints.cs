using Application.Dtos.Autores;
using Application.UseCases.Autores;
using System.Runtime.CompilerServices;

namespace LibreriaWebApi.EndPoints
{
    public static class AutoresEndpoints
    {

        public static void MapAutoresEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/autores")
                .WithTags("Autores");

            group.MapGet("/{id}", async (int id, ObtenerAutorPorIdUseCase useCase) =>
            {
                try
                {
                    var autor = await useCase.ExecuteAsync(id);
                    return Results.Ok(autor);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { error = ex.Message });
                }
            }).WithName("ObtenerAutorPorId")
            .WithSummary("Obtener un autor por su Id")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

          group.MapGet("/", async(ObtenerTodosAutoresUseCase useCase) =>
          {
              try
              {
                  var autores = await useCase.ExecuteAsync();
                  return Results.Ok(autores);
              }
              catch (Exception ex)
              {
                  return Results.InternalServerError(ex.Message);
              }
          }).WithName("ObtenerTodosLosAutores")
            .WithSummary("Obtener todos los autores")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

           group.MapPost("/", async(CrearAutorDto dto, CrearAutorUseCase useCase) =>
           {
               try
               {
                   var autor = await useCase.ExecuteAsync(dto);
                   return Results.Created($"/api/autores/{autor.autor_id}", autor);
               }
               catch (InvalidOperationException ex)
               {
                   return Results.BadRequest(new { error = ex.Message });
               }
               catch (ArgumentException ex)
               {
                   return Results.BadRequest(new { error = ex.Message });
               }
               catch (Exception ex)
               {
                   return Results.InternalServerError(ex.Message);
               }
           }).WithName("CrearAutor")
             .WithSummary("Crear una nuevo autor")
             .Produces(StatusCodes.Status201Created)
             .Produces(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status500InternalServerError);

            group.MapPut("/{id}", async(int id, ActualizarAutorDto dto,ActualizarActorUseCase useCase) =>
            {
                try
                {
                    var autor = await useCase.ExecuteAsync(id,dto);
                    return Results.Ok(autor);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { error = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            }).WithName("ActualizarAutor")
            .WithSummary("Actualizar un autor existente")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapDelete("/{id}", async(int id, BorrarAutorUseCase useCase) =>
            {
                try
                {
                    await useCase.ExecuteAsync(id);
                    return Results.NoContent();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(new { error = ex.Message });
                }
            }).WithName("DeleteAutor")
            .WithSummary("Eliminar un autor existente")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        }



    }
}
