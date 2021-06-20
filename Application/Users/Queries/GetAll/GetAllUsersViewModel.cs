﻿using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Users.Queries.GetAll
{
    public class GetAllUsersViewModel : IMapFrom<User>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, GetAllUsersViewModel>()
                   .ForMember(d => d.Role, opt => opt.MapFrom(s => s.Role.Name));
        }
    }
}