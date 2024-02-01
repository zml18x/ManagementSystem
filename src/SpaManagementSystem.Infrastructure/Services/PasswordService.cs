using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using SpaManagementSystem.Infrastructure.Services.Interfaces;

namespace SpaManagementSystem.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        private const int KeySize = 64;
        private const int Iterations = 350000;
        private readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;



        public void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            passwordSalt = GenerateSalt();
            passwordHash = ComputeHash(password, passwordSalt);
        }

        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, passwordSalt, Iterations, HashAlgorithm, KeySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, passwordHash);
        }

        public void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "Password cannot be empty");

            if (password.Length < 8 || password.Length > 100)
                throw new ArgumentException("The password should be between 8 and 100 characters long");

            Regex regex = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\\s).{8,100}$");

            if (!regex.IsMatch(password))
                throw new ArgumentException("The password should contain at least 1 lowercase letter, 1 uppercase letter, 1 number, 1 special character. It should be 8-100 characters long.");
        }

        private byte[] GenerateSalt()
        {
            byte[] passwordSalt = RandomNumberGenerator.GetBytes(KeySize);

            return passwordSalt;
        }

        private byte[] ComputeHash(string password, byte[] passwordSalt)
        {
            byte[] passwordHash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), passwordSalt, Iterations, HashAlgorithm, KeySize);

            return passwordHash;
        }
    }
}