
using DAL;
using Entites.Models.MessageModels;
using Entites.Models.NewsModels;
using Entites.Models.UserModels;
using Gamedya.Helper;
using ServiceLayer.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Gamedya.Controllers
{
    public class NewsCommentController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: NewsComment
        public ActionResult Index(int? id)
        {
            IEnumerable<NewsComment> comments = uow.NewsComment.GetCommentWithUser(a=>a.NewsId==id/* && a.IsOk==true*/);

            if (comments!=null && comments.Count()>0)
            {
                return PartialView(comments);
            }
            return null;
        }

        

        [HttpPost]
        public JsonResult AddComment(string comment,string guestName, int newsId)
        {
            string mesaj;

            if (guestName == "")
            {
                mesaj = "Lütfen adınızı giriniz";
                return Json(mesaj, JsonRequestBehavior.AllowGet);
            }

            if (comment.Length < 2)
            {
                mesaj = "Yorum 2 karakterden kısa olamaz";
                return Json(mesaj, JsonRequestBehavior.AllowGet);
            }
            
                NewsComment newsComment = new NewsComment();                
                             
                newsComment.Content = comment;
                newsComment.NewsId = newsId;
                newsComment.Date = DateTime.Now;
                if (User.Identity.IsAuthenticated )
                {
                    newsComment.GuestName = User.GetFullName();
                    newsComment.NewsUserId = User.GetUserId();
                }
                else if(guestName != null)
                {
                    newsComment.GuestName = guestName;
                }

                uow.NewsComment.Insert(newsComment);
                uow.Complete();
                mesaj = "Yorumunuz gönderilmiştir. Onaylandıktan sonra yayınlanacaktır.";
                return Json(mesaj, JsonRequestBehavior.AllowGet);
                      
        }
    }
}