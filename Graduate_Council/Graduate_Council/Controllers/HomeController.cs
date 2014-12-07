using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Graduate_Council.BLL;
using Graduate_Council.Model;

namespace Graduate_Council.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        NewInfoService newInfoService = new NewInfoService();
        public ActionResult Index()
        {
            List<NewInfo> list = newInfoService.GetPageList(1, 6);
            ViewData["list"] = list;
            return View();
        }

        public ActionResult DisplayNew()
        {
            return View();
        }

    }
}
