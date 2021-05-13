using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Roles.Queries.DropdowItem
{
    public class DropdownItemViewModel : IMapFrom<Role>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Role, DropdownItemViewModel>();
        }
    }
}
