using DAL;
using Entites.Models.GameModels;
using ServiceLayer.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GameAdmin.Controllers
{
    public class GeneralArticleController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: GameUpdate
        public ActionResult Index()
        {
            IEnumerable<GameUpdate> articles = uow.GameUpdate.GameUpdateDetails(a => a.ArticleType == ArticleType.General).OrderByDescending(a => a.Id);
            if (articles == null)
            {
                return null;
            }

            return View(articles);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameUpdate article = uow.GameUpdate.GameUpdateDetails(a => a.Id == id).FirstOrDefault();
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        public ActionResult Create()
        {
            ViewBag.GameId = new SelectList(uow.Game.GetAll(), "Id", "Name");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Title,Content,GameId,ArticleType")] GameUpdate article)
        {
            if (ModelState.IsValid)
            {
                article.Date = DateTime.Now;
                uow.GameUpdate.Insert(article);
                uow.Complete();
                return RedirectToAction("Index");
            }

            return View(article);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameUpdate article = uow.GameUpdate.GetById(id);
            ViewBag.GameId = new SelectList(uow.Game.GetAll(), "Id", "Name", article.GameId);

            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Title,Content,GameId,ArticleType")] GameUpdate article)
        {
            GameUpdate dbarticle = uow.GameUpdate.GetById(article.Id);
            dbarticle.GameId = article.GameId;
            dbarticle.Content = article.Content;
            dbarticle.ArticleType = article.ArticleType;
            dbarticle.Title = article.Title;


            if (ModelState.IsValid)
            {
                uow.GameUpdate.Update(dbarticle);
                uow.Complete();
                return RedirectToAction("Index");
            }
            return View(article);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameUpdate article = uow.GameUpdate.GetById(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GameUpdate article = uow.GameUpdate.GetById(id);
            uow.GameUpdate.Delete(article);
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