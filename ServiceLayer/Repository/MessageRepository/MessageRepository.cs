using DAL;
using Entites.Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.MessageRepository
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(GameNewsDbContext context)
            : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }
    }
}
