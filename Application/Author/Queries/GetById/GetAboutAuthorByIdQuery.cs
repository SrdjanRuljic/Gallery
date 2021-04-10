using MediatR;

namespace Application.Author.Queries.GetById
{
    public class GetAboutAuthorByIdQuery : IRequest<GetAboutAuthorByIdViewModel>
    {
        public long Id { get; set; }
    }
}
