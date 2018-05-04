using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using Asp.Net_Final.Models;


namespace Asp.Net_Final.Controllers
{
    public class LoginController : Controller
    {
        Entities db = new Entities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Email , string Password)
        {
            Reader currentReader = db.Readers.FirstOrDefault(r => r.Email == Email && r.Password == Password);
            if(currentReader != null)
            {
                Session["LoggedUser"] = currentReader;
                Session["ReaderBasket"] = new List<object>();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Email or Password is NOT correct";
                return View();
            }
        }

        public ActionResult LogOut()
        {
            Session["LoggedUser"] = null;
            Session["ReaderBasket"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}