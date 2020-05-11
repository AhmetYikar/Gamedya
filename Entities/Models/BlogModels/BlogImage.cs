using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models.BlogModels
{
    public class BlogImage
    {
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "255 karakterden fazla olamaz")]
        public string ImagePath { get; set; }


        public int BlogPostId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
    }
}
