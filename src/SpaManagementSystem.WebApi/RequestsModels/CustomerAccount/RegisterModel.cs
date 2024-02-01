using System.ComponentModel.DataAnnotations;

namespace SpaManagementSystem.WebApi.RequestsModels.CustomerAccount
{
    public class RegisterModel
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }
        public string? Preferences { get; set; }
    }
}