namespace SpaManagementSystem.Infrastructure.Dto
{
    public class CustomerAccountDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Preferences { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        


        public CustomerAccountDto(Guid id, string email, string phoneNumber, string firstName, string lastName, string gender, DateOnly dateOfBirth,
            string? preferences, bool emailConfirmed, bool phoneNumberConfirmed, bool twoFactorEnabled)
        {
            Id = id;
            Email = email;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Preferences = preferences;
            EmailConfirmed = emailConfirmed;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            TwoFactorEnabled = twoFactorEnabled;
        }
    }
}