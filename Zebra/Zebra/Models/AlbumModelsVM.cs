using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zebra.Models
{
    public class AlbumModelsVM
    {
        public int ID { get; set; }
        public int ID_album { get; set; }
        public MusicModels music { get; set; }
    }
}