namespace Voxerra_API.Functions.Registration
{
    public interface IUserRegistrationFunction
    {
        int GenerateCode();
        Task<bool> IsUserNameUnique(string userName);
        Task<bool> IsEmailUnique(string email);
        Task<bool> Registration(string userName, string password, string email);
        public (string encryptedPassword, byte[] salt) EncryptPassword(string password);
    }
}
