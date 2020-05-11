using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Entites.Models.UserModels;

namespace Entites.Models.GameModels
{
    public enum ArticleType : byte
    {

        Review,
        Update,
        General,

    }
    public class GameUpdate
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public ArticleType ArticleType { get; set; }

    }
}
