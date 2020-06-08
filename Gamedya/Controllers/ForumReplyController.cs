using DAL;
using Entites.Models.ForumModels;
using Entites.Models.Status;
using Gamedya.Helper;
using ServiceLayer.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gamedya.Controllers
{
    public class ForumReplyController : Controller
    {

        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: NewsComment
        public ActionResult Index(int? id)
        {
            IEnumerable<ForumReply> comments = uow.ForumReply.GetDetails(a => a.ForumPostId == id/* && a.IsOk==true*/);

            if (comments != null && comments.Count() > 0)
            {
                return PartialView(comments);
            }
            return null;
        }



        [HttpPost]
        public JsonResult AddComment( string comment, int forumPostId)
        {
            string mesaj;
            if (comment.Length<2)
            {
                mesaj = "yorum 2 karakterden kısa olamaz";
                return Json(mesaj, JsonRequestBehavior.AllowGet);
            }
            ForumReply forumReply = new ForumReply();

            forumReply.Content = comment;
            forumReply.ForumPostId = forumPostId;
            forumReply.Date = DateTime.Now;
            forumReply.NewsUserId = User.GetUserId();
            uow.ForumReply.Insert(forumReply);
            uow.Complete();

            mesaj = "Yorumunuz gönderilmiştir.";
            return Json(mesaj, JsonRequestBehavior.AllowGet);

        }

        // GET: LikeUnlike
        public JsonResult Like(int replyId)
        {
            int count = 0;

            if (GetCookie("forumLike", replyId) == null)
            {
                CreateCookie("forumLike", replyId);

                string userId = User.GetUserId();
                try
                {
                    ForumReply comment = uow.ForumReply.GetById(replyId);
                    LikeTable likeTable = new LikeTable();
                    likeTable.Status = Status.Like;
                    likeTable.Module = Module.ForumReply;
                    likeTable.ModuleId = replyId;
                    likeTable.NewsUserId = userId;
                    comment.LikeCount++;
                    uow.ForumReply.Update(comment);
                    uow.LikeTable.Insert(likeTable);
                    uow.Complete();
                    count = comment.LikeCount;
                    return Json(count, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    Json(count, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(count, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Dislike(int replyId)
        {
            int count = 0;

            if (GetCookie("forumDislike", replyId) == null)
            {
                CreateCookie("forumDislike", replyId);

                string userId = User.GetUserId();
                try
                {
                    ForumReply comment = uow.ForumReply.GetById(replyId);
                    LikeTable likeTable = new LikeTable();
                    likeTable.Status = Status.Unlike;
                    likeTable.Module = Module.ForumReply;
                    likeTable.ModuleId = replyId;
                    likeTable.NewsUserId = userId;
                    comment.UnLikeCount++;
                    uow.ForumReply.Update(comment);
                    uow.LikeTable.Insert(likeTable);
                    uow.Complete();
                    count = comment.UnLikeCount;
                    return Json(count, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    Json(count, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(count, JsonRequestBehavior.AllowGet);
        }

        private void CreateCookie(string name, int id)
        {
            name = name + id.ToString();
            HttpCookie cookieLike = new HttpCookie(name);
            cookieLike.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookieLike);
        }
        private string GetCookie(string name, int id)
        {
            name = name + id.ToString();
            if (Request.Cookies.AllKeys.Contains(name))
            {
                return Request.Cookies[name].Value;
            }
            return null;
        }
    }
}
