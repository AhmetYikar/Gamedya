using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites.Models.UserModels;

namespace Entites.Models.ForumModels
{
    public class ForumReply
    {
        public int Id { get; set; }

        public int ForumPostId { get; set; }
        public ForumPost ForumPost { get; set; }
        public string Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int LikeCount { get; set; }
        public int UnLikeCount { get; set; }

        public bool IsOk { get; set; }
        public string NewsUserId { get; set; }
        public NewsUser NewsUser { get; set; }
    }
}
