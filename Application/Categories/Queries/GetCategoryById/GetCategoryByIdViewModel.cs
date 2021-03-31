using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdViewModel : IMapFrom<Category>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, GetCategoryByIdViewModel>();
        }
    }
}
