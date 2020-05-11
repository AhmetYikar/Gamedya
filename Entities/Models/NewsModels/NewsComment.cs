using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites.Models.Status;
using Entites.Models.UserModels;

namespace Entites.Models.NewsModels
{
    public class NewsComment
    {
        public int Id { get; set; }

        //[MaxLength(255, ErrorMessage = "255 karakterden fazla olamaz")]
        // [Required(ErrorMessage ="Boş geçilemez")]
        public string Content { get; set; }
        public bool IsOk { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
        public string NewsUserId { get; set; }
        public NewsUser NewsUser { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string GuestName { get; set; }
        public int LikeCount { get; set; }
        public int UnLikeCount { get; set; }



    }
}
