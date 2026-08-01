using Data.Identity;
using Data.Persistence;
using Data.Repositories;
using Domain;
using Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services,string connectionString)
        {
            // Entity Framework
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Identity
            //services
            //    .AddIdentity<ApplicationUser, IdentityRole>(options =>
            //    {
            //        // Contraseña
            //        options.Password.RequireDigit = true;
            //        options.Password.RequireLowercase = true;
            //        options.Password.RequireUppercase = true;
            //        options.Password.RequireNonAlphanumeric = false;
            //        options.Password.RequiredLength = 8;

            //        // Usuario
            //        options.User.RequireUniqueEmail = true;

            //        // Bloqueo
            //        options.Lockout.MaxFailedAccessAttempts = 5;
            //        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            //    })
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddScoped<IRepository<AutorEntity, int>, AutorRepository>();
            services.AddScoped<IRepository<LibroEntity, int>, LibroRepository>();
            services.AddScoped<ILibroRepository<LibroEntity>,LibroRepository>();
            services.AddScoped<IRepository<PrestamoEntity, int>, PrestamoRepository>();
            services.AddScoped<IPrestamoRepository<PrestamoEntity>, PrestamoRepository>();

            return services;
        }
    }
}
