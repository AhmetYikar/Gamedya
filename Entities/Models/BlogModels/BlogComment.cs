using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites.Models.UserModels;


namespace Entites.Models.BlogModels
{
    public class BlogComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public bool IsOk { get; set; }
        public string NewsUserId { get; set; }
        public NewsUser NewsUser { get; set; }
    }
}
