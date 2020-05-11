using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites.Models.UserModels;

namespace Entites.Models.GameModels
{
    public class GameGenre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public ICollection<Game> Games { get; set; }


    }
}
