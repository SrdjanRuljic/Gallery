using MediatR;

namespace Application.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest
    {
        public long Id { get; set; }
    }
}
