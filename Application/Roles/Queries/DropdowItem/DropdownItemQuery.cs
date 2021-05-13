using MediatR;
using System.Collections.Generic;

namespace Application.Roles.Queries.DropdowItem
{
    public class DropdownItemQuery : IRequest<List<DropdownItemViewModel>>
    {
    }
}
