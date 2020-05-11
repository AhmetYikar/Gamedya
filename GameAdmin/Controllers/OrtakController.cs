using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameAdmin.Controllers
{
    public class OrtakController : Controller
    {
        // GET: Ortak
        public ActionResult Mesaj(string hataMesaji)
        {
            if (hataMesaji == null)
            {
                hataMesaji = "Hata mesajı";
            }
            ViewBag.Message = hataMesaji;
            return View();
        }
    }
}