using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models.NewsModels
{
    public class NewsImage
    {
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "255 karakterden fazla olamaz")]
        [Display(Name = "Resim Yolu")]
        public string ImagePath { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
    }
}
