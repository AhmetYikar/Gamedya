using DAL;
using Entites.Models.BlogModels;
using Entites.Models.Status;
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
    public class BlogCommentController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: NewsComment
        public ActionResult Index(int? id)
        {
            IEnumerable<BlogComment> comments = uow.BlogComment.GetDetails(a => a.BlogPostId == id/* && a.IsOk==true*/);

            if (comments != null && comments.Count() > 0)
            {
                return PartialView(comments);
            }
            return null;
        }



        [HttpPost]
        public JsonResult AddComment(string comment, string guestName, int blogPostId)
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

            BlogComment blogComment = new BlogComment();

            blogComment.Content = comment;
            blogComment.BlogPostId = blogPostId;
            blogComment.Date = DateTime.Now;
            if (User.Identity.IsAuthenticated)
            {
                blogComment.CommenterName = User.GetFullName();
                blogComment.NewsUserId = User.GetUserId();
            }
            else if (guestName != null)
            {
                blogComment.CommenterName = guestName;
            }

            uow.BlogComment.Insert(blogComment);
            uow.Complete();
            mesaj = "Yorumunuz gönderilmiştir. Onaylandıktan sonra yayınlanacaktır.";
            return Json(mesaj, JsonRequestBehavior.AllowGet);

        }

        // GET: LikeUnlike
        public JsonResult Like(int commentId)
        {
            int count = 0;

            if (GetCookie("blogLike", commentId) == null)
            {
                CreateCookie("blogLike", commentId);

                string userId = User.GetUserId();
                try
                {
                    BlogComment comment = uow.BlogComment.GetById(commentId);
                    LikeTable likeTable = new LikeTable();
                    likeTable.Status = Status.Like;
                    likeTable.Module = Module.BlogComment;
                    likeTable.ModuleId = commentId;
                    likeTable.NewsUserId = userId;
                    comment.LikeCount++;
                    uow.BlogComment.Update(comment);
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

        public JsonResult Dislike(int commentId)
        {
            int count = 0;

            if (GetCookie("blogDislike", commentId) == null)
            {
                CreateCookie("blogDislike", commentId);

                string userId = User.GetUserId();
                try
                {
                    BlogComment comment = uow.BlogComment.GetById(commentId);
                    LikeTable likeTable = new LikeTable();
                    likeTable.Status = Status.Unlike;
                    likeTable.Module = Module.NewsComent;
                    likeTable.ModuleId = commentId;
                    likeTable.NewsUserId = userId;
                    comment.UnLikeCount++;
                    uow.BlogComment.Update(comment);
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