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
    public partial class PersonSearchOpt : PageBase
    {
        protected int SysNo
        {
            set { ViewState["SysNo"] = value; }
            get { return Convert.ToInt32(ViewState["SysNo"]); }
        }
        private ILog _log = log4net.LogManager.GetLogger(typeof(PersonSearchOpt));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["SysNo"] != null && Request.QueryString["SysNo"].Trim() != "")
                {
                    try
                    {
                        SysNo = Convert.ToInt32(Request.QueryString["SysNo"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Assert(lblMsg, "关键字有误", -1);
                        return;
                    }
                }
                else
                {
                    SysNo = AppConst.IntNull;
                }
                //加载相关数据
                BindRep();
            }
        }

        protected void BindRep()
        {
            ddlPersonType.Items.Clear();
            ddlOrgan.Items.Clear();
            ddlClass.Items.Clear();

            ddlStatus.BindSelect(typeof(AppEnum.BiStatus));
            ddlStatus.RemoveItemByValue(-2);
            ddlIsLogin.BindSelect(typeof(AppEnum.YNStatus));
            ddlGender.BindSelect(typeof(AppEnum.Gendor));
            //机构列表
            Dictionary<int, OrganEntity> organList = BasicManager.GetInstance().GetOrganList();
            if (organList != null && organList.Count != 0)
            {
                for (int i = 0; i < organList.Count; i++)
                {
                    ddlOrgan.Items.Add(new ListItem(organList.Values.ElementAt(i).OrganName, organList.Values.ElementAt(i).SysNo.ToString()));
                }
            }
            ddlOrgan.Items.Insert(0, new ListItem(AppConst.PleaseSelectString, AppConst.IntNull.ToString()));
            ddlClass.Items.Insert(0, new ListItem(AppConst.PleaseSelectString, AppConst.IntNull.ToString()));
            Dictionary<int, PersonTypeEntity> personTypeList = BasicManager.GetInstance().GetPersonTypeList();
            if (personTypeList != null && personTypeList.Count != 0)
            {
                for (int i = 0; i < personTypeList.Count; i++)
                {
                    ddlPersonType.Items.Add(new ListItem(personTypeList.Values.ElementAt(i).TypeName, personTypeList.Values.ElementAt(i).SysNo.ToString()));
                }
            }
            ddlPersonType.Items.Insert(0, new ListItem(AppConst.PleaseSelectString, AppConst.IntNull.ToString()));
        }
    }
}