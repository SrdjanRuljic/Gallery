using MediatR;
using System.Collections.Generic;

namespace Application.Codes.Queries.GetParents
{
    public class GetParentsCodesQuery : IRequest<List<GetParentsCodesQueryViewModel>>
    {
        public long LanguageId { get; set; }
    }
}
