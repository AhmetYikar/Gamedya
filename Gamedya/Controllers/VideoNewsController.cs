﻿using DAL;
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
    public class VideoNewsController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

        // GET: 4 VideoNews
        public ActionResult VideoNews()
        {
            IEnumerable<News> videoNews = uow.News.GetNewsWithVideos(a => a.NewsPart == NewsPart.VideoNews).ToList()
                                                                    .OrderByDescending(a => a.Id).Take(4);

            if (videoNews != null && videoNews.Count() > 0)
            {
                IEnumerable<NewsViewWeb> videoNewsView = videoNews.Select(a => new NewsViewWeb
                {
                    Id = a.Id,
                    Title = a.Title,
                    Date = a.Date,
                    TinyImagePath = a.TinyImagePath,
                    VideoPath = a.NewsVideos.Count() > 0 ? a.NewsVideos.First().VideoPath : "",
                    Summary = a.Summary
                });
                return View(videoNewsView.ToList());
            }
            else
            {
                return null;
            }
        }

        public ActionResult AllVideoNews(string ara, string siralama, string sonArananKelime, int? sayfaNo)
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


            var liste = GetVideoNews();

            if (!string.IsNullOrWhiteSpace(ara))
            {
                liste = liste.Where(a => a.Title.Contains(ara));
            }



            int sayfaBuyuklugu = 10;
            int sayfaNumarasi = (sayfaNo ?? 1);


            return View(liste.ToPagedList(sayfaNumarasi, sayfaBuyuklugu));
        }

        private IEnumerable<NewsViewWeb> GetVideoNews()
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                IEnumerable<News> getvideonews = uow.News.Where(a => a.NewsPart == NewsPart.VideoNews);
                if (getvideonews != null)
                {

                    IEnumerable<NewsViewWeb> newsView = getvideonews.Select(a => new NewsViewWeb
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

            //IEnumerable<News> videoNews = uow.News.GetNewsWithVideos(a => a.NewsVideos.Count() > 0);
            //if (videoNews != null && videoNews.Count() > 0)
            //{
            //    return PartialView(videoNews);
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