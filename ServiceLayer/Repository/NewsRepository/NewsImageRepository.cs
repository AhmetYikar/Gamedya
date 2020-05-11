using DAL;
using Entites.Models.NewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.NewsRepository
{
    public class NewsImageRepository : GenericRepository<NewsImage>, INewsImageRepository
    {
        public NewsImageRepository(GameNewsDbContext context)
       : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }
    }
}
