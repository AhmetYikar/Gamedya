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
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository(GameNewsDbContext context)
       : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }

        public IEnumerable<News> GetNewsDetails(Expression<Func<News, bool>> predicate)
        {
            return _context.Set<News>()
                .Include(a => a.NewsUser)
                .Include(a => a.NewsImages)
                .Include(a => a.NewsCategory)
                .Include(a => a.NewsComments)
                .Include(a => a.NewsVideos).Where(predicate);
        }

        public IEnumerable<News> GetNewsWithImages(Expression<Func<News, bool>> predicate)
        {
            return _context.Set<News>().Include(a => a.NewsImages).Where(predicate);
        }

        public IEnumerable<News> GetNewsWithVideos(Expression<Func<News, bool>> predicate)
        {
            return _context.Set<News>().Include(a => a.NewsVideos).Where(predicate);
        }
        public IEnumerable<News> GetNewsWithComments(Expression<Func<News, bool>> predicate)
        {
            return _context.Set<News>().Include(a => a.NewsComments).Where(predicate);
        }

        public IEnumerable<News> GetNewsWithCategory(Expression<Func<News, bool>> predicate)
        {
            return _context.Set<News>().Include(a => a.NewsCategory).Where(predicate);
        }
    }
}
