
namespace Voxerra_API.Functions.Password
{
    public interface IPasswordFunction
    {
        int GeneratePasswordResetToken(string email);
        Task<bool> ResetPassword(string email);
        Task<Guid> ValidateCode(int code, string email);
        Task<PassCResult> ChangePasswordUsingCode(Guid guidAuth, string newPassword);
        Task<bool> SendCodeAgain(string email);
    }
}
