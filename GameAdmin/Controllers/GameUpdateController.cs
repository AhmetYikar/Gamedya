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
    public class GameUpdateController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: GameUpdate
        public ActionResult Index()
        {
            IEnumerable<GameUpdate> gameUpdates = uow.GameUpdate.GameUpdateDetails(a => a.ArticleType == ArticleType.Update).OrderByDescending(a => a.Id);
            if (gameUpdates == null)
            {
                return null;
            }

            return View(gameUpdates);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameUpdate gameUpdate = uow.GameUpdate.GameUpdateDetails(a => a.Id == id).FirstOrDefault();
            if (gameUpdate == null)
            {
                return HttpNotFound();
            }
            return View(gameUpdate);
        }

        public ActionResult Create()
        {
            ViewBag.GameId = new SelectList(uow.Game.GetAll(), "Id", "Name");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Title,Content,GameId,ArticleType")] GameUpdate gameUpdate)
        {
            if (ModelState.IsValid)
            {
                gameUpdate.Date = DateTime.Now;
                uow.GameUpdate.Insert(gameUpdate);
                uow.Complete();
                return RedirectToAction("Index");
            }

            return View(gameUpdate);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameUpdate gameUpdate = uow.GameUpdate.GetById(id);
            ViewBag.GameId = new SelectList(uow.Game.GetAll(), "Id", "Name", gameUpdate.GameId);

            if (gameUpdate == null)
            {
                return HttpNotFound();
            }
            return View(gameUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Title,Content,GameId,ArticleType")] GameUpdate gameUpdate)
        {
            GameUpdate dbgameUpdate = uow.GameUpdate.GetById(gameUpdate.Id);
            dbgameUpdate.GameId = gameUpdate.GameId;
            dbgameUpdate.Content = gameUpdate.Content;
            dbgameUpdate.ArticleType = gameUpdate.ArticleType;
            dbgameUpdate.Title = gameUpdate.Title;


            if (ModelState.IsValid)
            {
                uow.GameUpdate.Update(dbgameUpdate);
                uow.Complete();
                return RedirectToAction("Index");
            }
            return View(gameUpdate);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameUpdate gameUpdate = uow.GameUpdate.GetById(id);
            if (gameUpdate == null)
            {
                return HttpNotFound();
            }
            return View(gameUpdate);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GameUpdate gameUpdate = uow.GameUpdate.GetById(id);
            uow.GameUpdate.Delete(gameUpdate);
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