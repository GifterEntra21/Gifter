using GifterWebApplication.Models.Authentication;

namespace GifterWebApplication.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest model);
    }
}
