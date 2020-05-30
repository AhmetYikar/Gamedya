using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Entites.Models.BlogModels;
using GameAdmin.Helper;
using ServiceLayer.Uow;

namespace GameAdmin.Controllers
{
    [Authorize]

    public class BlogCommentController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        #region all
        // GET: BlogComment

        public ActionResult AllComments()
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {


                IEnumerable<BlogComment> blogComments = uow.BlogComment.GetAll().ToList();

                if (blogComments == null)
                {
                    return null;
                }

                return View(blogComments);
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

            IEnumerable<BlogComment> comments = uow.BlogComment.Where(a=>a.BlogPostId==id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogTitle = uow.BlogPost.GetById(id).Title;
            return View(comments);
        }
        #endregion

        #region edit

        // GET: BlogComment/Edit/5

        public ActionResult Edit(int? id)
        {
            BlogComment blogComment = uow.BlogComment.GetById(id);


            if (id == null || blogComment.NewsUserId != User.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (blogComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogPostId = uow.BlogPost.Where(a => a.Id == blogComment.BlogPostId);
            ViewBag.NewsUserId = uow.NewsUser.Where(a => a.Id == blogComment.NewsUserId);
            return View(blogComment);
        }

        // POST: NewsComments/Edit/5      
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "Id,Content,IsOk,BlogPostId,NewsUserId")] BlogComment blogComment)
        {
            var commentdb = uow.BlogComment.GetById(blogComment.Id);
            commentdb.IsOk = blogComment.IsOk;
            if (ModelState.IsValid)
            {
                uow.BlogComment.Update(commentdb);
                uow.Complete();
                return RedirectToAction("Index", new { id = commentdb.BlogPostId });
            }
            ViewBag.BlogPostId = uow.BlogPost.Where(a => a.Id == blogComment.BlogPostId);
            ViewBag.NewsUserId = uow.NewsUser.Where(a => a.Id == blogComment.NewsUserId);
            return View(blogComment);
        }

        #endregion

        #region delete
        // GET: BlogComment/Delete/5

        public ActionResult Delete(int? id)
        {
            BlogComment blogComment = uow.BlogComment.GetById(id);


            if (id == null || blogComment.NewsUserId != User.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (blogComment == null)
            {
                return HttpNotFound();
            }
            return View(blogComment);
        }

        // POST: BlogComment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogComment blogComment = uow.BlogComment.GetById(id);
            uow.BlogComment.Delete(blogComment);
            uow.Complete();
            return RedirectToAction("Details", "Blog", new { id = blogComment.BlogPostId });
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
