using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GameAdmin.Models
{
    public class BlogViewModel
    {

        public int Id { get; set; }
        [Required, MaxLength(255, ErrorMessage = "Başlık 255 karakterden fazla olamaz")]
        [Display(Name = "Blog Başlığı")]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Yayın Tarihi")]
        public DateTime Date { get; set; }

        [Display(Name = "Kapak resmi")]
        public string TinyImagePath { get; set; }
        public string NewsUserId { get; set; }
        public int CommentCount { get; set; }

    }
}