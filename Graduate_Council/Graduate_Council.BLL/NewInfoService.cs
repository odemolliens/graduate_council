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
        public List<NewInfo> GetPageList(int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageSize * pageIndex;
            List<NewInfo> list = newInfoDal.GetPageList(start, end);
            return list;
        }

        public int GetPageCount(int pageSize)
        {
            int recordCount = newInfoDal.GetRecordCount();
            int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        }

        public NewInfo GetNewInfo(int id)
        {
            return newInfoDal.GetNewInfo(id);
        }
        public bool DeleteNewInfo(int id)
        {
            return newInfoDal.DeleteNewInfo(id)>0;
        }
    }
}
