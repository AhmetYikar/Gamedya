using DAL;
using Entites.Models.ForumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ServiceLayer.Repository.ForumRepository
{
    public class ForumCategoryRepository : GenericRepository<ForumCategory>, IForumCategoryRepository
    {
        public ForumCategoryRepository(GameNewsDbContext context)
            : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }


        public IEnumerable<ForumCategory> Details(Expression<Func<ForumCategory, bool>> predicate)
        {
            return _context.Set<ForumCategory>()
                .Include(a => a.ForumPosts)
                .Include(a => a.ForumCategories)
                .Where(predicate);
        }

    }
}
