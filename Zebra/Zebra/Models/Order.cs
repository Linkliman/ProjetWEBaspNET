using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zebra.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int ID_User { get; set; }
        public Music ID_Music { get; set; }
        public Album ID_Album { get; set; }
    }
}