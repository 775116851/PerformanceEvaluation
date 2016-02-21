using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceEvaluation.PerformanceEvaluation.Info
{
    [Serializable]
    public class SessionInfo
    {
        public string IPAddress;
        public PersonInfoEntity User;
        //public Dictionary<int, Sys_PrivilegeEntity> PrivilegeHt;
        public List<Custom_Sys_MenuEntity> menuList;

        public SessionInfo()
        {

        }
    }
}
