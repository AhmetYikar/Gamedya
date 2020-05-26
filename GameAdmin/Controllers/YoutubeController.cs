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
    public class YoutubeController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: TwitchYoutube
        public ActionResult Index()
        {
            IEnumerable<TwitchYoutube> youtube = uow.TwitchYoutube.GetAll().Where(a => a.VideoPlatform == VideoPlatform.Youtube);
            return View(youtube);
        }

        // GET: TwitchYoutube/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            var youtube = uow.TwitchYoutube.GetTwitchYoutubeDetails(a => a.Id == id)
                                                                    .FirstOrDefault();
          
            if (youtube == null)
            {
                return HttpNotFound();
            }
            return View(youtube);
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
        public ActionResult Create([Bind(Include = "Id,Title,VideoPath,CoverImage,Date,ViewCount,IsActive,VideoPlatform")] TwitchYoutube youtube)
        {
            if (ModelState.IsValid)
            {
                youtube.Date = DateTime.Now;
                uow.TwitchYoutube.Insert(youtube);
                uow.Complete();
                return RedirectToAction("Index");
            }

            return View(youtube);
        }

        // GET: TwitchYoutube/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }        
            var youtube = uow.TwitchYoutube.GetById(id);

            if (youtube == null)
            {
                return HttpNotFound();
            }
            return View(youtube);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,VideoPath,CoverImage,Date,ViewCount,IsActive,VideoPlatform")] TwitchYoutube youtube)
        {
            
            if (ModelState.IsValid)
            {
                var youtubedb = uow.TwitchYoutube.GetById(youtube.Id);
                youtubedb.VideoPlatform = youtube.VideoPlatform;
                youtubedb.Title = youtube.Title;
                youtubedb.VideoPath = youtube.VideoPath;
                youtubedb.CoverImage = youtube.CoverImage;
                youtubedb.IsActive = youtube.IsActive;
                uow.TwitchYoutube.Update(youtubedb);
                uow.Complete();
                return RedirectToAction("Index");
            }
            return View(youtube);
        }

        // GET: TwitchYoutube/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TwitchYoutube youtube = uow.TwitchYoutube.GetById(id);
           
            if (youtube == null)
            {
                return HttpNotFound();
            }
            return View(youtube);
        }

        // POST: TwitchYoutube/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                TwitchYoutube youtube = uow.TwitchYoutube.GetById(id);

                uow.TwitchYoutube.Delete(youtube);
                uow.Complete();
                return RedirectToAction("Index");
            }

        }

       
    }
}
