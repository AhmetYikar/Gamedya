using DAL;
using Entites.Models.NewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.NewsRepository
{
    public class NewsVideoRepository : GenericRepository<NewsVideo>, INewsVideoRepository
    {
        public NewsVideoRepository(GameNewsDbContext context)
       : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }
    }
}
