using DAL;
using Entites.Models.BlogModels;
using ServiceLayer.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GameAdmin.Controllers
{
    [Authorize(Roles = "admin,yazar")]

    public class BlogCategoryController : Controller
    {
        #region İndex
        // GET: BlogCategory

        public ActionResult Index()
        {
            GameNewsDbContext db = new GameNewsDbContext();

            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {


                IEnumerable<BlogCategory> blogCategory = uow.BlogCategory.GetAll().ToList();

                if (blogCategory == null)
                {
                    return null;
                }


                return View(blogCategory);
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
                BlogCategory blogCategory = uow.BlogCategory.GetById(id);

                if (blogCategory == null)
                {
                    return null;
                }


                return View(blogCategory);

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
        public ActionResult Create([Bind(Include = "Id,CategoryName,Description,ParentId")] BlogCategory blogCategory)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                if (ModelState.IsValid)
                {
                    uow.BlogCategory.Insert(blogCategory);
                    uow.Complete();
                }

                if (blogCategory == null)
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
                BlogCategory blogCategory = uow.BlogCategory.GetById(id);

                if (blogCategory == null)
                {
                    return null;
                }

                return View(blogCategory);
            }

        }

        // POST: NewsCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "Id,CategoryName,Description,ParentId")] BlogCategory blogCategory)
        {

            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                if (ModelState.IsValid)
                {
                    uow.BlogCategory.Update(blogCategory);
                    uow.Complete();
                    return RedirectToAction("Index");
                }

                return View(blogCategory);
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
                BlogCategory blogCategory = uow.BlogCategory.GetById(id);

                if (blogCategory == null)
                {
                    return null;
                }


                return View(blogCategory);
            }

        }

        // POST: NewsCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                BlogCategory blogCategory = uow.BlogCategory.GetById(id);
                BlogPost blogPost = uow.BlogPost.Where(x => x.BlogCategoryId == blogCategory.Id).FirstOrDefault();

                if (blogPost != null)
                {
                    TempData["Message"] = "Blog postu olduğundan silinemedi";
                    return View(blogCategory);
                }

                else
                {
                    uow.BlogCategory.Delete(blogCategory);
                    uow.Complete();
                    return RedirectToAction("Index");
                }
            }

        }
        #endregion

    }
}