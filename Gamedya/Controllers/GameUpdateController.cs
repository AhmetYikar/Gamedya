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

namespace Gamedya.Controllers
{
    public class GameUpdateController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: GameReview
        public ActionResult Index()
        {

            IEnumerable<GameUpdate> gameUpdates = uow.GameUpdate.GameUpdateDetails(a => a.ArticleType == ArticleType.Update).OrderByDescending(a => a.Id);
            if (gameUpdates == null)
            {
                return null;
            }

            return View(gameUpdates);

        }


        // GET: GameReview/Details/5
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
