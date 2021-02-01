using AutoMapper;
using Gallery.Common;
using Gallery.WebAPI.Models;

namespace Gallery.WebAPI.AutoMapper
{
    public class PrictureProfile : Profile
    {
        public PrictureProfile()
        {
            CreateMap<PictureViewModel, PictureModel>();
            CreateMap<PictureModel, PictureViewModel>();

            CreateMap<PictureDetailsModel, PictureDetailsViewModel>();
        }
    }
}
