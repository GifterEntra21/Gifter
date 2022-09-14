using Entities;
using GifterWebApplication.Models.Authentication;

namespace GifterWebApplication.Models.Profile
{
    public class AuthenticationRequestProfile : AutoMapper.Profile
    {
        public AuthenticationRequestProfile()
        {
            CreateMap<AuthenticationRequest, User>();
        }
    }
}
