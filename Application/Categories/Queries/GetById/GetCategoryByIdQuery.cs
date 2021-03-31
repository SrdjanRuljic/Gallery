using MediatR;

namespace Application.Categories.Queries.GetById
{
    public class GetCategoryByIdQuery : IRequest<GetCategoryByIdViewModel>
    {
        public long Id { get; set; }
    }
}
