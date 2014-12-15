using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graduate_Council.Model;

namespace Graduate_Council.BLL
{
    public class BannerImgService
    {      
        DAL.BannerImgDal bannerImgDal = new DAL.BannerImgDal();
        public int GetPageCount(int pageSize)
        {
            int recordCount = bannerImgDal.GetRecordCount();
            int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        }
        public List<BannerImg> GetBannerList(int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<BannerImg> list = bannerImgDal.GetBannerList(start, end);
            return list;
        }
        public bool AddBanner(BannerImg bannerImg)
        {
            return bannerImgDal.AddBanner(bannerImg) > 0;
        }
        public bool UpdateBanner(BannerImg bannerImg)
        {
            return bannerImgDal.UpdateBanner(bannerImg) > 0;
        }
        public bool DeleteBanner(int id)
        {
            return bannerImgDal.DeleteBanner(id) > 0;
        }
        public BannerImg GetBanner(int id)
        {
            return bannerImgDal.GetBanner(id);
        }
        public List<BannerImg> GetIndexBannerList()
        {
            return bannerImgDal.GetIndexBannerList();
        }
    }
}
