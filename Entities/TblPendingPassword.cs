namespace Voxerra_API.Entities
{
    public class TblPendingPassword
    {
        public int Id { get; set; }
        public int Token {  get; set; }
        public string Email { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
