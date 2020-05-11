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
    public class MobilNewsController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: Mobil
        public ActionResult GetMobilNews()
        {
            IEnumerable<News> mobilNews = uow.News.GetNewsDetails(a => a.NewsPlatform == NewsPlatform.Mobile).ToList()
                                                                    .OrderByDescending(a => a.Id).Take(4);

            if (mobilNews != null && mobilNews.Count() > 0)
            {
                IEnumerable<NewsViewWeb> mobilView = mobilNews.Select(a => new NewsViewWeb
                {
                    Id = a.Id,
                    Title = a.Title,
                    Date = a.Date,
                    TinyImagePath = a.TinyImagePath,
                    BigImagePath = a.NewsImages.Count() > 0 ? a.NewsImages.First().ImagePath : "",
                    Summary = a.Summary
                });
                uow.Dispose();
                return View(mobilView.ToList());

            }
            else
            {
                return null;
            }
            
        }

        public ActionResult AllMobileNews()
        {
            IEnumerable<News> mobileNews = uow.News.Where(a => a.NewsPlatform.ToString() == "Mobile");
            if (mobileNews != null && mobileNews.Count() > 0)
            {
                return PartialView(mobileNews);
            }
            return null;
        }
    }
}