using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zebra.Models
{
    public class CommentModelsVM
    {
        public int ID { get; set; }
        public List<CommentModels> Comments { get; set; }
        public CommentModels Comment { get; set; }
    }
}