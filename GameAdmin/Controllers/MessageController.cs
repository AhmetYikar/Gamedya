using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using DAL;
using Entites.Models.MessageModels;
using Entites.Models.UserModels;
using Entities.Models;
using GameAdmin.Helper;
using ServiceLayer.Uow;

namespace GameAdmin.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        public ActionResult Index()
        {
            IEnumerable<GamedyaMessage> messages = uow.GamedyaMessage.GetAll();

            return View(messages);
        }

        public ActionResult MyMessages()
        {
            string id = User.GetUserId();
            IEnumerable<GamedyaMessage> myMessages = uow.GamedyaMessage
                                                    .GetMessageDetails(a => a.MessageRecipient.NewsUserId==id && a.ReceiverDel==false);
            
            return View(myMessages);
        }

        public ActionResult MySentMessages()
        {
            string id = User.GetUserId();


            IEnumerable<GamedyaMessage> mySentMessages = uow.GamedyaMessage
                                                        .Where(a => a.NewsUserId == id && a.SenderDel==false);
            
            return PartialView(mySentMessages);
        }


        public ActionResult GetFourMessage()
        {
            string id = User.GetUserId();

            IEnumerable<GamedyaMessage> messages = uow.GamedyaMessage
                                                  .GetMessageDetails(a => a.MessageRecipient.NewsUserId == id && a.ReceiverDel==false );
            if (messages!=null)
            {
                IEnumerable<GamedyaMessage> unReadMessages = messages.Where(a => a.IsRead == false);
                int unReadCount = unReadMessages.Count();
                ViewBag.unReadCount = unReadCount;
                return PartialView(messages.OrderByDescending(a => a.Id).Take(4));

            }
            return null;
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GamedyaMessage message = uow.GamedyaMessage.GetMessageDetails(a=>a.Id==id).FirstOrDefault();
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }


        //mesaj okunduğunda isRead 
        [HttpPost]
        public JsonResult MessageRead(int? id)
        {
            string userId=User.GetUserId();
            if (id != null)
            {
                GamedyaMessage message = uow.GamedyaMessage.GetMessageDetails(a=>a.Id==id).FirstOrDefault();
                if (message.MessageRecipient.NewsUserId==userId)
                {
                    message.IsRead = true;
                    uow.GamedyaMessage.Update(message);
                    uow.Complete();
                    return Json(JsonRequestBehavior.AllowGet);
                }
                
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        // GET: MessageRecipient/Create
        public ActionResult Create()
        {
            ViewBag.NewsUserId = new SelectList(uow.NewsUser.GetAll(), "Id", "Email");
            return View();
        }

        // GET: Message/Create
        public ActionResult MessageContent(string id)
        {
            if (id==null)
            {                
                return RedirectToAction("Create");
            }
            MessageRecipient messageRecipient = new MessageRecipient();
            NewsUser user = uow.NewsUser.Where(a=>a.Id==id).FirstOrDefault();
            messageRecipient.NewsUserId = id;
            uow.MessageRecipient.Insert(messageRecipient);
            uow.Complete();
            try
            {
                GamedyaMessage message = new GamedyaMessage();
                message.ReceiverName =user.FullName;
                message.MessageRecipientId = messageRecipient.Id;
                return PartialView(message);
            }
            catch
            {
                ViewBag.Message = "Mesaj gönderilemedi";
                return RedirectToAction("Create");
            }

        }

      
       

        // POST: Message/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,MessageRecipientId,ReceiverName")] GamedyaMessage message)
        {
            message.Date = DateTime.Now;
            message.NewsUserId = User.GetUserId();
            message.SenderName = User.GetFullName();
            if (ModelState.IsValid)
            {
                uow.GamedyaMessage.Insert(message);
                uow.Complete();
                ViewBag.MesajBasari = "Mesajınız başarıyla gönderilmiştir";
                return RedirectToAction("MyMessages");
            }
            ViewBag.MessageRecipientId = new SelectList(uow.MessageRecipient.GetAll(), "Id", "NewsUserId");
            ViewBag.NewsUserId = new SelectList(uow.NewsUser.GetAll(), "Id", "FullName");
            ViewBag.MesajHata = "Mesaj gönderilemedi";
            return View(message);

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GamedyaMessage message = uow.GamedyaMessage.GetById(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Notification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,string who)
        {

            GamedyaMessage message = uow.GamedyaMessage.GetById(id);
            //bu silme işlemini kullanan kişi mesajı alan kişi ise 
            //sadece alan kişi için silinir. db'de kalmaya devam eder.
           
            if (who=="receiver")
            {
                message.ReceiverDel = true;
                uow.GamedyaMessage.Update(message);
            }

            //bu silme işlemini kullanan kişi mesajı gönderen kişi ise 
            //sadece gönderen için silinir db'de kalmaya devam eder.
            else if (who == "sender")
            {
                message.SenderDel = true;
                uow.GamedyaMessage.Update(message);
            }

            //bu silme işlemini kullanan kişi admin ise ise
            //mesaj veri tabanından tamamen silinir
            else if (who == "admin")
            {
                uow.GamedyaMessage.Delete(message);
            }

            uow.GamedyaMessage.Update(message);
            uow.Complete();
            return RedirectToAction("MyMessages");

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
