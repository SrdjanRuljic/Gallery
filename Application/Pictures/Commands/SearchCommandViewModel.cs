using AutoMapper;
using Domain.Entities;
using Northwind.Application.Common.Mappings;

namespace Application.Pictures.Commands
{
    public class SearchCommandViewModel : IMapFrom<Picture>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Extension { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Picture, SearchCommandViewModel>();
        }
    }
}
