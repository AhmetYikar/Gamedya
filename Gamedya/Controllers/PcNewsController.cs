using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Entites.Models.NewsModels;
using Gamedya.Models;
using ServiceLayer.Uow;

namespace Gamedya.Controllers
{
    public class PcNewsController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: PcNews
        public ActionResult GetPcNews()
        {
            IEnumerable<News> pcNews = uow.News.GetNewsDetails(a => a.NewsPlatform.ToString() =="Pc").ToList()
                                                        .OrderByDescending(a => a.Id).Take(4);

            if (pcNews != null && pcNews.Count() > 0)
            {
                IEnumerable<NewsViewWeb> pcView = pcNews.Select(a => new NewsViewWeb
                {
                    Id = a.Id,
                    Title = a.Title,
                    Date = a.Date,
                    TinyImagePath = a.TinyImagePath,
                    BigImagePath = a.NewsImages.Count() > 0 ? a.NewsImages.First().ImagePath : "",
                    Summary = a.Summary
                });
                uow.Dispose();
                return View(pcView.ToList());

            }
            else
            {
                return null;
            }
        }

        public ActionResult AllPcNews()
        {
            IEnumerable<News> pcNews = uow.News.Where(a => a.NewsPlatform == NewsPlatform.Pc);
            if (pcNews != null && pcNews.Count() > 0)
            {
                return PartialView(pcNews);
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
