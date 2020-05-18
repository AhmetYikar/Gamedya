using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.NotificationRepository
{
    public interface INotificationRepository: IRepository<Notification>
    {
        IEnumerable<Notification> GetNotificationDetails();
    }
}
