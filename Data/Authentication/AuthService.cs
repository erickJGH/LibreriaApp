using Application.Authentication.Dtos;
using Application.Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace Data.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        public AuthService(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            try
            {
                var existingUser = await _userManager.FindByNameAsync(request.userName);
                if (existingUser != null)
                {
                    throw new Exception("El usuario ya existe.");
                }
                var user = new ApplicationUser
                {
                    UserName = request.userName,
                    Nombre = request.nombre
                };
                var result = await _userManager.CreateAsync(user, request.password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors
                        .Select(e => e.Description);

                    throw new Exception(
                        string.Join(", ", errors));
                }
                await _userManager.AddToRoleAsync(user,"Usuario");


            }
            catch (Exception)
            {
                throw new InvalidOperationException();
            }
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager
            .FindByNameAsync(request.userName);

            if (user == null)
            {
                throw new InvalidOperationException("Usuario o contraseña incorrectos.");
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user,request.password);

            if (!passwordValid)
            {
                throw new InvalidOperationException("Usuario o contraseña incorrectos.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var jwt = await _jwtService.GenerateTokenAsync( user.Id,user.UserName!,roles);

            return new LoginResponse
            {
                Token = jwt.Token,
                Expiration = jwt.Expiration
            };

        }


    }
}
