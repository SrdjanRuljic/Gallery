using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public class InsertUserCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public long RoleId { get; set; }
        public string Password { get; set; }
    }
}
