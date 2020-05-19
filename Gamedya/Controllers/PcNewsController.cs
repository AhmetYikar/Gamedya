using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Entites.Models.NewsModels;
using Gamedya.Models;
using ServiceLayer.Uow;
using PagedList;

namespace Gamedya.Controllers
{
    public class PcNewsController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: PcNews
        public ActionResult GetPcNews()
        {
            IEnumerable<News> pcNews = uow.News.GetNewsDetails(a => a.NewsPlatform.ToString() =="Pc").ToList()
                                                        .OrderByDescending(a => a.Id).Take(4);

            if (pcNews != null && pcNews.Count() > 0)
            {
                IEnumerable<NewsViewWeb> pcView = pcNews.Select(a => new NewsViewWeb
                {
                    Id = a.Id,
                    Title = a.Title,
                    Date = a.Date,
                    TinyImagePath = a.TinyImagePath,
                    BigImagePath = a.NewsImages.Count() > 0 ? a.NewsImages.First().ImagePath : "",
                    Summary = a.Summary
                });
                uow.Dispose();
                return View(pcView.ToList());

            }
            else
            {
                return null;
            }
        }

        public ActionResult AllPcNews(string ara, string siralama, string sonArananKelime, int? sayfaNo)
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


            var liste = PcNews();

            if (!string.IsNullOrWhiteSpace(ara))
            {
                liste = liste.Where(a => a.Title.Contains(ara));
            }



            int sayfaBuyuklugu = 10;
            int sayfaNumarasi = (sayfaNo ?? 1);


            return View(liste.ToPagedList(sayfaNumarasi, sayfaBuyuklugu));
        }

        private IEnumerable<NewsViewWeb> PcNews()
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                IEnumerable<News> pcnews = uow.News.Where(a => a.NewsPlatform == NewsPlatform.Pc);
                if (pcnews != null)
                {
                  
                    IEnumerable<NewsViewWeb> newsView = pcnews.Select(a => new NewsViewWeb
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Summary = a.Summary,
                        TinyImagePath = a.TinyImagePath,
                        Date = a.Date
                    }).OrderByDescending(a => a.Id);
                   
                    return newsView.ToList();
                }                      

               
                return null;
            }

            //IEnumerable<News> pcNews = uow.News.Where(a => a.NewsPlatform == NewsPlatform.Pc);
            //if (pcNews != null && pcNews.Count() > 0)
            //{
            //    return PartialView(pcNews);
            //}
            //return null;

        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
