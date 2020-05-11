using DAL;
using Entites.Models.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.UserRepository
{
    public class LikeTableRepository : GenericRepository<LikeTable>, ILikeTableRepository
    {
        public LikeTableRepository(GameNewsDbContext context)
          : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }
    }
}
