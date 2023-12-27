namespace AcademicProgressTracker.Application.Common.Interfaces
{
    public interface IPasswordHasher
    {
        void CreateHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
