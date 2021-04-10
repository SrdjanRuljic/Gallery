using MediatR;

namespace Application.Author.Commands.Update
{
    public class UpdateAboutAuthorCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string Content { get; set; }
        public string Extension { get; set; }
    }
}
