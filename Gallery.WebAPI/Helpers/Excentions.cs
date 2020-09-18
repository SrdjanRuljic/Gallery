using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.WebAPI.Helpers
{
    public static class Excentions
    {
        public static void AddApplicationExcention(this HttpResponse respounse, string message)
        {
            respounse.Headers.Add("Application-Exception", message);
            respounse.Headers.Add("Access-Control-Expose-Headers", "Application-Exception");
            respounse.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}
