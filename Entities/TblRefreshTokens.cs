namespace Voxerra_API.Entities
{
    public class TblRefreshTokens
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RefreshToken { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }
    }
}
