using ShaulisBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace ShaulisBlog.Controllers
{
    public class LoginController : Controller
    {
        private static ShaulisBlogContext db = new ShaulisBlogContext();

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Email, string password)
        {
            // Handle post (login)
            if (ModelState.IsValid) // Check validity
            {
                var v = db.Fans.Where(a => a.Email.Equals(Email) && a.Password.Equals(password)).FirstOrDefault();
                if (v != null)
                {
                    string sessionID = RandomString(64);
                    System.Web.HttpContext.Current.Session["SessionID"] = sessionID;
                    System.Web.HttpContext.Current.Session["FirstName"] = v.FirstName;
                    v.SessionID = sessionID;
                    db.SaveChanges();
                    return RedirectToAction("Index", "BlogPosts");
                }

            }
            return View();
        }

        public static bool IsFanLoggedIn()
        {
            return ((System.Web.HttpContext.Current.Session["SessionID"]) != null);
        }

        public static bool IsCurrentSessionValid()
        {
            object sessionId = System.Web.HttpContext.Current.Session["SessionID"];

            // Check if there is a user logged in
            if ( sessionId != null)
            {
                // Check if the current session is an active one
                var v = db.Fans.Where(a => a.SessionID.Equals(sessionId.ToString())).FirstOrDefault();
                return (v != null);
            }

            return (false);
        }

        public static bool IsAdmin()
        {
            object sessionId = System.Web.HttpContext.Current.Session["SessionID"];

            // Check if there is a user logged in
            if (sessionId != null)
            {
                // Check if the current session is an admin
                var v = db.Fans.Include("Permission").Where(a => a.SessionID.Equals(sessionId.ToString())).FirstOrDefault();
                if (v != null) {
                    return (v.Permission.type.Equals(PermissionType.ADMIN));
                }
            }

            return (false);
        }

        public static bool canPerfom(int userId, string actionName)
        {
            object sessionId = System.Web.HttpContext.Current.Session["SessionID"];

            // Check if there is a user logged in
            if (sessionId != null)
            {
                var v = db.Fans.Where(a => a.SessionID.Equals(sessionId.ToString())).FirstOrDefault();
                if (v != null)
                {
                    if ((v.ID == userId) || (actionName.Equals("Delete") && IsAdmin())) {
                        return true;
                    }
                }
            }

            return (false);
        }
        
        public ActionResult Logout()
        {
            string sessionId = System.Web.HttpContext.Current.Session["SessionID"].ToString();
            var v = db.Fans.Where(a => a.SessionID.Equals(sessionId)).FirstOrDefault();

            if (v != null)
            {
                v.SessionID = null;
                db.SaveChanges();
            }

            System.Web.HttpContext.Current.Session["SessionID"] = null;
            return RedirectToAction("Login");
        }

        public static int getUserId()
        {
            object sessionId = System.Web.HttpContext.Current.Session["SessionID"];

            // Check if there is a user logged in
            if (sessionId != null)
            {
                // Check if the current session is an active one
                var v = db.Fans.Where(a => a.SessionID.Equals(sessionId.ToString())).FirstOrDefault();
                return (v.ID);
            }

            return (-1);
        }
    }
}