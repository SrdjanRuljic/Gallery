using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Users.Queries.GetAll
{
    public class GetAllUsersViewModel : IMapFrom<AppUser>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppUser, GetAllUsersViewModel>();
        }
    }
}
