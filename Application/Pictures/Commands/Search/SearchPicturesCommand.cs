using Application.Common.Pagination.Models;
using MediatR;

namespace Application.Pictures.Commands.Search
{
    public class SearchPicturesCommand : PaginationViewModel, IRequest<PaginationResultViewModel<SearchPicturesCommandViewModel>>
    {
        public string Name { get; set; }
        public long? CategoryId { get; set; }
    }
}
