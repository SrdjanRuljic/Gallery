using Gallery.WebAPI.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Gallery.WebAPI._1_Startup
{
    public static class DIServices
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            services.AddTransient<IJwtFactory, JwtFactory>();

            services.AddOptions();

            return services;
        }
    }
}
