using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Pictures.Commands.Search
{
    public class SearchPicturesCommandViewModel : IMapFrom<Picture>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Extension { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Picture, SearchPicturesCommandViewModel>();
        }
    }
}
