using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Asp.Net_Final.Filters;
using System.Web.Helpers;
using Asp.Net_Final.Models;
using Asp.Net_Final.ViewModels;

namespace Asp.Net_Final.Controllers
{
    [AuthorizationController]
    public class HomeController : Controller
    {
        Entities db = new Entities();
        [AllowAnonymous]
        public ActionResult Index()
        {
            HomeIndexViewModel vm = new HomeIndexViewModel();
            vm.bookList = db.Books.ToList();
            vm.authorList = db.Authors.ToList();
            vm.genreList = db.Genres.ToList();
            return View(vm);
        }
        //[HttpPost]
        //public ActionResult Index(int id)
        //{
        //    return View();
        //}
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Reader()
        {
            HomeReaderViewModel vm = new HomeReaderViewModel();
            Reader curReader = Session["LoggedUser"] as Reader;
            vm.readerBookList = db.Book_To_Readers.Where(br => br.Status == 1 && br.Reader.id == curReader.id).ToList();
            return View(vm);
        }
        public ActionResult Read(int? id)
        {
            Book book = db.Books.Find(id);

            return File("~/Public/PDF/" + book.PDF, "application/pdf");
        }
    }
}