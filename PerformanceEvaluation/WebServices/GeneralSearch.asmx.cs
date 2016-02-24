using PerformanceEvaluation.PerformanceEvaluation.Biz;
using PerformanceEvaluation.PerformanceEvaluation.Info;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace PerformanceEvaluation.PerformanceEvaluation.WebServices
{
    /// <summary>
    /// GeneralSearch 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class GeneralSearch : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public ArrayList GetClassList(int organSysNo)
        {
            Dictionary<int, OrganEntity> dic = BasicManager.GetInstance().GetClassList(organSysNo);
            ArrayList reAL = new ArrayList();
            if (dic != null && dic.Count > 0)
            {
                for (int i = 0; i < dic.Count; i++)
                {
                    string[] itemArr = new string[2];
                    itemArr[0] = dic.Values.ElementAt(i).SysNo.ToString();
                    itemArr[1] = dic.Values.ElementAt(i).FunctionInfo.ToString();
                    reAL.Insert(i, itemArr);
                }
            }
            return reAL;
        }
    }
}
