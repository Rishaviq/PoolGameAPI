using Org.BouncyCastle.Security;
using System.Security.Cryptography;

namespace PoolGameAPI.Controllers
{
    public sealed class PasswordHasher : IPasswordHasher
    {

        private const int SaltSize = 16;
        private const int HashSize = 16;
        private const int Iterations = 100000;
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;
        public string Hash(string password) {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);


            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }
    
        
    }
           
}
