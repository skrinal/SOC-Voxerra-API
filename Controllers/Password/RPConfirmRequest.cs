namespace Voxerra_API.Controllers.Password;

public class RPConfirmRequest
{
    public string NewPassword { get; set; } = null!;
    public Guid GuidAuth { get; set; }
}