namespace Voxerra_API.Entities
{
    public class TblUserFriend
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public string NickName { get; set; }
        
        public virtual TblUser User { get; set; }
        public virtual TblUser Friend { get; set; }
    }
}
