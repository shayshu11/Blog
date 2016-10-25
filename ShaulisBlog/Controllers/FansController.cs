using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShaulisBlog.Models;
using System.Security.Cryptography;
using System.Web.Routing;

namespace ShaulisBlog.Controllers
{
    public class FansController : Controller
    {
        private ShaulisBlogContext db = new ShaulisBlogContext();

        // GET: Fans
        public ActionResult Index()
        {
            // Check if the current session is a valid SessionID fron the db
            if (LoginController.IsCurrentSessionValid())
            {
                var fans = db.Fans.Include(f => f.Permission);
                return View(fans.ToList());
            }
            
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Search(string searchString)
        {
            var fans = db.Fans.Include(b => b.Permission);
            if (!String.IsNullOrEmpty(searchString))
            { 
                fans = fans.Where(b => (b.FirstName + " " + b.LastName).Contains(searchString) ||
                                       (b.LastName + " " + b.FirstName).Contains(searchString));
            }
            return View("Index", fans.ToList());
        }

        // GET: Fans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // GET: Fans/Create
        public ActionResult Create()
        {
            ViewBag.permissionId = new SelectList(db.Permissions, "id", "type");
            return View();
        }

        // POST: Fans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Gender,DateOfBirth,Email,Password")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                fan.permissionId = 2;
                db.Fans.Add(fan);
                db.SaveChanges();
                return RedirectToAction("Login", "Login");
            }

            ViewBag.permissionId = new SelectList(db.Permissions, "id", "type", fan.permissionId);
            return View(fan);
        }

        // GET: Fans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            ViewBag.permissionId = new SelectList(db.Permissions, "id", "type", fan.permissionId);
            return View(fan);
        }

        // POST: Fans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Gender,DateOfBirth,permissionId")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.permissionId = new SelectList(db.Permissions, "id", "type", fan.permissionId);
            return View(fan);
        }

        // GET: Fans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: Fans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fan fan = db.Fans.Find(id);
            db.Fans.Remove(fan);
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
