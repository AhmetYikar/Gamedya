using DAL;
using ServiceLayer.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameAdmin.Helper;
using GameAdmin.Models;
using System.Net;
using Entites.Models.BlogModels;
using System.IO;
using Microsoft.Azure.Amqp.Framing;
using System.Drawing;
using PagedList;
using System.Web.Security;

namespace GameAdmin.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {

        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());
        // GET: Blog
        #region Index

        public ActionResult Index(string ara, string siralama, string sonArananKelime, int? sayfaNo)
        {
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
                IEnumerable<BlogViewModel> blogpost = uow.BlogPost.BlogWithCommentsAndUsers().ToList()
                                                          .Select(a => new BlogViewModel { Id = a.Id, Title = a.Title, Date = a.Date,NewsUserId=a.NewsUserId,
                                                              BloggerName = a.NewsUser.FullName,TinyImagePath = a.TinyImagePath, CommentCount=a.BlogComments.Count()
                                                          })
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

        #region Details

        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var blogPost = uow.BlogPost.GetBlogDetails(a => a.Id == id).FirstOrDefault();

            if (blogPost == null)
            {
                return HttpNotFound();
            }

            return View(blogPost);

        }
        #endregion

        #region CreateBlog
        [HttpGet]

        public ActionResult Create()
        {
            ViewBag.BlogCategoryId = new SelectList(uow.BlogCategory.GetAll(), "Id", "CategoryName");

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "Id,Title,Summary,Content,BlogCategoryId,TinyImagePath")] BlogPost blogpost)
        {

            if (ModelState.IsValid == false)
            {
                ViewBag.BlogCategoryId = new SelectList(uow.BlogCategory.GetAll(), "Id", "CategoryName");
                return View(blogpost);
            }

           
            BlogImage blogImage = new BlogImage();
            blogImage.BlogPostId = blogpost.Id;
            blogImage.ImagePath = blogpost.TinyImagePath;

            try
            {
                uow.BlogImage.Insert(blogImage);
                blogpost.Date = DateTime.Now;
                blogpost.EditDate = DateTime.Now;
                blogpost.NewsUserId = User.GetUserId();
                uow.BlogPost.Insert(blogpost);
                uow.Complete();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.BlogCategoryId = new SelectList(uow.BlogCategory.GetAll(), "Id", "CategoryName");
                return View(blogpost);
            }

        }

        #endregion


        #region Edit

        public ActionResult Edit(int? id)
        {
            var blogpost = uow.BlogPost.GetById(id);

            if (id == null || blogpost.NewsUserId != User.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.BlogCategoryId = new SelectList(uow.BlogCategory.GetAll(), "Id", "CategoryName", blogpost.BlogCategoryId);
            return View(blogpost);

        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Summary,Content,BlogCategoryId,Date,ViewCount,EditDate,TinyImagePath")] BlogPost blogpost)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BlogCategoryId = new SelectList(uow.BlogCategory.GetAll(), "Id", "CategoryName", blogpost.BlogCategoryId);
                return View(blogpost);
            }
            var blogdb = uow.BlogPost.GetById(blogpost.Id);

            blogdb.Title = blogpost.Title;
            blogdb.TinyImagePath = blogpost.TinyImagePath;
            blogdb.Summary = blogpost.Summary;
            blogdb.Content = blogpost.Content;
            blogdb.BlogCategoryId = blogpost.BlogCategoryId;
            blogdb.EditDate = DateTime.Now;
            try
            {
                uow.BlogPost.Update(blogdb);
                uow.Complete();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.BlogCategoryId = new SelectList(uow.BlogCategory.GetAll(), "Id", "CategoryName", blogpost.BlogCategoryId);
                return View(blogpost);
            }
                          
        }

        #endregion


        #region Delete

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogpost = uow.BlogPost.GetById(id);
            ViewBag.BlogCategoryId = new SelectList(uow.BlogCategory.GetAll(), "Id", "CategoryName", blogpost.BlogCategoryId);
            if (blogpost == null)
            {
                return HttpNotFound();
            }
            return View(blogpost);

        }

        // POST: NewsCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                BlogPost blogpost = uow.BlogPost.GetById(id);
                
                    uow.BlogPost.Delete(blogpost);
                    uow.Complete();
                    return RedirectToAction("Index");               
            }

        }

        #endregion



    }

}
