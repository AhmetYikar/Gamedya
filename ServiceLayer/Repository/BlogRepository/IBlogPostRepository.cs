using Entites.Models.BlogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.BlogRepository
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        IEnumerable<BlogPost> GetBlogDetails(Expression<Func<BlogPost, bool>> predicate);
        IEnumerable<BlogPost> GetBlogWithComments(Expression<Func<BlogPost, bool>> predicate);

    }
}
