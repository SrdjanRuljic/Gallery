using MediatR;

namespace Application.Pictures.Commands.Delete
{
    public class DeletePictureCommand : IRequest
    {
        public long Id { get; set; }
    }
}
