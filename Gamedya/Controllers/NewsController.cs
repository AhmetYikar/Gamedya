using DAL;
using Entites.Models.NewsModels;
using Gamedya.Models;
using PagedList;
using ServiceLayer.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Gamedya.Controllers
{
    public class NewsController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());


        public ActionResult Index(string ara, string siralama, string sonArananKelime, int? sayfaNo)
        {
            ViewBag.NewsCategoryId = new SelectList(uow.NewsCategory.GetAll(), "Id", "CategoryName");

            ViewBag.SonSiralama1 = siralama;
            //ViewBag.AdaGoreSirala = String.IsNullOrEmpty(siralama) ? "ZdenAya" : string.Empty;
            //ViewBag.SoyadaGoreSirala = siralama == "SoyadAdanZye" ? "SoyadZdenAya" : "SoyadAdanZye";

            if (ara != null)
            {
                sayfaNo = 1;
            }
            else
            {
                ara = sonArananKelime;
            }



            ViewBag.SonArananKelime1 = ara;

            if (String.IsNullOrEmpty(siralama))
            {
                ViewBag.AdaGoreSirala2 = "ZdenAya";
            }
            else
            {
                ViewBag.AdaGoreSirala2 = string.Empty;
            }


            var liste = GetNews().ToList();

            //indexte her bir haberin yorum sayısı gelsin diye yapıldı
            foreach (var item in liste)
            {
                List<NewsComment> comments = uow.NewsComment.Where(a => a.NewsId == item.Id).ToList();
                if (comments != null && comments.Count() > 0)
                {
                    item.CommentCount = comments.Count();
                }
            }

            if (!string.IsNullOrWhiteSpace(ara))
            {
                liste = liste.Where(a => a.Title.ToLower().Contains(ara.ToLower())).ToList();
            }



            int sayfaBuyuklugu = 10;
            int sayfaNumarasi = (sayfaNo ?? 1);

            return View(liste.ToPagedList(sayfaNumarasi, sayfaBuyuklugu));
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

        //News viewCount ekle. (Okuyan sayısı)
        [HttpPost]
        public JsonResult NewsRead(int? id)
        {
            if (id != null)
            {
                if (GetCookie("newsRead", id) == null)
                {
                    CreateCookie("newsRead", id);
                    News news = uow.News.GetById(id);
                    if (news != null)
                    {
                        news.ViewCount++;
                        uow.News.Update(news);
                        uow.Complete();
                        return Json(JsonRequestBehavior.AllowGet);
                    }
                }
            }

            return Json(JsonRequestBehavior.AllowGet);
        }

        private void CreateCookie(string name, int? id)
        {
            name = name + id.ToString();
            HttpCookie cookieLike = new HttpCookie(name);
            cookieLike.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(cookieLike);
        }
        private string GetCookie(string name, int? id)
        {
            name = name + id.ToString();
            if (Request.Cookies.AllKeys.Contains(name))
            {
                return Request.Cookies[name].Value;
            }
            return null;
        }
    }
}