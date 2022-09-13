using Entities;
using AutoMapper;
using GifterWebApplication.Models.Users;

namespace GifterWebApplication.Models.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<UserInsertViewModel, User>();
            CreateMap<UserDeleteViewModel, User>();
            CreateMap<UserLoginViewModel, User>();
            CreateMap<UserSelectViewModel, User>();
            CreateMap<User ,UserInsertViewModel >();
            CreateMap<User ,UserDeleteViewModel >();
            CreateMap<User,UserLoginViewModel>();
            CreateMap<User,UserSelectViewModel>();
        }
    }
}
