﻿using System;
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
                if (userInfo.Admin)
                {
                    return Content("ok1:登陆成功！");
                }
                else
                {
                    return Content("ok2:登陆成功！");
                }
            }
            else
            {
                return Content("no:用户名或密码错误！");
            }
        }        
    }
}
