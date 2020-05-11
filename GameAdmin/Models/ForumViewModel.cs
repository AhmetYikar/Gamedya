using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameAdmin.Models
{
    public class ForumViewModel
    {

        public int Id { get; set; }
        [Required, MaxLength(255, ErrorMessage = "Başlık 255 karakterden fazla olamaz")]
        [Display(Name = "Haber Başlığı")]
        public string ForumTitle { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Yayın Tarihi")]
        public DateTime Date { get; set; }

        [Display(Name = "Kapak resmi")]
        public string TinyImagePath { get; set; }
        public string NewsUserId { get; set; }

    }
}