using Entities.Models.TwitchYoutube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.TwitchYoutubes
{
    public interface ITwitchYoutubeRepository : IRepository<TwitchYoutube>
    {
        IEnumerable<TwitchYoutube> GetTwitchYoutubeDetails(Expression<Func<TwitchYoutube, bool>> predicate);
    }

   
}
