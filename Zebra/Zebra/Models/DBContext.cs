using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Zebra.Models
{
    public class DBContext
    {
        public DbSet<AlbumModels> Albums { get; set; }
        public DbSet<MusicModels> Musics { get; set; }
        public DbSet<OrderModels> Orders { get; set; }
        public DbSet<CommentModels> Comments { get; set; }
    }
}