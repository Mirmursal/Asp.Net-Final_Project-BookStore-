using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.Net_Final.Models;

namespace Asp.Net_Final.Areas.Admin.Controllers
{
   
    public class LoginController : Controller
    {
        Entities db = new Entities();
        // GET: Admin/Login
        public ActionResult Index()
        {
            Session["LoggedAdmin"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Email , string Pass)
        {
          AdminSetting curAdmin = db.AdminSettings.FirstOrDefault(a=>a.Email == Email && a.Password == Pass);
            if(curAdmin != null)
            {
                Session["LoggedAdmin"] = curAdmin;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Email or Password is NOT correct";
                return View();
            }
        }
    }
}