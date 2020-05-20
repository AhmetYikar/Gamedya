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
    public class ScienceNewsController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: ScienceNews
        public ActionResult GetScienceNews()
        {
            IEnumerable<News> scienceNews = uow.News.GetNewsDetails(a => a.NewsPart == NewsPart.Science).ToList()
                                                                    .OrderByDescending(a => a.Id).Take(4);

            if (scienceNews != null && scienceNews.Count()>0)
            {
                IEnumerable<NewsViewWeb> scienceView = scienceNews.Select(a => new NewsViewWeb
                {
                    Id = a.Id,
                    Title = a.Title,
                    Date = a.Date,
                    TinyImagePath = a.TinyImagePath,
                    BigImagePath = a.NewsImages.Count() > 0 ? a.NewsImages.First().ImagePath : "",
                    Summary = a.Summary
                });
                uow.Dispose();
                return View(scienceView.ToList());
            }
            else
            {
                return null;
            }
        }


        public ActionResult AllScienceNews(string ara, string siralama, string sonArananKelime, int? sayfaNo)
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


            var liste = ScienceNews();

            if (!string.IsNullOrWhiteSpace(ara))
            {
                liste = liste.Where(a => a.Title.Contains(ara));
            }



            int sayfaBuyuklugu = 10;
            int sayfaNumarasi = (sayfaNo ?? 1);


            return View(liste.ToPagedList(sayfaNumarasi, sayfaBuyuklugu));
        }

        private IEnumerable<NewsViewWeb> ScienceNews()
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                IEnumerable<News> sciencenews = uow.News.Where(a => a.NewsPart == NewsPart.Science);
                if (sciencenews != null)
                {

                    IEnumerable<NewsViewWeb> newsView = sciencenews.Select(a => new NewsViewWeb
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

            //IEnumerable<News> scienceNews = uow.News.GetNewsWithCategory(a => a.NewsCategory.CategoryName == "Bilim");
            //if (scienceNews != null && scienceNews.Count() > 0)
            //{
            //    return PartialView(scienceNews);
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