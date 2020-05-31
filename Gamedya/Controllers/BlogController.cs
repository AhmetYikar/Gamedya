using DAL;
using Entites.Models.BlogModels;
using Entites.Models.UserModels;
using Gamedya.Helper;
using Gamedya.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
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
    public class BlogController : Controller
    {
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
                IEnumerable<BlogViewModel> blogpost = uow.BlogPost.BlogWithCommentsAndUsers().Where(a=>a.IsOk==true).ToList()
                                                          .Select(a => new BlogViewModel
                                                          {
                                                              Id = a.Id,
                                                              Title = a.Title,
                                                              Date = a.Date,
                                                              NewsUserId = a.NewsUserId,
                                                              BloggerName = a.NewsUser.FullName,
                                                              TinyImagePath = a.TinyImagePath,
                                                              CommentCount = a.BlogComments.Count(),
                                                              Summary = a.Summary
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

        #region BloggersList
        public ActionResult BloggersList()
        {
            IEnumerable<NewsUser> bloggers = uow.NewsUser.UserWithBlogPosts(a => a.BlogPosts.Count()>0 && a.BlogPosts.Any(x=>x.IsOk==true) ).OrderByDescending(a => a.BlogPosts.Count());

            IEnumerable<BloggerViewModel> bloggersV = bloggers
                                                  .Select(a => new BloggerViewModel 
                                                  {
                                                      Id=a.Id,BloggerName=a.FullName, BlogCount=a.BlogPosts.Count() 
                                                  });

            if (bloggers != null)
            {               
                return View(bloggersV);
            }
            else
            {
                return HttpNotFound();
            }
        }
        #endregion

        #region Create
        [Authorize]
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

            try
            {
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


        #region BloggerBlogs
        public ActionResult BloggerBlogs(string userId)
        {
            if (userId==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<BlogViewModel> blogPosts = uow.BlogPost.BlogWithCommentsAndUsers().Where(a=>a.NewsUserId==userId && a.IsOk==true)
                                                          .Select(a => new BlogViewModel
                                                          {
                                                              Id = a.Id,
                                                              Title = a.Title,
                                                              Date = a.Date,
                                                              NewsUserId = a.NewsUserId,
                                                              BloggerName = a.NewsUser.FullName,
                                                              TinyImagePath = a.TinyImagePath,
                                                              CommentCount = a.BlogComments.Count()
                                                          })
                                                          .OrderByDescending(a => a.Id);
            if (blogPosts.Count()<1)
            {
                return HttpNotFound();
            }

            int pageNumber = (blogPosts.Count() / 8)+1;
            return View(blogPosts.ToPagedList(pageNumber, 8));
        }
        #endregion

        #region BloggersLatestBlogs
        public ActionResult BloggersLatestBlogs(string userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<BlogViewModel> blogPosts = uow.BlogPost.Where(a => a.NewsUserId == userId && a.IsOk == true).Take(6)
                                                          .Select(a => new BlogViewModel
                                                          {
                                                              Id = a.Id,
                                                              Title = a.Title,                                                              
                                                              TinyImagePath = a.TinyImagePath,
                                                              NewsUserId=a.NewsUserId
                                                          })
                                                          .OrderByDescending(a => a.Id);
            if (blogPosts.Count() < 1)
            {
                return HttpNotFound();
            }

            return PartialView(blogPosts);
        }
        #endregion

        #region LatestBlogs
        public ActionResult LatestBlogs()
        {

            IEnumerable<BlogPost> latestBlogs = uow.BlogPost.Where(a=>a.IsOk == true).ToList().OrderByDescending(a => a.Id).Take(6);

            if (latestBlogs != null && latestBlogs.Count() > 0)
            {
                IEnumerable<BlogViewModel> blogView = latestBlogs.Select(a => new BlogViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    TinyImagePath = a.TinyImagePath
                });
                uow.Dispose();
                return PartialView(blogView);

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
            IEnumerable<BlogPost> popularBlogs = uow.BlogPost.Where(a=>a.IsOk == true).ToList().OrderByDescending(a => a.ViewCount).Take(6);

            if (popularBlogs != null && popularBlogs.Count() > 0)
            {
                IEnumerable<BlogViewModel> blogView = popularBlogs.Select(a => new BlogViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    TinyImagePath = a.TinyImagePath
                });
                uow.Dispose();
                return PartialView(blogView);

            }
            else
            {
                return null;
            }
        }
        #endregion

        #region BloggersPopularBlogs
        public ActionResult BloggersPopularBlogs(string userId)
        {
            IEnumerable<BlogPost> popularBlogs = uow.BlogPost.Where(a => a.NewsUserId == userId && a.IsOk == true).ToList().OrderByDescending(a => a.ViewCount).Take(6);

            if (popularBlogs.Count()>0)
            {
                IEnumerable<BlogViewModel> blogView = popularBlogs.Select(a => new BlogViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    TinyImagePath = a.TinyImagePath
                });
                uow.Dispose();
                return PartialView(blogView);

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
            IEnumerable<BlogPost> blogPosts = uow.BlogPost.GetBlogDetails(a => a.BlogComments.Count() > 0 && a.IsOk == true).Take(6).ToList().OrderByDescending(a => a.BlogComments.Count());

            if (blogPosts != null && blogPosts.Count() > 0)
            {
                IEnumerable<BlogViewModel> newsView = blogPosts.Select(a => new BlogViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    TinyImagePath = a.TinyImagePath,
                    CommentCount = a.BlogComments.Count()
                });
                uow.Dispose();
                return PartialView(newsView);

            }
            else
            {
                return null;
            }
        }
        #endregion

        #region BloggersMostCommented
        public ActionResult BloggersMostCommented(string userId)
        {
            IEnumerable<BlogPost> blogPosts = uow.BlogPost.GetBlogDetails(a => a.NewsUserId == userId && a.BlogComments.Count() > 0 && a.IsOk == true)
                                                          .ToList().OrderByDescending(a => a.BlogComments.Count()).Take(6);

            if (blogPosts != null && blogPosts.Count() > 0)
            {
                IEnumerable<BlogViewModel> newsView = blogPosts.Select(a => new BlogViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    TinyImagePath = a.TinyImagePath,
                    CommentCount = a.BlogComments.Count()
                });
                uow.Dispose();
                return PartialView(newsView);

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
            var blogPost = uow.BlogPost.GetBlogDetails(a => a.Id == id && a.IsOk == true).FirstOrDefault();

            if (blogPost == null)
            {
                return HttpNotFound();
            }

            return View(blogPost);
        }
        #endregion

        //Blog viewCount ekle. (Okuyan sayısı)
        [HttpPost]
        public JsonResult BlogRead(int? id)
        {
            if (id!=null)
            {
                if (GetCookie("blogRead", id) == null)
                {
                    CreateCookie("blogRead", id);
                    BlogPost blogPost = uow.BlogPost.GetById(id);
                    if (blogPost != null)
                    {
                        blogPost.ViewCount++;
                        uow.BlogPost.Update(blogPost);
                        uow.Complete();
                        return Json(JsonRequestBehavior.AllowGet);
                    }
                }
            }   
            
            return Json(JsonRequestBehavior.AllowGet);
        }

        #region Edit

        public ActionResult Edit(int? id,string userId)
        {           

            if (id == null || userId != User.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var blogpost = uow.BlogPost.GetById(id);
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
        [Authorize]
        public ActionResult Delete(int? id, string userId)
        {
            if (id == null || userId!=User.GetUserId())
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


        private void CreateCookie(string name, int? id)
        {
            name = name + id.ToString();
            HttpCookie cookieLike = new HttpCookie(name);
            cookieLike.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(cookieLike);
        }
        private string GetCookie(string name, int? id)
        {
            name = name + id.ToString();
            if (Request.Cookies.AllKeys.Contains(name))
            {
                return Request.Cookies[name].Value;
            }
            return null;
        }
    }
}