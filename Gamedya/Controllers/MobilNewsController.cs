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
    public class MobilNewsController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: Mobil
        public ActionResult GetMobilNews()
        {
            IEnumerable<News> mobilNews = uow.News.GetNewsDetails(a => a.NewsPlatform == NewsPlatform.Mobile).ToList()
                                                                    .OrderByDescending(a => a.Id).Take(4);

            if (mobilNews != null && mobilNews.Count() > 0)
            {
                IEnumerable<NewsViewWeb> mobilView = mobilNews.Select(a => new NewsViewWeb
                {
                    Id = a.Id,
                    Title = a.Title,
                    Date = a.Date,
                    TinyImagePath = a.TinyImagePath,
                    BigImagePath = a.NewsImages.Count() > 0 ? a.NewsImages.First().ImagePath : "",
                    Summary = a.Summary
                });
                uow.Dispose();
                return View(mobilView.ToList());

            }
            else
            {
                return null;
            }
            
        }

        public ActionResult AllMobileNews(string ara, string siralama, string sonArananKelime, int? sayfaNo)
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


            var liste = MobileNews();

            if (!string.IsNullOrWhiteSpace(ara))
            {
                liste = liste.Where(a => a.Title.Contains(ara));
            }



            int sayfaBuyuklugu = 10;
            int sayfaNumarasi = (sayfaNo ?? 1);


            return View(liste.ToPagedList(sayfaNumarasi, sayfaBuyuklugu));
        }

        private IEnumerable<NewsViewWeb> MobileNews()
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                IEnumerable<News> mobilnews = uow.News.Where(a => a.NewsPlatform == NewsPlatform.Mobile);
                if (mobilnews != null)
                {

                    IEnumerable<NewsViewWeb> newsView = mobilnews.Select(a => new NewsViewWeb
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

            //IEnumerable<News> mobileNews = uow.News.Where(a => a.NewsPlatform.ToString() == "Mobile");
            //if (mobileNews != null && mobileNews.Count() > 0)
            //{
            //    return PartialView(mobileNews);
            //}
            //return null;
        }
    }
}