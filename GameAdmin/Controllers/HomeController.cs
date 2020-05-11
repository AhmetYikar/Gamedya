using DAL;
using GameAdmin.Models;
using ServiceLayer.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());


        // GET: Dashboard
        public ActionResult Index()
        {
            DashboardIndexViewModel model = new DashboardIndexViewModel
            {
                NewsCount = uow.News.GetAll().Count(),
                BlogCount = uow.BlogPost.GetAll().Count(),
                ForumCount = uow.ForumPost.GetAll().Count(),
                GameCount = uow.Game.GetAll().Count(),
                NewsCategoryCount = uow.NewsCategory.GetAll().Count(),
                BlogCategoryCount = uow.BlogCategory.GetAll().Count(),
                ForumCategoryCount = uow.ForumCategory.GetAll().Count(),
                UserCount = uow.NewsUser.GetAll().Count()

            };
            if (ModelState.IsValid)
            {
                return View(model);
            }
            return null;
        }

        public ActionResult About()
        {


            return View();
        }

        public ActionResult Contact()
        {


            return View();
        }
    }

}