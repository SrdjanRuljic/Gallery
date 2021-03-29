using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            profile.CreateMap<User, UserLoginDetailsViewModel>()
                .ForMember(d => d.Role, opt => opt.MapFrom(s => s.Role.Name));
        }
    }
}
