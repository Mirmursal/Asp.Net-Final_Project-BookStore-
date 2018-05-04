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
    public class Book_To_ReadersController : Controller
    {
        private Entities db = new Entities();

        // GET: Admin/Book_To_Readers
        public ActionResult Index()
        {
            var book_To_Readers = db.Book_To_Readers.Include(b => b.Book).Include(b => b.Reader);
            return View(book_To_Readers.ToList());
        }

        // GET: Admin/Book_To_Readers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_To_Readers book_To_Readers = db.Book_To_Readers.Find(id);
            if (book_To_Readers == null)
            {
                return HttpNotFound();
            }
            return View(book_To_Readers);
        }

        // GET: Admin/Book_To_Readers/Create
        public ActionResult Create()
        {
            ViewBag.Book_ID = new SelectList(db.Books, "id", "Name");
            ViewBag.Reader_ID = new SelectList(db.Readers, "id", "Name");
            return View();
        }

        // POST: Admin/Book_To_Readers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Book_ID,Reader_ID,Status")] Book_To_Readers book_To_Readers)
        {
            if (ModelState.IsValid)
            {
                db.Book_To_Readers.Add(book_To_Readers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Book_ID = new SelectList(db.Books, "id", "Name", book_To_Readers.Book_ID);
            ViewBag.Reader_ID = new SelectList(db.Readers, "id", "Name", book_To_Readers.Reader_ID);
            return View(book_To_Readers);
        }

        // GET: Admin/Book_To_Readers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_To_Readers book_To_Readers = db.Book_To_Readers.Find(id);
            if (book_To_Readers == null)
            {
                return HttpNotFound();
            }
            ViewBag.Book_ID = new SelectList(db.Books, "id", "Name", book_To_Readers.Book_ID);
            ViewBag.Reader_ID = new SelectList(db.Readers, "id", "Name", book_To_Readers.Reader_ID);
            return View(book_To_Readers);
        }

        // POST: Admin/Book_To_Readers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Book_ID,Reader_ID,Status")] Book_To_Readers book_To_Readers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book_To_Readers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Book_ID = new SelectList(db.Books, "id", "Name", book_To_Readers.Book_ID);
            ViewBag.Reader_ID = new SelectList(db.Readers, "id", "Name", book_To_Readers.Reader_ID);
            return View(book_To_Readers);
        }

        // GET: Admin/Book_To_Readers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_To_Readers book_To_Readers = db.Book_To_Readers.Find(id);
            if (book_To_Readers == null)
            {
                return HttpNotFound();
            }
            return View(book_To_Readers);
        }

        // POST: Admin/Book_To_Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book_To_Readers book_To_Readers = db.Book_To_Readers.Find(id);
            db.Book_To_Readers.Remove(book_To_Readers);
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
