using MediatR;

namespace Application.Categories.Commands.Update
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
