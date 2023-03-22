using System.Data.Entity;

namespace NetAtlas2.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext() : base("NetAltasDB")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Moderateur> Moderateurs { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Lien> Liens { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Reply> Replys { get; set; }
    }
    
}
