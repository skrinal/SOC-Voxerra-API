namespace Voxerra_API.Entities
{
    public class ChatAppContext(DbContextOptions<ChatAppContext> options) : DbContext(options)
    {
        public virtual DbSet<TblUser> Tblusers { get; set; } = null!;
        public virtual DbSet<TblUserFriend> Tbluserfriends { get; set; } = null!;
        public virtual DbSet<TblMessage> Tblmessages { get; set; } = null!;
        public virtual DbSet<TblPendingUser> Tblpendingusers { get; set; } = null!;
        public virtual DbSet<TblPendingPassword> Tblpendingpassword { get; set;} = null!;
    }
}
