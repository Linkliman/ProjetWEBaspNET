using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zebra.Models
{
    public class OrderModels
    {
        public int ID { get; set; }
        public int ID_User { get; set; }
        public MusicModels ID_Music { get; set; }
        public AlbumModels ID_Album { get; set; }
    }
}