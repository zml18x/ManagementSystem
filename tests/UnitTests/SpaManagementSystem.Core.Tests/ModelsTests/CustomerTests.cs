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
            var isUpdated = customer.UpdateBasicInfromation();

            // Assert
            isUpdated.Should().BeFalse();
            VerifyCustomerData(customer, id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences, notes);
        }

        [Fact]
        public void CustomerUpdateBasicInfromation_ShouldUpdateOnlyDataThatIsNotNull()
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

            var customers = Enumerable.Range(0, 4)
                .Select(_ => new Customer(id, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences, notes))
                .ToArray();

            // Act
            var newFirstName = "NewFirstName";
            var newLastName = "NewLastName";
            var newGender = "female";
            var newDateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-9));

            var isUpdated = new[]
            {
                customers[0].UpdateBasicInfromation(newFirstName),
                customers[1].UpdateBasicInfromation(newFirstName,newLastName),
                customers[2].UpdateBasicInfromation(newFirstName,newLastName,newGender),
                customers[3].UpdateBasicInfromation(newFirstName,newLastName,newGender,newDateOfBirth)
            };

            // Assert
            isUpdated[0].Should().BeTrue();
            VerifyCustomerData(customers[0], id, email, passwordSalt, passwordHash, phoneNumber, newFirstName, lastName, gender, dateOfBirth, preferences, notes);

            isUpdated[1].Should().BeTrue();
            VerifyCustomerData(customers[1], id, email, passwordSalt, passwordHash, phoneNumber, newFirstName, newLastName, gender, dateOfBirth, preferences, notes);

            isUpdated[2].Should().BeTrue();
            VerifyCustomerData(customers[2], id, email, passwordSalt, passwordHash, phoneNumber, newFirstName, newLastName, newGender, dateOfBirth, preferences, notes);

            isUpdated[3].Should().BeTrue();
            VerifyCustomerData(customers[3], id, email, passwordSalt, passwordHash, phoneNumber, newFirstName, newLastName, newGender, newDateOfBirth, preferences, notes);
        }

        private void VerifyCustomerData(Customer customer, Guid id, string email, byte[] passwordSalt, byte[] passwordHash, string phoneNumber, string firstName, string lastName, string gender, DateOnly dateOfBirth, string preferences, string notes)
        {
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
    }
}