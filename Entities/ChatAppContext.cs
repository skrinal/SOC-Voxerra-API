namespace Voxerra_API.Entities
{
    public class ChatAppContext:DbContext
    {
        public ChatAppContext (DbContextOptions<ChatAppContext> options) : base(options) 
        { }

        public virtual DbSet<TblUser> tblusers { get; set; } = null!;
        public virtual DbSet<TblUserFriend> tbluserfriends { get; set; } = null!;
        public virtual DbSet<TblMessage> tblmessages { get; set; } = null!;
    }
}
