using MediatR;

namespace Application.Users.Commands.UpdatePassword
{
    public class UpdatePasswordCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
    }
}
