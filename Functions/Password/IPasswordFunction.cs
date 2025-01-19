namespace Voxerra_API.Functions.Password
{
    public interface IPasswordFunction
    {
        int GeneratePasswordResetToken(string email);
        Task<bool> ResetPassword(int token, string newPassword);
    }
}
