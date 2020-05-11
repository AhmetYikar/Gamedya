using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Entites.Models.UserModels;

namespace Entites.Models.ForumModels
{
    public class ForumPost
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [MaxLength(255, ErrorMessage = "255 karakterden fazla olamaz")]
        [Display(Name = "Forum Başlığı")]
        public string ForumTitle { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Forum Özeti")]
        public string Summary { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]

        [Display(Name = "Forum İçeriği")]
        [AllowHtml]
        public string Content { get; set; }

        public int ViewCount { get; set; }

        public virtual ICollection<ForumReply> ForumReply { get; set; }
        public virtual ICollection<ForumImage> ForumImages { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name = "Son düzenleme Tarihi")]
        [DataType(DataType.Date)]
        public DateTime EditDate { get; set; }

        [Display(Name = "Aktif Mi?")]
        public bool IsActive { get; set; }

        [Display(Name = "Onayla?")]
        public bool IsOk { get; set; }

        public string NewsUserId { get; set; }
        public NewsUser NewsUser { get; set; }

        [Display(Name = "Kapak resmi")]
        public string TinyImagePath { get; set; }

        public int ForumCategoryId { get; set; }
        public virtual ForumCategory ForumCategory { get; set; }

    }
}
