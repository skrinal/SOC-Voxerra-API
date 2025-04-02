namespace Voxerra_API.Entities
{
    public class TblPendingPassword
    {
        public int Id { get; set; }
        public int Code {  get; set; }
        public string Email { get; set; } = null!;
        public Guid AuthString { get; set; } = Guid.Empty!;
        public DateTime ExpireTime { get; set; }
    }
}
