using AutoMapper;
using Gallery.Common;
using Gallery.WebAPI.Models;

namespace Gallery.WebAPI.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryModel, CategoryViewModel>();
            CreateMap<CategoryViewModel, CategoryModel>();
        }
    }
}
