using AcademicProgressTracker.Application.Common.Interfaces;
using System.Security.Cryptography;

namespace AcademicProgressTracker.Application.Auth
{
    public class RefreshToken : IToken
    {
        public string Token { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }

        public RefreshToken(int lifeTimeInDays)
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            Expires = DateTime.Now.AddDays(lifeTimeInDays);
            Created = DateTime.Now;
        }
    }
}
