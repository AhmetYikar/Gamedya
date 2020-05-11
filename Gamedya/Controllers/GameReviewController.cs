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
    public class GameReviewController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: GameReview
        public ActionResult Index()
        {

            IEnumerable<GameUpdate> gameReviews = uow.GameUpdate.GameUpdateDetails(a => a.ArticleType == ArticleType.Review).OrderByDescending(a => a.Id);
            if (gameReviews == null)
            {
                return null;
            }

            return View(gameReviews);

        }


        // GET: GameReview/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameUpdate gameReview = uow.GameUpdate.GameUpdateDetails(a => a.Id == id).FirstOrDefault();
            if (gameReview == null)
            {
                return HttpNotFound();
            }
            return View(gameReview);
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
