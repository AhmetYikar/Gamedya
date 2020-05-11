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
    public class LatestNewsController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: LatestAndPopuler
        public ActionResult GetLatestNews()
        {

            IEnumerable<News> latestNews = uow.News.GetAll().Take(6).ToList().OrderByDescending(a => a.Id);

            if (latestNews != null && latestNews.Count() > 0)
            {
                IEnumerable<NewsViewWeb> newsView = latestNews.Select(a => new NewsViewWeb
                {
                    Id = a.Id,
                    Title = a.Title,                   
                    TinyImagePath = a.TinyImagePath
                });
                uow.Dispose();
                return PartialView(newsView);

            }
            else
            {
                return null;
            }
        }

        public ActionResult GetPopularNews()
        {
            IEnumerable<News> latestNews = uow.News.GetAll().Take(6).ToList().OrderByDescending(a => a.ViewCount);

            if (latestNews != null && latestNews.Count() > 0)
            {
                IEnumerable<NewsViewWeb> newsView = latestNews.Select(a => new NewsViewWeb
                {
                    Id = a.Id,
                    Title = a.Title,
                    TinyImagePath = a.TinyImagePath
                });
                uow.Dispose();
                return PartialView(newsView);

            }
            else
            {
                return null;
            }
        }
    }
}