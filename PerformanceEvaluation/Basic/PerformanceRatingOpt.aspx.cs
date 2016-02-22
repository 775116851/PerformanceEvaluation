﻿using PerformanceEvaluation.Cmn;
using PerformanceEvaluation.PerformanceEvaluation.Biz;
using PerformanceEvaluation.PerformanceEvaluation.Code;
using PerformanceEvaluation.PerformanceEvaluation.Info;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PerformanceEvaluation.PerformanceEvaluation.Basic
{
    public partial class PerformanceRatingOpt : PageBase
    {
        //员工编号
        protected int SysNo
        {
            set { ViewState["SysNo"] = value; }
            get { return Convert.ToInt32(ViewState["SysNo"]); }
        }
        //已评分明细编号(类型20)
        protected int JXMXSysNo
        {
            set { ViewState["JXMXSysNo"] = value; }
            get { return Convert.ToInt32(ViewState["JXMXSysNo"]); }
        }
        //绩效周期
        protected string pfCycle
        {
            set { ViewState["pfCycle"] = value; }
            get { return Convert.ToString(ViewState["pfCycle"]); }
        }
        public DataSet repList = new DataSet();
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
                        //Assert(lblMsg, "关键字有误", -1);
                    }
                }
                else
                {
                    SysNo = AppConst.IntNull;
                }
                if (Request.QueryString["JXMXSysNo"] != null && Request.QueryString["JXMXSysNo"].Trim() != "")
                {
                    try
                    {
                        JXMXSysNo = Convert.ToInt32(Request.QueryString["JXMXSysNo"].ToString());
                    }
                    catch (Exception ex)
                    {
                        //Assert(lblMsg, "关键字有误", -1);
                    }
                }
                else
                {
                    JXMXSysNo = AppConst.IntNull;
                }
                if (Request.QueryString["pfCycle"] != null && Request.QueryString["pfCycle"].Trim() != "")
                {
                    try
                    {
                        pfCycle = Request.QueryString["pfCycle"].ToString();
                    }
                    catch (Exception ex)
                    {
                        //Assert(lblMsg, "关键字有误", -1);
                    }
                }
                else
                {
                    pfCycle = AppConst.StringNull;
                }
                //加载相关数据
                BindRep();
            }
        }

        //绑定列表方法
        protected void BindRep()
        {
            lblSJ.Text = LoginSession.User.Name;
            PersonInfoEntity pieLower = BasicManager.GetInstance().LoadUser(SysNo);
            if(pieLower == null || pieLower.Status != (int)AppEnum.BiStatus.Valid)
            {
                Assert(lblMsg, "下级员工不存在或状态无效，请联系管理员", -1);
                return;
            }
            lblXJ.Text = pieLower.Name;
            Hashtable ht = new Hashtable();
            ht.Add("ParentPersonSysNo", LoginSession.User.SysNo);
            ht.Add("LowerPersonSysNo", SysNo);
            ht.Add("pfCycle", pfCycle);
            repList = BasicManager.GetInstance().GetJXKHMX(ht);
            if (Util.HasMoreRow(repList))
            {
                Rep1.DataSource = repList.Tables[0];
                Rep1.DataBind();
            }
            else
            {
                Rep1.DataSource = null;
                Rep1.DataBind();
            }
        }

        protected void Rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
            }
        }

        //提交数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int fCount = 0;
            string errorMessage = string.Empty;
            DateTime dtTime = DateTime.Now;
            List<JXMXBEntity> list = new List<JXMXBEntity>();
            for (int i = 0; i < Rep1.Items.Count;i++ )
            {
                TextBox txtMScore = Rep1.Items[i].FindControl("txtMScore") as TextBox;
                HtmlInputHidden ipt_JXScore = Rep1.Items[i].FindControl("ipt_JXScore") as HtmlInputHidden;
                string iScore = txtMScore.Text;//评分值
                string jxScore = ipt_JXScore.Value;//满分值
                if(string.IsNullOrEmpty(iScore) || iScore == ((int)AppConst.IntNull).ToString())
                {
                    Assert(lblMsg, "请先将评分项填写完整后，再提交！", -1);
                    return;
                }
                if (!(Util.IsInteger(iScore.Trim()) && Convert.ToInt32(iScore.Trim()) > 0))
                {
                    Assert(lblMsg, "排序项应填入正整数值，请重新修改后再保存！", -1);
                    return;
                }
                int mIScore = Convert.ToInt32(iScore);
                int mJxScore = 0;
                if (!string.IsNullOrEmpty(ipt_JXScore.Value) && ipt_JXScore.Value != ((int)AppConst.DecimalNull).ToString() && int.TryParse(ipt_JXScore.Value, out mJxScore))
                {
                }
                else
                {
                    mJxScore = 100;//默认满分100分
                }
                if(mIScore > mJxScore)
                {
                    Assert(lblMsg, "评分项应填入正整数值，且评分值不大于 " + mJxScore + " 分，请重新修改后再保存！", -1);
                    return;
                }
                HtmlInputHidden ipt_SysNo = Rep1.Items[i].FindControl("ipt_SysNo") as HtmlInputHidden;
                HtmlInputHidden ipt_JXId = Rep1.Items[i].FindControl("ipt_JXId") as HtmlInputHidden;
                HtmlInputHidden ipt_JXGrad = Rep1.Items[i].FindControl("ipt_JXGrad") as HtmlInputHidden;
                HtmlInputHidden ipt_JXMXScore = Rep1.Items[i].FindControl("ipt_JXMXScore") as HtmlInputHidden;
                HtmlInputHidden ipt_JXCategory = Rep1.Items[i].FindControl("ipt_JXCategory") as HtmlInputHidden;
                HtmlInputHidden ipt_JXMXSysNo = Rep1.Items[i].FindControl("ipt_JXMXSysNo") as HtmlInputHidden;

                //拼接评分明细表
                JXMXBEntity jMX = new JXMXBEntity();
                if (JXMXSysNo == AppConst.IntNull)//新增
                {
                    jMX.SysNo = AppConst.IntNull;
                    jMX.LowerPersonSysNo = SysNo;
                    jMX.JXCategory = Convert.ToInt32(ipt_JXCategory.Value.Trim());
                    jMX.JXSysNo = Convert.ToInt32(ipt_SysNo.Value.Trim());
                    jMX.ParentPersonSysNo = LoginSession.User.SysNo;
                    jMX.JXCycle = pfCycle;
                    jMX.JXMXCategory = (int)AppEnum.JXMXCategory.MX;
                    jMX.CreateTime = dtTime;
                    jMX.CreateUserSysNo = LoginSession.User.SysNo;
                }
                else
                {
                    jMX.SysNo = Convert.ToInt32(ipt_JXMXSysNo.Value.Trim());
                }
                jMX.JXScore = mIScore;
                jMX.LastUpdateTime = dtTime;
                jMX.LastUpdateUserSysNo = LoginSession.User.SysNo;
                list.Add(jMX);
            }
            if(list.Count <= 0)
            {
                Assert(lblMsg, "请先将评分项填写完整后，再提交！", -1);
                return;
            }

        }
    }
}