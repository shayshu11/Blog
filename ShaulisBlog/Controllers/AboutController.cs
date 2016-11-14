using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShaulisBlog.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                return View();
            }

            return RedirectToAction("Login","Login");
        }
    }
}
