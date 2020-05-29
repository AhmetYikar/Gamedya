using DAL;
using Entites.Models.NewsModels;
using GameAdmin.Helper;
using GameAdmin.Models;
using Microsoft.Azure.Amqp.Framing;
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
using System.Xml.Serialization;
using static System.Net.WebRequestMethods;

namespace GameAdmin.Controllers
{


    [Authorize]
    public class NewsController : Controller
    {

        #region Index

        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());
        // GET: News

        public ActionResult Index(string ara, string siralama, string sonArananKelime, int? sayfaNo)
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


            var liste = GetNews().ToList();

            //indexte her bir haberin yorum sayısı gelsin diye yapıldı
            foreach (var item in liste)
            {
                List<NewsComment> comments = uow.NewsComment.Where(a => a.NewsId == item.Id).ToList();
                if (comments != null && comments.Count() > 0)
                {
                    item.CommentCount = comments.Count();
                }
            }

            if (!string.IsNullOrWhiteSpace(ara))
            {
                liste = liste.Where(a => a.Title.ToLower().Contains(ara.ToLower())).ToList();
            }



            int sayfaBuyuklugu = 10;
            int sayfaNumarasi = (sayfaNo ?? 1);

            return View(liste.ToPagedList(sayfaNumarasi, sayfaBuyuklugu));
        }

        private IEnumerable<NewsViewModel> GetNews()
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                IEnumerable<NewsViewModel> news = uow.News.GetAll().ToList()
                                                          .Select(a => new NewsViewModel { Id = a.Id, Title = a.Title, Date = a.Date, TinyImagePath = a.TinyImagePath })
                                                          .OrderByDescending(a => a.Id);




                if (news != null)
                {
                    uow.Dispose();

                    return news;
                }
                else
                {
                    return null;
                }

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
            var news = uow.News.GetNewsDetails(a => a.Id == id).FirstOrDefault();


            if (news == null)
            {
                return HttpNotFound();
            }


            return View(news);

        }




        #endregion

        #region Create
        [HttpGet]

        public ActionResult Create()
        {
            ViewBag.NewsCategoryId = new SelectList(uow.NewsCategory.GetAll(), "Id", "CategoryName", string.Empty);
            ViewBag.NewsPart = string.Empty;
            ViewBag.NewsPlatform = string.Empty;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "Id,Title,Summary,Content,NewsCategoryId,Date,EditDate,IsActive,TinyImagePath,NewsUserId,NewsPlatform,NewsPart")] News news, HttpPostedFileBase file, string videoPath)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.NewsCategoryId = new SelectList(uow.NewsCategory.GetAll(), "Id", "CategoryName");                
                return View(news);
            }

            string fileName = string.Empty;
            string extension = string.Empty;
            NewsImage newsImage = new NewsImage();
            newsImage.NewsId = news.Id;
            newsImage.ImagePath = news.TinyImagePath;


            if (videoPath != null)
            {
                NewsVideo newsVideo = new NewsVideo();
                newsVideo.NewsId = news.Id;
                newsVideo.VideoPath = videoPath;
                uow.NewsVideo.Insert(newsVideo);
            }

            if (file != null && file.ContentLength > 0 && file.ContentLength < 2 * 1024 * 1024)
            {
                extension = Path.GetExtension(file.FileName);

                if (extension.Contains("pdf") || extension.Contains("doc") || extension.Contains("docx"))
                {
                    return RedirectToAction("index");
                }

                fileName = Guid.NewGuid() + ".png";
                file.SaveAs(Path.Combine(Server.MapPath("/Content/BigImages/"), fileName));

                var path = Path.Combine(Server.MapPath("/Content/TinyImages/"), fileName.Replace(".png", "-thumb.png"));

                Image image = Image.FromStream(file.InputStream, true);

                int imgWidth = 110;
                int imgHeight = 95;

                Image thumb = image.GetThumbnailImage(imgWidth, imgHeight, () => false, IntPtr.Zero);
                thumb.Save(path);

                news.TinyImagePath = "/Content/TinyImages/" + fileName.Replace(".png", "-thumb.png");

                newsImage.ImagePath = "/Content/BigImages/" + fileName;
            }
            try
            {
                uow.NewsImage.Insert(newsImage);
                news.Date = DateTime.Now;
                news.EditDate = DateTime.Now;
                news.NewsUserId = User.GetUserId();
                uow.News.Insert(news);
                uow.Complete();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.NewsCategoryId = new SelectList(uow.NewsCategory.GetAll(), "Id", "CategoryName");
               
                return View(news);
            }

        }


        #endregion

        #region Edit

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = uow.News.GetById(id);
            if (news == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.NewsCategoryId = new SelectList(uow.NewsCategory.GetAll(), "Id", "CategoryName", news.NewsCategoryId);
            return View(news);
        }

        // POST: NewsCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "Id,Title,Summary,Content,NewsCategoryId,Date,EditDate,IsActive,ViewCount,TinyImagePath,NewsPlatform,NewsPart")] News news, HttpPostedFileBase file)
        {
            var newsdb = uow.News.GetById(news.Id);

            MessageId message;
            string Error = "Dosya uzantısı uygun olmadığından eklenemedi!";
            string fileName = string.Empty;
            string extension = string.Empty;

            if (file != null && file.ContentLength > 0 && file.ContentLength < 2 * 1024 * 1024)
            {
                extension = Path.GetExtension(file.FileName);

                if (extension.Contains("pdf") || extension.Contains("doc") || extension.Contains("docx"))
                {
                    message = Error;
                    return RedirectToAction("index", new { Message = message });
                }
                else
                {

                }
                fileName = Guid.NewGuid() + ".png";

                var path = Path.Combine(Server.MapPath("/Content/TinyImages/"), fileName.Replace(".png", "-thumb.png"));

                Image image = Image.FromStream(file.InputStream, true);

                int imgWidth = 110;
                int imgHeight = 95;

                Image thumb = image.GetThumbnailImage(imgWidth, imgHeight, () => false, IntPtr.Zero);
                thumb.Save(path);
                newsdb.TinyImagePath = "/Content/TinyImages/" + fileName.Replace(".png", "-thumb.png");
            }

            newsdb.Title = news.Title;
            newsdb.Summary = news.Summary;
            newsdb.Content = news.Content;
            newsdb.NewsCategoryId = news.NewsCategoryId;
            newsdb.EditDate = DateTime.Now;
            newsdb.IsActive = news.IsActive;
            newsdb.NewsPart = news.NewsPart;
            newsdb.NewsPlatform = news.NewsPlatform;


            if (ModelState.IsValid)
            {
                uow.News.Update(newsdb);
                uow.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.NewsCategoryId = new SelectList(uow.NewsCategory.GetAll(), "Id", "CategoryName", news.NewsCategoryId);
            return View(news);
        }



        #endregion

        #region DeleteNews

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = uow.News.GetById(id);

            if (news == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.NewsCategoryId = new SelectList(uow.NewsCategory.GetAll(), "Id", "CategoryName", news.NewsCategoryId);

            return View(news);

        }

        // POST: NewsCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                News news = uow.News.GetById(id);

                uow.News.Delete(news);
                uow.Complete();
                return RedirectToAction("Index");
            }

        }

        #endregion

    }

}
