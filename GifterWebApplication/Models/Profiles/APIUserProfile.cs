using Entities;
using GifterWebApplication.Models.Users;

namespace GifterWebApplication.Models.Profiles
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<UserInsertViewModel, APIUser>();
            CreateMap<UserDeleteViewModel, APIUser>();
            CreateMap<UserLoginViewModel, APIUser>();
            CreateMap<UserSelectViewModel, APIUser>();
            CreateMap<UserUpdateViewModel, APIUser>();

            CreateMap<APIUser, UserInsertViewModel>();
            CreateMap<APIUser, UserDeleteViewModel>();
            CreateMap<APIUser, UserLoginViewModel>();
            CreateMap<APIUser, UserSelectViewModel>();
            CreateMap<APIUser, UserUpdateViewModel>();
        }
    }
}
