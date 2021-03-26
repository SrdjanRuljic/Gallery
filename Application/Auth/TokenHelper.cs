using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth
{
    public static class TokenHelper
    {
        public static object GenerateJwt(string username, 
                                         string role, 
                                         IJwtFactory jwtFactory) => new
        {
            auth_token = jwtFactory.GenerateEncodedToken(username, role)
        };
    }
}
