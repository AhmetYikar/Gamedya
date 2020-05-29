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
        public string Summary { get; set; }
        
        [Display(Name = "Yayın Tarihi")]
        public DateTime Date { get; set; }

       
        public string TinyImagePath { get; set; }
        public string NewsUserId { get; set; }
       
        public int ReplyCount { get; set; }
    }
}