namespace Voxerra_API.Functions.Email
{
    public interface IEmailFunction {
        Task SendEmail(string toEmail, string subject, string body);
    }
}