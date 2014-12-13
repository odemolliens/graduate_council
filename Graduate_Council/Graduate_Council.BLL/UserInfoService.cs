using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graduate_Council.Model;

namespace Graduate_Council.BLL
{
    public class UserInfoService
    {
        DAL.UserInfoDal userInfoDal = new DAL.UserInfoDal();
        public UserInfo GetUserInfo(string userName, string password)
        {
            return userInfoDal.GetUserInfo(userName, password);
        }
        public int GetPageCount(int pageSize)
        {
            int recordCount = userInfoDal.GetRecordCount();
            int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        }
        public List<UserInfo> GetUserList(int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<UserInfo> list = userInfoDal.GetUserList(start, end);
            return list;
        }
        public bool AddUser(UserInfo userInfo)
        {
            return userInfoDal.AddUser(userInfo) > 0;
        }        
        public bool UpdatePassword(string password,int id)
        {
            return userInfoDal.UpdatePassword(password,id) > 0;
        }
        public bool DeleteUser(int id)
        {
           return userInfoDal.DeleteUser(id) > 0;
        }
        public UserInfo GetUser(int id)
        {
            return userInfoDal.GetUser(id);
        }
    }
}
