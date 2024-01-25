﻿namespace AcademicProgressTracker.Domain.Entities
{
    public class User
    {
        public Guid? Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

        public Guid? RoleId { get; set; }
        public Role? Role { get; set; }

        public bool TokenExpired()
        {
            return TokenExpires < DateTime.Now;
        }
    }
}