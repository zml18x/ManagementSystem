using FluentAssertions;
using SpaManagementSystem.Core.Models;

namespace SpaManagementSystem.Core.Tests.ModelsTests
{
    public class CustomerTests
    {
        [Fact]
        public void CustomerUpdateBasicInformation_ShouldNotUpdateAnyData_WhenAllFieldsAreNull()
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

            var customer = new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences, notes);

            // Act
            var result = customer.UpdateBasicInfromation();

            // Assert
            result.Should().BeFalse();
            customer.Id.Should().Be(id);
            customer.Email.Should().Be(email);
            customer.PasswordHash.Should().Equal(passwordHash);
            customer.PasswordSalt.Should().Equal(passwordSalt);
            customer.PhoneNumber.Should().Be(phoneNumber);
            customer.FirstName.Should().Be(firstName);
            customer.LastName.Should().Be(lastName);
            customer.Gender.Should().Be(gender);
            customer.DateOfBirth.Should().Be(dateOfBirth);
            customer.Preferences.Should().Be(preferences);
            customer.Notes.Should().Be(notes);
        }

        [Fact]
        public void CustomerUpdateBasicInformation_ShouldUpdateAllFields()
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

            var customer = new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences, notes);
            var customerOldLastUpdateAt = customer.LastUpdateAt;

            // Act
            var newFirstName = "NewFirstName";
            var newLastName = "NewLastName";
            var newGender = "female";
            var newDateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-9));
            var newPreferences = "NewPreferences";


            var result = customer.UpdateBasicInfromation(newFirstName, newLastName, newGender, newDateOfBirth, newPreferences);

            // Assert
            result.Should().BeTrue();
            customer.LastUpdateAt.Should().NotBeOnOrBefore(customerOldLastUpdateAt);
            customer.Id.Should().Be(id);
            customer.Email.Should().Be(email);
            customer.PasswordHash.Should().Equal(passwordHash);
            customer.PasswordSalt.Should().Equal(passwordSalt);
            customer.PhoneNumber.Should().Be(phoneNumber);
            customer.FirstName.Should().Be(newFirstName);
            customer.LastName.Should().Be(newLastName);
            customer.Gender.Should().Be(newGender);
            customer.DateOfBirth.Should().Be(newDateOfBirth);
            customer.Preferences.Should().Be(newPreferences);
            customer.Notes.Should().Be(notes);
        }
    }
}