using MediatR;

namespace Application.Users.Queries.GetById
{
    public class GetUserByIdQuery : IRequest<GetUserByIdViewModel>
    {
        public long Id { get; set; }
    }
}
