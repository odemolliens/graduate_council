using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graduate_Council.DAL;
using Graduate_Council.Model;

namespace Graduate_Council.BLL
{
    
    public class LinkInfoService
    {
        LinkInfoDal linkInfoDal = new LinkInfoDal();
        public int GetPageCount(int pageSize)
        {
            int recordCount = linkInfoDal.GetRecordCount();
            int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        }
        public List<LinkInfo> GetLinkList(int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<LinkInfo> list = linkInfoDal.GetLinkList(start, end);
            return list;
        }
        public bool AddLink(LinkInfo linkInfo)
        {
            return linkInfoDal.AddLink(linkInfo) > 0;
        }
        public LinkInfo GetLink(int id)
        {
            return linkInfoDal.GetLink(id);
        }
        public bool UpdateLink(LinkInfo linkInfo)
        {
            return linkInfoDal.UpdateLink(linkInfo) > 0;
        }
        public bool DeleteLink(int id)
        {
            return linkInfoDal.DeleteLink(id) > 0;
        }
        public List<LinkInfo> Top15Link()
        {
            List<LinkInfo> list = linkInfoDal.Top15List();
            return list;
        }
    }
}
