using DAL;
using Entites.Models.ForumModels;
using GameAdmin.Helper;
using ServiceLayer.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GameAdmin.Controllers
{
    [Authorize(Roles = "admin,yazar")]

    public class ForumReplyController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        #region all        

        public ActionResult AllReply()
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {

                IEnumerable<ForumReply> forumReplies = uow.ForumReply.GetAll().ToList();

                if (forumReplies == null)
                {
                    return null;
                }
                return View(forumReplies);
            }
        }
        #endregion

        #region İndex

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ForumPost forumPost = uow.ForumPost.GetForumWithReply(a => a.Id == id).FirstOrDefault();
            IEnumerable<ForumReply> comments = forumPost.ForumReply.ToList();
            if (comments != null)
            {
                comments.FirstOrDefault().ForumPost = forumPost;

            }

            if (comments == null)
            {
                return HttpNotFound();
            }

            return View(comments);
        }
        #endregion

        #region edit        

        // GET: BlogComment/Edit/5
        public ActionResult Edit(int? id)
        {
            ForumReply forumReplies = uow.ForumReply.GetById(id);


            if (id == null || forumReplies.NewsUserId != User.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (forumReplies == null)
            {
                return HttpNotFound();
            }
            ViewBag.ForumPostId = uow.BlogPost.Where(a => a.Id == forumReplies.ForumPostId);
            ViewBag.NewsUserId = uow.NewsUser.Where(a => a.Id == forumReplies.NewsUserId);
            return View(forumReplies);
        }

        // POST: NewsComments/Edit/5      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,IsOk,ForumPostId,NewsUserId")] ForumReply forumReplies)
        {
            var commentdb = uow.ForumReply.GetById(forumReplies.Id);
            commentdb.IsOk = forumReplies.IsOk;
            if (ModelState.IsValid)
            {
                uow.ForumReply.Update(commentdb);
                uow.Complete();
                return RedirectToAction("Index", new { id = commentdb.ForumPostId });
            }
            ViewBag.ForumPostId = uow.ForumPost.Where(a => a.Id == forumReplies.ForumPostId);
            ViewBag.NewsUserId = uow.NewsUser.Where(a => a.Id == forumReplies.NewsUserId);
            return View(forumReplies);
        }
        #endregion

        #region SİL        

        // GET: BlogComment/Delete/5
        public ActionResult Delete(int? id)
        {
            ForumReply forumReplies = uow.ForumReply.GetById(id);


            if (id == null || forumReplies.NewsUserId != User.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (forumReplies == null)
            {
                return HttpNotFound();
            }
            return View(forumReplies);
        }

        // POST: BlogComment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ForumReply forumReplies = uow.ForumReply.GetById(id);
            uow.ForumReply.Delete(forumReplies);
            uow.Complete();
            return RedirectToAction("Index", new { id = forumReplies.ForumPostId });
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
