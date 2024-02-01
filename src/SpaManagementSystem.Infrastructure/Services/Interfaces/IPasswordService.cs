namespace SpaManagementSystem.Infrastructure.Services.Interfaces
{
    public interface IPasswordService
    {
        void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
        void ValidatePassword(string password);
    }
}