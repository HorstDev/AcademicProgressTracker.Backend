namespace AcademicProgressTracker.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

        public ICollection<Role> Roles { get; set; } = new List<Role>();
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        public ICollection<Profile> Profiles { get; set; } = new List<Profile>();

        public User(string email, byte[] passwordHash, byte[] passwordSalt)
        {
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public bool TokenExpired()
        {
            return TokenExpires < DateTime.Now;
        }
    }
}
