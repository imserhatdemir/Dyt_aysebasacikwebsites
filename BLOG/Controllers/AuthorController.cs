﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BLOG.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public PartialViewResult AuthorResult()
        {
            return PartialView();
        }
    }
}