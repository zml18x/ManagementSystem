namespace SpaManagementSystem.Core.Models
{
    public sealed class Customer : User
    {
        public string FirstName {get; private set;}
        public string LastName {get; private set;}
        public string Gender {get; private set;}
        public DateOnly DateOfBirth {get; private set;}
        public string? Preferences {get; private set;}
        public string? Notes {get; private set;}


        
        public Customer(Guid id, string email, byte[] passwordSalt, byte[] passwordHash, string phoneNumber, string firstName, string lastName, string gender,
            DateOnly dateOfBirth, string? preferences = null, string? notes = null, bool twoFactorEnabled = false) 
            : base(id, email, passwordSalt, passwordHash, phoneNumber, twoFactorEnabled)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Preferences = preferences;
            Notes = notes;
        }
    }
}