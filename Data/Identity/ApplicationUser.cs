using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public string Nombre { get; set; } = string.Empty;
    }
}
