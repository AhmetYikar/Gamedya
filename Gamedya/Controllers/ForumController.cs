using DAL;
using Entites.Models.ForumModels;
using Gamedya.Helper;
using Gamedya.Models;
using PagedList;
using ServiceLayer.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Gamedya.Controllers
{
    public class ForumController : Controller
    {
        // GET: Forum
        private UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());
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
                liste = liste.Where(a => a.ForumTitle.Contains(ara)).ToList();
            }


            int sayfaBuyuklugu = 10;
            int sayfaNumarasi = (sayfaNo ?? 1);


            return View(liste.ToPagedList(sayfaNumarasi, sayfaBuyuklugu));
        }

        private IEnumerable<ForumViewModel> GetBlog()
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                IEnumerable<ForumViewModel> blogpost = uow.ForumPost.GetForumWithReplysAndUsers().ToList()
                                                          .Select(a => new ForumViewModel
                                                          {
                                                              Id = a.Id,
                                                              ForumTitle = a.ForumTitle,
                                                              Date = a.Date,
                                                              NewsUserId = a.NewsUserId,                                                       
                                                             
                                                              ReplyCount = a.ForumReply.Count(),
                                                            
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

        #region LatesForums
        public ActionResult LatestForums()
        {

            IEnumerable<ForumPost> latestForums = uow.ForumPost.GetAll().Take(6).ToList().OrderByDescending(a => a.Id);

            if (latestForums != null && latestForums.Count() > 0)
            {
                IEnumerable<ForumViewModel> forumView = latestForums.Select(a => new ForumViewModel
                {
                    Id = a.Id,
                    ForumTitle = a.ForumTitle,
                   
                });
                uow.Dispose();
                return PartialView(forumView);

            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Popular
        public ActionResult Popular()
        {
            IEnumerable<ForumPost> popularForums = uow.ForumPost.GetAll().Take(6).ToList().OrderByDescending(a => a.ViewCount);

            if (popularForums != null && popularForums.Count() > 0)
            {
                IEnumerable<ForumViewModel> forumView = popularForums.Select(a => new ForumViewModel
                {
                    Id = a.Id,
                    ForumTitle = a.ForumTitle,
                   
                });
                uow.Dispose();
                return PartialView(forumView);

            }
            else
            {
                return null;
            }
        }
        #endregion

        #region MostCommented
        public ActionResult MostCommented()
        {
            IEnumerable<ForumPost> forumPosts = uow.ForumPost.GetForumDetail(a => a.ForumReply.Count() > 0).Take(6).ToList().OrderByDescending(a => a.ForumReply.Count());

            if (forumPosts != null && forumPosts.Count() > 0)
            {
                IEnumerable<ForumViewModel> forumView = forumPosts.Select(a => new ForumViewModel
                {
                    Id = a.Id,
                    ForumTitle = a.ForumTitle,                   
                    ReplyCount = a.ForumReply.Count()
                });
                uow.Dispose();
                return PartialView(forumView);

            }
            else
            {
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
            var forumPost = uow.ForumPost.GetForumDetail(a => a.Id == id).FirstOrDefault();

            if (forumPost == null)
            {
                return HttpNotFound();
            }

            return View(forumPost);
        }
        #endregion

        //Blog viewCount ekle. (Okuyan sayısı)
        [HttpPost]
        public JsonResult ForumRead(int? id)
        {
            if (id != null)
            {
                ForumPost forumPost = uow.ForumPost.GetById(id);
                if (forumPost != null)
                {
                    forumPost.ViewCount++;
                    uow.ForumPost.Update(forumPost);
                    uow.Complete();
                    return Json(JsonRequestBehavior.AllowGet);
                }

            }
            return Json(JsonRequestBehavior.AllowGet);
        }

       


        #region Delete
        [Authorize]
        public ActionResult Delete(int? id, string userId)
        {
            if (id == null || userId != User.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumPost forumpost = uow.ForumPost.GetById(id);
            ViewBag.ForumCategoryId = new SelectList(uow.ForumCategory.GetAll(), "Id", "CategoryName", forumpost.ForumCategoryId);
            if (forumpost == null)
            {
                return HttpNotFound();
            }
            return View(forumpost);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var uow = new UnitOfWork(new GameNewsDbContext()))
            {
                ForumPost forumpost = uow.ForumPost.GetById(id);

                uow.ForumPost.Delete(forumpost);
                uow.Complete();
                return RedirectToAction("Index");
            }

        }

        #endregion
    }
}