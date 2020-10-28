using Microsoft.Extensions.Configuration;
using System.IO;

namespace Gallery.DAL
{
    internal class Connection
    {
        public string ConnectionString()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build().GetSection("ConnectionStrings").GetSection("GalleryDb").Value;
        }
    }
}
