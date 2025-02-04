namespace Voxerra_API.Entities
{
    public class TblUserSettings
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LoginAlertsEnabled { get; set; }
        public string WhereIsUserLoggedIn { get; set; } = String.Empty;
        
        public virtual TblUser User { get; set; } = null!;
    }
}

