using log4net;
using PerformanceEvaluation.Cmn;
using PerformanceEvaluation.PerformanceEvaluation.Biz;
using PerformanceEvaluation.PerformanceEvaluation.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceEvaluation.PerformanceEvaluation.Basic
{
    public partial class PerformanceRating : PageBase
    {
        private ILog _log = log4net.LogManager.GetLogger(typeof(PerformanceRating));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitUi();
            }
        }

        private void InitUi()
        {
            ddlPF.BindStatus(typeof(AppEnum.YNStatus),true);
        }

        //加权汇总
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (LoginSession.User.UserType == 2)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("userSysNo", LoginSession.User.SysNo);
                    ht.Add("pfCycle", DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00"));
                    Tuple<bool, string> result = BasicManager.GetInstance().SaveJQHZ(ht);
                    if (result.Item1 == true)
                    {
                        Assert(lblMsg, result.Item2, 1);
                    }
                    else
                    {
                        Assert(lblMsg, result.Item2, -1);
                        return;
                    }
                }
                else
                {
                    Assert(lblMsg, "非绩效管理员不允许进行加权汇总操作", -1);
                    return;
                }
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("加权汇总操作出现异常，异常信息：{0};异常详情：{1}", ex.Message, ex));
                Assert(lblMsg, "加权汇总操作出现异常:" + ex.Message, -1);
            }
            
        }
    }
}