using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Users.Queries.GetById
{
    public class GetUserByIdViewModel : IMapFrom<User>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public long RoleId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, GetUserByIdViewModel>();
        }
    }
}
