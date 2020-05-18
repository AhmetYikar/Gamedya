using Entites.Models.NewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.NewsRepository
{
    public interface INewsRepository : IRepository<News>
    {
        IEnumerable<News> GetNewsWithImages(Expression<Func<News, bool>> predicate);
        IEnumerable<News> GetNewsWithVideos(Expression<Func<News, bool>> predicate);

        IEnumerable<News> GetNewsWithComments(Expression<Func<News, bool>> predicate);
        IEnumerable<News> GetNewsWithCategory(Expression<Func<News, bool>> predicate);

        IEnumerable<News> GetNewsDetails(Expression<Func<News, bool>> predicate);

    }
}
