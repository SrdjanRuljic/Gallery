﻿using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;
using System.Linq;

namespace Application.Users.Queries.LogedInUserData
{
    public class LogedInUserDataViewModel : IMapFrom<AppUser>
    {
        public string DisplayName { get; set; }
        public bool IsAdmin { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppUser, LogedInUserDataViewModel>()
                .ForMember(d => d.DisplayName, opt => opt.MapFrom(s => (string.IsNullOrEmpty(s.FirstName) &&
                                                                       string.IsNullOrEmpty(s.LastName)) ?
                                                                       "NN" :
                                                                       s.FirstName + " " + s.LastName))
                .ForMember(d => d.IsAdmin, opt => opt.MapFrom(s => s.UserRoles.Any(x => x.Role
                                                                                         .Name
                                                                                         .Contains(Domain.Roles.Admin.ToString()))));
        }
    }
}
