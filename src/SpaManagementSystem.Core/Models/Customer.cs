using System.Text.RegularExpressions;

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
            : base(id, email, passwordSalt, passwordHash, phoneNumber)
        {
            FirstName = ValidateFirstName(firstName);
            LastName = ValidateLastName(lastName);
            Gender = ValidateGender(gender);
            DateOfBirth = ValidateDateOfBirth(dateOfBirth);
            Preferences = ValidatePreferences(preferences);
            Notes = ValidateNotes(notes);
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

        private string ValidateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 1 || firstName == string.Empty)
                throw new ArgumentNullException("FirstName cannot be null or empty");


            Regex regex = new Regex("^[a-zA-Z .]+$");

            if (!regex.IsMatch(firstName))
                throw new ArgumentException("FirstName contains prohibited characters");

            return firstName;
        }

        private string ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 1 || lastName == string.Empty)
                throw new ArgumentNullException("FirstName cannot be null or empty");


            Regex regex = new Regex("^[a-zA-Z .]+$");

            if (!regex.IsMatch(lastName))
                throw new ArgumentException("LastName contains prohibited characters");

            return lastName;
        }

        private string ValidateGender(string gender)
        {
            if (string.IsNullOrWhiteSpace(gender))
                throw new ArgumentNullException(nameof(gender), "Gender cannot be null");

            gender = gender.ToLower();

            if (gender != "male" && gender != "female")
                throw new ArgumentException("Invalid gender value", nameof(gender));

            return gender;
        }

        private DateOnly ValidateDateOfBirth(DateOnly dateOfBirth)
        {
            if (dateOfBirth == DateOnly.MinValue)
                throw new ArgumentException("Date of birth cannot be empty", nameof(dateOfBirth));

            if (dateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow.Date))
                throw new ArgumentException("Date of birth cannot be in the future", nameof(dateOfBirth));

            return dateOfBirth;
        }

        private string? ValidatePreferences(string? preferences)
        {
            if (!string.IsNullOrWhiteSpace(preferences) && preferences.Length > 1000)
                throw new ArgumentOutOfRangeException("Preferences length cannot exceed 1000 characters", nameof(preferences));

            return preferences;
        }

        private string? ValidateNotes(string? notes)
        {
            if (!string.IsNullOrWhiteSpace(notes) && notes.Length > 1000)
                throw new ArgumentOutOfRangeException("Notes length cannot exceed 1000 characters", nameof(notes));

            return notes;
        }
    }
}