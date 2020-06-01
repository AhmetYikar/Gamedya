using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gamedya.Models
{
    public class ForumIndexViewModel
    {
        public int ForumTitleCount { get; set; }

        public int ReplyCount { get; set; }

        public int ForumCount { get; set; }

        public string LastPost { get; set; }
    }
}