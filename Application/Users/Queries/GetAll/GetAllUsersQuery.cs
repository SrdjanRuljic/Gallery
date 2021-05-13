using MediatR;
using System.Collections.Generic;

namespace Application.Users.Queries.GetAll
{
    public class GetAllUsersQuery : IRequest<List<GetAllUsersViewModel>>
    {
    }
}
