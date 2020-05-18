using Entites.Models.UserModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public enum NModule
    {
        News,
        GameUpdate,
        BlogPost,
        ForumPost
    }
    public class Notification
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public ICollection<NewsUser> NewsUsers { get; set; }
        public NModule Module { get; set; }
        public int NModuleId { get; set; }

    }
}
