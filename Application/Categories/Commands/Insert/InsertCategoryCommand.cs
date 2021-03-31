using MediatR;

namespace Application.Categories.Commands.Insert
{
    public class InsertCategoryCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
