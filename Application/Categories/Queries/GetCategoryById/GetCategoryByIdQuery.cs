using MediatR;

namespace Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<GetCategoryByIdViewModel>
    {
        public long Id { get; set; }
    }
}
