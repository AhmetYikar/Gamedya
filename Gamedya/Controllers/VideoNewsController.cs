using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gamedya.Controllers
{
    public class VideoNewsController : Controller
    {
        // GET: VideoNews
        public ActionResult GetVideoNews()
        {
            return View();
        }
    }
}