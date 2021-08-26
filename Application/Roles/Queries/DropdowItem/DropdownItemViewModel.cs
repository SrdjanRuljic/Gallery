using AutoMapper;
using Gallery.Application.Common.Mappings;
using Microsoft.AspNetCore.Identity;

namespace Application.Roles.Queries.DropdowItem
{
    public class DropdownItemViewModel : IMapFrom<IdentityRole>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IdentityRole, DropdownItemViewModel>();
        }
    }
}
