using Entites.Models.ForumModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Gamedya.Models
{
    public class ForumViewModel
    {
        public int Id { get; set; }
      
        [Display(Name = "Forum Başlığı")]
        public string ForumTitle { get; set; }

        [Display(Name = "Kategori")]
        public string CategoryName { get; set; }

        public int CategoryId { get; set; }


        [Display(Name = "Yayın Tarihi")]      
        public DateTime Date { get; set; }

        [Display(Name = "Görüntülenme")]
        public int ViewCount { get; set; }

        public string NewsUserId { get; set; }

        public string ForumUser { get; set; } = "";

        [Display(Name = "Yorum Sayısı")]
        public int ReplyCount { get; set; }


        public string GenerateSlug()
        {
            string phrase = string.Format("{0}-{1}-{2}", Id, ForumTitle, Date.ToShortDateString());

            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        private string RemoveAccent(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}