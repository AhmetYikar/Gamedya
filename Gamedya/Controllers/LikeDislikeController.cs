using DAL;
using Entites.Models.NewsModels;
using Entites.Models.Status;
using Entites.Models.UserModels;
using Gamedya.Helper;
using ServiceLayer.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Gamedya.Controllers
{
    public class LikeDislikeController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: LikeUnlike
        public JsonResult Like(int commentId)
        {
            int count = 0;

            if (GetCookie("like", commentId) == null)
            {
                CreateCookie("like", commentId);

                string userId = User.GetUserId();
                try
                {
                    NewsComment comment = uow.NewsComment.GetById(commentId);
                    LikeTable likeTable = new LikeTable();
                    likeTable.Status = Status.Like;
                    likeTable.Module = Module.NewsComent;
                    likeTable.ModuleId = commentId;
                    likeTable.NewsUserId = userId;
                    comment.LikeCount++;
                    uow.NewsComment.Update(comment);
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

            if (GetCookie("dislike", commentId) == null)
            {
                CreateCookie("dislike", commentId);

                string userId = User.GetUserId();
                try
                {
                    NewsComment comment = uow.NewsComment.GetById(commentId);
                    LikeTable likeTable = new LikeTable();
                    likeTable.Status = Status.Unlike;
                    likeTable.Module = Module.NewsComent;
                    likeTable.ModuleId = commentId;
                    likeTable.NewsUserId = userId;
                    comment.UnLikeCount++;
                    uow.NewsComment.Update(comment);
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

        private void CreateCookie(string name,int id)
        {
            name = name + id.ToString();
            HttpCookie cookieLike = new HttpCookie(name);
            cookieLike.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookieLike);
        }
        private string GetCookie(string name,int id)
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