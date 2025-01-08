namespace Voxerra_API.Controllers.Registration
{
    public class ConfirmRegistrationRequest
    {
        public string Email { get; set; } = null!;
        public int Code { get; set; }
    }

}
