using Entites.Models.GameModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites.Models.UserModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Entites.Models.Status;

namespace Entites.Models.NewsModels
{
    public enum NewsPlatform : byte
    {
        Xbox,
        Pc,
        Playstation,
        Mobile,
        Nintendo,
        VR
    }


    public class News
    {
        public int Id { get; set; }

        [Required, MaxLength(255, ErrorMessage = "Başlık 255 karakterden fazla olamaz")]
        [Display(Name = "Haber Başlığı")]
        public string Title { get; set; }
        [Display(Name = "Haber Özeti")]
        public string Summary { get; set; }

        [Display(Name = "Haber İçeriği")]
        [AllowHtml]
        public string Content { get; set; }
        public string NewsUserId { get; set; }
        public NewsUser NewsUser { get; set; }
        [Display(Name = "Kategori Adı")]
        public int NewsCategoryId { get; set; }
        public virtual NewsCategory NewsCategory { get; set; }
        public virtual ICollection<NewsComment> NewsComments { get; set; }

        [Display(Name = "Yayın Tarihi")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [Display(Name = "Son düzenleme Tarihi")]
        [DataType(DataType.Date)]
        public DateTime EditDate { get; set; }


        [Display(Name = "Aktif Mi?")]
        public bool IsActive { get; set; }
        [Display(Name = "Görüntülenmes Sayısı")]
        public int ViewCount { get; set; }

        public virtual ICollection<NewsImage> NewsImages { get; set; }

        [Display(Name = "Kapak resmi")]

        public string TinyImagePath { get; set; }
        public virtual ICollection<NewsVideo> NewsVideos { get; set; }

        public NewsPlatform NewsPlatform { get; set; }


    }
}
