using MediatR;

namespace Application.Products.Commands.Delete
{
    public class DeleteProductCommand : IRequest
    {
        public long Id { get; set; }
    }
}
