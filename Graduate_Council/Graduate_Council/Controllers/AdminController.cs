using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Graduate_Council.BLL;
using Graduate_Council.Model;
using System.IO;
namespace Graduate_Council.Controllers
{
    public class AdminController : BaseController
    {
        NewInfoService newInfoService = new NewInfoService();
        LinkInfoService linkInfoService = new LinkInfoService();
        BannerImgService bannerImgService = new BannerImgService();
        UserInfoService userInfoService = new UserInfoService();
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
            int pageSize = 10;
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
            if (Request["IsDisplay"] != null)
            {
                if (Request["IsDisplay"].ToString() == "on")
                {
                    newInfo.IsDisplay = true;
                }
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
            if (Request["IsDisplay"] != null)
            {
                if (Request["IsDisplay"].ToString() == "on")
                {
                    newInfo.IsDisplay = true;
                }
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
        public ActionResult LinkManage()
        {
            int pageIndex = Convert.ToInt32(Request["pageIndex"]);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            int pageSize = 15;
            int pageCount = linkInfoService.GetPageCount(pageSize);
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<LinkInfo> list = linkInfoService.GetLinkList(pageIndex, pageSize);
            ViewData["list"] = list;
            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pagePre"] = (pageIndex - 1) < 1 ? 1 : (pageIndex - 1);
            ViewData["pageNext"] = (pageIndex + 1) > pageCount ? pageCount : (pageIndex + 1);
            return View();
        }
        [HttpGet]
        public ActionResult AddLink()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddLink(LinkInfo linkInfo)
        {
            if (string.IsNullOrEmpty(linkInfo.Name))
            {
                return Content("请链接名称");
            }
            if (string.IsNullOrEmpty(linkInfo.Link))
            {
                return Content("请输入链接地址");
            }
            if (string.IsNullOrEmpty(linkInfo.Category))
            {
                return Content("请选择链接类别");
            }
            if (linkInfoService.AddLink(linkInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        public ActionResult EditLink()
        {
            int id = Convert.ToInt32(Request["Id"]);
            id = id < 1 ? 1 : id;
            LinkInfo linkInfo = linkInfoService.GetLink(id);
            ViewData.Model = linkInfo;
            return View();
        }
        public ActionResult UpdateLink(LinkInfo linkInfo)
        {
            if (string.IsNullOrEmpty(linkInfo.Name))
            {
                return Content("请链接名称");
            }
            if (string.IsNullOrEmpty(linkInfo.Link))
            {
                return Content("请输入链接地址");
            }
            if (string.IsNullOrEmpty(linkInfo.Category))
            {
                return Content("请选择链接类别");
            }
            if (linkInfoService.UpdateLink(linkInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        public ActionResult DeleteLink()
        {
            int id = Convert.ToInt32(Request["Id"]);
            if (linkInfoService.DeleteLink(id))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        public ActionResult BannerManage()
        {
            int pageIndex = Convert.ToInt32(Request["pageIndex"]);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            int pageSize = 6;
            int pageCount = bannerImgService.GetPageCount(pageSize);
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<BannerImg> list = bannerImgService.GetBannerList(pageIndex, pageSize);
            ViewData["list"] = list;
            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pagePre"] = (pageIndex - 1) < 1 ? 1 : (pageIndex - 1);
            ViewData["pageNext"] = (pageIndex + 1) > pageCount ? pageCount : (pageIndex + 1);
            return View();
        }
        public ActionResult DeleteBanner()
        {
            int id = Convert.ToInt32(Request["Id"]);
            if (bannerImgService.DeleteBanner(id))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        public ActionResult EditBanner()
        {
            int id = Convert.ToInt32(Request["Id"]);
            id = id < 1 ? 1 : id;
            BannerImg bannerImg = bannerImgService.GetBanner(id);
            ViewData.Model = bannerImg;
            return View();
        }
        #region 上传文件
        public ActionResult FileUpload()
        {
            HttpPostedFileBase postFile = Request.Files["uploadImg"];
            if (postFile == null)
            {
                return Content("no:请选择文件！");
            }
            else
            {
                string fileName = Path.GetFileName(postFile.FileName);
                string fileExt = Path.GetExtension(fileName);
                if ((fileExt == ".jpg")||(fileExt == ".png"))
                {
                    //string dir = "/Img/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                    //Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));//创建文件夹
                    string newFileName = Guid.NewGuid().ToString();
                    string fullDir = "/images/banner/" + newFileName + fileExt;//文件的完整路径
                    postFile.SaveAs(Request.MapPath(fullDir));
                    return Content("ok:" + fullDir);
                }
                else
                {
                    return Content("no:文件格式不正确！");
                }
            }
        }
        #endregion
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
                if ((fileExt == ".doc") || (fileExt == ".docx")||(fileExt == ".zip") ||(fileExt == ".rar") ||(fileExt == ".xls") ||(fileExt == ".xlsx"))
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

        public ActionResult UpdateBanner(BannerImg bannerImg)
        {
            if (Request["IsVisible"].ToString() == "on")
            {
                bannerImg.IsVisible = true;
            }
            if (bannerImg.Path == null)
            {
                bannerImg.Path = Request["Path1"].ToString();
            }
            if (bannerImgService.UpdateBanner(bannerImg))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        [HttpGet]
        public ActionResult AddBanner()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBanner(BannerImg bannerImg)
        {
            if (Request["IsVisible"]!=null)
            {
                if (Request["IsVisible"].ToString() == "on")
                {
                    bannerImg.IsVisible = true;
                }
            }
            if (bannerImg.Path == null)
            {
                return Content("请先选择图片并上传！");
            }
            if (bannerImgService.AddBanner(bannerImg))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        public ActionResult UserManage()
        {            
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
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(UserInfo userInfo)
        {
            if (string.IsNullOrEmpty(userInfo.Name))
            {
                return Content("请输入用户名");
            }
            if (string.IsNullOrEmpty(userInfo.Password))
            {
                return Content("请输入密码");
            }
            if (userInfo.Password != Request["RePassword"].ToString())
            {
                return Content("两次输入的密码不一样");
            }
            if (Request["Admin"] != null)
            {
                if (Request["Admin"].ToString() == "on")
                {
                    userInfo.Admin = true;
                }
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
            int id = Convert.ToInt32(Request["Id"]);
            id = id < 1 ? 1 : id;
            UserInfo userInfo = userInfoService.GetUser(id);
            ViewData.Model = userInfo;
            return View();
        }
        public ActionResult UpdatePassword(UserInfo userInfo)
        {
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
