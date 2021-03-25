using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUserByUsername
{
    public class GetUserByUsernameQuery : IRequest<UserLoginDetailsViewModel>
    {
        public string Username { get; set; }
    }
}
