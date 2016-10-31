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
    public class BlogPostsController : Controller
    {
        private ShaulisBlogContext db = new ShaulisBlogContext();

        // GET: BlogPosts
        public ActionResult Index()
        {
            var blogPosts = db.BlogPosts.Include(b => b.Author).Include(b => b.Comments).Include("Comments.Author");
            var temp = from post in blogPosts
                       select new { Comments = post.Comments,  };
            return View(blogPosts.ToList());
        }

        // Redirect to view of Advanced Search
        public ActionResult AdvancedSearch()
        {
            return View();
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

            return View("Index", blogPosts.ToList());
        }

        public ActionResult Search(string searchString)
        {
            var blogPosts = db.BlogPosts.Include(b => b.Author).Include(b => b.Comments);
            if (!String.IsNullOrEmpty(searchString))
            {
                blogPosts = blogPosts.Where(b => b.Author.FirstName.Contains(searchString) || 
                                                 b.Author.LastName.Contains(searchString) || 
                                                 b.Content.Contains(searchString) || 
                                                 b.Title.Contains(searchString));
            }
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
          /*  BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            */
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
    }
}
