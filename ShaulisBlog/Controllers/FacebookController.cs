using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShaulisBlog.Controllers
{
    public class FacebookController : Controller
    {
        // GET: Facebook
        public ActionResult Index()
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                return View();
            }

            return RedirectToAction("Login", "Login");
        }
    }
}
