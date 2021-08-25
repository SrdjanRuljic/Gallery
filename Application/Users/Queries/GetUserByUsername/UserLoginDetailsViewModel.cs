using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Users.Queries.GetUserByUsername
{
    public class UserLoginDetailsViewModel : IMapFrom<User>
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserLoginDetailsViewModel>();
        }
    }
}
