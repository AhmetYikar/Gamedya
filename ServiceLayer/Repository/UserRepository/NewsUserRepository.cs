using DAL;
using Entites.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.UserRepository
{
    public class NewsUserRepository : GenericRepository<NewsUser>, INewsUserRepository
    {
        public NewsUserRepository(GameNewsDbContext context)
        : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }
    }
}
