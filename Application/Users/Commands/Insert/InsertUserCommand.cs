using MediatR;

namespace Application.Users.Commands.Insert
{
    public class InsertUserCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string RoleId { get; set; }
        public string Password { get; set; }
    }
}
