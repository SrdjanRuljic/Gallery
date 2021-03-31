using MediatR;
using System.Collections.Generic;

namespace Application.Categories.Queries.GetAll
{
    public class GetAllCategoriesQuery : IRequest<List<GetAllCategoriesViewModel>>
    {
    }
}
