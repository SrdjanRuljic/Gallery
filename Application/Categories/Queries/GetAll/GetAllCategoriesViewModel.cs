using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Categories.Queries.GetAll
{
    public class GetAllCategoriesViewModel : IMapFrom<Category>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, GetAllCategoriesViewModel>();
        }
    }
}
