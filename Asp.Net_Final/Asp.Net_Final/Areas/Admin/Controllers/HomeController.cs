using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.Net_Final.Filters;
using Asp.Net_Final.Models;
namespace Asp.Net_Final.Areas.Admin.Controllers
{
    [AuthorizationController]
    public class HomeController : Controller
    {
        Entities db = new Entities();
        // GET: Admin/Home
        [AuthorizationController]
        public ActionResult Index()
        {
            return View();
        }
    }
}