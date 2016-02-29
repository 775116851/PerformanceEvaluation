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
    public partial class PerformanceHistory_Ajax : PageBase
    {
        protected int PageCount = 0;
        protected int PageSize = 20;
        protected int MaxPages = 0;
        protected int PageIndex = 1;
        protected int BeginIndex = 0;
        protected int EndIndex = 0;
        protected string Begin = "";
        protected string End = "";
        private ILog _log = log4net.LogManager.GetLogger(typeof(PerformanceHistory_Ajax));
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
            try
            {
                Hashtable ht = new Hashtable();
                if (Request.Form["txtPersonName"] != null && Request.Form["txtPersonName"].ToString() != "")
                {
                    ht.Add("Name", Request.Form["txtPersonName"].Trim());
                }
                if (Request.Form["ddlYY"] != null && Request.Form["ddlYY"].ToString() != "" && Convert.ToInt32(Request.Form["ddlYY"]) != AppConst.IntNull)
                {
                    if (Request.Form["ddlMM"] != null && Request.Form["ddlMM"].ToString() != "" && Convert.ToInt32(Request.Form["ddlMM"]) != AppConst.IntNull)
                    {
                        string kMM = Request.Form["ddlMM"].Trim();
                        int iMM = Convert.ToInt32(kMM);
                        if(iMM <= 9)
                        {
                            kMM = "0" + iMM;
                        }
                        ht.Add("pfCycle", Request.Form["ddlYY"].Trim() + kMM);
                    }
                    else
                    {
                        ht.Add("pfCycleM", Request.Form["ddlYY"].Trim());
                    }
                }
                if (Request.Form["ddlLevel$ddlEnum"] != null && Request.Form["ddlLevel$ddlEnum"].ToString() != "" && Convert.ToInt32(Request.Form["ddlLevel$ddlEnum"]) != AppConst.IntNull)
                {
                    ht.Add("JXLevel", Convert.ToInt32(Request.Form["ddlLevel$ddlEnum"]).ToString());
                }
                if (LoginSession.User.UserType == 1)
                {
                    ht.Add("MType", "1");
                    ht.Add("ParentPersonSysNo", LoginSession.User.SysNo);
                    ht.Add("OrganSysNo", LoginSession.User.OrganSysNo);
                    if (LoginSession.User.ClassSysNo != AppConst.IntNull)
                    {
                        if (LoginSession.User.EJBAdmin == (int)AppEnum.YNStatus.Yes)
                        {
                            if (Request.Form["ddlClass"] != null && Request.Form["ddlClass"].ToString() != "" && Convert.ToInt32(Request.Form["ddlClass"]) != AppConst.IntNull)
                            {
                                ht.Add("ClassSysNo", Convert.ToInt32(Request.Form["ddlClass"]).ToString());
                            }
                        }
                        else
                        {
                            ht.Add("ClassSysNo", LoginSession.User.ClassSysNo);
                        }
                    }
                }
                else//绩效管理员和公司老大
                {
                    ht.Add("MType", "2");
                    if (Request.Form["ddlOrgan"] != null && Request.Form["ddlOrgan"].ToString() != "" && Convert.ToInt32(Request.Form["ddlOrgan"]) != AppConst.IntNull)
                    {
                        ht.Add("OrganSysNo", Convert.ToInt32(Request.Form["ddlOrgan"]).ToString());
                    }
                    if (Request.Form["ddlClass"] != null && Request.Form["ddlClass"].ToString() != "" && Convert.ToInt32(Request.Form["ddlClass"]) != AppConst.IntNull)
                    {
                        ht.Add("ClassSysNo", Convert.ToInt32(Request.Form["ddlClass"]).ToString());
                    }
                }
                DataSet ds = BasicManager.GetInstance().GetJXKHHistory(PageIndex, PageSize, ht);
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
                //int sysNo = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "SysNo"));
                //Literal ltLink = (Literal)e.Item.FindControl("litLink");
            }
        }
    }
}