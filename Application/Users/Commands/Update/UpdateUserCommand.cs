using MediatR;

namespace Application.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string RoleId { get; set; }
    }
}
