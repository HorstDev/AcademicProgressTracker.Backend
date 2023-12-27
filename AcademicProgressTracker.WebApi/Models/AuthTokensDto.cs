using AcademicProgressTracker.Application.Auth;

namespace AcademicProgressTracker.WebApi.Models
{
    public class AuthTokensDTO
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }

        public AuthTokensDTO(AccessToken accessToken, RefreshToken refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
