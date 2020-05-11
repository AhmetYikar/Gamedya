using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Entites.Models.GameModels;
using Entites.Models.NewsModels;
using ServiceLayer.Uow;

namespace GameAdmin.Controllers
{
    [Authorize(Roles = "admin,yazar")]
    public class NewsCategoryController : Controller
    {
        #region index  
        public ActionResult Index()
        {

            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {


                IEnumerable<NewsCategory> newsCategory = uow.NewsCategory.GetAll().ToList();

                if (newsCategory == null)
                {
                    return null;
                }


                return View(newsCategory);
            }

        }
        #endregion

        #region details  
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                NewsCategory newsCategories = uow.NewsCategory.GetById(id);

                if (newsCategories == null)
                {
                    return null;
                }


                return View(newsCategories);

            }
        }
        #endregion

        #region create  
        // GET: NewsCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryName,Description,ParentId")] NewsCategory newsCategory)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                if (ModelState.IsValid)
                {
                    uow.NewsCategory.Insert(newsCategory);
                    uow.Complete();
                }

                if (newsCategory == null)
                {
                    return null;
                }

                return RedirectToAction("Index");

            }

        }
        #endregion

        #region edit  
        // GET: NewsCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                NewsCategory newsCategory = uow.NewsCategory.GetById(id);

                if (newsCategory == null)
                {
                    return null;
                }

                return View(newsCategory);
            }

        }

        // POST: NewsCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryName,Description,ParentId")] NewsCategory newsCategory)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                if (ModelState.IsValid)
                {
                    uow.NewsCategory.Update(newsCategory);
                    uow.Complete();
                    return RedirectToAction("Index");
                }

                return View(newsCategory);
            }
        }
        #endregion

        #region delete  
        // GET: NewsCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                NewsCategory newsCategory = uow.NewsCategory.GetById(id);

                if (newsCategory == null)
                {
                    return null;
                }

                return View(newsCategory);
            }

        }

        // POST: NewsCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                NewsCategory newsCategory = uow.NewsCategory.GetById(id);
                News news = uow.News.Where(x => x.NewsCategoryId == newsCategory.Id).FirstOrDefault();


                if (news != null)
                {
                    TempData["Message"] = "Haberolduğundan silinemedi";
                    return View(newsCategory);
                }


                else
                {
                    uow.NewsCategory.Delete(newsCategory);
                    uow.Complete();
                    return RedirectToAction("Index");
                }


            }

        }
        #endregion

    }
}
