using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Authentication.Dtos
{
    public class LoginRequest
    {
        public string userName { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;
    }
}
