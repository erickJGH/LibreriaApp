using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Identity
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles =
            {
            "Admin",
            "Usuario"
        };


            foreach (var role in roles)
            {
                var exists = await roleManager
                    .RoleExistsAsync(role);


                if (!exists)
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
        {
            var userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();


            const string adminRole = "Admin";


            // Verificar que exista el rol Admin
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(
                    new IdentityRole(adminRole));
            }


            // Buscar usuario administrador
            var adminUser = await userManager
                .FindByNameAsync("admin");


            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Nombre = "Administrador"
                };


                var result = await userManager
                    .CreateAsync(user, "!Admin123");


                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(
                        user,
                        adminRole);
                }
            }
        }
    }
}
