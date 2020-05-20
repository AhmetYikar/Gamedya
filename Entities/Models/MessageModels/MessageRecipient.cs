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
        public string NewsUserId { get; set; }
        public NewsUser NewsUser { get; set; }
        public ICollection<GamedyaMessage> GamedyaMessages { get; set; }
    }
}
