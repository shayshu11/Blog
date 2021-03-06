﻿using System;
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
    public class CommentsController : Controller
    {
        private ShaulisBlogContext db = new ShaulisBlogContext();
        private static int currPostId;

        public ActionResult Index(int postId)
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                currPostId = postId;
                var comments = db.Comments.Where(c => c.PostId == postId).Include(c => c.Author).Include(c => c.BlogPost);
                ViewBag.Post = db.BlogPosts.FirstOrDefault(b => b.ID == currPostId);

                // Creates the list of gender values for the filter combobox
                ViewBag.genders = new SelectList(Enum.GetNames(typeof(Gender)));

                return View(comments.ToList());
            }

            return RedirectToAction("Login", "Login");
        }

        public ActionResult Search(string searchString)
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                var comments = db.Comments.Where(c => c.PostId == currPostId).Include(b => b.Author);
                if (!String.IsNullOrEmpty(searchString))
                {
                    comments = comments.Where(b => b.Author.FirstName.Contains(searchString) ||
                                                     b.Author.LastName.Contains(searchString) ||
                                                     b.Content.Contains(searchString) ||
                                                     b.Title.Contains(searchString));
                }

                ViewBag.Post = db.BlogPosts.FirstOrDefault(b => b.ID == currPostId);

                // Creates the list of gender values for the filter combobox
                ViewBag.genders = new SelectList(Enum.GetNames(typeof(Gender)));

                return View("Index", comments.ToList());
            }

            return RedirectToAction("Login", "Login");
        }

        public ActionResult FilterByGender()
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                var comments = db.Comments.Where(c => c.PostId == currPostId);
                string gender = "";
                if (!String.IsNullOrEmpty(Request.Form["genders"]))
                {
                    gender = Request.Form["genders"];
                    Gender wantedGender = (Gender)Enum.Parse(typeof(Gender), gender);

                    comments =
                        (from comment in comments
                         join fan in db.Fans on comment.WriterId equals fan.ID
                         where fan.Gender == wantedGender
                         select comment).Include(b => b.Author);
                }

                ViewBag.Post = db.BlogPosts.FirstOrDefault(b => b.ID == currPostId);

                // Creates the list of gender values for the filter combobox and preserve the selected option
                ViewBag.genders = new SelectList(Enum.GetNames(typeof(Gender)), gender);

                return View("Index", comments.ToList());
            }

            return RedirectToAction("Login", "Login");
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Comment comment = db.Comments.Find(id);
                if (comment == null)
                {
                    return HttpNotFound();
                }
                return View(comment);
            }

            return RedirectToAction("Login", "Login");
        }

        // GET: Comments/Create
        public ActionResult Create(int? postId)
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                ViewBag.PostId = postId;
                currPostId = (int)postId;
                return View();
            }

            return RedirectToAction("Login", "Login");
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Content,Title")] Comment comment)
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                if (ModelState.IsValid)
                {
                    comment.CommentDate = DateTime.Now;
                    comment.WriterId = LoginController.getUserId();
                    comment.PostId = currPostId;
                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return RedirectToAction("Index", "BlogPosts");
                }

                ViewBag.WriterId = new SelectList(db.Fans, "ID", "FirstName", comment.WriterId);
                ViewBag.PostId = new SelectList(db.BlogPosts, "ID", "Content", comment.PostId);
                return View(comment);
            }

            return RedirectToAction("Login", "Login");
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Comment comment = db.Comments.Find(id);
                if (comment == null)
                {
                    return HttpNotFound();
                }
                ViewBag.WriterId = new SelectList(db.Fans, "ID", "FirstName", comment.WriterId);
                ViewBag.PostId = new SelectList(db.BlogPosts, "ID", "Content", comment.PostId);
                return View(comment);
            }

            return RedirectToAction("Login", "Login");
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,WriterId,PostId,Content,Title,CommentDate")] Comment comment)
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                if (ModelState.IsValid)
                {
                    comment.UpdateDate = DateTime.Now;
                    db.Entry(comment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.WriterId = new SelectList(db.Fans, "ID", "FirstName", comment.WriterId);
                ViewBag.PostId = new SelectList(db.BlogPosts, "ID", "Content", comment.PostId);
                return View(comment);
            }

            return RedirectToAction("Login", "Login");
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            // Check if a user is logged in
            if (ShaulisBlog.Controllers.LoginController.IsFanLoggedIn())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Comment comment = db.Comments.Find(id);
                if (comment == null)
                {
                    return HttpNotFound();
                }

                return View(comment);
            }

            return RedirectToAction("Login", "Login");
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();

            if (db.Comments.Where(c => c.PostId == currPostId).Count() == 0)
            {
                return RedirectToAction("Index", "BlogPosts");
            }

            return RedirectToAction("Index", new { postId = currPostId });
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
