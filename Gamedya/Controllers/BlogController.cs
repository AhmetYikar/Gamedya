﻿using DAL;
using Entites.Models.BlogModels;
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
    public class BlogController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());
        #region Index
        public ActionResult Index(string ara, string siralama, string sonArananKelime, int? sayfaNo)
        {
            ViewBag.BlogCategoryId = new SelectList(uow.BlogCategory.GetAll(), "Id", "CategoryName");

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


            var liste = GetBlog().ToList();

            //indexte her bir blogun yorum sayısı gelsin diye yapıldı
            foreach (var item in liste)
            {
                List<BlogComment> comments = uow.BlogComment.Where(a => a.BlogPostId == item.Id).ToList();
                if (comments != null && comments.Count() > 0)
                {
                    item.CommentCount = comments.Count();
                }
            }

            if (!string.IsNullOrWhiteSpace(ara))
            {
                liste = liste.Where(a => a.Title.Contains(ara)).ToList();
            }
            int sayfaBuyuklugu = 10;
            int sayfaNumarasi = (sayfaNo ?? 1);

            return View(liste.ToPagedList(sayfaNumarasi, sayfaBuyuklugu));
        }

        private IEnumerable<BlogViewModel> GetBlog()
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                IEnumerable<BlogViewModel> blogpost = uow.BlogPost.GetAll().ToList()
                                                          .Select(a => new BlogViewModel { Id = a.Id, Title = a.Title, Date = a.Date, NewsUserId = a.NewsUserId, TinyImagePath = a.TinyImagePath })
                                                          .OrderByDescending(a => a.Id);

                if (blogpost != null)
                {
                    uow.Dispose();
                    return blogpost;
                }
                return null;
            }
        }

        #endregion


    }
}