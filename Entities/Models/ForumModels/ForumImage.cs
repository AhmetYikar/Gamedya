using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models.ForumModels
{
    public class ForumImage
    {

        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "255 karakterden fazla olamaz")]
        public string ImagePath { get; set; }
        public int ForumPostId { get; set; }
        public ForumPost ForumPost { get; set; }
    }
}
