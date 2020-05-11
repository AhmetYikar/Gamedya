using DAL;
using Entites.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ServiceLayer.Repository.GameRepository
{
    public class GameUpdateRepository : GenericRepository<GameUpdate>, IGameUpdateRepository
    {
        public GameUpdateRepository(GameNewsDbContext context)
        : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }

        public IEnumerable<GameUpdate> GameUpdateDetails(Expression<Func<GameUpdate, bool>> predicate)
        {
            return _context.Set<GameUpdate>().Include(a => a.Game).Where(predicate);
        }
    }
}
