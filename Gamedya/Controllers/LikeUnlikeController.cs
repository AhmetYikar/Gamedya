using DAL;
using Entites.Models.NewsModels;
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
    public class LikeUnlikeController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: LikeUnlike
        public JsonResult Like(int commentId)
        {
            NewsComment comment= uow.NewsComment.GetById(commentId);           
            LikeTable likeTable = new LikeTable();
            try
            {
                comment.LikeCount++;
                likeTable.Status = Status.Like;
                likeTable.Module = Module.NewsComent;
                likeTable.ModuleId = commentId;
                likeTable.NewsUserId = User.GetUserId();
                uow.NewsComment.Update(comment);
                uow.LikeTable.Insert(likeTable);
                uow.Complete();
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult UnLike(int commentId)
        {
            NewsComment comment = uow.NewsComment.GetById(commentId);
            LikeTable likeTable = new LikeTable();
            try
            {
                comment.UnLikeCount++;
                likeTable.Status = Status.Unlike;
                likeTable.Module = Module.NewsComent;
                likeTable.ModuleId = commentId;
                likeTable.NewsUserId = User.GetUserId();
                uow.NewsComment.Update(comment);
                uow.LikeTable.Insert(likeTable);
                uow.Complete();
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}