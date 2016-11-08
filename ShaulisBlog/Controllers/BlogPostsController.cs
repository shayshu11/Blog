using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShaulisBlog.Models;
using System.Globalization;

namespace ShaulisBlog.Controllers
{
    public class BlogPostsController : Controller
    {
        private ShaulisBlogContext db = new ShaulisBlogContext();

        // GET: BlogPosts
        public ActionResult Index()
        {
            var blogPosts = db.BlogPosts.Include(b => b.Author).Include(b => b.Comments).Include("Comments.Author");

            // Creates the list of gender values for the filter combobox
            ViewBag.genders = new SelectList(Enum.GetNames(typeof(Gender)));

            return View(blogPosts.ToList());
        }

        // Redirect to view of Advanced Search
        public ActionResult AdvancedSearch()
        {
            return View();
        }

        // Redirect to the group by user view
        public ActionResult GroupBy()
        {
            var blogPosts = db.BlogPosts.Include(b => b.Author).ToList();

            var blogPostsGrouped =
                (from post in blogPosts
                 group post by post.WriterId);

            return View(blogPostsGrouped);
        }

        // This function searches by crossing multiple fields
        public ActionResult StartAdvancedSearch(string author, string title, DateTime? date = null)
        {
            var blogPosts = db.BlogPosts.Include(b => b.Author).Include(b => b.Comments).Include("Comments.Author");

            if (!String.IsNullOrEmpty(author))
            {
                blogPosts = blogPosts.Where(b => (b.Author.FirstName + " " + b.Author.LastName).Contains(author) ||
                                       (b.Author.LastName + " " + b.Author.FirstName).Contains(author));
            }

            if (!String.IsNullOrEmpty(title))
            {
                blogPosts = blogPosts.Where(b => b.Title.Contains(title));
            }

            if(date != null)
            {
                blogPosts = blogPosts.Where(b => DbFunctions.TruncateTime(b.PostDate) == DbFunctions.TruncateTime(date.Value));
            }

            // Creates the list of gender values for the filter combobox
            ViewBag.genders = new SelectList(Enum.GetNames(typeof(Gender)));

            return View("Index", blogPosts.ToList());
        }

        public ActionResult Search(string searchString)
        {
            var blogPosts = db.BlogPosts.Include(b => b.Author).Include(b => b.Comments).Include("Comments.Author");
            if (!String.IsNullOrEmpty(searchString))
            {
                blogPosts = blogPosts.Where(b => b.Author.FirstName.Contains(searchString) || 
                                                 b.Author.LastName.Contains(searchString) || 
                                                 b.Content.Contains(searchString) || 
                                                 b.Title.Contains(searchString));
            }

            // Creates the list of gender values for the filter combobox
            ViewBag.genders = new SelectList(Enum.GetNames(typeof(Gender)));

            return View("Index", blogPosts.ToList());
        }

        public ActionResult FilterByGender()
        {
            var blogPosts = db.BlogPosts.AsQueryable();
            string gender = "";
            if (!String.IsNullOrEmpty(Request.Form["genders"]))
            {
                gender = Request.Form["genders"];
                Gender wantedGender = (Gender)Enum.Parse(typeof(Gender), gender);

                blogPosts =
                    (from post in blogPosts
                     join fan in db.Fans on post.WriterId equals fan.ID
                     where fan.Gender == wantedGender
                     select post).Include(b => b.Author).Include(b => b.Comments).Include("Comments.Author");
            }

            // Creates the list of gender values for the filter combobox and preserve the selected option
            ViewBag.genders = new SelectList(Enum.GetNames(typeof(Gender)), gender);

            return View("Index", blogPosts.ToList());
        }

        // GET: BlogPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        public ActionResult ViewComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index", "Comments", new { postId = id });
        }

        // GET: BlogPosts/Create
        public ActionResult Create()
        {
            ViewBag.WriterId = new SelectList(db.Fans, "ID", "FirstName");
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Content")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                blogPost.PostDate = DateTime.Now;
                blogPost.WriterId = LoginController.getUserId();
                db.BlogPosts.Add(blogPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WriterId = new SelectList(db.Fans, "ID", "FirstName", blogPost.WriterId);
            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.WriterId = new SelectList(db.Fans, "ID", "FirstName", blogPost.WriterId);
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,WriterId,Content,Title,PostDate")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                blogPost.UpdateDate = DateTime.Now;
                db.Entry(blogPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WriterId = new SelectList(db.Fans, "ID", "FirstName", blogPost.WriterId);
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.BlogPosts.Find(id);
            db.BlogPosts.Remove(blogPost);
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

        public ActionResult EditComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Edit", "Comments", new { id = id });
        }

        public ActionResult Comment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            
            return RedirectToAction("Create", "Comments", new { postId = id });
        }

        public ActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Delete", "Comments", new { Id = id });
        }

        public ActionResult DetailsComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Details", "Comments", new { Id = id });
        }


        public ActionResult Statistics()
        {
            return View();
        }

        // Gets the data about user registration for statistics view
        public JsonResult GetPostsStats()
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
                var currMonthRegs = db.BlogPosts
                    .Where(post => post.PostDate.Month == currMonth && post.PostDate.Year == currYear)
                    .Include("Author");

                womenSum = currMonthRegs.Where(post => post.Author.Gender == Gender.FEMALE).Count();
                menSum = currMonthRegs.Where(post => post.Author.Gender == Gender.MALE).Count();

                objList.Add(new
                {
                    month = mfi.GetMonthName(currMonth),
                    women = womenSum,
                    men = menSum
                });
            }

            return Json(objList, JsonRequestBehavior.AllowGet);
        }
    }
}
