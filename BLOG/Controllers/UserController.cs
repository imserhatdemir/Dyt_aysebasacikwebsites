﻿using BusinnessLayer.Concrete;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BLOG.Controllers
{
 
    public class UserController : Controller
    {
        // GET: User
        UserProfileManager userprofile = new UserProfileManager();
        BlogManager bm = new BlogManager();

        public ActionResult Index()
        {
         
            return View();
        }
        public PartialViewResult Partial1(string p)
        {
            p = (string)Session["Mail"];
            var profilevalues = userprofile.GetAuthorByMail(p);
            return PartialView(profilevalues);
        }
        
        public ActionResult UpdateUserProfile(Author p)
        {
            userprofile.EditAuthor(p);
            return RedirectToAction("Index");
        }
      
        public ActionResult AuthorBlogList(string p)
        {
            p = (string)Session["Mail"];
            Context c = new Context();
            int id = c.Authors.Where(x => x.AuthorMail == p).Select(y => y.AuthorID).FirstOrDefault();
            var blogs = userprofile.GetBlogByAuthor(id);
            return View(blogs);
        }
        [HttpGet]
        public ActionResult UpdateBlog(int id)
        {

            Blog blog = bm.FindBlog(id);
            Context c = new Context();
            List<SelectListItem> values = (from x in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.values = values;
            List<SelectListItem> aut = (from x in c.Authors.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.AuthorName,
                                            Value = x.AuthorID.ToString()
                                        }).ToList();
            ViewBag.aut = aut;
            return View(blog);
        }
        [HttpPost]
        public ActionResult UpdateBlog(Blog p)
        {
            bm.UpdateBlog(p);
            return RedirectToAction("AuthorBlogList");
        }
        public ActionResult DeleteBlog(int id)
        {
            bm.DeleteBlogBL(id);
            return RedirectToAction("AuthorBlogList");
        }
        [HttpGet]
        public ActionResult AddNewBlog()
        {
            Context c = new Context();
            List<SelectListItem> values = (from x in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.values = values;
            List<SelectListItem> aut = (from x in c.Authors.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.AuthorName,
                                            Value = x.AuthorID.ToString()
                                        }).ToList();
            ViewBag.aut = aut;
            return View();
        }
        [HttpPost]
        public ActionResult AddNewBlog(Blog b)
        {

            bm.BlogAddBL(b);
            return RedirectToAction("AuthorBlogList");
        }
        
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("AuthorIndex", "Login");
        }

    }
}