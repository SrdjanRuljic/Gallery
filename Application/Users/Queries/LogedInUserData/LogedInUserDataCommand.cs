using MediatR;

namespace Application.Users.Queries.LogedInUserData
{
    public class LogedInUserDataCommand : IRequest<LogedInUserDataViewModel>
    {
        public string Username { get; set; }
    }
}
