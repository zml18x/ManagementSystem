using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using SpaManagementSystem.Infrastructure.Services.Interfaces;

namespace SpaManagementSystem.Infrastructure.Services
{
    /// <summary>
    /// Provides methods for hashing passwords securely and validating password strength.
    /// </summary>
    public class PasswordService : IPasswordService
    {
        private const int KeySize = 64;
        private const int Iterations = 350000;
        private readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;



        /// <summary>
        /// Hashes the given password using a randomly generated salt.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="passwordHash">Output parameter for the generated password hash.</param>
        /// <param name="passwordSalt">Output parameter for the generated password salt.</param>
        public void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            passwordSalt = GenerateSalt();
            passwordHash = ComputeHash(password, passwordSalt);
        }

        /// <summary>
        /// Verifies whether the provided password matches the given password hash and salt.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="passwordHash">The hash of the original password.</param>
        /// <param name="passwordSalt">The salt used for hashing the original password.</param>
        /// <returns>True if the password is verified; otherwise, false.</returns>
        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, passwordSalt, Iterations, HashAlgorithm, KeySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, passwordHash);
        }

        /// <summary>
        /// Validates the strength of a password according to specified criteria.
        /// </summary>
        /// <param name="password">The password to validate.</param>
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

        /// <summary>
        /// Generates a cryptographically secure random salt.
        /// </summary>
        /// <returns>The generated salt.</returns>
        private byte[] GenerateSalt()
        {
            byte[] passwordSalt = RandomNumberGenerator.GetBytes(KeySize);

            return passwordSalt;
        }

        /// <summary>
        /// Computes the hash of the password using the given salt.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="passwordSalt">The salt used for hashing.</param>
        /// <returns>The computed hash.</returns>
        private byte[] ComputeHash(string password, byte[] passwordSalt)
        {
            byte[] passwordHash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), passwordSalt, Iterations, HashAlgorithm, KeySize);

            return passwordHash;
        }
    }
}