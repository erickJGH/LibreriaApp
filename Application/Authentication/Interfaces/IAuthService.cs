using Application.Authentication.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Authentication.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest request);

        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
