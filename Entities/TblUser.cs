namespace Voxerra_API.Entities
{
    public class TblUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[] StoredSalt { get; set; } = null!;
        public string AvatarSourceName { get; set; } = null!;
        public string Bio { get; set; } = string.Empty;
        public bool IsOnline { get; set; }
        public DateTime LastLogonTime { get; set; }
        public DateTime CreationDate { get; set; }
        
        public virtual TblUserSettings? UserSettings { get; set; }
        public virtual ICollection<TblTwoFactorAuth> TwoFactorAuths { get; set; } = new List<TblTwoFactorAuth>();
        public virtual ICollection<TblUserFriend> Friends { get; set; } = new List<TblUserFriend>();
        public virtual ICollection<TblMessage> SentMessages { get; set; } = new List<TblMessage>();
        public virtual ICollection<TblMessage> ReceivedMessages { get; set; } = new List<TblMessage>();
        public virtual ICollection<TblPendingFriendRequest> SentFriendRequests { get; set; } = new List<TblPendingFriendRequest>();
        public virtual ICollection<TblPendingFriendRequest> ReceivedFriendRequests { get; set; } = new List<TblPendingFriendRequest>();
    }
}
