using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Pictures.Queries.GetDetailsById
{
    public class GetDetailsByIdViewModel : IMapFrom<Picture>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Extension { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Picture, GetDetailsByIdViewModel>()
                   .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name));

            profile.CreateMap<Picture, GetDetailsByIdViewModel>()
                   .ForMember(d => d.Content, opt => opt.MapFrom(s => string.IsNullOrEmpty(s.Content) ?
                                                                      "/assets/images/no-image.png" :
                                                                      s.Content));
        }
    }
}
