using DAL;
using Entites.Models.ForumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ServiceLayer.Repository.ForumRepository
{
    public class ForumCategoryRepository : GenericRepository<ForumCategory>, IForumCategoryRepository
    {
        public ForumCategoryRepository(GameNewsDbContext context)
            : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }

      
    }
}
