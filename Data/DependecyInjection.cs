using Application.Authentication.Configuration;
using Application.Authentication.Interfaces;
using Data.Authentication;
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
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Te hace falta la conexion a la bd");
            
            // Entity Framework
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                    // Contraseña
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 8;

                    // Bloqueo
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                    options.SignIn.RequireConfirmedAccount = false;
                   
           })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            //servicios de autenticacion
           
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();

            //repositorios
            services.AddScoped<IRepository<AutorEntity, int>, AutorRepository>();
            services.AddScoped<IRepository<LibroEntity, int>, LibroRepository>();
            services.AddScoped<ILibroRepository<LibroEntity>,LibroRepository>();
            services.AddScoped<IRepository<PrestamoEntity, int>, PrestamoRepository>();
            services.AddScoped<IPrestamoRepository<PrestamoEntity>, PrestamoRepository>();

            return services;
        }
    }
}
