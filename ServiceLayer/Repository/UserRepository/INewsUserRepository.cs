using Entites.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.UserRepository
{
    public interface INewsUserRepository : IRepository<NewsUser>
    {
        IEnumerable<NewsUser> UserWithGames();
        IEnumerable<NewsUser> UserWithRoles(Expression<Func<NewsUser, bool>> predicate);

    }
}
