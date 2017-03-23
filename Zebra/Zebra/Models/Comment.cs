using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zebra.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int ID_User { get; set; }
        public Album ID_Album { get; set; }
        public Music ID_Music { get; set; }
        public string Content { get; set; }
    }
}