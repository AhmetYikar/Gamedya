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

        #region Details


        [Route("{id}-{title}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var blogpost = uow.BlogPost.GetBlogDetails(a => a.Id == id).FirstOrDefault();

            if (blogpost == null)
            {
                return HttpNotFound();
            }

            return View(blogpost);

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

        public ActionResult Create([Bind(Include = "Id,Title,Summary,Content,BlogCategoryId,Date,ViewCount")] BlogPost blogpost, HttpPostedFileBase file)
        {


            string fileName = string.Empty;
            string extension = string.Empty;


            if (file != null && file.ContentLength > 0 && file.ContentLength < 2 * 1024 * 1024)
            {
                extension = Path.GetExtension(file.FileName);

                if (extension.Contains("pdf") || extension.Contains("doc") || extension.Contains("docx"))
                {

                    return RedirectToAction("index");
                }
                else
                {

                }

                fileName = Guid.NewGuid() + ".png";

                var path = Path.Combine(Server.MapPath("/Content/BlogImages/"), fileName.Replace(".png", "-thumb.png"));

                Image image = Image.FromStream(file.InputStream, true);

                int imgWidth = 110;
                int imgHeight = 95;

                Image thumb = image.GetThumbnailImage(imgWidth, imgHeight, () => false, IntPtr.Zero);
                thumb.Save(path);
                blogpost.TinyImagePath = "/Content/BlogImages/" + fileName.Replace(".png", "-thumb.png");
            }



            if (ModelState.IsValid)
            {

                uow.BlogPost.Insert(new BlogPost
                {
                    Title = blogpost.Title,
                    Summary = blogpost.Summary,
                    Content = blogpost.Content,
                    BlogCategoryId = blogpost.BlogCategoryId,
                    Date = DateTime.Now,
                    EditDate = DateTime.Now,
                    ViewCount = blogpost.ViewCount,
                    TinyImagePath = blogpost.TinyImagePath,
                    NewsUserId = User.GetUserId()
                });

                uow.Complete();

                return RedirectToAction("Index");

            }
            ViewBag.BlogCategoryId = new SelectList(uow.BlogCategory.GetAll(), "Id", "CategoryName");
            return View(blogpost);

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

        // POST: NewsCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "Id,Title,Summary,Content,BlogCategoryId,Date,ViewCount,EditDate")] BlogPost blogpost, HttpPostedFileBase file)
        {
            var blogdb = uow.BlogPost.GetById(blogpost.Id);




            string fileName = string.Empty;
            string extension = string.Empty;

            if (file != null && file.ContentLength > 0 && file.ContentLength < 2 * 1024 * 1024)
            {
                extension = Path.GetExtension(file.FileName);

                if (extension.Contains("pdf") || extension.Contains("doc") || extension.Contains("docx"))
                {

                    return RedirectToAction("index");
                }
                else
                {

                }
                fileName = Guid.NewGuid() + ".png";

                var path = Path.Combine(Server.MapPath("/Content/BlogImages/"), fileName.Replace(".png", "-thumb.png"));

                Image image = Image.FromStream(file.InputStream, true);

                int imgWidth = 110;
                int imgHeight = 95;

                Image thumb = image.GetThumbnailImage(imgWidth, imgHeight, () => false, IntPtr.Zero);
                thumb.Save(path);
                blogdb.TinyImagePath = "/Content/BlogImages/" + fileName.Replace(".png", "-thumb.png");
            }

            blogdb.Title = blogpost.Title;
            blogdb.Summary = blogpost.Summary;
            blogdb.Content = blogpost.Content;
            blogdb.BlogCategoryId = blogpost.BlogCategoryId;
            blogdb.EditDate = DateTime.Now;
            blogdb.ViewCount = blogpost.ViewCount;

            if (ModelState.IsValid)
            {

                uow.BlogPost.Update(blogdb);
                uow.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.BlogCategoryId = new SelectList(uow.BlogCategory.GetAll(), "Id", "CategoryName", blogpost.BlogCategoryId);
            return View(blogpost);
        }




        #endregion


        #region DeleteBlog

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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
