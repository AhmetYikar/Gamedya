using Entites.Models.MessageModels;
using Entites.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class MessageRecipient
    {
        public int Id { get; set; }
        public bool IsRead { get; set; }
        public int MessageId { get; set; }
        public Message Message { get; set; }
        public string NewsUserId { get; set; }
        public NewsUser NewsUser { get; set; }
    }
}
