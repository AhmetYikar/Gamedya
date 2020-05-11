using Entites.Models.NewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.NewsRepository
{
    public interface INewsCommentRepositroy : IRepository<NewsComment>
    {
        IEnumerable<NewsComment> GetCommentWithUser(Expression<Func<NewsComment, bool>> predicate);

    }
}
