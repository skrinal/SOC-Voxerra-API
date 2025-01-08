namespace Voxerra_API.Helpers
{
    public interface EmailMessage {
        Task<bool> SendEmail(string toEmail, string subject, string body);
    }
}