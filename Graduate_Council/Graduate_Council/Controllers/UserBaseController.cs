using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graduate_Council.Controllers
{
    public class UserBaseController : Controller
    {
        //
        // GET: /UserBase/

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserInfo"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/Login/Index");
            }            
            base.OnActionExecuting(filterContext);
        }

    }
}
