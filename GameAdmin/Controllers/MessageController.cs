using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Entites.Models.MessageModels;
using Entites.Models.UserModels;
using GameAdmin.Helper;
using ServiceLayer.Uow;

namespace GameAdmin.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: Message
        public ActionResult Index()
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {


                IEnumerable<Message> messages = uow.Message.GetAll().ToList();

                if (messages == null)
                {
                    return null;
                }


                return View(messages);
            }

        }

        // GET: Message/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                Message message = uow.Message.GetById(id);
                NewsUser user = uow.NewsUser.Where(a => a.Id == message.NewsUserId).FirstOrDefault();
                message.NewsUser = user;
                if (message == null)
                {
                    return null;
                }

                return View(message);

            }
        }

        // GET: Message/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Message/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,Date,NewsUserId")] Message message)
        {


            message.Date = DateTime.Now;
            message.NewsUserId = User.GetUserId();
            if (ModelState.IsValid)
            {
                uow.Message.Insert(message);
                uow.Complete();
                ViewBag.MesajBasari = "Mesajınız başarıyla gönderilmiştir";
                return View(message);
            }

            ViewBag.MesajHata = "Mesaj gönderilemedi";
            return View(message);
        }


        // GET: Message/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = uow.Message.GetById(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = uow.Message.GetById(id);
            uow.Message.Delete(message);
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
