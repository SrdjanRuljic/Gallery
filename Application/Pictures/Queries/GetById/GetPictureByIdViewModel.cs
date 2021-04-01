using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Pictures.Queries.GetById
{
    public class GetPictureByIdViewModel : IMapFrom<Picture>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Extension { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Picture, GetPictureByIdViewModel>()
                   .ForMember(d => d.Content, opt => opt.MapFrom(s => string.IsNullOrEmpty(s.Content) ?
                                                                      "/assets/images/no-image.png" :
                                                                      s.Content));
        }
    }
}
