using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Authentication.Dtos
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
