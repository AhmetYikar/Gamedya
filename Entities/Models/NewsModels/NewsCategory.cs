using Entites.Models.NewsModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites.Models.UserModels;

namespace Entites.Models.GameModels
{
    public class NewsCategory
    {
        public int Id { get; set; }

        [MaxLength(150, ErrorMessage = "150 karakterden fazla olamaz")]
        [Display(Name = "Kategori Adı")]
        public string CategoryName { get; set; }

        [MaxLength(255, ErrorMessage = "255 karakterden fazla olamaz")]
        [Display(Name = "Kategori Açıklaması")]
        public string Description { get; set; }

        public virtual ICollection<News> News { get; set; }
        [Display(Name = "Alt Kategori Id")]
        public int? ParentId { get; set; }
        public ICollection<NewsCategory> NewsCategories { get; set; }

    }
}
