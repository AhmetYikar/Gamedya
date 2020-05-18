using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Migrations;
using Entites.Models.UserModels;
using Entities.Models;
using GameAdmin.Helper;
using ServiceLayer.Uow;

namespace GameAdmin.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: Notification
        [Authorize(Roles ="admin")]
        public ActionResult Index()
        {
            return View(uow.Notification.GetAll().ToList());
        }

        public ActionResult UserNotification()
        {
            string id = User.GetUserId();
            
                IEnumerable<Notification> notifications = uow.Notification.GetNotificationDetails();
                var getNotifications = new List<Notification>();
                foreach (var notification in notifications)
                {
                    IEnumerable<NewsUser> users = notification.NewsUsers;
                    foreach (var user in users)
                    {
                        if (user.Id == id)
                        {
                            getNotifications.Add(notification);
                            break;
                        }
                    }
                }
                return View(getNotifications);
            
        }

        public ActionResult GetFourNotification()
        {
            string id = User.GetUserId();

            List<Notification> notifications = uow.Notification.GetNotificationDetails().ToList();
            if (notifications!=null)
            {
                List<Notification> getNotifications = new List<Notification>();
                foreach (var notification in notifications)
                {
                    List<NewsUser> users = notification.NewsUsers.ToList();
                    foreach (var user in users)
                    {
                        if (user.Id == id)
                        {
                            getNotifications.Add(notification);
                            break;
                        }
                    }
                }
                return PartialView(getNotifications.OrderByDescending(a => a.Id).ToList());
            }
            return null;

        }



        // GET: Notification/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notification/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,IsRead,Date")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                uow.Notification.Insert(notification);
                uow.Complete();
                return RedirectToAction("Index");
            }

            return View(notification);
        }

        
        // GET: Notification/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = uow.Notification.GetById(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: Notification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notification notification = uow.Notification.GetById(id);
            uow.Notification.Delete(notification);
            uow.Complete();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
