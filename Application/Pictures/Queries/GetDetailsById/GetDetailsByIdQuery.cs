using MediatR;

namespace Application.Pictures.Queries.GetDetailsById
{
    public class GetDetailsByIdQuery : IRequest<GetDetailsByIdViewModel>
    {
        public long Id { get; set; }
    }
}
