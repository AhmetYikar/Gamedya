using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Entites.Models.UserModels;

namespace Entites.Models.BlogModels
{
    public class BlogPost
    {
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "255 karakterden fazla olamaz")]
        [Required(ErrorMessage = "Boş geçilemez")]
        [Display(Name = "Blog Başlığı")]
        public string Title { get; set; }

        [Display(Name = "Blog Özeti")]
        [Required(ErrorMessage = "Boş geçilemez")]
        public string Summary { get; set; }

        [Display(Name = "Blog İçeriği")]
        [AllowHtml]
        [Required(ErrorMessage = "Boş geçilemez")]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Blog Yayın Tarihi")]
        public DateTime Date { get; set; }

        [Display(Name = "Son düzenleme Tarihi")]
        [DataType(DataType.Date)]
        public DateTime EditDate { get; set; }

        public int ViewCount { get; set; }
        public ICollection<BlogImage> BlogImages { get; set; }
        public ICollection<BlogComment> BlogComments { get; set; }
        public string NewsUserId { get; set; }
        public NewsUser NewsUser { get; set; }

        [Display(Name = "Kapak resmi")]
        public string TinyImagePath { get; set; }

        public int BlogCategoryId { get; set; }
        public virtual BlogCategory BlogCategory { get; set; }
    }
}
