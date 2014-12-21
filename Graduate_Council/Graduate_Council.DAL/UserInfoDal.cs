using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graduate_Council.Model;
using System.Data;
using System.Data.SqlClient;

namespace Graduate_Council.DAL
{
    public class UserInfoDal
    {
        public UserInfo GetUserInfo(string userName, string password)
        {
            string sql = "select * from T_UserInfo where Name=@userName and Password=@password";
            SqlParameter[] pars = { new SqlParameter("@userName", SqlDbType.VarChar, 50), new SqlParameter("password", SqlDbType.VarChar, 50) };
            pars[0].Value = userName;
            pars[1].Value = password;
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, pars);
            UserInfo userInfo = null;
            if (dt.Rows.Count > 0)
            {
                userInfo = new UserInfo();
                LoadEntity(userInfo, dt.Rows[0]);
            }
            return userInfo;

        }
        public void LoadEntity(UserInfo userInfo, DataRow dr)
        {
            userInfo.Id = Convert.ToInt32(dr["Id"]);
            userInfo.Name = dr["Name"].ToString();//dr["Name"]!=DBNull.Value ? dr["Name"].ToString():string.Empty
            userInfo.Password = dr["Password"].ToString();
            userInfo.Admin = Convert.ToBoolean(dr["Admin"]);
        }

        public List<UserInfo> GetUserList(int start, int end)
        {
            string sql = "select * from(select row_number() over (order by Id) as num, * from T_UserInfo)as t where t.num>=@start and t.num<=@end";
            SqlParameter[] pars = {new SqlParameter("@start",start),
                                  new SqlParameter("@end",end)};
            List<UserInfo> list = null;
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, pars);
            if (dt.Rows.Count > 0)
            {
                list = new List<UserInfo>();
                UserInfo userInfo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    userInfo = new UserInfo();
                    LoadEntity(userInfo, dr);
                    list.Add(userInfo);
                }
            }
            return list;
        }

        public int DeleteUser(int id)
        {
            string sql = "delete from T_UserInfo where Id=@Id";
            SqlParameter par = new SqlParameter("@Id", DbType.Int32);
            par.Value = id;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, par);
        }
        public int UpdatePassword(string password,int id)
        {
            string sql = "update T_UserInfo set Password=@Password where Id=@Id";
            SqlParameter[] pars = {new SqlParameter("@Id",DbType.Int32),
                                  new SqlParameter("@Password",SqlDbType.VarChar,50)};                                  
            pars[0].Value = id;
            pars[1].Value = password;            
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);

        }
        public int AddUser(UserInfo userInfo)
        {
            string sql = "insert into T_UserInfo(Name,Password,Admin) values(@Name,@Password,@Admin)";
            SqlParameter[] pars = {
                                  new SqlParameter("@Name",SqlDbType.VarChar,50),
                                  new SqlParameter("@Password",SqlDbType.VarChar,50),
                                  new SqlParameter("@Admin",SqlDbType.Bit)};
            pars[0].Value = userInfo.Name;
            pars[1].Value = userInfo.Password;
            pars[2].Value = userInfo.Admin;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);

        }
        public int GetRecordCount()
        {
            string sql = "select count(*) from T_UserInfo";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
        }
        public UserInfo GetUser(int id)

        {
            string sql = "select * from T_UserInfo where Id=@Id";
            SqlParameter par = new SqlParameter("@Id", SqlDbType.Int);
            par.Value = id;
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, par);
            UserInfo userInfo = null;
            if (dt.Rows.Count > 0)
            {
                userInfo = new UserInfo();
                LoadEntity(userInfo, dt.Rows[0]);
            }
            return userInfo;
        }        
    }
}
