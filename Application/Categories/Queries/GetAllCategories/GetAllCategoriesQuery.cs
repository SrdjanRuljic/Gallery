using MediatR;
using System.Collections.Generic;

namespace Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<List<GetAllCategoriesViewModel>>
    {
    }
}
