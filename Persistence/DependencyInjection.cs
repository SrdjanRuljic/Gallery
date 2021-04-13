using Application.Common.Interfaces;
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

            services.AddDbContext<GalleryMySqlDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("GalleryMySqlDb")));

            services.AddScoped<IGalleryDbContext>(provider =>
                provider.GetService<GalleryDbContext>());

            services.AddScoped<IGalleryMySqlDbContext>(provider =>
                provider.GetService<GalleryMySqlDbContext>());

            return services;
        }
    }
}
