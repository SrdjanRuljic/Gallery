using AutoMapper;
using Gallery.Common;
using Gallery.WebAPI.Models;

namespace Gallery.WebAPI.AutoMapper
{
    public class DropdownProfile : Profile
    {
        public DropdownProfile()
        {
            CreateMap<DropdownItemModel, DropdownItemViewModel>();
        }
    }
}
