using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graduate_Council.Model;
using System.Data;
using System.Data.SqlClient;


namespace Graduate_Council.DAL
{
    public class BannerImgDal
    {
        public void LoadEntity(BannerImg bannerImg, DataRow dr)
        {
            bannerImg.Id = Convert.ToInt32(dr["Id"]);
            bannerImg.Path = dr["Path"].ToString();//dr["Name"]!=DBNull.Value ? dr["Name"].ToString():string.Empty
            bannerImg.IsVisible = Convert.ToBoolean(dr["IsVisible"]);
        }

        public List<BannerImg> GetBannerList(int start, int end)
        {
            string sql = "select * from(select row_number() over (order by Id) as num, * from T_BannerImg)as t where t.num>=@start and t.num<=@end";
            SqlParameter[] pars = {new SqlParameter("@start",start),
                                  new SqlParameter("@end",end)};
            List<BannerImg> list = null;
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, pars);
            if (dt.Rows.Count > 0)
            {
                list = new List<BannerImg>();
                BannerImg bannerImg = null;
                foreach (DataRow dr in dt.Rows)
                {
                    bannerImg = new BannerImg();
                    LoadEntity(bannerImg, dr);
                    list.Add(bannerImg);
                }
            }
            return list;
        }

        public int DeleteBanner(int id)
        {
            string sql = "delete from T_BannerImg where Id=@Id";
            SqlParameter par = new SqlParameter("@Id", DbType.Int32);
            par.Value = id;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, par);
        }
        public int UpdateBanner(BannerImg bannerImg)
        {
            string sql = "update T_BannerImg set Path=@Path,IsVisible=@IsVisible where Id=@Id";
            SqlParameter[] pars = {new SqlParameter("@Id",DbType.Int32),
                                  new SqlParameter("@Path",SqlDbType.VarChar,200),
                                  new SqlParameter("@IsVisible",SqlDbType.Bit)};
            pars[0].Value = bannerImg.Id;
            pars[1].Value = bannerImg.Path;
            pars[2].Value = bannerImg.IsVisible;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);

        }
        public int AddBanner(BannerImg bannerImg)
        {
            string sql = "insert into T_BannerImg(Path,IsVisible) values(@Path,@IsVisible)";
            SqlParameter[] pars = {
                                  new SqlParameter("@Path",SqlDbType.VarChar,200),
                                  new SqlParameter("@IsVisible",SqlDbType.Bit)};
            pars[0].Value = bannerImg.Path;
            pars[1].Value = bannerImg.IsVisible;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);

        }
        public int GetRecordCount()
        {
            string sql = "select count(*) from T_BannerImg";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
        }
        public BannerImg GetBanner(int id)
        {
            string sql = "select * from T_BannerImg where Id=@Id";
            SqlParameter par = new SqlParameter("@Id", SqlDbType.Int);
            par.Value = id;
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, par);
            BannerImg bannerImg = null;
            if (dt.Rows.Count > 0)
            {
                bannerImg = new BannerImg();
                LoadEntity(bannerImg, dt.Rows[0]);
            }
            return bannerImg;
        }
        /// <summary>
        /// 返回要显示的图片
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<BannerImg> GetIndexBannerList()
        {
            string sql = "select * from T_BannerImg where IsVisible=@IsVisible";
            SqlParameter pars = new SqlParameter("@IsVisible", SqlDbType.Bit);
            pars.Value = true;
            List<BannerImg> list = null;
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, pars);
            if (dt.Rows.Count > 0)
            {
                list = new List<BannerImg>();
                BannerImg bannerImg = null;
                foreach (DataRow dr in dt.Rows)
                {
                    bannerImg = new BannerImg();
                    LoadEntity(bannerImg, dr);
                    list.Add(bannerImg);
                }
            }
            return list;
        }
    }
}
