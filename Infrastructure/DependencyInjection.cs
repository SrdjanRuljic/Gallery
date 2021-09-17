﻿using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Auth;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                                IConfiguration configuration)
        {

            services.AddTransient<IJwtFactory, JwtFactory>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("GalleryDb")));

            services.AddIdentityCore<AppUser>()
                    .AddRoles<AppRole>()
                    .AddRoleManager<RoleManager<AppRole>>()
                    .AddRoleValidator<RoleValidator<AppRole>>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidIssuer = configuration["Jwt:Issuer"],

                            ValidateAudience = false,
                            ValidAudience = configuration["Jwt:Audience"],

                            ValidateLifetime = true,

                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                        };
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole(Domain.Roles.Admin.ToString()));
            });

            return services;
        }
    }
}
