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
using System.Globalization;

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

        // Redirect to view of Advanced Search
        public ActionResult AdvancedSearch()
        {
            return View();
        }

        // This function searches by crossing multiple fields
        public ActionResult StartAdvancedSearch(string name, ShaulisBlog.Models.Gender? gender, DateTime? date = null)
        {
            var fans = db.Fans.Include(b => b.Permission);

            // Check if Gender was inserted by the user to search
            if (gender != null)
            {
                fans = fans.Where(b => b.Gender == gender);
            }

            // Check if Name was inserted by the user to search
            if (!String.IsNullOrEmpty(name))
            {
                fans = fans.Where(b => (b.FirstName + " " + b.LastName).Contains(name) ||
                                       (b.LastName + " " + b.FirstName).Contains(name));
            }

            // Check if Date was inserted by the user to search
            if (date != null)
            {
                fans = fans.Where(b => DbFunctions.TruncateTime(b.DateOfBirth) == DbFunctions.TruncateTime(date.Value));
            }

            return View("Index", fans.ToList());
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
                Permission userPerm = db.Permissions.Where(p => p.type.ToString().Equals(PermissionType.USER.ToString())).FirstOrDefault();
                fan.permissionId = userPerm.id;
                fan.CreationDate = DateTime.Now;
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
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Gender,DateOfBirth,permissionId,Email,Password,CreationDate")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fan).State = EntityState.Modified;
                bool isSelfEdit = LoginController.getUserId() == fan.ID;

                // If the user deleted himseld
                if (isSelfEdit)
                {
                    fan.SessionID = System.Web.HttpContext.Current.Session["SessionID"].ToString();
                    db.SaveChanges();
                    return RedirectToAction("Index", "BlogPosts");
                }

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
            bool isSelfDelete = LoginController.getUserId() == fan.ID;
            db.Fans.Remove(fan);
            db.SaveChanges();

            // If the user deleted himseld
            if (isSelfDelete) {
                RedirectToAction("Logout", "Login");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Statistics()
        {
            return View();
        }

        // Gets the data about user registration for statistics view
        public JsonResult GetRegisterStats()
        {
            var objList = new List<object>();
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();

            for (int monthIndex = 5; monthIndex >= 0; monthIndex--)
            {
                int currMonth = (DateTime.Today.Month - monthIndex + 12) % 12;
                int currYear = DateTime.Today.Year;
                int womenSum, menSum;

                // To save on filtering loops we first find the wanted month registrations
                // And then filter by gender
                var currMonthRegs = db.Fans.Where(fan => fan.CreationDate.Month == currMonth && fan.CreationDate.Year == currYear);

                womenSum = currMonthRegs.Where(fan => fan.Gender == Gender.FEMALE).Count();
                menSum = currMonthRegs.Where(fan => fan.Gender == Gender.MALE).Count();

                objList.Add(new { month = mfi.GetMonthName(currMonth),
                                  women = womenSum,
                                  men = menSum });
            }

            return Json(objList, JsonRequestBehavior.AllowGet);
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
