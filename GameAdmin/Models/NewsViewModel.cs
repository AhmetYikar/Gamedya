using Entites.Models.NewsModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace GameAdmin.Models
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        [Required, MaxLength(255, ErrorMessage = "Başlık 255 karakterden fazla olamaz")]
        [Display(Name = "Haber Başlığı")]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Yayın Tarihi")]
        public DateTime Date { get; set; }
        [Display(Name = "Kapak resmi")]
        public string TinyImagePath { get; set; }

        public int CommentCount { get; set; }


    }
}