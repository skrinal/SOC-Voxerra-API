namespace Voxerra_API.Functions.Password
{
    public interface IPasswordFunction
    {
        string GeneratePasswordResetToken(string email);
        Task<bool> ResetPassword(string token, string newPassword);
    }
}
