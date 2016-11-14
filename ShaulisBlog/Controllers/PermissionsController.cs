using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShaulisBlog.Models;

namespace ShaulisBlog.Controllers
{
    public class PermissionsController : Controller
    {
        private ShaulisBlogContext db = new ShaulisBlogContext();

        // GET: Permissions
        public ActionResult Index()
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                return View(db.Permissions.ToList());
            }

            return RedirectToAction("Login", "Login");
        }

        public ActionResult Search(string searchString)
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                var permissions = db.Permissions.AsQueryable();
                if (!String.IsNullOrEmpty(searchString))
                {
                    permissions = permissions.Where(b => b.type.ToString().Contains(searchString));
                }
                return View("Index", permissions.ToList());
            }

            return RedirectToAction("Login", "Login");
        }

        // GET: Permissions/Details/5
        public ActionResult Details(int? id)
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Permission permission = db.Permissions.Find(id);
                if (permission == null)
                {
                    return HttpNotFound();
                }
                return View(permission);
            }

            return RedirectToAction("Login", "Login");
        }

        // GET: Permissions/Create
        public ActionResult Create()
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                return View();
            }

            return RedirectToAction("Login", "Login");
        }

        // POST: Permissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,type,canPost,canComment,canDeletePost,canDeleteSelfComment,canDeleteAllComments")] Permission permission)
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                if (ModelState.IsValid)
                {
                    db.Permissions.Add(permission);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(permission);
            }

            return RedirectToAction("Login", "Login");
        }

        // GET: Permissions/Edit/5
        public ActionResult Edit(int? id)
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Permission permission = db.Permissions.Find(id);
                if (permission == null)
                {
                    return HttpNotFound();
                }
                return View(permission);
            }

            return RedirectToAction("Login", "Login");
        }

        // POST: Permissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,type,canPost,canComment,canDeletePost,canDeleteSelfComment,canDeleteAllComments")] Permission permission)
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(permission).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(permission);
            }

            return RedirectToAction("Login", "Login");
        }

        // GET: Permissions/Delete/5
        public ActionResult Delete(int? id)
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Permission permission = db.Permissions.Find(id);
                if (permission == null)
                {
                    return HttpNotFound();
                }
                return View(permission);
            }

            return RedirectToAction("Login", "Login");
        }

        // POST: Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Permission permission = db.Permissions.Find(id);
            db.Permissions.Remove(permission);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
