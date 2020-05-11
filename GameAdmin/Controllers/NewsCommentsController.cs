using System;
using ServiceLayer.Uow;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Entites.Models.NewsModels;

namespace GameAdmin.Controllers
{
    [Authorize(Roles = "admin,yazar")]
    public class NewsCommentsController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());
        #region all        
        public ActionResult AllComments()
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {


                IEnumerable<NewsComment> newsComments = uow.NewsComment.GetAll().ToList();

                if (newsComments == null)
                {
                    return null;
                }
                return View(newsComments);
            }
        }
        #endregion

        #region index
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            News news = uow.News.GetNewsWithComments(a => a.Id == id).FirstOrDefault();
            if (news.NewsComments != null && news.NewsComments.Count() > 0)
            {
                IEnumerable<NewsComment> comments = news.NewsComments.ToList();
                return View(comments);
            }


            else
            {
                return RedirectToAction("Mesaj", "Ortak", new { hataMesaji = "Bu habere ait yorum bulunamadı" });
                //return HttpNotFound();
            }


        }
        #endregion

        #region edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsComment newsComment = uow.NewsComment.GetById(id);
            if (newsComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewsId = uow.News.Where(a => a.Id == newsComment.NewsId);
            ViewBag.NewsUserId = uow.NewsUser.Where(a => a.Id == newsComment.NewsUserId);
            return View(newsComment);
        }

        // POST: NewsComments/Edit/5      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,IsOk,NewsId,NewsUserId")] NewsComment newsComment)
        {
            var commentdb = uow.NewsComment.GetById(newsComment.Id);
            commentdb.IsOk = newsComment.IsOk;
            if (ModelState.IsValid)
            {
                uow.NewsComment.Update(commentdb);
                uow.Complete();
                return RedirectToAction("Index", new { id = commentdb.NewsId });
            }
            ViewBag.NewsId = uow.News.Where(a => a.Id == newsComment.NewsId);
            ViewBag.NewsUserId = uow.NewsUser.Where(a => a.Id == newsComment.NewsUserId);
            return View(newsComment);
        }
        #endregion

        #region delete
        // GET: NewsComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsComment newsComment = uow.NewsComment.GetById(id);
            if (newsComment == null)
            {
                return HttpNotFound();
            }
            return View(newsComment);
        }

        // POST: NewsCommentsDeneme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsComment newsComment = uow.NewsComment.GetById(id);
            uow.NewsComment.Delete(newsComment);
            uow.Complete();
            return RedirectToAction("Index", new { id = newsComment.NewsId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
