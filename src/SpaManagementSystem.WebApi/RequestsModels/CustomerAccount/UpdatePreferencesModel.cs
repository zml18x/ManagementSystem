using System.ComponentModel.DataAnnotations;

namespace SpaManagementSystem.WebApi.RequestsModels.CustomerAccount
{
    public class UpdatePreferencesModel
    {
        [MaxLength(1000)]
        public string? Preferences { get; set; }
    }
}