using DAL;
using Entites.Models.NewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace ServiceLayer.Repository.NewsRepository
{
    public class NewsCommentRepositroy : GenericRepository<NewsComment>, INewsCommentRepositroy
    {
        public NewsCommentRepositroy(GameNewsDbContext context)
     : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }

        public IEnumerable<NewsComment> GetCommentWithUser(Expression<Func<NewsComment, bool>> predicate)
        {
            return _context.Set<NewsComment>()
                .Include(a => a.NewsUser).Where(predicate);
        }

    }
}
