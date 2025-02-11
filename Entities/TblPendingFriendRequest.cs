namespace Voxerra_API.Entities
{
    public class TblPendingFriendRequest
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        
        public virtual TblUser FromUser { get; set; }
        public virtual TblUser ToUser { get; set; }
    }
}

