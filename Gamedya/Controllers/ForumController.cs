using DAL;
using Entites.Models.ForumModels;
using Gamedya.Helper;
using Gamedya.Models;
using Microsoft.Ajax.Utilities;
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

        public ActionResult Index()
        {
            var categories = uow.ForumCategory.Where(a=>a.ParentId==null);
            if (categories==null)
            {
                return null;
            }
            List<int> ids = new List<int>();
            foreach (var item in categories)
            {
                ids.Add(item.Id);
            }

            ForumIndexViewModel model = new ForumIndexViewModel
            {
                //ForumCount = uow.ForumPost.GetAll().Count(),
                CategoryCount = categories.Count(),
                CategoryIds=ids
            };
            if (ModelState.IsValid)
            {
                return View(model);
            }
            return null;
        }


        #region ForumByCategory

        public ActionResult ForumByCategory(int? categoryId)
        {
            if (categoryId !=null)
            {
               
                var category = uow.ForumCategory.GetById(categoryId);                

                ForumByCategoryViewModel model = new ForumByCategoryViewModel
                {
                    CategoryName=category.CategoryName,
                  
                    iconClass=category.Description,
                    SubCategories = uow.ForumCategory.Details(a => a.ParentId == category.Id).ToList()

                };
                if (ModelState.IsValid)
                {
                    return PartialView(model);
                }
            }
            
            return null;
        }

        #endregion
        

        #region ForumBySubCategory

        #endregion
        public ActionResult ForumBySubCategory(int? categoryId)
        {
            if (categoryId==null)
            {
                return null;
            }
            var category = uow.ForumCategory.Details(a => a.Id == categoryId).FirstOrDefault();
            if (category != null)
            {
                int replyCount = 0;
                List<ForumPost> posts = uow.ForumPost.GetForumWithReplies(a=>a.ForumCategoryId==category.Id).ToList();
                if (posts.Count<1)
                {
                    return null;
                }
                foreach (var post in posts)
                {
                    replyCount = replyCount + post.ForumReplies.Count();
                }

                ForumByCategoryViewModel model = new ForumByCategoryViewModel
                {
                    ForumCount = category.ForumPosts.Count(),
                    ReplyCount = replyCount,                   
                    LastPost = posts.LastOrDefault().ForumTitle
                };
                if (ModelState.IsValid)
                {
                    return PartialView(model);
                }
            }
           
            return null;
        }


        //#region Sayfalama
        //public ActionResult Sayfalama(string ara, string siralama, string sonArananKelime, int? sayfaNo)
        //{
        //    ViewBag.SonSiralama1 = siralama;
        //    //ViewBag.AdaGoreSirala = String.IsNullOrEmpty(siralama) ? "ZdenAya" : string.Empty;
        //    //ViewBag.SoyadaGoreSirala = siralama == "SoyadAdanZye" ? "SoyadZdenAya" : "SoyadAdanZye";

        //    if (ara != null)
        //    {
        //        sayfaNo = 1;
        //    }
        //    else
        //    {
        //        ara = sonArananKelime;
        //    }


        //    ViewBag.SonArananKelime1 = ara;


        //    if (String.IsNullOrEmpty(siralama))
        //    {
        //        ViewBag.AdaGoreSirala2 = "ZdenAya";
        //    }
        //    else
        //    {
        //        ViewBag.AdaGoreSirala2 = string.Empty;
        //    }


        //    var liste = GetForum().ToList();


        //    if (!string.IsNullOrWhiteSpace(ara))
        //    {
        //        liste = liste.Where(a => a.ForumTitle.Contains(ara)).ToList();
        //    }


        //    int sayfaBuyuklugu = 10;
        //    int sayfaNumarasi = (sayfaNo ?? 1);


        //    return View(liste.ToPagedList(sayfaNumarasi, sayfaBuyuklugu));
        //}

        //private IEnumerable<ForumViewModel> GetForum()
        //{
        //    using (var uow = new UnitOfWork(new GameNewsDbContext()))
        //    {
        //        IEnumerable<ForumViewModel> forumposts = uow.ForumPost.GetForumWithRepliesAndUsers().ToList()
        //                                                  .Select(a => new ForumViewModel
        //                                                  {
        //                                                      Id = a.Id,
        //                                                      ForumTitle = a.ForumTitle,
        //                                                      Date = a.Date,
        //                                                      NewsUserId = a.NewsUserId,
        //                                                      ForumUser = a.NewsUser.FullName,
        //                                                      ReplyCount = a.ForumReplies.Count(),

        //                                                  })
        //                                                  .OrderByDescending(a => a.Id);

        //        if (forumposts != null)
        //        {
        //            uow.Dispose();
        //            return forumposts;
        //        }

        //        return null;
        //    }
        //}
        //#endregion

        #region Create
        [Authorize]
        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }           
                var forumCategory = uow.ForumCategory.GetById(id);
                ViewBag.ForumCategoryName = forumCategory.CategoryName;
                ForumPost forum = new ForumPost();
                forum.ForumCategoryId = forumCategory.Id;
                return View(forum);

        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ForumTitle,Content,ForumCategoryId")] ForumPost forumpost)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.ForumCategoryId = new SelectList(uow.ForumCategory.GetAll(), "Id", "CategoryName");
                return View(forumpost);
            }            

            try
            {
                forumpost.Date = DateTime.Now;
                forumpost.NewsUserId = User.GetUserId();
                uow.ForumPost.Insert(forumpost);
                uow.Complete();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.ForumCategoryId = new SelectList(uow.ForumCategory.GetAll(), "Id", "CategoryName");
                return View(forumpost);
            }

        }

        #endregion

        #region LatesForums
        public ActionResult LatestForums()
        {

            IEnumerable<ForumPost> latestForums = uow.ForumPost.GetForumcategoryAndUsers().ToList().OrderByDescending(a => a.Id).Take(6);

            if (latestForums != null)
            {

                IEnumerable<ForumViewModel> forumView = latestForums.Select(a => new ForumViewModel
                {
                    Id = a.Id,
                    ForumTitle = a.ForumTitle,
                    ForumUser = a.NewsUser.FullName,
                    Date = a.Date,
                    CategoryName = a.ForumCategory.CategoryName,
                    ViewCount= a.ViewCount


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
            IEnumerable<ForumPost> forumPosts = uow.ForumPost.GetForumDetail(a => a.ForumReplies.Count() > 0).ToList().Take(6).OrderByDescending(a => a.ForumReplies.Count());

            if (forumPosts != null && forumPosts.Count() > 0)
            {
                IEnumerable<ForumViewModel> forumView = forumPosts.Select(a => new ForumViewModel
                {
                    Id = a.Id,
                    ForumTitle = a.ForumTitle,
                    ReplyCount = a.ForumReplies.Count()
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

        #region gamereviews
        public ActionResult GameReviews(int? categoryId)
        {
            
            if (categoryId==null || categoryId<1)
            {
                return null;
            }
            IEnumerable<ForumPost> gamereview = uow.ForumPost.GetForumcategoryAndUsers().Where(a=>a.ForumCategoryId==categoryId).ToList().OrderByDescending(a => a.Id);

            if (gamereview != null && gamereview.Count()>0 )
            {

                IEnumerable<ForumViewModel> forumView = gamereview.Select(a => new ForumViewModel
                {
                    Id = a.Id,
                    ForumTitle = a.ForumTitle,
                    ForumUser = a.NewsUser.FullName,
                    Date = a.Date,
                    CategoryName = a.ForumCategory.CategoryName,
                    CategoryId=a.ForumCategoryId

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
    }
}