using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graduate_Council.Model
{
    public class NewsCategory
    {
        public static Dictionary<string, List<string>> newsCategory = new Dictionary<string, List<string>>() { 
            {"FrontNews",new List<string>(){"学术活动","文体活动","社会实践","校园小记","实事播报","欢迎投稿"}},
            {"CouncilDynamicNews",new List<string>(){"研会动态"}},
            {"BranchNews",new List<string>(){"各分会名称","分会实况","分会链接"}},
            {"CampusEssay",new List<string>(){"校园油记"}},
            {"NoticeNews",new List<string>(){"通知公告"}},
            {"JobNews",new List<string>(){"就业信息"}},
            {"CouncilExpNews",new List<string>(){"精品活动","结构设置","规章制度","研会历史"}}
        };
    }
}
