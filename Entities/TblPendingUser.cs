namespace Voxerra_API.Entities
{
    public class TblPendingUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte[] StoredSalt { get; set; } = null!;
        public int VerificationCode { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
