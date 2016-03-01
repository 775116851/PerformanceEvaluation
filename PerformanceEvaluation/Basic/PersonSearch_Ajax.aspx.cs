using log4net;
using PerformanceEvaluation.Cmn;
using PerformanceEvaluation.PerformanceEvaluation.Biz;
using PerformanceEvaluation.PerformanceEvaluation.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceEvaluation.PerformanceEvaluation.Basic
{
    public partial class PersonSearch_Ajax : PageBase
    {
        protected int PageCount = 0;
        protected int PageSize = 20;
        protected int MaxPages = 0;
        protected int PageIndex = 1;
        protected int BeginIndex = 0;
        protected int EndIndex = 0;
        protected string Begin = "";
        protected string End = "";
        private ILog _log = log4net.LogManager.GetLogger(typeof(PersonSearch_Ajax));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSession.User.UserType != 2)
            {
                return;
            }
            if (Request.QueryString["PageIndex"] != null)
            {
                PageIndex = Convert.ToInt32(Request.QueryString["PageIndex"]);
                BindRep1();
            }
        }

        protected void BindRep1()
        {
            try
            {
                Hashtable ht = new Hashtable();
                if (Request.Form["txtPersonName"] != null && Request.Form["txtPersonName"].ToString() != "")
                {
                    ht.Add("Name", Request.Form["txtPersonName"].Trim());
                }
                if (Request.Form["ddlStatus$ddlEnum"] != null && Request.Form["ddlStatus$ddlEnum"].ToString() != "" && Convert.ToInt32(Request.Form["ddlStatus$ddlEnum"]) != AppConst.IntNull)
                {
                    ht.Add("Status", Convert.ToInt32(Request.Form["ddlStatus$ddlEnum"]).ToString());
                }
                if (Request.Form["ddlOrgan"] != null && Request.Form["ddlOrgan"].ToString() != "" && Convert.ToInt32(Request.Form["ddlOrgan"]) != AppConst.IntNull)
                {
                    ht.Add("OrganSysNo", Convert.ToInt32(Request.Form["ddlOrgan"]).ToString());
                }
                if (Request.Form["ddlClass"] != null && Request.Form["ddlClass"].ToString() != "" && Convert.ToInt32(Request.Form["ddlClass"]) != AppConst.IntNull)
                {
                    ht.Add("ClassSysNo", Convert.ToInt32(Request.Form["ddlClass"]).ToString());
                }
                if (Request.Form["ddlPersonType"] != null && Request.Form["ddlPersonType"].ToString() != "" && Convert.ToInt32(Request.Form["ddlPersonType"]) != AppConst.IntNull)
                {
                    ht.Add("PersonTypeSysNo", Convert.ToInt32(Request.Form["ddlPersonType"]).ToString());
                }
                ht.Add("IsAdmin", 0);
                DataSet ds = BasicManager.GetInstance().GetPersonList(PageIndex, PageSize, ht);
                PageCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                BeginIndex = (PageIndex - 1) * PageSize + 1;
                MaxPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(PageCount) / Convert.ToDouble(PageSize)));
                EndIndex = (PageIndex * PageSize) > PageCount ? PageCount : (PageIndex * PageSize);
                Rep1.DataSource = ds.Tables[0];
                Rep1.DataBind();
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("绩效历史查询异常，异常信息：{0};异常详情：{1}", ex.Message, ex));
            }
        }

        protected void Rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int sysNo = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "SysNo"));
                Literal ltLink = (Literal)e.Item.FindControl("litLink");
                ltLink.Text += " <a target='_blank' href='PersonSearchOpt.aspx?SysNo=" + sysNo + "&OptType=0'>查看</a> ";
                ltLink.Text += " <a target='_blank' href='PersonSearchOpt.aspx?SysNo=" + sysNo + "&OptType=1'>修改</a> ";
            }
        }
    }
}