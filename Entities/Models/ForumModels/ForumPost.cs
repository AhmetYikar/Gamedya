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

        [Display(Name = "Forum İçeriği")]
        [AllowHtml]
        public string Content { get; set; }

        [Display(Name = "Görüntülenme Sayısı")]
        public int ViewCount { get; set; }

        public virtual ICollection<ForumReply> ForumReplies { get; set; }
      
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
      
        [Display(Name = "Aktif Mi?")]
        public bool IsActive { get; set; }

        [Display(Name = "Onayla?")]
        public bool IsOk { get; set; }

        public string NewsUserId { get; set; }
        public NewsUser NewsUser { get; set; }
     
        public int ForumCategoryId { get; set; }
        public virtual ForumCategory ForumCategory { get; set; }

    }
}
