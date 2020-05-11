using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites.Models.UserModels;

namespace Entites.Models.GameModels
{

    public enum Platform : byte
    {

        Xbox,
        Pc,
        Playstation,
        Mobile,
        Nintendo,
        VR
    }




    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string NewsUserId { get; set; }
        public NewsUser NewsUser { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public ICollection<GameUpdate> GameUpdates { get; set; }
        public int GameGenreId { get; set; }
        public GameGenre GameGenre { get; set; }
        public Platform GamePlatform { get; set; }

    }
}
