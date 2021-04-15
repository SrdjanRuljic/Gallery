using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Auth;
using Infrastructure.Elasticsearch;
using Infrastructure.Elasticsearch.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<Product>>();
            services.AddSingleton(typeof(ILogger), logger);
            services.AddTransient<IJwtFactory, JwtFactory>();
            services.AddTransient<IBlogService, FileBlogService>();
            services.AddElasticsearch(configuration);

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

            return services;
        }
    }
}
