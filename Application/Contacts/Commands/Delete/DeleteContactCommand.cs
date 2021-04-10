using MediatR;

namespace Application.Contacts.Commands.Delete
{
    public class DeleteContactCommand : IRequest
    {
        public long Id { get; set; }
    }
}
