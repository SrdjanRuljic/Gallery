using AutoMapper;
using Gallery.Common;
using Gallery.WebAPI.Models;

namespace Gallery.WebAPI.AutoMapper
{
    public class AboutAuthorProfile : Profile
    {
        public AboutAuthorProfile()
        {
            CreateMap<AboutAuthorViewModel, AboutAuthorModel>();
            CreateMap<AboutAuthorModel, AboutAuthorViewModel>();
        }
    }
}
