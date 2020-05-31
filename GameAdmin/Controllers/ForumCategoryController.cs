using DAL;
using Entites.Models.ForumModels;
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
    public class ForumCategoryController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());
        // GET: ForumCategory
        #region index

        public ActionResult Index()
        {

            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {


                IEnumerable<ForumCategory> forumcategories = uow.ForumCategory.GetAll().ToList();

                if (forumcategories == null)
                {
                    return null;
                }


                return View(forumcategories);
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
              

                ForumCategory forumcategory = uow.ForumCategory.GetById(id);

                if (forumcategory == null)
                {
                    return null;
                }
                ViewBag.ParentName = uow.ForumCategory.GetById(forumcategory.ParentId).CategoryName;

                return View(forumcategory);

            }
        }
        #endregion

        #region create

        // GET: NewsCategory/Create
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(uow.ForumCategory.GetAll(), "Id", "CategoryName", string.Empty);
            return View();
        }

        // POST: NewsCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "Id,CategoryName,Description,ParentId")] ForumCategory forumcategories)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                if (ModelState.IsValid)
                {
                    uow.ForumCategory.Insert(forumcategories);
                    uow.Complete();
                }

                if (forumcategories == null)
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
           
                ForumCategory forumcategories = uow.ForumCategory.GetById(id);

                if (forumcategories == null)
                {
                    return null;
                }
                ViewBag.ParentId = new SelectList(uow.ForumCategory.GetAll(), "Id", "CategoryName", string.Empty);
                return View(forumcategories);
            

        }

        // POST: NewsCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryName,Description,ParentId")] ForumCategory forumcategories)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                if (ModelState.IsValid)
                {
                    uow.ForumCategory.Update(forumcategories);
                    uow.Complete();
                    return RedirectToAction("Index");
                }

                return View(forumcategories);
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
                ForumCategory forumcategories = uow.ForumCategory.GetById(id);

                if (forumcategories == null)
                {
                    return null;
                }

                return View(forumcategories);
            }

        }

        // POST: NewsCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                ForumCategory forumcategories = uow.ForumCategory.GetById(id);
                ForumPost forumPost = uow.ForumPost.Where(x => x.ForumCategoryId == forumcategories.Id).FirstOrDefault();

                if (forumPost != null)
                {
                    TempData["Message"] = "Forum postu olduğundan silinemedi";
                    return View(forumcategories);
                }

                else
                {
                    uow.ForumCategory.Delete(forumcategories);
                    uow.Complete();
                    return RedirectToAction("Index");
                }

            }

        }
        #endregion

    }
}
