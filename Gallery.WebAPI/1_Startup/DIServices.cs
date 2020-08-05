using Gallery.BLL;
using Gallery.BLL.Interfaces;
using Gallery.WebAPI.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Gallery.WebAPI._1_Startup
{
    public static class DIServices
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            services.AddTransient<IJwtFactory, JwtFactory>();
            services.AddTransient<IUsersBusiness, UsersBusiness>();
            services.AddTransient<ICategoryBusiness, CategoryBusiness>();
            services.AddTransient<IRolesBusiness, RolesBusiness>();
            services.AddTransient<IPicturesBusiness, PicturesBusiness>();

            services.AddOptions();

            return services;
        }
    }
}
