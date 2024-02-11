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



        public User(Guid id, string email, byte[] passwordSalt, byte[] passwordHash, string phoneNumber, bool twoFactorEnabled = false)
        {
            Id = id;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
            EmailConfirmed = false;
            PhoneNumberConfirmed = false;
            TwoFactorEnabled = twoFactorEnabled;
            AccesFailedCount = 0;
            LockoutEnabled = false;
            LockoutEnd = null;
            CreatedAt = DateTime.UtcNow;
            LastUpdateAt = CreatedAt;
            DeactivatedAt = null;
        }
    }
}