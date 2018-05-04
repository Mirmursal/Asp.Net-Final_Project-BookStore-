using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asp.Net_Final.Models;

namespace Asp.Net_Final.Areas.Admin.Controllers
{
    public class BooksController : Controller
    {
        private Entities db = new Entities();

        // GET: Admin/Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Genre);
            return View(books.ToList());
        }

        // GET: Admin/Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Admin/Books/Create
        public ActionResult Create()
        {
            ViewBag.Genres = db.Genres.ToList();
            return View();
        }

        // POST: Admin/Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Book book , HttpPostedFileBase Image)
        {
            ViewBag.Genres = db.Genres.ToList();

            if (book.Name == null || book.Image == null || book.TotalCount == 0 ||
                book.Description == null || book.Genres_ID == null )
            {
                ViewBag.Error = "There is empty fields";
                return View();
            }
            else
            {
                //image begin
                if (Image.ContentLength <= 2 * 1024 * 1024)
                {
                    if (Image.ContentType.ToLower() == "image/jpg" ||
                        Image.ContentType.ToLower() == "image/png" ||
                        Image.ContentType.ToLower() == "image/jpeg" ||
                        Image.ContentType.ToLower() == "image/gif")
                    {
                        //condition? first_expression : second_expression;

                        var originalname = Path.GetFileName(Image.FileName);
                        string fieldID = Guid.NewGuid().ToString().Replace("-", "");
                        string filename = fieldID + originalname;
                        var path = Path.Combine(Server.MapPath("~/Public/images/"), filename);
                        Image.SaveAs(path);
                        book.Image = filename;

                        db.Books.Add(book);
                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ViewBag.Error = "Please upload image format";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error = "Size can't be more than 2 mb";
                    return View();

                }
                //image end
            }
        }

        // GET: Admin/Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.Genres_ID = new SelectList(db.Genres, "id", "Name", book.Genres_ID);
            return View(book);
        }

        // POST: Admin/Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Image,PDF,TotalCount,BusyCount,Description,Genres_ID,ISBN,Status")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Genres_ID = new SelectList(db.Genres, "id", "Name", book.Genres_ID);
            return View(book);
        }

        // GET: Admin/Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Admin/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
