
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL;
using Entites.Models.ForumModels;

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
                .Include(a => a.ForumCategory)
                .Include(a => a.ForumReplies).Where(predicate);
        }

        IEnumerable<ForumPost> IForumPostRepository.GetForumWithReplies(Expression<Func<ForumPost, bool>> predicate)
        {
            return _context.Set<ForumPost>()
                           .Include(a => a.ForumReplies).Where(predicate);
        }

        IEnumerable<ForumPost> IForumPostRepository.GetForumWithRepliesAndUsers()
        {
            return _context.Set<ForumPost>()
                           .Include(a => a.NewsUser)
                           .Include(a => a.ForumReplies);
        }
    }
}
