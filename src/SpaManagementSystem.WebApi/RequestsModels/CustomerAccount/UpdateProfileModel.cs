namespace SpaManagementSystem.WebApi.RequestsModels.CustomerAccount
{
    public class UpdateProfileModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
    }
}