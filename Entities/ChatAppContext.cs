namespace Voxerra_API.Entities
{
    public class ChatAppContext:DbContext
    {
        public ChatAppContext (DbContextOptions<ChatAppContext> options) : base(options) 
        { }

        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;
    }
}
