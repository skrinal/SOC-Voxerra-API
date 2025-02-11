namespace Voxerra_API.Entities
{
    public class TblPendingPassword
    {
        public int Id { get; set; }
        public int Code {  get; set; }
        public string Email { get; set; } = null!;
        public DateTime ExpireTime { get; set; }
    }
}
