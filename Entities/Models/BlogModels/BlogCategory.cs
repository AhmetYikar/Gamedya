using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models.BlogModels
{
    public class BlogCategory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez")]
        [MaxLength(150, ErrorMessage = "150 karakterden fazla olamaz")]
        public string CategoryName { get; set; }

        [MaxLength(255, ErrorMessage = "255 karakterden fazla olamaz")]
        public string Description { get; set; }

        public int? ParentId { get; set; }
        public ICollection<BlogCategory> BlogCategories { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
