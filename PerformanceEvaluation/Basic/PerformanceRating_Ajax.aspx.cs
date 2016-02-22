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
    public partial class PerformanceRating_Ajax : PageBase
    {
        protected int PageCount = 0;
        protected int PageSize = 20;
        protected int MaxPages = 0;
        protected int PageIndex = 1;
        protected int BeginIndex = 0;
        protected int EndIndex = 0;
        protected string Begin = "";
        protected string End = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["PageIndex"] != null)
            {
                PageIndex = Convert.ToInt32(Request.QueryString["PageIndex"]);
                BindRep1();
            }
        }

        protected void BindRep1()
        {
            Hashtable ht = new Hashtable();
            if (Request.Form["txtPersonName"] != null && Request.Form["txtPersonName"].ToString() != "")
            {
                ht.Add("PersonName", Request.Form["txtPersonName"].Trim());
            }
            if (Request.Form["ddlPF$ddlEnum"] != null && Request.Form["ddlPF$ddlEnum"].ToString() != "" && Convert.ToInt32(Request.Form["ddlPF$ddlEnum"]) != AppConst.IntNull)
            {
                ht.Add("IsPF", Convert.ToInt32(Request.Form["ddlPF$ddlEnum"]).ToString());
            }
            ht.Add("ParentPersonSysNo", LoginSession.User.SysNo);
            DataSet ds = BasicManager.GetInstance().GetJXPFSearchDs(PageIndex, PageSize, ht);
            PageCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            BeginIndex = (PageIndex - 1) * PageSize + 1;
            MaxPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(PageCount) / Convert.ToDouble(PageSize)));
            EndIndex = (PageIndex * PageSize) > PageCount ? PageCount : (PageIndex * PageSize);
            Rep1.DataSource = ds.Tables[0];
            Rep1.DataBind();
        }

        protected void Rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //PerformanceRating
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int sysNo = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "SysNo"));
                string pfCycle = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "JXZQ"));
                int mIsPF = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IsPF"));
                string JXMXSysNo = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "JXMXSysNo"));
                Literal ltLink = (Literal)e.Item.FindControl("litLink");
                if (mIsPF == (int)AppEnum.YNStatus.Yes)
                {
                    ltLink.Text += " <a target='_blank' href='PerformanceRatingOpt.aspx?SysNo=" + sysNo + "&pfCycle=" + pfCycle + "&JXMXSysNo=" + JXMXSysNo + "'>修改评分</a> ";
                }
                else
                {
                    ltLink.Text += " <a target='_blank' href='PerformanceRatingOpt.aspx?SysNo=" + sysNo + "&pfCycle=" + pfCycle + "&JXMXSysNo=-9999'>评分</a> ";
                }
                
                //if (LoginSession.User.UserType == (int)AppEnum.UserType.Operator)//管理员拥有的权限
                //{
                //    ltLink.Text += " <a target='_blank' href='OrganInfoManagerOpt.aspx?SysNo=" + sysNo + "'>修改机构</a> ";

                //}
                //ltLink.Text += " <a target='_blank' href='###' onclick='openWindowSetPeriod(" + sysNo + ");return false'>设置期数</a> ";
                //ltLink.Text += " <a target='_blank' href='MasterCity.aspx?OrganSysNo=" + sysNo + "'>主站城市</a> ";
                //ltLink.Text += " <a target='_blank' href='OrganCategory.aspx?OrganSysNo=" + sysNo + "'>设置类别</a> ";
                //ltLink.Text += " <a target='_blank' href='OrganSaleChannel.aspx?OrganSysNo=" + sysNo + "'>销售渠道</a> ";
                //ltLink.Text += " <a target='_blank' href='OrganSaleChannelBind.aspx?OrganSysNo=" + sysNo + "'>一级机构与销售渠道</a> ";
            }
        }

        protected string FormatLevel(string sLevel)
        {
            int mLevel;
            if (!string.IsNullOrEmpty(sLevel))
            {
                if (int.TryParse(sLevel, out mLevel) && mLevel != AppConst.IntNull && Enum.IsDefined(typeof(AppEnum.JXLevel), mLevel))
                {
                    sLevel = AppEnum.GetDescription(typeof(AppEnum.JXLevel), mLevel);
                }
                else
                {
                    sLevel = "";
                }
            }
            return sLevel;
        }
    }
}