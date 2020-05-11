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
    public class ForumPostRepository : GenericRepository<ForumPost>, IForumPostRepository
    {
        public ForumPostRepository(GameNewsDbContext context)
         : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }

        public IEnumerable<ForumPost> GetForumDetail(Expression<Func<ForumPost, bool>> predicate)
        {
            return _context.Set<ForumPost>()
                .Include(a => a.ForumImages)
                .Include(a => a.ForumCategory)
                .Include(a => a.ForumReply).Where(predicate);
        }

        IEnumerable<ForumPost> IForumPostRepository.GetForumWithReply(Expression<Func<ForumPost, bool>> predicate)
        {
            return _context.Set<ForumPost>()
                           .Include(a => a.ForumReply).Where(predicate);
        }
    }
}
