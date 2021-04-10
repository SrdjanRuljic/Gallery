using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Author.Queries.GetById
{
    public class GetAboutAuthorByIdViewModel : IMapFrom<AboutAuthor>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string Content { get; set; }
        public string Extension { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AboutAuthor, GetAboutAuthorByIdViewModel>()
                   .ForMember(d => d.Content, opt => opt.MapFrom(s => string.IsNullOrEmpty(s.Content) ?
                                                                      "/assets/images/no-image.png" :
                                                                      s.Content));
        }
    }
}
