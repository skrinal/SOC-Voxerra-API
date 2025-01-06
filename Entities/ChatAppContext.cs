namespace Voxerra_API.Entities
{
    public class ChatAppContext:DbContext
    {
        public ChatAppContext (DbContextOptions<ChatAppContext> options) : base(options) 
        { }

        public virtual DbSet<TblUser> Tblusers { get; set; } = null!;
        public virtual DbSet<TblUserFriend> Tbluserfriends { get; set; } = null!;
        public virtual DbSet<TblMessage> Tblmessages { get; set; } = null!;
    }
}
