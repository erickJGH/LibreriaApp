using Application.Authentication.Dtos;
using Application.Authentication.Interfaces;

namespace LibreriaWebApi.EndPoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(
            this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/auth")
                .WithTags("Authentication");


            group.MapPost("/register",
                async (
                    RegisterRequest dto,
                    IAuthService authService) =>
                {
                    try
                    {
                        await authService.RegisterAsync(dto);

                        return Results.Ok(new
                        {
                            message = "Usuario creado correctamente"
                        });
                    }
                    catch (Exception ex)
                    {
                        return Results.BadRequest(new
                        {
                            error = ex.Message
                        });
                    }

                })
                .WithName("Register")
                .WithSummary("Registrar un nuevo usuario")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .RequireAuthorization(policy =>
                {
                    policy.RequireRole("Admin");
                });



            group.MapPost("/login",
                async (
                    LoginRequest dto,
                    IAuthService authService) =>
                {
                    try
                    {
                        var response =
                            await authService.LoginAsync(dto);

                        return Results.Ok(response);
                    }
                    catch (Exception ex)
                    {
                        return Results.Unauthorized();
                    }

                })
                .WithName("Login")
                .WithSummary("Iniciar sesión")
                .Produces<LoginResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

        }
    }
}
