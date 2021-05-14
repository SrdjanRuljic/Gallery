using MediatR;

namespace Application.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest
    {
        public long Id { get; set; }
    }
}
