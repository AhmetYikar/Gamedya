using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gamedya.Controllers
{
    public class MoreNewsController : Controller
    {
        // GET: MoreNews
        public ActionResult GetMoreNews()
        {
          //  Random rnd = new Random();
            return View();
        }
    }
}