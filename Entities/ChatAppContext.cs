namespace Voxerra_API.Entities
{
    public class ChatAppContext(DbContextOptions<ChatAppContext> options) : DbContext(options)
    {
        public virtual DbSet<TblUser> Tblusers { get; set; } = null!;
        public virtual DbSet<TblUserFriend> Tbluserfriends { get; set; } = null!;
        public virtual DbSet<TblMessage> Tblmessages { get; set; } = null!;
        public virtual DbSet<TblPendingUser> Tblpendingusers { get; set; } = null!;
        public virtual DbSet<TblPendingPassword> Tblpendingpassword { get; set;} = null!;
        public virtual DbSet<TblPendingFriendRequest> Tblpendingfriendrequest { get; set; } = null!;
        public virtual DbSet<TblUserSettings> Tblusersettings { get; set; } = null!;
        public virtual DbSet<TblTwoFactorAuth> Tbltwofactorauth { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblUser>()
                .HasOne(x => x.UserSettings)
                .WithOne(s => s.User)
                .HasForeignKey<TblUserSettings>(s => s.UserId);
        }
    }
}
