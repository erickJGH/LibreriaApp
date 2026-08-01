
using Application.Dtos.Prestamos;
using Application.UseCases.Prestamos;

namespace LibreriaWebApi.EndPoints
{
    public static class PrestamosEndPoints
    {
        public static void MapPrestamosEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/prestamos")
                .WithTags("Prestamos");

            group.MapGet("/{id}", async (int id, ObtenerPrestamoPorIdUseCase useCase) =>
            {
                try
                {
                    var prestamo = await useCase.ExecuteAsync(id);
                    return Results.Ok(prestamo);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { error = ex.Message });
                }
            }).WithName("ObtenerPrestamoPorId")
            .WithSummary("Obtener un prestamo por su Id")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/", async (ObtenerTodosLosPrestamosUseCase useCase) =>
            {
                try
                {
                    var prestamos = await useCase.ExecuteAsync();
                    return Results.Ok(prestamos);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            }).WithName("ObtenerTodosLosPrestamos")
              .WithSummary("Obtener todos los prestamos")
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status500InternalServerError);

            group.MapGet("/no-devueltos", async (ObtenerPrestamosNoDevueltosUseCase useCase) =>
            {
                try
                {
                    var prestamos = await useCase.ExecuteAsync();
                    return Results.Ok(prestamos);
                }   
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            }).WithName("No-devueltos")
              .WithSummary("Obtener todos los prestamos no devueltos")
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status500InternalServerError);

            group.MapPost("/", async (CrearPrestamoDto dto, CrearPrestamoUseCase useCase) =>
            {
                try
                {
                    var prestamo = await useCase.ExecuteAsync(dto);
                    return Results.Created($"/api/prestamos/{prestamo.prestamo_id}", prestamo);
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
            }).WithName("CrearPrestamo")
              .WithSummary("Crear un nuevo prestamo")
              .Produces(StatusCodes.Status201Created)
              .Produces(StatusCodes.Status400BadRequest)
              .Produces(StatusCodes.Status500InternalServerError);

            group.MapPut("/{id}", async (int id, RegistrarTerminoPrestamoDto dto, RegistrarDevolucionPrestamoUseCase useCase) =>
            {
                try
                {
                    var prestamo = await useCase.ExecuteAsync(id,dto);
                    return Results.Ok(prestamo);
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
            }).WithName("DevolverPrestamo")
            .WithSummary("Devolver un prestamo")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization(policy =>
            {
                policy.RequireRole("Admin");
            });

            group.MapDelete("/{id}", async (int id, BorrarPrestamoUseCase useCase) =>
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
            }).WithName("DeletePrestamo")
            .WithSummary("Eliminar un prestamo existente")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization(policy =>
            {
                policy.RequireRole("Admin");
            });

        }
    }
}
