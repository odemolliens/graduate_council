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
        }
    }
}
