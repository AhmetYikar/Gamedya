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
using Entites.Models.UserModels;
using Entities.Models;
using ServiceLayer.Uow;

namespace GameAdmin.Controllers
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

        // GET: GameReview/Create
        public ActionResult Create()
        {
            ViewBag.GameId = new SelectList(uow.Game.GetAll(), "Id", "Name");

            return View();
        }

        // POST: GameReview/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Title,Content,GameId,ArticleType")] GameUpdate gameReview)
        {

            if (ModelState.IsValid)
            {                            

                //game review kısmı
                gameReview.Date = DateTime.Now;
                uow.GameUpdate.Insert(gameReview);
                uow.Complete();

                //ilgili kişilere bildirim göndermek için
                Notification notification = new Notification();
                IEnumerable<NewsUser> users = uow.NewsUser.UserWithGames();
                var notifyUsers = new List<NewsUser>();
                string gameName = "";
                foreach (var user in users)
                {
                    IEnumerable<Game> games = user.MyFavouriteGames;
                    foreach (var game in games)
                    {
                        if (game.Id == gameReview.GameId)
                        {
                            notifyUsers.Add(user);
                            gameName = game.Name;
                            break;
                        }
                    }
                }

                if (notifyUsers.Count() > 0)
                {
                    notification.NewsUsers = notifyUsers.ToList();
                    notification.Content = gameName + " oyunuyla ilgili '" + gameReview.Title + "' yazısı yayında";
                    notification.Date = DateTime.Now;
                    notification.NModuleId = gameReview.Id;
                    uow.Notification.Insert(notification);
                    uow.Complete();
                }
                return RedirectToAction("Index");
            }

            return View(gameReview);
        }

        // GET: GameReview/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameUpdate gameReview = uow.GameUpdate.GetById(id);
            ViewBag.GameId = new SelectList(uow.Game.GetAll(), "Id", "Name", gameReview.GameId);

            if (gameReview == null)
            {
                return HttpNotFound();
            }
            return View(gameReview);
        }

        // POST: GameReview/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Title,Content,GameId,ArticleType")] GameUpdate gameReview)
        {
            GameUpdate dbGameReview = uow.GameUpdate.GetById(gameReview.Id);
            dbGameReview.GameId = gameReview.GameId;
            dbGameReview.Content = gameReview.Content;
            dbGameReview.ArticleType = gameReview.ArticleType;
            dbGameReview.Title = gameReview.Title;


            if (ModelState.IsValid)
            {
                uow.GameUpdate.Update(dbGameReview);
                uow.Complete();
                return RedirectToAction("Index");
            }
            return View(gameReview);
        }

        // GET: GameReview/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameUpdate gameReview = uow.GameUpdate.GetById(id);
            if (gameReview == null)
            {
                return HttpNotFound();
            }
            return View(gameReview);
        }

        // POST: GameReview/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GameUpdate gameReview = uow.GameUpdate.GetById(id);
            uow.GameUpdate.Delete(gameReview);
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
