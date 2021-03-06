﻿using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zebra.Models
{
    public class AlbumModels
    {
        public int ID { get; set; }
        [StringLength(60, MinimumLength = 1)] //on peut ajouter ErrorMessage ="..." pour message perso
        public string Title { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public List<string> Genre { get; set; }
        public decimal prix { get; set; }
        public int Note { get; set; }
        public List<SimpleArtist> ID_User { get; set; }
        public string Created_by { get; set; }
        public List<MusicModels> Musics { get; set; }
    }
}
