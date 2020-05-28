using Entites.Models.BlogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.BlogRepository
{
    public interface IBlogCommentRepository : IRepository<BlogComment>
    {
        IEnumerable<BlogComment> GetDetails(Expression<Func<BlogComment, bool>> predicate);

    }
}
