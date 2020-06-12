using DAL;
using Entites.Models.NewsModels;
using Gamedya.Models;
using PagedList;
using ServiceLayer.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gamedya.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());
        public ActionResult Index()
        {            
                return View();                      

        }

        private IEnumerable<NewsViewWeb> GetNews()
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                IEnumerable<NewsViewWeb> news = uow.News.GetAll().ToList()
                                                          .Select(a => new NewsViewWeb { Id = a.Id, Title = a.Title, Summary = a.Summary, Date = a.Date, TinyImagePath = a.TinyImagePath })
                                                          .OrderByDescending(a => a.Id);




                if (news != null)
                {
                    uow.Dispose();

                    return news;
                }
                else
                {
                    return null;
                }

            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



    }
}