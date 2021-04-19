using MediatR;

namespace Application.Products.Queries.GetById
{
    public class GetProductByIdQuery : IRequest<GetProductByIdViewModel>
    {
        public long Id { get; set; }
    }
}
