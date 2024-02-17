using System.Text.RegularExpressions;

namespace SpaManagementSystem.Core.Models
{
    public abstract class User
    {
        public Guid Id {get; protected set;}
        public string Email {get; protected set;}
        public byte[] PasswordSalt {get; protected set;}
        public byte[] PasswordHash {get; protected set;}
        public string PhoneNumber {get; protected set;}
        public bool EmailConfirmed {get; protected set;}
        public bool PhoneNumberConfirmed {get; protected set;}
        public bool TwoFactorEnabled {get; protected set;}
        public int AccesFailedCount {get; protected set;}
        public bool LockoutEnabled {get; protected set;}
        public DateTime? LockoutEnd {get; protected set;}
        public DateTime CreatedAt {get; protected set;}
        public DateTime LastUpdateAt {get; protected set;}
        public DateTime? DeactivatedAt {get; protected set;}



        public User(Guid id, string email, byte[] passwordSalt, byte[] passwordHash, string phoneNumber)
        {
            Id = ValidateId(id);
            Email = ValidateEmail(email);
            PasswordHash = ValidatePasswordHash(passwordHash);
            PasswordSalt = ValidatePasswordSalt(passwordSalt);
            PhoneNumber = ValidatePhoneNumber(phoneNumber);
            EmailConfirmed = false;
            PhoneNumberConfirmed = false;
            TwoFactorEnabled = false;
            AccesFailedCount = 0;
            LockoutEnabled = false;
            LockoutEnd = null;
            CreatedAt = DateTime.UtcNow;
            LastUpdateAt = CreatedAt;
            DeactivatedAt = null;
        }



        private Guid ValidateId(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("User Id cannot be a Guid.Empty", nameof(id));

            return id;
        }

        private string ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "Email cannot be null");

            if (email.Contains(' '))
                throw new ArgumentException(nameof(email), "Email cannot contains whitespace");

            Regex regex = new Regex("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$");

            if (!regex.IsMatch(email))
                throw new ArgumentException("Invalid email format", nameof(email));

            return email;
        }

        private byte[] ValidatePasswordHash(byte[] passwordHash)
        {
            if(passwordHash == null || passwordHash.Length < 1)
                throw new ArgumentNullException(nameof(passwordHash),"PasswordHash cannot be null");

            return passwordHash;
        }

        private byte[] ValidatePasswordSalt(byte[] passwordSalt)
        {

            if (passwordSalt == null || passwordSalt.Length < 1)
                throw new ArgumentNullException(nameof(passwordSalt), "PasswordSalt cannot be null");

            return passwordSalt;
        }

        private string ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length < 1)
                throw new ArgumentNullException(nameof(phoneNumber), "Phone Number cannot be null or white space");

            Regex regex = new Regex("^[0-9]+$");

            if (!regex.IsMatch(phoneNumber))
                throw new ArgumentException("The phone number can only consist of digits", nameof(phoneNumber));

            return phoneNumber;
        }
    }
}