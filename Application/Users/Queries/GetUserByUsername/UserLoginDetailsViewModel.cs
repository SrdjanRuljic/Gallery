using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;
using System.Linq;

namespace Application.Users.Queries.GetUserByUsername
{
    public class UserLoginDetailsViewModel : IMapFrom<AppUser>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppUser, UserLoginDetailsViewModel>()
                .ForMember(s => s.Role, opt => opt.MapFrom(d => d.UserRoles
                                                                 .Select(x => x.Role.Name)
                                                                 .FirstOrDefault()));
        }
    }
}
