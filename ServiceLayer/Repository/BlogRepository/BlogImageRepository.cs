using DAL;
using Entites.Models.BlogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.BlogRepository
{
    public class BlogImageRepository : GenericRepository<BlogImage>, IBlogImageRepository
    {

        public BlogImageRepository(GameNewsDbContext context)
            : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }
    }
}
