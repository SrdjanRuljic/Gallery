using AutoMapper;
using Gallery.Common;
using Gallery.Common.UserModels;
using Gallery.WebAPI.Models;

namespace Gallery.WebAPI.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserListVirewModel>();
            CreateMap<LogedInUserData, LogedInUserDataViewModel>();
            CreateMap<InsertUserViewModel, InsertUserModel>();
            CreateMap<UpdateUserModel, UpdateUserViewModel>();
            CreateMap<UpdateUserViewModel, UpdateUserModel>();
            CreateMap<ListUserModel, UserListVirewModel>();
        }
    }
}
