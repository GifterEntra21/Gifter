using GifterWebApplication.Models.Authentication;

namespace GifterWebApplication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest model);
    }
}
