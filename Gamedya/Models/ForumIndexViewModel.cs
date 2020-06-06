using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gamedya.Models
{
    public class ForumIndexViewModel
    {
        public int CategoryCount { get; set; }
        public List<int> CategoryIds { get; set; }
        public int ForumCount { get; set; }

    }
}