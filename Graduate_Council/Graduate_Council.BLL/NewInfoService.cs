using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graduate_Council.Model;
using Graduate_Council.DAL;

namespace Graduate_Council.BLL
{
    public class NewInfoService
    {
        NewInfoDal newInfoDal = new NewInfoDal();
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageIndex">当页页码值</param>
        /// <param name="pageSize">每页显示的记录</param>
        /// <returns></returns>
        public List<NewInfo> GetPageList(int pageIndex, int pageSize,string tableName)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageSize * pageIndex;
            List<NewInfo> list = newInfoDal.GetPageList(start, end, tableName);
            return list;
        }
        public List<NewInfo> GetPageListByCat(int pageIndex, int pageSize, string tableName,string category)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageSize * pageIndex;
            List<NewInfo> list = newInfoDal.GetPageListByCat(start, end, tableName, category);
            return list;
        }

        public int GetPageCount(int pageSize,string tableName)
        {
            int recordCount = newInfoDal.GetRecordCount(tableName);
            int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        }
        public int GetPageCountByCat(int pageSize, string tableName,string category)
        {
            int recordCount = newInfoDal.GetRecordCountByCat(tableName,category);
            int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        }

        public NewInfo GetNewInfo(int id,string tableName)
        {
            return newInfoDal.GetNewInfo(id, tableName);
        }
        public bool DeleteNewInfo(int id, string tableName)
        {
            return newInfoDal.DeleteNewInfo(id, tableName) > 0;
        }
        public bool AddNewInfo(NewInfo newInfo, string tableName)
        {
            return newInfoDal.AddNewInfo(newInfo, tableName) > 0;
        }

        public bool UpdateNewInfo(NewInfo newInfo, string tableName)
        {
            return newInfoDal.UpdateNewInfo(newInfo, tableName) > 0;
        }
        public void UpdatePageView(int pageView, string tableName,int id)
        {
            newInfoDal.UpdatePageView(pageView, tableName,id);
            return;
        }
    }
}
