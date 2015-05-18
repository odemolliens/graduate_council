﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Graduate_Council.BLL;
using Graduate_Council.Model;
using System.IO;

namespace Graduate_Council.Controllers
{
    public class UserController : UserBaseController
    {
        //
        // GET: /User/

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
            int pageCount = newInfoService.GetPageCount(pageSize, tableName);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<NewInfo> list = newInfoService.GetPageList(pageIndex, pageSize, tableName);
            ViewData["Name"] = Request["Name"];
            ViewData["list"] = list;
            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pagePre"] = (pageIndex - 1) < 1 ? 1 : (pageIndex - 1);
            ViewData["pageNext"] = (pageIndex + 1) > pageCount ? pageCount : (pageIndex + 1);
            return View();
        }
        [HttpGet]
        public ActionResult AddNewInfo()
        {
            ViewData["Name"] = Request["Name"];
            ViewData["Msg"] = Request["Msg"];
            return View();
        }
        public ActionResult AddDataDownload()
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
            newInfo.IsDisplay = false;
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
            NewInfo newInfo = newInfoService.GetNewInfo(id, tableName);
            ViewData.Model = newInfo;
            return View();
        }
                 
        public ActionResult FileUploadData()
        {
            HttpPostedFileBase postFile = Request.Files["uploadData"];
            if (postFile == null)
            {
                return Content("no:请选择文件！");
            }
            else
            {
                string fileName = Path.GetFileName(postFile.FileName);
                string fileExt = Path.GetExtension(fileName);
                if ((fileExt == ".doc") || (fileExt == ".docx") || (fileExt == ".zip") || (fileExt == ".rar") || (fileExt == ".xls") || (fileExt == ".xlsx"))
                {
                    string dir = "/DataDownload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));//创建文件夹
                    string fullDir = dir + fileName;//文件的完整路径
                    postFile.SaveAs(Request.MapPath(fullDir));
                    return Content("ok:" + fullDir);
                }
                else
                {
                    return Content("no:文件格式不正确！");
                }
            }
        }       
               
    }
}