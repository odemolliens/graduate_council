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
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        NewInfoService newInfoService = new NewInfoService();
        #region 分页列表
        public ActionResult Index()
        {
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 2;
            int pageCount = newInfoService.GetPageCount(pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<NewInfo> list = newInfoService.GetPageList(pageIndex, pageSize);
            ViewData["list"] = list;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            return View();
        }
        #endregion

        #region 获取新闻详细信息
        public ActionResult GetNewDetail()
        {
            int id = Convert.ToInt32(Request["id"]);
            NewInfo newInfo = newInfoService.GetNewInfo(id);
            return Json(newInfo, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 删除新闻
        public ActionResult DeleteNewInfo()
        {
            int id = int.Parse(Request["id"]);
            if (newInfoService.DeleteNewInfo(id))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 添加新闻
        public ActionResult ShowAddInfo()
        {
            return View();
        }
        #endregion

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
                if (fileExt == ".jpg")
                {
                    string dir = "/Img/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));//创建文件夹
                    string newFileName = Guid.NewGuid().ToString();
                    string fullDir = dir + newFileName + fileExt;//文件的完整路径
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

    }
}
