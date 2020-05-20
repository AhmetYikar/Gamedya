using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Gamedya.Models
{
    public class NewsViewWeb
    {
        public int Id { get; set; }


        public string Title { get; set; }

        public string Summary { get; set; }

        public string TinyImagePath { get; set; }
        public string BigImagePath { get; set; }
        public string VideoPath { get; set; }

        public DateTime Date { get; set; }
        public int CommentCount { get; set; }
        
        public string GenerateSlug()
        {
            string phrase = string.Format("{0}-{1}-{2}", Id, Title, Date.ToShortDateString());

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