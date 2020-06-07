using Entites.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models.Status
{
    public enum Status
    {
        Like,
        Unlike,
    }

    public enum Module
    {
        News,
        NewsComent,
        BlogPost,
        BlogComment,
        ForumPost,
        ForumReply
    }

    public class LikeTable
    {
        public int ID { get; set; }
        public Status Status { get; set; }
        public Module Module { get; set; }
        public int ModuleId { get; set; }
        public string NewsUserId { get; set; }
        public NewsUser NewsUser { get; set; }
    }
}
