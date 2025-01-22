namespace Voxerra_API.Functions.Email
{
    public class EmailDetails
    {
        public int Code { get; set; }
        public string ToEmail { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public bool RegistrationEmail { get; set; } = false;
        public bool PasswordEmail { get; set; } = false;

    }
}
