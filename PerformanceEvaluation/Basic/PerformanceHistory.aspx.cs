using log4net;
using PerformanceEvaluation.Cmn;
using PerformanceEvaluation.PerformanceEvaluation.Biz;
using PerformanceEvaluation.PerformanceEvaluation.Code;
using PerformanceEvaluation.PerformanceEvaluation.Info;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceEvaluation.PerformanceEvaluation.Basic
{
    public partial class PerformanceHistory : PageBase
    {
        private ILog _log = log4net.LogManager.GetLogger(typeof(PerformanceHistory));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitUi();
            }
        }

        private void InitUi()
        {
            int newYY = DateTime.Now.Year;
            int newMM = DateTime.Now.Month;
            ddlYY.Items.Clear();
            ddlMM.Items.Clear();
            ddlOrgan.Items.Clear();
            ddlClass.Items.Clear();
            for (int oldYY = 2015; oldYY <= newYY;oldYY++ )
            {
                ddlYY.Items.Add(new ListItem(oldYY.ToString(), oldYY.ToString()));
            }
            ddlYY.Items.Insert(0, new ListItem(AppConst.AllSelectString, AppConst.IntNull.ToString()));
            for (int i = 1; i <= 12;i++ )
            {
                ddlMM.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlMM.Items.Insert(0, new ListItem(AppConst.AllSelectString, AppConst.IntNull.ToString()));
            ddlLevel.BindStatus(typeof(AppEnum.JXLevel), true);
            //机构列表
            Dictionary<int, OrganEntity> organList = BasicManager.GetInstance().GetOrganList();
            if (organList != null && organList.Count != 0)
            {
                for (int i = 0; i < organList.Count; i++)
                {
                    ddlOrgan.Items.Add(new ListItem(organList.Values.ElementAt(i).OrganName, organList.Values.ElementAt(i).SysNo.ToString()));
                } 
            }
            ddlOrgan.Items.Insert(0, new ListItem(AppConst.AllSelectString, AppConst.IntNull.ToString()));
            ddlClass.Items.Insert(0, new ListItem(AppConst.AllSelectString, AppConst.IntNull.ToString()));
            btnSave.Visible = false;
            if(LoginSession.User.UserType == 2)
            {
                btnSave.Visible = true;
            }
            if (LoginSession.User.UserType != 2 && LoginSession.User.UserType != 3)//非绩效管理员和公司老大
            {
                ddlYY.SelectedValue = newYY.ToString();
                ddlMM.SelectedValue = newMM.ToString();

                //职能室列表
                Dictionary<int, OrganEntity> classList = BasicManager.GetInstance().GetClassList(1);
                if (classList != null && classList.Count != 0)
                {
                    for (int i = 0; i < classList.Count; i++)
                    {
                        ddlClass.Items.Add(new ListItem(classList.Values.ElementAt(i).FunctionInfo, classList.Values.ElementAt(i).SysNo.ToString()));
                    }
                }

                ddlOrgan.SelectedIndex = LoginSession.User.OrganSysNo;
                ddlOrgan.Enabled = false;
                if (LoginSession.User.EJBAdmin == (int)AppEnum.YNStatus.Yes)//二级部管理人员可选择职能室
                {

                }
                else
                {
                    if(LoginSession.User.ClassSysNo != AppConst.IntNull)
                    {
                        ddlClass.SelectedIndex = LoginSession.User.ClassSysNo;
                    }
                    ddlClass.Enabled = false;
                }
            }
        }

        protected void ddlOrgan_SelectedIndexChanged(object sender, EventArgs e)
        {
            int organSysNo = ddlOrgan.SelectedIndex;
            
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