using MediatR;

namespace Application.Categories.Commands.UpdateCategoryCommand
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
