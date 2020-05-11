using DAL;
using Entites.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.NewsRepository
{
    public class NewsCategoryRepository : GenericRepository<NewsCategory>, INewsCategoryRepository
    {
        public NewsCategoryRepository(GameNewsDbContext context)
       : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }


    }
}
