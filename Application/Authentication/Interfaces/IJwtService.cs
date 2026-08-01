using Application.Authentication.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Authentication.Interfaces
{
    public interface IJwtService
    {
        Task<LoginResponse> GenerateTokenAsync(
        string userId,
        string userName,
        IEnumerable<string> roles);
    }
}
