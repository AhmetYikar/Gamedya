using Entites.Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.MessageRepository
{
    public interface IMessageRepository : IRepository<GamedyaMessage>
    {
        IEnumerable<GamedyaMessage> GetMessageDetails(Expression<Func<GamedyaMessage, bool>> predicate);

    }
}
