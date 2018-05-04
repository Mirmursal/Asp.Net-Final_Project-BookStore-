using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.Net_Final.Models;

namespace Asp.Net_Final.Controllers
{
    public class AJAXController : Controller
    {
        Entities db = new Entities();
        // GET: AJAX
        public ActionResult SortAuthor(int? authorID)
        {
            List<Book> sortBooks = new List<Book>();
            foreach (Book_To_Authors bta in db.Book_To_Authors)
            {
                if(bta.Author_ID == authorID && bta.Book.Status == 1)
                {
                    sortBooks.Add(new Book {
                        id = bta.Book.id,
                        Name = bta.Book.Name,
                        Image = bta.Book.Image,
                        PDF = bta.Book.PDF,
                        TotalCount = bta.Book.TotalCount,
                        BusyCount = bta.Book.BusyCount,
                        Description = bta.Book.Description,
                        Status = bta.Book.Status
                    });
                }
            }
            return Json(new
            {
                status = 200,
                error = "",
                data = sortBooks
            },JsonRequestBehavior.AllowGet);
        }

        public ActionResult SortGenre(int? genreID)
        {
            List<Book> sortBooks = new List<Book>();
            foreach (Book bk in db.Books)
            {
                if (bk.Genres_ID == genreID && bk.Status==1)
                {
                    sortBooks.Add(new Book
                    {
                        id = bk.id,
                        Name = bk.Name,
                        Image = bk.Image,
                        PDF = bk.PDF,
                        TotalCount = bk.TotalCount,
                        BusyCount = bk.BusyCount,
                        Description = bk.Description,
                        Status = bk.Status
                    });
                }
            }
            return Json(new
            {
                status = 200,
                error = "",
                data = sortBooks
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckLogin()
        {
            if (Session["LoggedUser"] == null)
            {
                return Json(new
                {
                    status = 215,
                    error = "User is not sign in",
                    data = ""
                });
            }
            else
            {
                return Json(new
                {
                    status = 200,
                    error = "",
                    data = ""
                });
            }
        }

        public ActionResult CheckIsRead(int bookID)
        {
            Reader curReader = Session["LoggedUser"] as Reader;
            if(db.Book_To_Readers.Any(btr=>btr.Reader_ID == curReader.id && btr.Book_ID == bookID && btr.Status == 1))
            {
                return Json(new
                {
                    status = 215,
                    error = "This book alredy bought",
                    data = ""
                });
            }
            else
            {
                return Json(new
                {
                    status = 200,
                    error = "",
                    data = ""
                });
            }
        }
        public ActionResult CheckLimit()
        {
            Reader curReader = Session["LoggedUser"] as Reader;
            var limit = 0;
            foreach (Book_To_Readers btr in db.Book_To_Readers)
            {
                if(btr.Reader_ID == curReader.id)
                {
                    limit++;
                }
            }
            if (limit < 5)
            {
                return Json(new
                {
                    status = 200,
                    error = "",
                    data = ""
                });
            }
            else
            {
                return Json(new
                {
                    status = 215,
                    error = "Your basket is full",
                    data = ""
                });
            }
        }
        public ActionResult CheckBookCount(int bookID)
        {
            Book selectedBook = db.Books.FirstOrDefault(bk=>bk.id==bookID && bk.Status ==1);
            if(selectedBook.TotalCount - selectedBook.BusyCount > 0)
            {
                return Json(new
                {
                    status = 200,
                    error = "",
                    data = ""
                });
            }
            else
            {
                return Json(new
                {
                    status = 215,
                    error = "Bu kitabdan qalmayib",
                    data = ""
                });
            }
        }
        public ActionResult AddBook(int bookID)
        {
            Book selectedBook = db.Books.FirstOrDefault(bk => bk.id == bookID && bk.Status == 1);
            selectedBook.BusyCount++;
            db.SaveChanges();

            Reader curReader = Session["LoggedUser"] as Reader;
            db.Book_To_Readers.Add(new Book_To_Readers
            {
               Book_ID = selectedBook.id,
               Reader_ID = curReader.id,
               Status = 1
            });
            db.SaveChanges();
            return Json(new
            {
                status = 200,
                error = "",
                data = ""
            });
        }
        public ActionResult GiveBack(int bookID)
        {
            Reader curReader = Session["LoggedUser"] as Reader;
            Book selectedBook = db.Books.FirstOrDefault(bk => bk.id == bookID);
            Book_To_Readers selectedRow = db.Book_To_Readers.FirstOrDefault(btr => btr.Book_ID == bookID && btr.Reader_ID == curReader.id);
            db.Book_To_Readers.Remove(selectedRow);
            selectedBook.BusyCount--;
            db.SaveChanges();

            return Json(new
            {
                status = 200,
                error = "",
                data = ""
            });
        }
        public ActionResult BookDetails(int bookID)
        {
            //Kitabi Tapdim Ashagida
            Book selectedBook = db.Books.FirstOrDefault(bk => bk.id == bookID && bk.Status == 1);
            //Muellif Listi Yaradiram asagida
            List<Author> ListAuthors = new List<Author>();
            //Kitab Muellif cedvelinden secilen kitablar olan rowlari tapiram foreachin icinde
            List<Book_To_Authors> ListBta = new List<Book_To_Authors>();
            //secilen rowlari yaratdigim muellif kitab tipinde olan liste elave edirem
            foreach (Book_To_Authors bta in db.Book_To_Authors)
            {
                if(bta.Book_ID == selectedBook.id)
                {
                    //burada elave edirem
                    ListBta.Add(bta);
                }
            }
            if(ListBta.Count == 0)
            {
                ListAuthors.Add(new Author
                {
                    Name = "Book's Author is",
                    Lastname = "anonumys",
                    Image = null,
                    Status = 1
                });
            }
            else
            {
                foreach (var item in ListBta)
                {
                    if (item.Book_ID == selectedBook.id)
                    {
                        ListAuthors.Add(new Author
                        {
                            Name = item.Author.Name,
                            Lastname = item.Author.Lastname,
                            Image = item.Author.Image,
                            Status = item.Author.Status
                        });
                    }
                }
            }
            
            Book_To_Authors selectedRow = db.Book_To_Authors.FirstOrDefault(bta => bta.Book_ID == selectedBook.id && bta.Status == 1);
            List<Book> ListBooks = new List<Book>();

            ListBooks.Add(new Book
            {
                id = selectedBook.id,
                Name = selectedBook.Name,
                Image = selectedBook.Image,
                PDF = selectedBook.PDF,
                TotalCount = selectedBook.TotalCount,
                BusyCount = selectedBook.BusyCount,
                Description = selectedBook.Description,
                Status = selectedBook.Status
            });
            var result = new { BookList = ListBooks, AuthorList = ListAuthors };
            //return Json(result, JsonRequestBehavior.AllowGet);

            return Json(new
            {
                status = 200,
                error = "",
                data = result
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Search(string text)
        {
            List<Book> selectedBooks = new List<Book>();
            foreach (Book bk in db.Books)
            {
                if (bk.Name.ToLower().Contains(text.ToLower()))
                {
                    selectedBooks.Add(new Book
                    {
                        id = bk.id,
                        Name = bk.Name,
                        Image = bk.Image,
                        PDF = bk.PDF,
                        TotalCount = bk.TotalCount,
                        BusyCount = bk.BusyCount,
                        Description = bk.Description,
                        Status = bk.Status
                    });
                }
            }
            return Json(new
            {
                status = 200,
                error = "",
                data = selectedBooks
            }, JsonRequestBehavior.AllowGet);
        }

    }
}