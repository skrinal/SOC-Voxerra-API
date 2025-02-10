namespace Voxerra_API.Entities;

public class TblTwoFactorAuth
{
    public int Id { get; set; }
    public int Code { get; set; }
    public DateTime ExpireTime { get; set; }
}