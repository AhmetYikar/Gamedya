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
    public class MostCommentNewsController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: PopularNews
        public ActionResult MostComment()
        {
            IEnumerable<News> mostComment = uow.News.GetNewsDetails(a=>a.NewsComments.Count()>0).Take(6).ToList().OrderByDescending(a => a.NewsComments.Count());

            if (mostComment != null && mostComment.Count() > 0)
            {
                IEnumerable<NewsViewWeb> newsView = mostComment.Select(a => new NewsViewWeb
                {
                    Id = a.Id,
                    Title = a.Title,
                    TinyImagePath = a.TinyImagePath,
                    CommentCount=a.NewsComments.Count()
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