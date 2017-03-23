using System.Data.Entity;

namespace Zebra.Models
{
    public class AlbumDBContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Music> Musics { get; set; }

    }
}
