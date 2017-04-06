using SpotifyAPI.Web.Models;
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
        public string ID { get; set; }
        [StringLength(60, MinimumLength = 1)] //on peut ajouter ErrorMessage ="..." pour message perso
        public string Title { get; set; }
        public List<Image> Image { get; set; }
        public string ReleaseDate { get; set; }
        public List<string> Genre { get; set; }
        public decimal prix { get; set; }
        public int Note { get; set; }
        public List<SimpleArtist> ID_User { get; set; }
    }
}
