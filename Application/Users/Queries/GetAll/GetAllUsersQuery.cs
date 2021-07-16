using Application.Common.Pagination.Models;
using MediatR;

namespace Application.Users.Queries.GetAll
{
    public class GetAllUsersQuery : PaginationViewModel, IRequest<PaginationResultViewModel<GetAllUsersViewModel>>
    {
    }
}
