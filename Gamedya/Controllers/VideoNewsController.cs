using DAL;
using Entites.Models.NewsModels;
using Gamedya.Models;
using ServiceLayer.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gamedya.Controllers
{
    public class VideoNewsController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: 4 VideoNews
        public ActionResult VideoNews()
        {
            IEnumerable<News> videoNews = uow.News.GetNewsWithVideos(a => a.NewsVideos.Count() > 0).ToList()
                                                        .OrderByDescending(a => a.Id).Take(4);

            if (videoNews != null && videoNews.Count() > 0)
            {
                IEnumerable<NewsViewWeb> videoNewsView = videoNews.Select(a => new NewsViewWeb
                {
                    Id = a.Id,
                    Title = a.Title,
                    Date = a.Date,
                    TinyImagePath = a.TinyImagePath,
                    VideoPath = a.NewsVideos.Count() > 0 ? a.NewsVideos.First().VideoPath : "",
                    Summary = a.Summary
                });
                return View(videoNewsView.ToList());
            }
            else
            {
                return null;
            }
        }

        public ActionResult AllVideoNews()
        {
            IEnumerable<News> videoNews = uow.News.GetNewsWithVideos(a => a.NewsVideos.Count() > 0);
            if (videoNews != null && videoNews.Count() > 0)
            {
                return PartialView(videoNews);
            }
            return null;
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