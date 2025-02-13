namespace Voxerra_API.Functions.Email
{
    public class EmailDetails
    {
        public int Code { get; set; }
        public string UserName { get; set; } = null!;
        public string ToEmail { get; set; } = null!;
        public string IpAdress { get; set; } = null!;
        public string Location { get; set; } = null!;
        public bool RegistrationEmail { get; set; } = false;
        public bool PasswordEmail { get; set; } = false;
        public bool TwoAuthEmail { get; set; } = false;
        public bool AlertEmail { get; set; } = false;
    }
}
