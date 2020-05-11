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
    public class ScienceNewsController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: ScienceNews
        public ActionResult GetScienceNews()
        {
            IEnumerable<News> scienceNews = uow.News.GetNewsDetails(a => a.NewsCategory.CategoryName=="Bilim" ).ToList()                                                       
                                                        .OrderByDescending(a => a.Id).Take(4);

            if (scienceNews != null && scienceNews.Count()>0)
            {
                IEnumerable<NewsViewWeb> scienceView = scienceNews.Select(a => new NewsViewWeb
                {
                    Id = a.Id,
                    Title = a.Title,
                    Date = a.Date,
                    TinyImagePath = a.TinyImagePath,
                    BigImagePath = a.NewsImages.Count() > 0 ? a.NewsImages.First().ImagePath : "",
                    Summary = a.Summary
                });
                uow.Dispose();
                return View(scienceView.ToList());
            }
            else
            {
                return null;
            }
        }


        public ActionResult AllScienceNews()
        {
            IEnumerable<News> scienceNews = uow.News.GetNewsWithCategory(a => a.NewsCategory.CategoryName == "Bilim");
            if (scienceNews != null && scienceNews.Count() > 0)
            {
                return PartialView(scienceNews);
            }
            return null;
        }
    }
}