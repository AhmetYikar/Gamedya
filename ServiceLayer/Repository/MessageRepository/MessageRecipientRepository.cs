using DAL;
using Entites.Models.MessageModels;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


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
