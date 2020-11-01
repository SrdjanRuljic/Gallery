using AutoMapper;
using Gallery.DTO;
using Gallery.WebAPI.Models;

namespace Gallery.WebAPI.AutoMapper
{
    public class AboutAuthorProfile : Profile
    {
        public AboutAuthorProfile()
        {
            CreateMap<AboutAuthorViewModel, AboutAuthorDTO>();
            CreateMap<AboutAuthorDTO, AboutAuthorViewModel>();
        }
    }
}
