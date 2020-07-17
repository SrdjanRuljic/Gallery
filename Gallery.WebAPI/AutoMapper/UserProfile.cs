using AutoMapper;
using Gallery.Common;
using Gallery.WebAPI.Models;

namespace Gallery.WebAPI.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserListVirewModel>();
            CreateMap<LogedInUserData, LogedInUserDataViewModel>();
            CreateMap<InsertUserViewModel, UserModel>();
            CreateMap<UserModel, UpdateUserViewModel>();
            CreateMap<UpdateUserViewModel, UserModel>();
        }
    }
}
