namespace Voxerra_API.Functions.Email
{
    public interface IEmailFunction {
        Task SendEmail(EmailDetails emailDetails);
    }
}