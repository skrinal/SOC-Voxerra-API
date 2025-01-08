namespace Voxerra_API.Entities
{
    public class TblPendingPassword
    {
        public int Id { get; set; }
        public string Token {  get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
