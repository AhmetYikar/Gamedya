using DAL;
using Entites.Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ServiceLayer.Repository.MessageRepository
{
    public class MessageRepository : GenericRepository<GamedyaMessage>, IMessageRepository
    {
        public MessageRepository(GameNewsDbContext context)
            : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }
        public IEnumerable<GamedyaMessage> GetMessageDetails(Expression<Func<GamedyaMessage, bool>> predicate)
        {
            return _context.Set<GamedyaMessage>()
                .Include(a => a.NewsUser)
                .Include(a => a.MessageRecipient)
                .Where(predicate);
        }
    }
}
