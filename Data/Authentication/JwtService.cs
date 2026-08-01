using Application.Authentication.Dtos;
using Application.Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Authentication.Configuration;
using Application.Authentication.Dtos;
using Application.Authentication.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace Data.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public Task<LoginResponse> GenerateTokenAsync(string userId,string userName,IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub,userId),
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new(ClaimTypes.Name,userName)
            };

            claims.AddRange(
                roles.Select(role =>
                new Claim(
                    ClaimTypes.Role,
                    role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( _jwtOptions.Key));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var expiration =DateTime.UtcNow.AddMinutes( _jwtOptions.ExpireMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return Task.FromResult(
                new LoginResponse
                {
                    Token = new JwtSecurityTokenHandler()
                        .WriteToken(token),

                    Expiration = expiration
                });


        }





    }
}
