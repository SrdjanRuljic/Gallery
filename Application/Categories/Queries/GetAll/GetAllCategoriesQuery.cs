using Application.Common.Pagination.Models;
using MediatR;

namespace Application.Categories.Queries.GetAll
{
    public class GetAllCategoriesQuery : PaginationViewModel, IRequest<PaginationResultViewModel<GetAllCategoriesViewModel>>
    {
    }
}
