using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zebra.Models
{
    public class MusicModels
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [DisplayFormat(DataFormatString = "{0:Y}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal prix { get; set; }
        public int ID_User { get; set; }
        public int Note { get; set; }
        public AlbumModels ID_Album { get; set; }
    }
}