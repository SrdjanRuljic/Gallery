using MediatR;

namespace Application.Contacts.Commands.Insert
{
    public class InsertContactCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
