using Entities;
using GifterWebApplication.Models.Users;

namespace GifterWebApplication.Models.Profiles
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<UserLoginViewModel, APIUser>();
            CreateMap<APIUser, UserLoginViewModel>();

        }
    }
}
