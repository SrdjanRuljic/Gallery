using MediatR;

namespace Application.Contacts.Queries.GetById
{
    public class GetContactByIdQuery : IRequest<GetContactByIdViewModel>
    {
        public long Id { get; set; }
    }
}
