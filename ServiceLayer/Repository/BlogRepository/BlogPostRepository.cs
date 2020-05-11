using DAL;
using Entites.Models.BlogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ServiceLayer.Repository.BlogRepository
{
    public class BlogPostRepository : GenericRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(GameNewsDbContext context)
            : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }

        public IEnumerable<BlogPost> GetBlogDetails(Expression<Func<BlogPost, bool>> predicate)
        {
            return _context.Set<BlogPost>()
                .Include(a => a.NewsUser)
                .Include(a => a.BlogImages)
                .Include(a => a.BlogComments)
                .Include(a => a.BlogCategory).Where(predicate);
        }



        IEnumerable<BlogPost> IBlogPostRepository.GetBlogWithComments(Expression<Func<BlogPost, bool>> predicate)
        {
            return _context.Set<BlogPost>()
                           .Include(a => a.BlogComments).Where(predicate);
        }
    }
}
