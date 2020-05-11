using DAL;
using Entites.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.GameRepository
{
    public class GameGenreRepository : GenericRepository<GameGenre>, IGameGenreRepository
    {
        public GameGenreRepository(GameNewsDbContext context)
         : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }
    }
}
