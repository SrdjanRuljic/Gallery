using MediatR;

namespace Application.Users.Queries.LogedInUserData
{
    public class LogedInUserDataQuery : IRequest<LogedInUserDataViewModel>
    {
        public string Username { get; set; }
    }
}
