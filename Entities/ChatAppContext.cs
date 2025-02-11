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
            
            modelBuilder.Entity<TblTwoFactorAuth>()
                .HasOne(tfa => tfa.User)
                .WithMany(u => u.TwoFactorAuths)
                .HasForeignKey(tfa => tfa.UserId);
            
            modelBuilder.Entity<TblUserFriend>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.Friends)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<TblUserFriend>()
                .HasOne(uf => uf.Friend)
                .WithMany()
                .HasForeignKey(uf => uf.FriendId)
                .OnDelete(DeleteBehavior.Restrict);
            
            
            modelBuilder.Entity<TblMessage>()
                .HasOne(m => m.FromUser)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TblMessage>()
                .HasOne(m => m.ToUser)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<TblPendingFriendRequest>()
                .HasOne(pfr => pfr.FromUser)
                .WithMany(u => u.SentFriendRequests)
                .HasForeignKey(pfr => pfr.FromUserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<TblPendingFriendRequest>()
                .HasOne(pfr => pfr.ToUser)
                .WithMany(u => u.ReceivedFriendRequests)
                .HasForeignKey(pfr => pfr.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
