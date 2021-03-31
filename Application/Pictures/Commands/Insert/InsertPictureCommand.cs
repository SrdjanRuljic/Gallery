using MediatR;

namespace Application.Pictures.Commands.Insert
{
    public class InsertPictureCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Extension { get; set; }
    }
}
