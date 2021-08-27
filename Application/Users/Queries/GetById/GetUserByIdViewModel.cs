using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;
using System.Linq;

namespace Application.Users.Queries.GetById
{
    public class GetUserByIdViewModel : IMapFrom<AppUser>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string RoleId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppUser, GetUserByIdViewModel>()
                   .ForMember(s => s.RoleId, opt => opt.MapFrom(d => d.UserRoles
                                                                      .Select(x => x.RoleId)
                                                                      .FirstOrDefault()));
        }
    }
}
