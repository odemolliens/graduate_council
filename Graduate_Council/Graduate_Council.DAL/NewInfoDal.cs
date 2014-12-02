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
            string sql = "select * from (select row_number() over (order by id) as num, * from T_News) as t where t.num>=@start and t.num<=@end";
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
        }
        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount()
        {
            string sql = "select count(*) from T_News";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
        }
        /// <summary>
        /// 获取一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NewInfo GetNewInfo(int id)
        {
            string sql = "select * from T_News where Id = @id";
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
            string sql = "delete from T_News where Id = @id";
            SqlParameter par = new SqlParameter("@id", DbType.Int32);
            par.Value = id;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, par);
        }
    }
}
