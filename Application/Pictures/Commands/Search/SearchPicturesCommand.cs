using MediatR;
using System.Collections.Generic;

namespace Application.Pictures.Commands.Search
{
    public class SearchPicturesCommand : IRequest<List<SearchPicturesCommandViewModel>>
    {
        public string Name { get; set; }
        public long? CategoryId { get; set; }
    }
}
