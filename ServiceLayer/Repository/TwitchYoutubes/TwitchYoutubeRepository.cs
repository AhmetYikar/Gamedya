using DAL;
using Entities.Models.TwitchYoutube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.TwitchYoutubes
{
    public class TwitchYoutubeRepository : GenericRepository<TwitchYoutube>, ITwitchYoutubeRepository
    {
        public TwitchYoutubeRepository(GameNewsDbContext context)
         : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }

        public IEnumerable<TwitchYoutube> GetTwitchYoutubeDetails(Expression<Func<TwitchYoutube, bool>> predicate)
        {
            return _context.Set<TwitchYoutube>();              
        }


    }
}
