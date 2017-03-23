using System.Data.Entity;

namespace Zebra.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
