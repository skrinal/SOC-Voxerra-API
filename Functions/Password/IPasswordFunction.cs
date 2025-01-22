namespace Voxerra_API.Functions.Password
{
    public interface IPasswordFunction
    {
        Task<bool> ChangePasswordUsingToken(string email, int token, string newPassword);
        int GeneratePasswordResetToken(string email);
        Task<bool> ResetPassword(string email);
    }
}
