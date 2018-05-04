using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asp.Net_Final.Areas.Admin.Controllers
{
    public class AuthorizationController : AuthorizeAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }
            if (HttpContext.Current.Session["LoggedAdmin"] == null)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
            }
        }
    }
}