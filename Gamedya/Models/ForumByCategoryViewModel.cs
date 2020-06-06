using Entites.Models.ForumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gamedya.Models
{
    public class ForumByCategoryViewModel
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        public List<ForumCategory> SubCategories { get; set; }

        public int ReplyCount { get; set; }

        public int ForumCount { get; set; }

        public string LastPost { get; set; }

        //Forumlarda anasayfada kullanılan farklı ikonları bu şekilde taşıdık
        public string iconClass { get; set; }
    }
}