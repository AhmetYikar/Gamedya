using Entites.Models.ForumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.ForumRepository
{
    public interface IForumPostRepository : IRepository<ForumPost>
    {
        IEnumerable<ForumPost> GetForumDetail(Expression<Func<ForumPost, bool>> predicate);
        IEnumerable<ForumPost> GetForumWithReplies(Expression<Func<ForumPost, bool>> predicate);
        IEnumerable<ForumPost> GetForumWithRepliesAndUsers();
    }
}
