using Entites.Models.UserModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models.MessageModels
{
    public class Message
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Boş geçilemez")]
        [Display(Name = "Mesaj metni")]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Mesaj tarihi")]
        public DateTime Date { get; set; }

        public string NewsUserId { get; set; }
        public NewsUser NewsUser { get; set; }

    }
}
