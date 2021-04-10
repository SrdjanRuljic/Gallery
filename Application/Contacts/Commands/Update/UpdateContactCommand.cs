using MediatR;

namespace Application.Contacts.Commands.Update
{
    public class UpdateContactCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
