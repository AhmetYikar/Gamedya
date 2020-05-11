using DAL;
using Entites.Models.ForumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.ForumRepository
{
    public class ForumReplyRepository : GenericRepository<ForumReply>, IForumReplyRepository
    {
        public ForumReplyRepository(GameNewsDbContext context)
         : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }
    }
}
