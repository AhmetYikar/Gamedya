using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites.Models.UserModels;

namespace Entites.Models.ForumModels
{
    public class ForumCategory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [MaxLength(150, ErrorMessage = "150 karakterden fazla olamaz")]
        [Display(Name = "Kategori Adı")]
        public string CategoryName { get; set; }

        [MaxLength(255, ErrorMessage = "255 karakterden fazla olamaz")]
        [Display(Name = "Kategori Açıklaması")]
        public string Description { get; set; }
        [Display(Name = "Kategori Alt Kategori Id")]
        public int? ParentId { get; set; }

        public ICollection<ForumCategory> ForumCategories { get; set; }
        public ICollection<ForumPost> ForumPosts { get; set; }


    }
}
