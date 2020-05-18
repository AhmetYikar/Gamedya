using DAL;
using Entities.Models;
using ServiceLayer.Repository.NotificationRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ServiceLayer.Repository.MessageRepository
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(GameNewsDbContext context)
            : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }

        public IEnumerable<Notification> GetNotificationDetails()
        {
            return _context.Set<Notification>()
                .Include(a => a.NewsUsers);                
        }
    }
}
