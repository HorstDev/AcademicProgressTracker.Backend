using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicProgressTracker.Application.Common.Interfaces
{
    public interface IPasswordHasher
    {
        void CreateHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
