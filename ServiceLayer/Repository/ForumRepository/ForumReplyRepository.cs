using DAL;
using Entites.Models.ForumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace ServiceLayer.Repository.ForumRepository
{
    public class ForumReplyRepository : GenericRepository<ForumReply>, IForumReplyRepository
    {
        public ForumReplyRepository(GameNewsDbContext context)
         : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }

        public IEnumerable<ForumReply> GetDetails(Expression<Func<ForumReply, bool>> predicate)
        {
            return _context.Set<ForumReply>()
                .Include(a => a.NewsUser).Where(predicate);
        }
    }
}
