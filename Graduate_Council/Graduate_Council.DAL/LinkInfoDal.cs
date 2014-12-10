using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graduate_Council.Model;
using System.Data.SqlClient;
using System.Data;

namespace Graduate_Council.DAL
{
    public class LinkInfoDal
    {
        public List<LinkInfo> GetLinkList(int start, int end)
        {
            string sql = "select * from(select row_number() over (order by Id DESC) as num, * from T_LinkInfo)as t where t.num>=@start and t.num<=@end";
            SqlParameter[] pars = {new SqlParameter("@start",start),
                                  new SqlParameter("@end",end)};
            List<LinkInfo> list = null;
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, pars);
            if (dt.Rows.Count > 0)
            {
                list = new List<LinkInfo>();
                LinkInfo linkInfo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    linkInfo = new LinkInfo();
                    LoadData(linkInfo, dr);
                    list.Add(linkInfo);
                }
            }
            return list;
        }

        private void LoadData(LinkInfo linkInfo,DataRow dr)
        {
            linkInfo.Id=Convert.ToInt32(dr["Id"]);
            linkInfo.Name = dr["Name"].ToString();
            linkInfo.Link = dr["Link"].ToString();
            linkInfo.Category = dr["Category"].ToString();
        }

        public int DeleteLink(int id)
        {
            string sql = "delete from T_LinkInfo where Id=@Id";
            SqlParameter par = new SqlParameter("@Id",DbType.Int32);
            par.Value = id;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, par);
        }
        public int UpdateLink( LinkInfo linkInfo)
        {
            string sql = "update T_LinkInfo set Name=@Name,Link=@Link,Category=@Category where Id=@Id";
            SqlParameter[] pars = {new SqlParameter("@Id",DbType.Int32),
                                  new SqlParameter("@Name",SqlDbType.NVarChar,50),
                                  new SqlParameter("@Link",SqlDbType.VarChar,200),
                                  new SqlParameter("@Category",SqlDbType.NVarChar,50)};
            pars[0].Value = linkInfo.Id;
            pars[1].Value = linkInfo.Name;
            pars[2].Value = linkInfo.Link;
            pars[3].Value = linkInfo.Category;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
 
        }
        public int AddLink(LinkInfo linkInfo)
        {
            string sql = "insert into T_LinkInfo(Name,Link,Category) values(@Name,@Link,@Category)";
            SqlParameter[] pars = {
                                  new SqlParameter("@Name",SqlDbType.NVarChar,50),
                                  new SqlParameter("@Link",SqlDbType.VarChar,200),
                                  new SqlParameter("@Category",SqlDbType.NVarChar,50)};
            pars[0].Value = linkInfo.Name;
            pars[1].Value = linkInfo.Link;
            pars[2].Value = linkInfo.Category;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);

        }
        public int GetRecordCount()
        {
            string sql = "select count(*) from T_LinkInfo";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
        }
        public LinkInfo GetLink(int id)
        {
            string sql = "select * from T_LinkInfo where Id=@Id";
            SqlParameter par = new SqlParameter("@Id", SqlDbType.Int);
            par.Value = id;
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text,par);
            LinkInfo linkInfo = null;
            if (dt.Rows.Count > 0)
            {
                linkInfo = new LinkInfo();
                LoadData(linkInfo, dt.Rows[0]);
            }
            return linkInfo;
        }
        public List<LinkInfo> Top15List()
        {
            string sql = "select * from((select top 5 * from T_LinkInfo where Category='资料下载')union all(select top 5 * from T_LinkInfo where Category='校内链接')union all(select top 5 * from T_LinkInfo where Category='友情链接'))t";
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text);
            List<LinkInfo> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<LinkInfo>();
                LinkInfo linkInfo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    linkInfo = new LinkInfo();
                    LoadData(linkInfo, dr);
                    list.Add(linkInfo);
                }
            }
            return list;
        }
    }
}
