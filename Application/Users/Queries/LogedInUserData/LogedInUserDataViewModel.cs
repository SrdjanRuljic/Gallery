using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Users.Queries.LogedInUserData
{
    public class LogedInUserDataViewModel : IMapFrom<User>
    {
        public string DisplayName { get; set; }
        public bool IsAdmin { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, LogedInUserDataViewModel>()
                .ForMember(d => d.DisplayName, opt => opt.MapFrom(s => (string.IsNullOrEmpty(s.FirstName) &&
                                                                       string.IsNullOrEmpty(s.LastName)) ?
                                                                       "NN" :
                                                                       s.FirstName + " " + s.LastName));
        }
    }
}
