using DAL;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.MessageRepository
{
    public class MessageRecipientRepository : GenericRepository<MessageRecipient>, IMessageRecipientRepository
    {
        public MessageRecipientRepository(GameNewsDbContext context)
           : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }
    }
}
