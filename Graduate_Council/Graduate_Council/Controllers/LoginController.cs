using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Graduate_Council.BLL;
using Graduate_Council.Model;

namespace Graduate_Council.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        UserInfoService userInfoService = new UserInfoService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserLogin()
        {
            string userName = Request["UserName"];
            string password = Request["Password"];
            if (string.IsNullOrEmpty(userName))
            {
                return Content("no:用户名不能为空！");
            }
            if (string.IsNullOrEmpty(password))
            {
                return Content("no:密码不能为空！");
            }            
            UserInfo userInfo = userInfoService.GetUserInfo(userName, password);
            if (userInfo != null)
            {
                Session["UserInfo"] = userInfo;
                return Content("ok:登陆成功！");
            }
            else
            {
                return Content("no:用户名或密码错误！");
            }
        }
        public ActionResult UserManage()
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("/Login/Index");
            }
            int pageIndex = Convert.ToInt32(Request["pageIndex"]);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            int pageSize = 5;
            int pageCount = userInfoService.GetPageCount(pageSize);
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<UserInfo> list = userInfoService.GetUserList(pageIndex, pageSize);
            ViewData["list"] = list;
            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pagePre"] = (pageIndex - 1) < 1 ? 1 : (pageIndex - 1);
            ViewData["pageNext"] = (pageIndex + 1) > pageCount ? pageCount : (pageIndex + 1);
            return View();
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("/Login/Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(UserInfo userInfo)
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("/Login/Index");
            }
            if (string.IsNullOrEmpty(userInfo.Name))
            {
                return Content("请输入用户名");
            }
            if (string.IsNullOrEmpty(userInfo.Password))
            {
                return Content("请输入密码");
            }
            if (userInfo.Password!=Request["RePassword"].ToString())
            {
                return Content("两次输入的密码不一样");
            }
            if (userInfoService.AddUser(userInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        public ActionResult EditUser()
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("/Login/Index");
            }
            int id = Convert.ToInt32(Request["Id"]);
            id = id < 1 ? 1 : id;
            UserInfo userInfo = userInfoService.GetUser(id);
            ViewData.Model = userInfo;
            return View();
        }
        public ActionResult UpdatePassword(UserInfo userInfo)
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("/Login/Index");
            }
            if (string.IsNullOrEmpty(userInfo.Password))
            {
                return Content("请输入密码");
            }
            if (userInfo.Password != Request["RePassword"].ToString())
            {
                return Content("两次输入的密码不一样");
            }
            if (userInfoService.UpdatePassword(userInfo.Password, userInfo.Id))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        public ActionResult DeleteUser()
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("/Login/Index");
            }
            int id = Convert.ToInt32(Request["Id"]);
            if (userInfoService.DeleteUser(id))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

    }
}
