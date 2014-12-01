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

        public int GetRecordCount()
        {
            string sql = "select count(*) from T_News";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
        }
    }
}
