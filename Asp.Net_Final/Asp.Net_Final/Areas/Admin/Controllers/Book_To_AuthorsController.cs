using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asp.Net_Final.Models;

namespace Asp.Net_Final.Areas.Admin.Controllers
{
    public class Book_To_AuthorsController : Controller
    {
        private Entities db = new Entities();

        // GET: Admin/Book_To_Authors
        public ActionResult Index()
        {
            var book_To_Authors = db.Book_To_Authors.Include(b => b.Author).Include(b => b.Book);
            return View(book_To_Authors.ToList());
        }

        // GET: Admin/Book_To_Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_To_Authors book_To_Authors = db.Book_To_Authors.Find(id);
            if (book_To_Authors == null)
            {
                return HttpNotFound();
            }
            return View(book_To_Authors);
        }

        // GET: Admin/Book_To_Authors/Create
        public ActionResult Create()
        {
            ViewBag.Author_ID = new SelectList(db.Authors, "id", "Name");
            ViewBag.Book_ID = new SelectList(db.Books, "id", "Name");
            return View();
        }

        // POST: Admin/Book_To_Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Book_ID,Author_ID,Status")] Book_To_Authors book_To_Authors)
        {
            if (ModelState.IsValid)
            {
                db.Book_To_Authors.Add(book_To_Authors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Author_ID = new SelectList(db.Authors, "id", "Name", book_To_Authors.Author_ID);
            ViewBag.Book_ID = new SelectList(db.Books, "id", "Name", book_To_Authors.Book_ID);
            return View(book_To_Authors);
        }

        // GET: Admin/Book_To_Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_To_Authors book_To_Authors = db.Book_To_Authors.Find(id);
            if (book_To_Authors == null)
            {
                return HttpNotFound();
            }
            ViewBag.Author_ID = new SelectList(db.Authors, "id", "Name", book_To_Authors.Author_ID);
            ViewBag.Book_ID = new SelectList(db.Books, "id", "Name", book_To_Authors.Book_ID);
            return View(book_To_Authors);
        }

        // POST: Admin/Book_To_Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Book_ID,Author_ID,Status")] Book_To_Authors book_To_Authors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book_To_Authors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Author_ID = new SelectList(db.Authors, "id", "Name", book_To_Authors.Author_ID);
            ViewBag.Book_ID = new SelectList(db.Books, "id", "Name", book_To_Authors.Book_ID);
            return View(book_To_Authors);
        }

        // GET: Admin/Book_To_Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_To_Authors book_To_Authors = db.Book_To_Authors.Find(id);
            if (book_To_Authors == null)
            {
                return HttpNotFound();
            }
            return View(book_To_Authors);
        }

        // POST: Admin/Book_To_Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book_To_Authors book_To_Authors = db.Book_To_Authors.Find(id);
            db.Book_To_Authors.Remove(book_To_Authors);
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
