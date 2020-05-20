using Entites.Models.UserModels;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models.MessageModels
{
    public class GamedyaMessage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Boş geçilemez")]
        [Display(Name = "Mesaj metni")]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Mesaj tarihi")]
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
        public string ReceiverName { get; set; }
        public string SenderName { get; set; }
        public bool ReceiverDel { get; set; }
        public bool SenderDel { get; set; }
        public string NewsUserId { get; set; }
        public NewsUser NewsUser { get; set; }
        public int MessageRecipientId { get; set; }
        public MessageRecipient MessageRecipient { get; set; }


    }
}
