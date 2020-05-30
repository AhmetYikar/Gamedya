using DAL;
using Entites.Models.ForumModels;
using GameAdmin.Helper;
using GameAdmin.Models;
using PagedList;
using ServiceLayer.Uow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GameAdmin.Controllers
{
    [Authorize(Roles = "admin,yazar")]

    public class ForumController : Controller
    {


        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());
        // GET: Forum
        #region Index        

        public ActionResult Index(string ara, string siralama, string sonArananKelime, int? sayfaNo)
        {
            ViewBag.ForumCategoryId = new SelectList(uow.ForumCategory.GetAll(), "Id", "CategoryName");

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


            var liste = GetForum();

            if (!string.IsNullOrWhiteSpace(ara))
            {
                liste = liste.Where(a => a.ForumTitle.Contains(ara));
            }



            int sayfaBuyuklugu = 10;
            int sayfaNumarasi = (sayfaNo ?? 1);


            return View(liste.ToPagedList(sayfaNumarasi, sayfaBuyuklugu));
        }

        private IEnumerable<ForumViewModel> GetForum()
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                IEnumerable<ForumViewModel> forums = uow.ForumPost.GetAll().ToList()
                                                          .Select(a => new ForumViewModel { Id = a.Id, ForumTitle = a.ForumTitle, Date = a.Date, NewsUserId = a.NewsUserId })
                                                          .OrderByDescending(a => a.Id);

                if (forums != null)
                {
                    uow.Dispose();
                    return forums;
                }
                return null;
            }
        }

        #endregion

        #region Details


        [Route("{id}-{title}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var forumpost = uow.ForumPost.GetForumDetail(a => a.Id == id).FirstOrDefault();

            if (forumpost == null)
            {
                return HttpNotFound();
            }


            return View(forumpost);

        }



        #endregion

        #region CreateForum
        [HttpGet]

        public ActionResult Create()
        {
            ViewBag.ForumCategoryId = new SelectList(uow.ForumCategory.GetAll(), "Id", "CategoryName",string.Empty);
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ForumTitle,Content,ForumCategoryId,Date,IsActive")] ForumPost forumPost)
        {
          
            if (ModelState.IsValid)
            {
                uow.ForumPost.Insert(new ForumPost
                {
                    Date = DateTime.Now,                                     
                    ForumTitle = forumPost.ForumTitle,                   
                    Content = forumPost.Content,
                    ForumCategoryId = forumPost.ForumCategoryId,
                    IsActive = forumPost.IsActive,
                  
                });
                uow.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.ForumCategoryId = new SelectList(uow.ForumCategory.GetAll(), "Id", "CategoryName");
            return View(forumPost);
        }
        #endregion

        #region Edit

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var forumpost = uow.ForumPost.GetById(id);
            ViewBag.ForumCategoryId = new SelectList(uow.ForumCategory.GetAll(), "Id", "CategoryName", forumpost.ForumCategoryId);
            return View(forumpost);
        }

        // POST: NewsCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ForumTitle,Content,ForumCategoryId,IsActive,IsOk")] ForumPost forumpost)
        {

            if (ModelState.IsValid)
            {
                var forumpostdb = uow.ForumPost.GetById(forumpost.Id);

                if (forumpostdb == null)
                {
                    ViewBag.ForumCategoryId = new SelectList(uow.ForumCategory.GetAll(), "Id", "CategoryName", forumpost.ForumCategoryId);
                    return View(forumpost);
                }
              
                forumpostdb.ForumTitle = forumpost.ForumTitle;
                forumpostdb.Content = forumpost.Content;
                forumpostdb.ForumCategoryId = forumpost.ForumCategoryId;
                forumpostdb.IsActive = forumpost.IsActive;
                forumpostdb.IsOk = forumpost.IsOk;
                uow.ForumPost.Update(forumpostdb);
                uow.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.ForumCategoryId = new SelectList(uow.ForumCategory.GetAll(), "Id", "CategoryName", forumpost.ForumCategoryId);
            return View(forumpost);
        }

        #endregion

        #region DeleteForum

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumPost forumpost = uow.ForumPost.GetById(id);
            ViewBag.ForumCategoryId = new SelectList(uow.ForumCategory.GetAll(), "Id", "CategoryName", forumpost.ForumCategoryId);

            return View(forumpost);

        }
        // POST: NewsCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                ForumPost ForumPost = uow.ForumPost.GetById(id);

                uow.ForumPost.Delete(ForumPost);
                uow.Complete();
                return RedirectToAction("Index");
            }

        }
    }

    #endregion

}

