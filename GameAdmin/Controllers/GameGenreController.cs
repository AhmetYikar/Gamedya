using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Entites.Models.GameModels;
using ServiceLayer.Uow;

namespace GameAdmin.Controllers
{
    [Authorize(Roles = "admin,yazar")]

    public class GameGenreController : Controller
    {
        #region index  
        public ActionResult Index()
        {

            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {


                IEnumerable<GameGenre> gameGenre = uow.GameGenre.GetAll().ToList();

                if (gameGenre == null)
                {
                    return null;
                }


                return View(gameGenre);
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
                GameGenre gameGenre = uow.GameGenre.GetById(id);

                if (gameGenre == null)
                {
                    return null;
                }


                return View(gameGenre);

            }
        }
        #endregion

        #region create  
        public ActionResult Create()
        {
            return View();
        }

        // POST: GameGenre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GenreName")] GameGenre gameGenre)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                if (ModelState.IsValid)
                {
                    uow.GameGenre.Insert(gameGenre);
                    uow.Complete();
                }

                if (gameGenre == null)
                {
                    return null;
                }

                return RedirectToAction("Index");

            }
        }
        #endregion

        #region edit  
        // GET: GameGenre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                GameGenre gameGenre = uow.GameGenre.GetById(id);

                if (gameGenre == null)
                {
                    return null;
                }

                return View(gameGenre);
            }
        }

        // POST: GameGenre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GenreName")] GameGenre gameGenre)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                if (ModelState.IsValid)
                {
                    uow.GameGenre.Update(gameGenre);
                    uow.Complete();
                    return RedirectToAction("Index");
                }

                return View(gameGenre);
            }
        }
        #endregion

        #region delete  
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                GameGenre gameGenre = uow.GameGenre.GetById(id);

                if (gameGenre == null)
                {
                    return null;
                }

                return View(gameGenre);
            }
        }

        // POST: GameGenre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                GameGenre gameGenre = uow.GameGenre.GetById(id);

                uow.GameGenre.Delete(gameGenre);
                uow.Complete();
                return RedirectToAction("Index");
            }

        }
        #endregion
    }
}
