using FluentAssertions;
using SpaManagementSystem.Core.Models;

namespace SpaManagementSystem.Core.Tests.ModelsTests
{
    public class CustomerValidationTests
    {
        [Fact]
        public void CustomerConstructor_SetsProperties_Correctly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "example@mail.com";
            var passwordSalt = new byte[64];
            var passwordHash = new byte[128];
            var phoneNumber = "123456789";
            var firstName = "TestFirstName";
            var lastName = "TestLastName";
            var gender = "male";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var preferences = "ExamplePreferences";
            var notes = "ExampleNotes";

            // Act
            var customer = new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences, notes);

            // Assert
            customer.Id.Should().Be(id);
            customer.Email.Should().Be(email);
            customer.PasswordSalt.Should().Equal(passwordSalt);
            customer.PasswordHash.Should().Equal(passwordHash);
            customer.PhoneNumber.Should().Be(phoneNumber);
            customer.FirstName.Should().Be(firstName);
            customer.LastName.Should().Be(lastName);
            customer.Gender.Should().Be(gender);
            customer.DateOfBirth.Should().Be(dateOfBirth);
            customer.Preferences.Should().Be(preferences);
            customer.Notes.Should().Be(notes);
        }

        [Fact]
        public void CustomerValidateEmail_ShouldThrowArgumentNullException_WhenEmailIsNullOrEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var passwordSalt = new byte[64];
            var passwordHash = new byte[64];
            var phoneNumber = "123456789";
            var firstName = "TestFirstName";
            var lastName = "TestLastName";
            var gender = "male";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var preferences = "ExamplePreferences";
            var notes = "ExampleNotes";

            // Act
            var actions = new[]
            {
                () => new Customer(id, null, passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences, notes),
                () => new Customer(id, string.Empty, passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences, notes),
                () => new Customer(id, " ", passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences, notes),
            };

            // Assert
            foreach (var action in actions)
                action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CustomerValidateEmail_ShouldThrowArgumentException_WhenEmailContainsForbiddenCharacters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var passwordSalt = new byte[64];
            var passwordHash = new byte[64];
            var phoneNumber = "123456789";
            var firstName = "TestFirstName";
            var lastName = "TestLastName";
            var gender = "male";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var preferences = "ExamplePreferences";
            var notes = "ExampleNotes";

            // Act
            var actions = new List<Action>();
            var allowedCharacters = new char[] { '-', '.', '_', '@' };

            for (var i = 0; i < 128; i++)
            {
                if (allowedCharacters.Contains((char)i))
                    continue;

                if (i == 48)
                {
                    i = 57;
                    continue;
                }
                else if(i == 65)
                {
                    i = 90;
                    continue;
                }
                else if(i == 97)
                {
                    i = 122;
                    continue;
                }

                actions.Add(() => new Customer(id, $"ex{(char)i}ample@mail.com", passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences, notes));
            }

            // Assert
            foreach (var action in actions)
                action.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void CustomerValidateEmail_ShouldThrowArgumentException_WhenEmailFormatIsInvalid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var passwordSalt = new byte[64];
            var passwordHash = new byte[64];
            var phoneNumber = "123456789";
            var firstName = "TestFirstName";
            var lastName = "TestLastName";
            var gender = "male";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var preferences = "ExamplePreferences";
            var notes = "ExampleNotes";

            // Act
            var invalidEmails = new List<string>
            {
                "@",
                "@example",
                "@exmaple.com",
                "example@",
                "example@e@example.com",
                "example@example",
                "example.example.com",
                "@example.com",
                "example@",
                "example@.com",
                "example.com@",
                "exa mple@example.com",
                "example@exa mple.com",
                "example@example..com",
                "example@example.com.",
                " example@example.com",
                " example@example.com ",
            };

            var actions = new List<Action>();

            foreach(var email in invalidEmails)
                actions.Add(() => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences, notes));

            // Assert
            foreach (var action in actions)
                action.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void CustomerValidatePasswordHash_ShouldThrowArgumentNullExcepiton_WhenPasswordHashIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "example@mail.com";
            var passwordSalt = new byte[64];
            var phoneNumber = "123456789";
            var firstName = "TestFirstName";
            var lastName = "TestLastName";
            var gender = "male";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var preferences = "ExamplePreferences";
            var notes = "ExampleNotes";

            // Act
            Action action = () => new Customer(id, email, passwordSalt, null, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences, notes);

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CustomerValidatePasswordSalt_ShouldThrowArgumentNullExcepiton_WhenPasswordSaltIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "example@mail.com";
            var passwordHash = new byte[64];
            var phoneNumber = "123456789";
            var firstName = "TestFirstName";
            var lastName = "TestLastName";
            var gender = "male";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var preferences = "ExamplePreferences";
            var notes = "ExampleNotes";

            // Act
            Action action = () => new Customer(id, email, null, passwordHash, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences, notes);

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CustomerValidatePhoneNumber_ShouldThrowArgumentNullException_WhenPhoneNumberIsNullOrEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "example@mail.com";
            var passwordSalt = new byte[64];
            var passwordHash = new byte[64];
            var firstName = "TestFirstName";
            var lastName = "TestLastName";
            var gender = "male";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var preferences = "ExamplePreferences";
            var notes = "ExampleNotes";

            // Act
            var actions = new[]
            {
                () => new Customer(id, email, passwordSalt, passwordHash, null, firstName, lastName, gender, dateOfBirth, preferences, notes),
                () => new Customer(id, email, passwordSalt, passwordHash, string.Empty, firstName, lastName, gender, dateOfBirth, preferences, notes),
                () => new Customer(id, email, passwordSalt, passwordHash, " ", firstName, lastName, gender, dateOfBirth, preferences, notes),
            };

            // Assert
            foreach(var action in actions)
                action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CustomerValidatePhoneNumber_ShouldThrowArgumentException_WhenPhoneNumberContainsInvalidCharactersx()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "example@mail.com";
            var passwordSalt = new byte[64];
            var passwordHash = new byte[64];
            var firstName = "TestFirstName";
            var lastName = "TestLastName";
            var gender = "male";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var preferences = "ExamplePreferences";
            var notes = "ExampleNotes";

            // Act
            var actions = new List<Action>();

            for(var i = 0; i < 128; i++)
            {
                if(i == 48)
                {
                    i = 57;
                    continue;
                }

                actions.Add(() => new Customer(id, email, passwordSalt, passwordHash, $"123{(char)i}45609", firstName, lastName, gender, dateOfBirth, preferences, notes));
            }

            // Assert
            foreach (var action in actions)
                action.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void CustomerValidateFirstName_ShouldThrowArgumentNullExcpetion_WhenFirstNameIsNullOrEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "example@mail.com";
            var passwordSalt = new byte[64];
            var passwordHash = new byte[64];
            var phoneNumber = "123456789";
            var lastName = "TestLastName";
            var gender = "male";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var preferences = "ExamplePreferences";
            var notes = "ExampleNotes";

            // Act
            var actions = new[]
            {
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, null, lastName, gender, dateOfBirth, preferences, notes),
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, string.Empty, lastName, gender, dateOfBirth, preferences, notes),
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, " ", lastName, gender, dateOfBirth, preferences, notes),
            };

            // Assert
            foreach (var action in actions)
                action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CustomerValidateFirstName_ShouldThrowArgumentException_WhenFirstNameContainsInvalidCharacters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "example@mail.com";
            var passwordSalt = new byte[64];
            var passwordHash = new byte[64];
            var phoneNumber = "123456789";
            var lastName = "TestLastName";
            var gender = "male";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var preferences = "ExamplePreferences";
            var notes = "ExampleNotes";

            // Act
            var actions = new List<Action>();
            var allowedCharacters = new[] { '.', ' ' };

            for (var i = 0; i < 128; i++)
            {
                if (allowedCharacters.Contains((char)i))
                    continue;

                if (i == 65)
                {
                    i = 90;
                    continue;
                }
                else if(i == 97)
                {
                    i = 122;
                    continue;
                }

                actions.Add(() => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, $"Name{(char)i}", lastName, gender, dateOfBirth, preferences, notes));
            }

            // Assert
            foreach (var action in actions)
                action.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void CustomerValidateLastName_ShouldThrowArgumentNullExcpetion_WhenLastNameIsNullOrEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "example@mail.com";
            var passwordSalt = new byte[64];
            var passwordHash = new byte[64];
            var phoneNumber = "123456789";
            var firstName = "TestFirstName";
            var gender = "male";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var preferences = "ExamplePreferences";
            var notes = "ExampleNotes";

            // Act
            var actions = new[]
            {
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, null, gender, dateOfBirth, preferences, notes),
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, string.Empty, gender, dateOfBirth, preferences, notes),
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, " ", gender, dateOfBirth, preferences, notes),
            };

            // Assert
            foreach (var action in actions)
                action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CustomerValidateLastName_ShouldThrowArgumentException_WhenLastNameContainsInvalidCharacters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "example@mail.com";
            var passwordSalt = new byte[64];
            var passwordHash = new byte[64];
            var phoneNumber = "123456789";
            var firstName = "TestFirstName";
            var gender = "male";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var preferences = "ExamplePreferences";
            var notes = "ExampleNotes";

            // Act
            var actions = new List<Action>();
            var allowedCharacters = new[] { '.', ' ' };

            for (var i = 0; i < 128; i++)
            {
                if(allowedCharacters.Contains((char)i))
                    continue;

                if (i == 65)
                {
                    i = 90;
                    continue;
                }
                else if (i == 97)
                {
                    i = 122;
                    continue;
                }

                actions.Add(() => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, $"Name{(char)i}", gender, dateOfBirth, preferences, notes));
            }

            // Assert
            foreach (var action in actions)
                action.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void CustomerValidateGender_ShouldThrowArgumentNullException_WhenGenderIsNullOrEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "example@mail.com";
            var passwordSalt = new byte[64];
            var passwordHash = new byte[64];
            var phoneNumber = "123456789";
            var firstName = "TestFirstName";
            var lastName = "TestLastName";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var preferences = "ExamplePreferences";
            var notes = "ExampleNotes";

            // Act
            var actions = new Action[]
            {
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, null, dateOfBirth, preferences, notes),
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, string.Empty, dateOfBirth, preferences, notes),
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, " ", dateOfBirth, preferences, notes),
            };

            // Assert
            foreach (var action in actions)
                action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CustomerValidateGender_ShouldThrowArgumentException_WhenGenderDoesNotEqualMaleOrFemale()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "example@mail.com";
            var passwordSalt = new byte[64];
            var passwordHash = new byte[64];
            var phoneNumber = "123456789";
            var firstName = "TestFirstName";
            var lastName = "TestLastName";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var preferences = "ExamplePreferences";
            var notes = "ExampleNotes";

            // Act
            var actions = new[]
            {
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, "test", dateOfBirth, preferences, notes),
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, "123", dateOfBirth, preferences, notes),
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, "###", dateOfBirth, preferences, notes),
            };

            var actionsCorrectGender = new[]
            {
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, "male", dateOfBirth, preferences, notes),
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, "female", dateOfBirth, preferences, notes),
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, "MALE", dateOfBirth, preferences, notes),
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, "FEMALE", dateOfBirth, preferences, notes)
            };

            // Assert
            foreach (var action in actions)
                action.Should().ThrowExactly<ArgumentException>();

            foreach (var action in actionsCorrectGender)
                action.Should().NotThrow();
        }

        [Fact]
        public void CustomerValidateDateOfBirth_ShouldThrowArgumentNException_WhenDateOfBirthIsMinValueOrDateIsFromFuture()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "example@mail.com";
            var passwordSalt = new byte[64];
            var passwordHash = new byte[64];
            var phoneNumber = "123456789";
            var firstName = "TestFirstName";
            var lastName = "TestLastName";
            var gender = "male";
            var preferences = "ExamplePreferences";
            var notes = "ExampleNotes";

            // Act
            var actions = new[]
            {
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, DateOnly.FromDateTime(DateTime.MinValue), preferences, notes),
                () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1).Date), preferences, notes)
            };

            // Assert
            foreach (var action in actions)
                action.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void CustomerValidatePreferences_ShouldThrowArgumentOutOfRangeException_WhenPreferencesIsLongerThanThousandCharacters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "example@mail.com";
            var passwordSalt = new byte[64];
            var passwordHash = new byte[64];
            var phoneNumber = "123456789";
            var firstName = "TestFirstName";
            var lastName = "TestLastName";
            var gender = "male";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var notes = "ExampleNotes";

            // Act
            var preferences = "x".PadRight(1001);
            var action = () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences, notes);

            // Assert
            action.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void CustomerValidateNotes_ShouldThrowArgumentOutOfRangeException_WhenPreferencesIsLongerThanThousandCharacters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "example@mail.com";
            var passwordSalt = new byte[64];
            var passwordHash = new byte[64];
            var phoneNumber = "123456789";
            var firstName = "TestFirstName";
            var lastName = "TestLastName";
            var gender = "male";
            var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var preferences = "ExamplePreferences";

            // Act
            var notes = "x".PadRight(1001);
            var action = () => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences, notes);

            // Assert
            action.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
    }
}