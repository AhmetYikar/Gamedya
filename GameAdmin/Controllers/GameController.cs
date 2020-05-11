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
    [Authorize(Roles = "admin,yazar")]

    public class GameController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());
        #region index  

        public ActionResult Index()
        {

            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                IEnumerable<Game> games = uow.Game.GetAll().ToList().
                                                            Select(a => new Game { Id = a.Id, Name = a.Name, Publisher = a.Publisher, GamePlatform = a.GamePlatform, GameGenreId = a.GameGenreId })
                                                           .OrderByDescending(a => a.Id);
                if (games == null)
                {
                    return null;
                }
                return View(games);
            }

        }
        #endregion

        #region details  

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                Game games = uow.Game.GetById(id);

                if (games == null)
                {
                    return null;
                }

                return View(games);

            }
        }
        #endregion

        #region create  

        public ActionResult Create()
        {
            ViewBag.GameGenreId = new SelectList(uow.GameGenre.GetAll(), "Id", "GenreName");

            return View();
        }

        // POST: NewsCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Publisher,ReleaseDate,GamePlatform,GameGenreId")] Game games)
        {

            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                if (ModelState.IsValid)
                {
                    uow.Game.Insert(games);
                    uow.Complete();
                }

                if (games == null)
                {
                    return null;
                }

                return RedirectToAction("Index");

            }

        }

        #endregion

        #region edit  
        // GET: NewsCategory/Edit/5

        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Game game = uow.Game.GetById(id);
            ViewBag.GameGenreId = new SelectList(uow.GameGenre.GetAll(), "Id", "GenreName", game.GameGenreId);

            if (game == null)
            {
                return null;
            }

            return View(game);

        }

        // POST: NewsCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name, Publisher, ReleaseDate, GamePlatform,GameGenreId")] Game game)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                if (ModelState.IsValid)
                {
                    uow.Game.Update(game);
                    uow.Complete();
                    return RedirectToAction("Index");
                }

                return View(game);
            }
        }

        #endregion

        #region delete         

        // GET: NewsCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                Game game = uow.Game.GetById(id);

                if (game == null)
                {
                    return null;
                }

                return View(game);
            }

        }

        // POST: NewsCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                Game game = uow.Game.GetById(id);

                uow.Game.Delete(game);
                uow.Complete();
                return RedirectToAction("Index");
            }

        }
        #endregion
    }
}