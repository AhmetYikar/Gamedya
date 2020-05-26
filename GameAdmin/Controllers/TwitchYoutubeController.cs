using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Entities.Models.TwitchYoutube;
using ServiceLayer.Uow;

namespace GameAdmin.Controllers
{
    public class TwitchYoutubeController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: TwitchYoutube
        public ActionResult Index()
        {
            return View(uow.TwitchYoutube.GetAll());
        }

        // GET: TwitchYoutube/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            var twitchYoutube = uow.TwitchYoutube.GetTwitchYoutubeDetails(a => a.Id == id).FirstOrDefault();
          
            if (twitchYoutube == null)
            {
                return HttpNotFound();
            }
            return View(twitchYoutube);
        }

        // GET: TwitchYoutube/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: TwitchYoutube/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,VideoPath,CoverImage,Date,ViewCount,IsActive,VideoPlatform")] TwitchYoutube twitchYoutube)
        {
            if (ModelState.IsValid)
            {
                uow.TwitchYoutube.Insert(twitchYoutube);
                uow.Complete();
                return RedirectToAction("Index");
            }

            return View(twitchYoutube);
        }

        // GET: TwitchYoutube/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }        
            var twitchYoutube = uow.TwitchYoutube.GetById(id);

            if (twitchYoutube == null)
            {
                return HttpNotFound();
            }
            return View(twitchYoutube);
        }

        // POST: TwitchYoutube/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,VideoPath,CoverImage,Date,ViewCount,IsActive,VideoPlatform")] TwitchYoutube twitchYoutube)
        {

            var twitchYoutubedb = uow.TwitchYoutube.GetById(twitchYoutube.Id);
            twitchYoutubedb.VideoPlatform = twitchYoutubedb.VideoPlatform;
            twitchYoutubedb.Title = twitchYoutubedb.Title;
            twitchYoutubedb.VideoPath = twitchYoutubedb.VideoPath;
            twitchYoutubedb.CoverImage = twitchYoutubedb.CoverImage;
            twitchYoutubedb.IsActive = twitchYoutubedb.IsActive;

            if (ModelState.IsValid)
            {
                uow.TwitchYoutube.Update(twitchYoutube);
                uow.Complete();
                return RedirectToAction("Index");
            }
            return View(twitchYoutube);
        }

        // GET: TwitchYoutube/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TwitchYoutube twitchYoutube = uow.TwitchYoutube.GetById(id);
           
            if (twitchYoutube == null)
            {
                return HttpNotFound();
            }
            return View(twitchYoutube);
        }

        // POST: TwitchYoutube/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                TwitchYoutube twitchYoutube = uow.TwitchYoutube.GetById(id);

                uow.TwitchYoutube.Delete(twitchYoutube);
                uow.Complete();
                return RedirectToAction("Index");
            }

        }

       
    }
}
