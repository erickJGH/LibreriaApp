
using Application.Dtos.Libros;
using Application.UseCases.Libros;

namespace LibreriaWebApi.EndPoints
{
    public static class LibrosEndpoints
    {
        public static void MaplibrosEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/libros")
                .WithTags("Libros");

            group.MapGet("/{id}", async (int id, ObtenerLibroPorIdUseCase useCase) =>
            {
                try
                {
                    var libro = await useCase.ExecuteAsync(id);
                    return Results.Ok(libro);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { error = ex.Message });
                }
            }).WithName("ObtenerLibroPorId")
            .WithSummary("Obtener un libro por su Id")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/", async (ObtenerTodosLosLibrosUseCase useCase) =>
            {
                try
                {
                    var libros = await useCase.ExecuteAsync();
                    return Results.Ok(libros);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            }).WithName("ObtenerTodosLosLibros")
              .WithSummary("Obtener todos los libros")
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status500InternalServerError);

            group.MapGet("/antes-de-2000", async (ObtenerLibrosAntesde2000UseCase useCase) =>
            {
                try
                {
                    var libros = await useCase.ExecuteAsync();
                    return Results.Ok(libros);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            }).WithName("antes-de-2000")
              .WithSummary("Obtener libros publicados antes de 2000")
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status500InternalServerError);

            group.MapPost("/", async (CrearLibroDto dto, CrearLibroUseCase useCase) =>
            {
                try
                {
                    var libro = await useCase.ExecuteAsync(dto);
                    return Results.Created($"/api/libros/{libro.libro_id}", libro);
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
            }).WithName("CrearLibro")
              .WithSummary("Crear un nuevo libro")
              .Produces(StatusCodes.Status201Created)
              .Produces(StatusCodes.Status400BadRequest)
              .Produces(StatusCodes.Status500InternalServerError);

            group.MapPut("/{id}", async (int id, ActualizarLibroDto dto, ActualizarLibroUseCase useCase) =>
            {
                
                try
                {
                    var libro = await useCase.ExecuteAsync(id,dto);
                    return Results.Ok(libro);
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
            }).WithName("ActualizarLibro")
            .WithSummary("Actualizar un libro existente")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapDelete("/{id}", async (int id, BorrarLibroUseCase useCase) =>
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
            }).WithName("DeleteLibro")
            .WithSummary("Eliminar un libro existente")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        }
    }
}
