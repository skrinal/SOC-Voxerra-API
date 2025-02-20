namespace Voxerra_API.Functions.Password
{
    public interface IPasswordFunction
    {
        Task<bool> ChangePasswordUsingCode(string email, int code, string newPassword);
        int GeneratePasswordResetToken(string email);
        Task<bool> ResetPassword(string email);
        Task<bool> SendCodeAgain(string email);
    }
}
