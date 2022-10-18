using Entities;
using GifterWebApplication.Models.Authentication;

namespace GifterWebApplication.Models.Profiles
{

        public class AuthenticationRequestProfile : AutoMapper.Profile
        {
            public AuthenticationRequestProfile()
            {
                CreateMap<AuthenticationRequest, APIUser>();
            }
        }
    
}
