using MediatR;
using System.Collections.Generic;

namespace Application.Contacts.Queries.GetAll
{
    public class GetAllContactsQuery : IRequest<List<GetAllContactsViewModel>>
    {
    }
}
