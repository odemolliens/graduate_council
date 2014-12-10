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
        public List<NewInfo> GetPageList(int start, int end,string tableName)
        {
            string sql = "select * from (select row_number() over (order by Date DESC) as num, * from " + tableName + ") as t where t.num>=@start and t.num<=@end";
            SqlParameter[] pars = { new SqlParameter("@start", start), new SqlParameter("@end", end)};
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
        public List<NewInfo> GetPageListByCat(int start, int end, string tableName, string category)
        {
            string sql = "select * from (select row_number() over (order by Date) as num, * from " + tableName + " where Category=@Category) as t where t.num>=@start and t.num<=@end";
            SqlParameter[] pars = { new SqlParameter("@start", start), new SqlParameter("@end", end) ,new SqlParameter("@Category",category) };
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
            newInfo.Category = dr["Category"].ToString();
            newInfo.PageView = Convert.ToInt32(dr["PageView"]);
        }
        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount(string tableName)
        {
            string sql = "select count(*) from " + tableName;            
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
        }
        /// <summary>
        /// 获取一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NewInfo GetNewInfo(int id,string tableName)
        {
            string sql = "select * from "+tableName+" where Id = @id";
            SqlParameter[] pars = {new SqlParameter("@id", DbType.Int32)};
            pars[0].Value = id;
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
        public int DeleteNewInfo(int id, string tableName)
        {
            string sql = "delete from "+tableName+" where Id = @id";
            SqlParameter[] par = { new SqlParameter("@id", DbType.Int32)};
            par[0].Value = id;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, par);
        }
        /// <summary>
        /// 新增新闻
        /// </summary>
        /// <param name="newInfo"></param>
        /// <returns></returns>
        public int AddNewInfo(NewInfo newInfo,string tableName)
        {
            string sql = "insert into "+tableName+"(Title,Author,Detail,Date,Category,PageView) values(@Title,@Author,@Detail,@Date,@Category,@PageView)";
            SqlParameter[] pars = {new SqlParameter("@Title",SqlDbType.NVarChar,50),
                                  new SqlParameter("@Author",SqlDbType.NVarChar,50),
                                  new SqlParameter("@Detail",SqlDbType.NVarChar),
                                  new SqlParameter("@Date",SqlDbType.SmallDateTime),
                                  new SqlParameter("@Category",SqlDbType.NVarChar,50),
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
        public int UpdateNewInfo(NewInfo newInfo,string tableName)
        {
            string sql = "update "+tableName+" set Title = @Title, Author=@Author,Detail = @Detail,Date = @Date, Category=@Category,PageView=@PageView where Id = @Id";
            SqlParameter[] pars = {new SqlParameter("@Title",SqlDbType.NVarChar,50),
                                       new SqlParameter("@Author",SqlDbType.NVarChar,50),
                                       new SqlParameter("@Detail",SqlDbType.NVarChar),
                                       new SqlParameter("@Date",SqlDbType.SmallDateTime),
                                       new SqlParameter("@Id",SqlDbType.Int),
                                       new SqlParameter("@Category",SqlDbType.NVarChar,50),
                                        new SqlParameter("@PageView",SqlDbType.Int)
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
        /// <summary>
        /// 更新PageView
        /// </summary>
        /// <param name="newInfo"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public void UpdatePageView(int pageView, string tableName, int id)
        {
            string sql = "update " + tableName + " set PageView=@PageView where Id = @Id";
            SqlParameter[] pars = {new SqlParameter("@PageView",SqlDbType.Int),
                                  new SqlParameter("@Id",SqlDbType.Int)};
            pars[0].Value = pageView;
            pars[1].Value = id;
            SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
            return;
        }
    }
}
