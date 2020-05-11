using DAL;
using ServiceLayer.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Gamedya.Controllers
{
    public class NewsDetailsController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());
        // GET: NewsDetails
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = uow.News.GetNewsDetails(a => a.Id == id).FirstOrDefault();


            if (news == null)
            {
                return HttpNotFound();
            }


            return View(news);
        }
    }
}