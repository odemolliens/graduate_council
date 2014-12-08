﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Graduate_Council.BLL;
using Graduate_Council.Model;
namespace Graduate_Council.Controllers
{
    public class AdminController : Controller
    {
        NewInfoService newInfoService = new NewInfoService();
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            ViewData["Name"] = "CouncilDynamicNews";
            ViewData["Msg"] = "研会动态";
            return View();
        }
        public ActionResult ReturnTopView()
        {
            return View();
        }
        public ActionResult ReturnLeftView()
        {
            return View();
        }
        /// <summary>
        /// 返回新闻列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnRightView()
        {
            string tableName = "T_" + Request["Name"];
            ViewData["Msg"] = Request["Msg"];
            int pageIndex = Convert.ToInt32(Request["PageIndex"]);
            int pageSize = 5;
            int pageCount = newInfoService.GetPageCount(pageSize,tableName);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<NewInfo> list = newInfoService.GetPageList(pageIndex, pageSize,tableName);
            ViewData["Name"] = Request["Name"];
            ViewData["list"] = list;
            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pagePre"] = (pageIndex - 1) < 1 ? 1 : (pageIndex - 1);
            ViewData["pageNext"] = (pageIndex + 1) > pageCount ? pageCount : (pageIndex + 1);
            return View();
        }
        public ActionResult DeleteNewInfo()
        {
            string tableName = "T_" + Request["Name"];
            int id = Convert.ToInt32(Request["id"]);
            if (newInfoService.DeleteNewInfo(id, tableName))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        [HttpGet]
        public ActionResult AddNewInfo()
        {
            ViewData["Name"] = Request["Name"];
            ViewData["Msg"] = Request["Msg"];
            return View();
        }
        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="newInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewInfo(NewInfo newInfo)
        {
            string tableName = "T_" + Request["Name"];
            newInfo.PageView = 0;
            if (newInfo.Title == null)
            {
                return Content("请输入标题");
            }
            if (string.IsNullOrEmpty(newInfo.Author))
            {
                return Content("请输入来源");
            }
            if (string.IsNullOrEmpty(newInfo.Detail))
            {
                return Content("请输入新闻内容");
            }
            if (newInfo.SubDateTime == new DateTime(1,1,1,0,0,0))
            {
                return Content("请输入并且正确输入日期,时间格式"+DateTime.Now.ToString());
            }
            if (newInfoService.AddNewInfo(newInfo, tableName))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }

        }

        public ActionResult EditNew()
        {
            string tableName = "T_" + Request["Name"];
            ViewData["Name"] = Request["Name"];
            ViewData["Msg"] = Request["Msg"];
            int id = Convert.ToInt32(Request["Id"]);
            NewInfo newInfo = newInfoService.GetNewInfo(id,tableName);
            ViewData.Model = newInfo;
            return View();           
        }
        [ValidateInput(false)]
        public ActionResult UpdateNewInfo(NewInfo newInfo)
        {
            string tableName = "T_" + Request["Name"];           
            if (newInfo.Title == null)
            {
                return Content("请输入标题");
            }
            if (string.IsNullOrEmpty(newInfo.Author))
            {
                return Content("请输入来源");
            }
            if (string.IsNullOrEmpty(newInfo.Detail))
            {
                return Content("请输入新闻内容");
            }
            if (newInfo.SubDateTime == new DateTime(1, 1, 1, 0, 0, 0))
            {
                return Content("请输入并且正确输入日期,时间格式" + DateTime.Now.ToString());
            }
            if (newInfoService.UpdateNewInfo(newInfo, tableName))
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
