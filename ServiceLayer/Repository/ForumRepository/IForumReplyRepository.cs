using Entites.Models.ForumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.ForumRepository
{
    public interface IForumReplyRepository : IRepository<ForumReply>
    {
        IEnumerable<ForumReply> GetDetails(Expression<Func<ForumReply, bool>> predicate);
    }
}
