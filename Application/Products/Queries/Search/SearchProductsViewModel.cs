using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Products.Queries.Search
{
    public class SearchProductsViewModel : IMapFrom<Product>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Extension { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, SearchProductsViewModel>()
                   .ForMember(d => d.Content, opt => opt.MapFrom(s => string.IsNullOrEmpty(s.Content) ?
                                                                      "/assets/images/no-image.png" :
                                                                      s.Content));
        }
    }
}
