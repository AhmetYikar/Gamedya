using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameAdmin.Models
{
    public class DashboardIndexViewModel
    {
        public int NewsCount { get; set; }

        public int BlogCount { get; set; }

        public int ForumCount { get; set; }

        public int GameCount { get; set; }

        public int NewsCategoryCount { get; set; }

        public int BlogCategoryCount { get; set; }

        public int ForumCategoryCount { get; set; }

        public int UserCount { get; set; }


    }
}