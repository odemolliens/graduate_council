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
    }
}
