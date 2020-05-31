using Entites.Models.ForumModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gamedya.Models
{
    public class ForumViewModel
    {
        public int Id { get; set; }
      
        [Display(Name = "Forum Başlığı")]
        public string ForumTitle { get; set; }

        [Display(Name = "Kategori")]
        public string CategoryName { get; set; }

        [Display(Name = "Yayın Tarihi")]      
        public DateTime Date { get; set; }

        [Display(Name = "Görüntülenme")]
        public string ViewCount { get; set; }

        public string NewsUserId { get; set; }

        public string ForumUser { get; set; } = "";

        [Display(Name = "Yorum Sayısı")]
        public int ReplyCount { get; set; }
    }
}