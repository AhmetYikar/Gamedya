using Entites.Models.ForumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.ForumRepository
{
    public interface IForumCategoryRepository : IRepository<ForumCategory>
    {
        IEnumerable<ForumCategory> Details(Expression<Func<ForumCategory, bool>> predicate);

    }
}
