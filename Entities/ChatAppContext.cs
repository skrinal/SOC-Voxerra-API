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
        public virtual DbSet<TblGroups> Tblgroups { get; set; } = null!;
        public virtual DbSet<TblGroupMembers> Tblgroupmembers { get; set; } = null!;
        public virtual DbSet<TblGroupMessages> Tblgroupmessages { get; set; } = null!;
        
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
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TblPendingFriendRequest>()
                .HasOne(pfr => pfr.ToUser)
                .WithMany(u => u.ReceivedFriendRequests)
                .HasForeignKey(pfr => pfr.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            
            modelBuilder.Entity<TblGroups>()
                .HasOne(g => g.Creator)
                .WithMany()
                .HasForeignKey(g => g.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TblGroupMembers>()
                .HasOne(gm => gm.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(gm => gm.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TblGroupMembers>()
                .HasOne(gm => gm.User)
                .WithMany()
                .HasForeignKey(gm => gm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TblGroupMessages>()
                .HasOne(gm => gm.Group)
                .WithMany(g => g.Messages)
                .HasForeignKey(gm => gm.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TblGroupMessages>()
                .HasOne(gm => gm.Sender)
                .WithMany()
                .HasForeignKey(gm => gm.SenderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
