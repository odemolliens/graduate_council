using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graduate_Council.Model;
using System.Data;
using System.Data.SqlClient;

namespace Graduate_Council.DAL
{
    public class NewInfoDal
    {
        /// <summary>
        /// 获取新闻集合
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<NewInfo> GetPageList(int start, int end)
        {
            string sql = "select * from (select row_number() over (order by id) as num, * from T_CouncilDynamicNews) as t where t.num>=@start and t.num<=@end";
            SqlParameter[] pars = { new SqlParameter("@start", start), new SqlParameter("@end", end) };
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<NewInfo> newlist = null;
            if (dt.Rows.Count > 0)
            {
                newlist = new List<NewInfo>();
                NewInfo newInfo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    newInfo = new NewInfo();
                    LoadEntity(dr, newInfo);
                    newlist.Add(newInfo);
                }
            }
            return newlist;

        }

        private void LoadEntity(DataRow dr, NewInfo newInfo)
        {
            newInfo.Id = Convert.ToInt32(dr["Id"]);
            newInfo.Title = dr["Title"].ToString();
            newInfo.Author = dr["Author"].ToString();
            newInfo.SubDateTime = Convert.ToDateTime(dr["Date"]);
            newInfo.Detail = dr["Detail"].ToString();
            newInfo.Category = Convert.ToInt32(dr["Category"]);
            newInfo.PageView = Convert.ToInt32(dr["PageView"]);
        }
        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount()
        {
            string sql = "select count(*) from T_CouncilDynamicNews";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
        }
        /// <summary>
        /// 获取一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NewInfo GetNewInfo(int id)
        {
            string sql = "select * from T_CouncilDynamicNews where Id = @id";
            SqlParameter pars = new SqlParameter("@id", DbType.Int32);
            pars.Value = id;
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, pars);
            NewInfo newInfo = null;
            if (dt.Rows.Count > 0)
            {
                newInfo = new NewInfo();
                LoadEntity(dt.Rows[0], newInfo);
            }
            return newInfo;
        }
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteNewInfo(int id)
        {
            string sql = "delete from T_CouncilDynamicNews where Id = @id";
            SqlParameter par = new SqlParameter("@id", DbType.Int32);
            par.Value = id;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, par);
        }
        /// <summary>
        /// 新增新闻
        /// </summary>
        /// <param name="newInfo"></param>
        /// <returns></returns>
        public int AddNewInfo(NewInfo newInfo)
        {
            string sql = "insert into T_CouncilDynamicNews(Title,Author,Detail,Date,Category,PageView) values(@Title,@Author,@Detail,@Date,@Category,@PageView)";
            SqlParameter[] pars = {new SqlParameter("@Title",SqlDbType.NVarChar,50),
                                  new SqlParameter("@Author",SqlDbType.NVarChar,50),
                                  new SqlParameter("@Detail",SqlDbType.NVarChar),
                                  new SqlParameter("@Date",SqlDbType.SmallDateTime),
                                  new SqlParameter("@Category",SqlDbType.SmallInt),
                                  new SqlParameter("@PageView",SqlDbType.Int)
                                  };
            pars[0].Value = newInfo.Title;
            pars[1].Value = newInfo.Author;
            pars[2].Value = newInfo.Detail;
            pars[3].Value = newInfo.SubDateTime;
            pars[4].Value = newInfo.Category;
            pars[5].Value = newInfo.PageView;

            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);

        }
        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="newInfo"></param>
        /// <returns></returns>
        public int UpdateNewInfo(NewInfo newInfo)
        {
            string sql = "update T_CouncilDynamicNews set Title = @Title, Author=@Author,Detail = @Detail,Date = @Date, Category=@Category,PageView=@PageView where Id = @Id";
            SqlParameter[] pars = {new SqlParameter("@Title",SqlDbType.NVarChar,50),
                                       new SqlParameter("@Author",SqlDbType.NVarChar,50),
                                       new SqlParameter("@Detail",SqlDbType.NVarChar),
                                       new SqlParameter("@Date",SqlDbType.SmallDateTime),
                                       new SqlParameter("@Id",SqlDbType.Int),
                                       new SqlParameter("@Category",SqlDbType.SmallInt),
                                        new SqlParameter("@ViewPage",SqlDbType.Int)
                                   };
            pars[0].Value = newInfo.Title;
            pars[1].Value = newInfo.Author;
            pars[2].Value = newInfo.Detail;
            pars[3].Value = newInfo.SubDateTime;
            pars[4].Value = newInfo.Id;
            pars[5].Value = newInfo.Category;
            pars[6].Value = newInfo.PageView;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }
    }
}
