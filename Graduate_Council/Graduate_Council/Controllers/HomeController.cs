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
        LinkInfoService linkInfoService = new LinkInfoService();
        BannerImgService bannerImgService = new BannerImgService();
        public ActionResult Index()
        {

            List<NewInfo> list4 = newInfoService.GetPageList(1, 6, "T_CouncilDynamicNews");
            List<NewInfo> list1 = newInfoService.GetPageList(1, 6, "T_FrontNews");
            List<NewInfo> list2 = newInfoService.GetPageList(1, 6, "T_NoticeNews");
            List<NewInfo> list3 = newInfoService.GetPageList(1, 6, "T_CampusEssay");
            List<NewInfo> list5 = newInfoService.GetPageList(1, 6, "T_BranchNews");
            List<NewInfo> list6 = newInfoService.GetPageList(1, 6, "T_JobNews");
            List<LinkInfo> listInfo = linkInfoService.Top15Link();
            List<BannerImg> bannerList = bannerImgService.GetIndexBannerList();
            ViewData["listInfo"] = listInfo;
            ViewData["bannerList"] = bannerList;
            ViewData["list1"] = list1;
            ViewData["list2"] = list2;
            ViewData["list3"] = list3;
            ViewData["list4"] = list4;
            ViewData["list5"] = list5;
            ViewData["list6"] = list6;
            return View();
        }

        public ActionResult DisplayNews()
        {
            List<BannerImg> bannerList = bannerImgService.GetIndexBannerList();            
            Random random = new Random();
            int flag = random.Next(0, bannerList.Count);
            if (bannerList != null)
            {
                ViewData["banner"] = bannerList[flag].Path;
            }
            List<LinkInfo> listInfo = linkInfoService.Top15Link();
            ViewData["listInfo"] = listInfo;
            int id = Convert.ToInt32(Request["Id"]);
            string tableName = "T_" + Request["Name"];
            NewInfo newInfo = newInfoService.GetNewInfo(id, tableName);
            int pageView = ++newInfo.PageView;
            newInfoService.UpdatePageView(pageView, tableName, id);
            ViewData.Model = newInfo;
            ViewData["Name"] = Request["Name"];
            return View();
        }
        public ActionResult DisplayList()
        {
            List<BannerImg> bannerList = bannerImgService.GetIndexBannerList();
            Random random = new Random();
            int flag = random.Next(0, bannerList.Count);
            if (bannerList != null)
            {
                ViewData["banner"] = bannerList[flag].Path;
            }
            List<LinkInfo> listInfo = linkInfoService.Top15Link();
            ViewData["listInfo"] = listInfo;
            string tableName = "T_" + Request["Name"];
            ViewData["Name"] = Request["Name"];
            int pageIndex = Convert.ToInt32(Request["pageIndex"]) < 1?1:Convert.ToInt32(Request["pageIndex"]);
            int pageSize = 2;
            if (string.IsNullOrEmpty(Request["Cat"]))
            {
                int pageCount = newInfoService.GetPageCount(pageSize, tableName);
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                List<NewInfo> list = newInfoService.GetPageList(pageIndex, pageSize, tableName);                                
                ViewData["list"] = list;
                ViewData["pageCount"] = pageCount;
                ViewData["pageIndex"] = pageIndex;
                ViewData["pagePre"] = (pageIndex - 1) < 1 ? 1 : (pageIndex - 1);
                ViewData["pageNext"] = (pageIndex + 1) > pageCount ? pageCount : (pageIndex + 1);
            }
            else
            {
                int cat = Convert.ToInt32(Request["Cat"]) < 1 ? 1 : Convert.ToInt32(Request["Cat"]);
                cat--;
                string category = NewsCategory.newsCategory[Request["Name"].ToString()][cat];
                int pageCount = newInfoService.GetPageCountByCat(pageSize, tableName, category);
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                List<NewInfo> list = newInfoService.GetPageListByCat(pageIndex, pageSize, tableName,category);
                ViewData["Cat"] = Request["Cat"];
                ViewData["list"] = list;
                ViewData["pageCount"] = pageCount;
                ViewData["pageIndex"] = pageIndex;
                ViewData["pagePre"] = (pageIndex - 1) < 1 ? 1 : (pageIndex - 1);
                ViewData["pageNext"] = (pageIndex + 1) > pageCount ? pageCount : (pageIndex + 1);
            }
            return View();
        }
        public ActionResult DisplayDownloadList()
        {
            List<BannerImg> bannerList = bannerImgService.GetIndexBannerList();
            Random random = new Random();
            int flag = random.Next(0, bannerList.Count);
            if (bannerList != null)
            {
                ViewData["banner"] = bannerList[flag].Path;
            }
            List<LinkInfo> listInfo = linkInfoService.Top15Link();
            ViewData["listInfo"] = listInfo;
            string tableName = "T_" + Request["Name"];
            ViewData["Name"] = Request["Name"];
            int pageIndex = Convert.ToInt32(Request["pageIndex"]) < 1 ? 1 : Convert.ToInt32(Request["pageIndex"]);
            int pageSize = 2;
            if (string.IsNullOrEmpty(Request["Cat"]))
            {
                int pageCount = newInfoService.GetPageCount(pageSize, tableName);
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                List<NewInfo> list = newInfoService.GetPageList(pageIndex, pageSize, tableName);
                ViewData["list"] = list;
                ViewData["pageCount"] = pageCount;
                ViewData["pageIndex"] = pageIndex;
                ViewData["pagePre"] = (pageIndex - 1) < 1 ? 1 : (pageIndex - 1);
                ViewData["pageNext"] = (pageIndex + 1) > pageCount ? pageCount : (pageIndex + 1);
            }
            else
            {
                int cat = Convert.ToInt32(Request["Cat"]) < 1 ? 1 : Convert.ToInt32(Request["Cat"]);
                cat--;
                string category = NewsCategory.newsCategory[Request["Name"].ToString()][cat];
                int pageCount = newInfoService.GetPageCountByCat(pageSize, tableName, category);
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                List<NewInfo> list = newInfoService.GetPageListByCat(pageIndex, pageSize, tableName, category);
                ViewData["Cat"] = Request["Cat"];
                ViewData["list"] = list;
                ViewData["pageCount"] = pageCount;
                ViewData["pageIndex"] = pageIndex;
                ViewData["pagePre"] = (pageIndex - 1) < 1 ? 1 : (pageIndex - 1);
                ViewData["pageNext"] = (pageIndex + 1) > pageCount ? pageCount : (pageIndex + 1);
            }
            return View();
        }
    }
}
