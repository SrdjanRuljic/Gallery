using MediatR;

namespace Application.Categories.Commands.InsertCategoryCommand
{
    public class InsertCategoryCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
