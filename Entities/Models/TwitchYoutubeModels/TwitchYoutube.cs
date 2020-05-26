using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.TwitchYoutube
{
    public enum VideoPlatform : byte
    {
        Youtube,
        Twitch
    }


    public class TwitchYoutube
    {

        public int Id { get; set; }

        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Display(Name = "Video Yolu")]
        public string VideoPath { get; set; }

        [Display(Name = "Kapak Resmi")]
        public  string CoverImage { get; set; }

        [Display(Name = "Yayın Tarihi")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Görüntülenme Sayısı")]
        public int ViewCount { get; set; }

        [Display(Name = "Aktif Mi?")]
        public bool IsActive { get; set; }

        public VideoPlatform VideoPlatform { get; set; }
    }
}
