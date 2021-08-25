using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
                                                             IConfiguration configuration)
        {
            services.AddDbContext<GalleryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("GalleryDb")));

            services.AddIdentityCore<User>()
                    .AddRoles<Role>()
                    .AddRoleManager<RoleManager<Role>>()
                    .AddRoleValidator<RoleValidator<Role>>()
                    .AddEntityFrameworkStores<GalleryDbContext>();

            services.AddScoped<IGalleryDbContext>(provider =>
                provider.GetService<GalleryDbContext>());

            return services;
        }
    }
}
