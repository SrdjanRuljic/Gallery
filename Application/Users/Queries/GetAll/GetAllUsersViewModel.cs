using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;
using System.Linq;

namespace Application.Users.Queries.GetAll
{
    public class GetAllUsersViewModel : IMapFrom<AppUser>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppUser, GetAllUsersViewModel>()
                   .ForMember(s => s.Role, opt => opt.MapFrom(d => d.UserRoles
                                                                    .Select(x => x.Role.Name)
                                                                    .FirstOrDefault()));
        }
    }
}
