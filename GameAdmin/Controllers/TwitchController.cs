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
    public class TwitchController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: TwitchYoutube
        public ActionResult Index()
        {
            IEnumerable<TwitchYoutube> twitch = uow.TwitchYoutube.GetAll().Where(a => a.VideoPlatform == VideoPlatform.Twitch);
            return View(twitch);
        }

        // GET: TwitchYoutube/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            var twitch = uow.TwitchYoutube.GetTwitchYoutubeDetails(a => a.Id == id)
                                                                    .FirstOrDefault();
          
            if (twitch == null)
            {
                return HttpNotFound();
            }
            return View(twitch);
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
        public ActionResult Create([Bind(Include = "Id,Title,VideoPath,CoverImage,ViewCount,IsActive,VideoPlatform")] TwitchYoutube twitch)
        {
            if (ModelState.IsValid)
            {
                twitch.Date = DateTime.Now;
                uow.TwitchYoutube.Insert(twitch);
                uow.Complete();
                return RedirectToAction("Index");
            }

            return View(twitch);
        }

        // GET: TwitchYoutube/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }        
            var twitch = uow.TwitchYoutube.GetById(id);

            if (twitch == null)
            {
                return HttpNotFound();
            }
            return View(twitch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,VideoPath,CoverImage,Date,ViewCount,IsActive,VideoPlatform")] TwitchYoutube twitch)
        {
            
            if (ModelState.IsValid)
            {
                var twitchdb = uow.TwitchYoutube.GetById(twitch.Id);
                twitchdb.VideoPlatform = twitch.VideoPlatform;
                twitchdb.Title = twitch.Title;
                twitchdb.VideoPath = twitch.VideoPath;
                twitchdb.CoverImage = twitch.CoverImage;
                twitchdb.IsActive = twitch.IsActive;
                uow.TwitchYoutube.Update(twitchdb);
                uow.Complete();
                return RedirectToAction("Index");
            }
            return View(twitch);
        }

        // GET: TwitchYoutube/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TwitchYoutube twitch = uow.TwitchYoutube.GetById(id);
           
            if (twitch == null)
            {
                return HttpNotFound();
            }
            return View(twitch);
        }

        // POST: TwitchYoutube/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                TwitchYoutube twitch = uow.TwitchYoutube.GetById(id);

                uow.TwitchYoutube.Delete(twitch);
                uow.Complete();
                return RedirectToAction("Index");
            }

        }

       
    }
}
