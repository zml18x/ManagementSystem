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



        /// <summary>
        /// Updates the basic information of a customer.
        /// </summary>
        /// <param name="firstName">The new first name of the customer.</param>
        /// <param name="lastName">The new last name of the customer.</param>
        /// <param name="gender">The new gender of the customer.</param>
        /// <param name="dateOfBirth">The new date of birth of the customer.</param>
        /// <param name="preferences">The new preferences of the customer.</param>
        /// <returns>True if any information was updated; otherwise, false.</returns>
        public bool UpdateBasicInfromation(string? firstName = null, string? lastName = null, string? gender = null, DateOnly? dateOfBirth = null, string? preferences = null)
        {
            var anyInfoUpdated = false;

            if (firstName != null)
            {
                FirstName = firstName;
                anyInfoUpdated = true;
            }
                
            if (lastName != null)
            {
                LastName = lastName;
                anyInfoUpdated = true;
            }

            if (gender != null)
            {
                Gender = gender;
                anyInfoUpdated = true;
            }

            if (dateOfBirth != null)
            {
                DateOfBirth = (DateOnly)dateOfBirth;
                anyInfoUpdated = true;
            }

            if (preferences != null)
            {
                Preferences = preferences;
                anyInfoUpdated = true;
            }

            if(anyInfoUpdated)
                LastUpdateAt = DateTime.UtcNow;
            
            return anyInfoUpdated;
        }
    }
}