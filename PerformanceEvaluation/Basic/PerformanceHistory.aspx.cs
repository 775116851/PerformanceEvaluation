using allinpay.Mall.Cmn;
using log4net;
using PerformanceEvaluation.Cmn;
using PerformanceEvaluation.PerformanceEvaluation.Biz;
using PerformanceEvaluation.PerformanceEvaluation.Code;
using PerformanceEvaluation.PerformanceEvaluation.Info;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
                    if(LoginSession.User.ClassSysNo != AppConst.IntNull)
                    {
                        ddlClass.SelectedValue = LoginSession.User.ClassSysNo.ToString();
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
                    _log.Info(string.Format("加权汇总操作完成，完成时间：{0};返回信息：{1}", DateTime.Now, result.Item2));
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

        //导出
        protected void btExport_Click(object sender, EventArgs e)
        {
            Hashtable ht = new Hashtable();
            if(!string.IsNullOrEmpty(txtPersonName.Text.Trim()))
            {
                ht.Add("Name", txtPersonName.Text.Trim());
            }
            if(ddlYY.SelectedValue != AppConst.IntNull.ToString())
            {
                if (ddlMM.SelectedValue != AppConst.IntNull.ToString())
                {
                    string kMM = ddlMM.SelectedValue;
                    int iMM = Convert.ToInt32(kMM);
                    if (iMM <= 9)
                    {
                        kMM = "0" + iMM;
                    }
                    ht.Add("pfCycle", ddlYY.SelectedValue + kMM);
                }
                else
                {
                    ht.Add("pfCycleM", ddlYY.SelectedValue);
                }
            }
            if(ddlLevel.KeyValue != AppConst.IntNull)
            {
                ht.Add("JXLevel", ddlLevel.KeyValue);
            }
            if (LoginSession.User.UserType == 1)
            {
                ht.Add("ParentPersonSysNo", LoginSession.User.SysNo);
                ht.Add("OrganSysNo", LoginSession.User.OrganSysNo);
                if (LoginSession.User.ClassSysNo != AppConst.IntNull)
                {
                    if (LoginSession.User.EJBAdmin == (int)AppEnum.YNStatus.Yes)
                    {
                        if (!string.IsNullOrEmpty(hidClassSysNo.Value.Trim()) && hidClassSysNo.Value != AppConst.IntNull.ToString())
                        {
                            ht.Add("ClassSysNo", Convert.ToInt32(hidClassSysNo.Value.Trim()));
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
                if (ddlOrgan.SelectedValue != AppConst.IntNull.ToString())
                {
                    ht.Add("OrganSysNo", ddlOrgan.SelectedValue);
                }
                if (!string.IsNullOrEmpty(hidClassSysNo.Value.Trim()) && hidClassSysNo.Value != AppConst.IntNull.ToString())
                {
                    ht.Add("ClassSysNo", Convert.ToInt32(hidClassSysNo.Value.Trim()));
                }
            }
            List<DataTable> dtList = new List<DataTable>();
            List<string> workSheetNameList = new List<string>();
            //记录运行时间
            Stopwatch myWatch2 = Stopwatch.StartNew();
            DataSet ds = BasicManager.GetInstance().GetJXKHHistory(1, 10000000, ht);
            myWatch2.Stop();
            _log.Info("导出绩效历史数据;正式访问数据库查询所有列耗时(毫秒)：" + myWatch2.ElapsedMilliseconds);
            if (Util.HasMoreRow(ds))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("编号");
                dt.Columns.Add("绩效周期");
                dt.Columns.Add("员工姓名");
                dt.Columns.Add("二级部");
                dt.Columns.Add("职能室");
                dt.Columns.Add("入职时间");
                dt.Columns.Add("绩效得分");
                dt.Columns.Add("绩效等级");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow newDr = dt.NewRow();
                    newDr["编号"] = Convert.ToString(dr["SysNo"]);
                    newDr["绩效周期"] = Convert.ToString(dr["JXZQ"]);
                    newDr["员工姓名"] = Convert.ToString(dr["Name"]);
                    newDr["二级部"] = Convert.ToString(dr["OrganName"]);
                    newDr["职能室"] = Convert.ToString(dr["FunctionInfo"]);
                    newDr["入职时间"] = Convert.ToString(dr["EntryDate"]);
                    newDr["绩效得分"] = Convert.ToString(dr["JXScore"]);
                    newDr["绩效等级"] = AppEnum.GetDescription(typeof(AppEnum.JXLevel), Convert.ToInt32(dr["JXLevel"]));
                    dt.Rows.Add(newDr);
                }
                workSheetNameList.Add("绩效历史列表");
                dtList.Add(dt);
            }
            if (dtList.Count == 0)
            {
                Assert(lblMsg, "没有符合条件的数据！", -1);
                return;
            }
            //记录运行时间
            Stopwatch myWatch3 = Stopwatch.StartNew();
            try
            {
                ExcelHelper eh = new ExcelHelper(dtList, workSheetNameList, "绩效历史列表导出(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ")");
                eh.WriteMultiWorkSheetExcelToClient();
            }
            catch (Exception ex)
            {
                myWatch3.Stop();
                _log.Info("导出绩效历史数据;写入到Excel耗时(毫秒)：" + myWatch3.ElapsedMilliseconds );
                _log.Error(string.Format("导出绩效历史数据出现异常，异常信息：{0} ;异常详情：{1}", ex.Message, ex));
                Assert(lblMsg, ex.Message, -1);
                //txtProductID.Text = ex.Message;
            }
        }
    }
}