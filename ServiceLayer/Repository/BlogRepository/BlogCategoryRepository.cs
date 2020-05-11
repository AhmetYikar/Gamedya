using DAL;
using Entites.Models.BlogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ServiceLayer.Repository.BlogRepository
{

    public class BlogCategoryRepository : GenericRepository<BlogCategory>, IBlogCategoryRepository
    {

        public BlogCategoryRepository(GameNewsDbContext context)
            : base(context)
        {

        }


        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }

    }
}
