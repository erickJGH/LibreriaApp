using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Authentication.Configuration
{
    public class JwtOptions
    {
        public const string SectionName = "Jwt";

        public string Key { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public int ExpireMinutes { get; set; }
    }
}
