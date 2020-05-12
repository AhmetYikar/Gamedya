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
    public class GeneralArticleController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: GameReview
        public ActionResult Index()
        {

            IEnumerable<GameUpdate> articles = uow.GameUpdate.GameUpdateDetails(a => a.ArticleType == ArticleType.General).OrderByDescending(a => a.Id);
            if (articles == null)
            {
                return null;
            }

            return View(articles);

        }


        // GET: GameReview/Details/5
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
