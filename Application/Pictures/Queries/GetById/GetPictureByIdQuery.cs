using MediatR;

namespace Application.Pictures.Queries.GetById
{
    public class GetPictureByIdQuery : IRequest<GetPictureByIdViewModel>
    {
        public long Id { get; set; }
    }
}
