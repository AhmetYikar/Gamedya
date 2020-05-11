using Entites.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.GameRepository
{
    public interface IGameUpdateRepository : IRepository<GameUpdate>
    {
        IEnumerable<GameUpdate> GameUpdateDetails(Expression<Func<GameUpdate, bool>> predicate);

    }


}
