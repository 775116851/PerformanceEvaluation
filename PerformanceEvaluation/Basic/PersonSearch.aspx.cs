using log4net;
using PerformanceEvaluation.Cmn;
using PerformanceEvaluation.PerformanceEvaluation.Biz;
using PerformanceEvaluation.PerformanceEvaluation.Code;
using PerformanceEvaluation.PerformanceEvaluation.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceEvaluation.PerformanceEvaluation.Basic
{
    public partial class PersonSearch : PageBase
    {
        private ILog _log = log4net.LogManager.GetLogger(typeof(PersonSearch));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitUi();
            }
        }

        private void InitUi()
        {
            ddlPersonType.Items.Clear();
            ddlOrgan.Items.Clear();
            ddlClass.Items.Clear();

            ddlStatus.BindStatus(typeof(AppEnum.BiStatus), true);
            ddlStatus.RemoveItemByValue(-2);
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
            Dictionary<int, PersonTypeEntity> personTypeList = BasicManager.GetInstance().GetPersonTypeList();
            if (personTypeList != null && personTypeList.Count != 0)
            {
                for (int i = 0; i < personTypeList.Count; i++)
                {
                    ddlPersonType.Items.Add(new ListItem(personTypeList.Values.ElementAt(i).TypeName, personTypeList.Values.ElementAt(i).SysNo.ToString()));
                }
            }
            ddlPersonType.Items.Insert(0, new ListItem(AppConst.AllSelectString, AppConst.IntNull.ToString()));
            //btnSave.Visible = false;
            //if (LoginSession.User.UserType == 2)
            //{
            //    btnSave.Visible = true;
            //}
            if (LoginSession.User.UserType != 2 && LoginSession.User.UserType != 3)//非绩效管理员和公司老大
            {
                //职能室列表
                Dictionary<int, OrganEntity> classList = BasicManager.GetInstance().GetClassList(LoginSession.User.OrganSysNo);
                if (classList != null && classList.Count != 0)
                {
                    for (int i = 0; i < classList.Count; i++)
                    {
                        ddlClass.Items.Add(new ListItem(classList.Values.ElementAt(i).FunctionInfo, classList.Values.ElementAt(i).SysNo.ToString()));
                    }
                }

                ddlOrgan.SelectedValue = LoginSession.User.OrganSysNo.ToString();
                ddlOrgan.Enabled = false;
                if (LoginSession.User.EJBAdmin == (int)AppEnum.YNStatus.Yes)//二级部管理人员可选择职能室
                {

                }
                else
                {
                    if (LoginSession.User.ClassSysNo != AppConst.IntNull)
                    {
                        ddlClass.SelectedValue = LoginSession.User.ClassSysNo.ToString();
                    }
                    ddlClass.Enabled = false;
                }
            }
        }
    }
}