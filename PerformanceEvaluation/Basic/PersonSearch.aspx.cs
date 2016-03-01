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
    public partial class PersonSearch : PageBase
    {
        private ILog _log = log4net.LogManager.GetLogger(typeof(PersonSearch));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitUi();
                if (LoginSession.User.UserType != 2)
                {
                    Assert(lblMsg, "非绩效管理员不允许操作", -1);
                    return;
                }
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

        //导出
        protected void btExport_Click(object sender, EventArgs e)
        {
            if (LoginSession.User.UserType != 2)
            {
                Assert(lblMsg, "非绩效管理员不允许操作", -1);
                return;
            }
            Hashtable ht = new Hashtable();
            if (!string.IsNullOrEmpty(txtPersonName.Text.Trim()))
            {
                ht.Add("Name", txtPersonName.Text.Trim());
            }
            if(ddlStatus.KeyValue != AppConst.IntNull)
            {
                ht.Add("Status", ddlStatus.KeyValue);
            }
            if(ddlOrgan.SelectedValue != AppConst.IntNull.ToString())
            {
                ht.Add("OrganSysNo", Convert.ToInt32(ddlOrgan.SelectedValue));
            }
            if (!string.IsNullOrEmpty(hidClassSysNo.Value.Trim()) && hidClassSysNo.Value != AppConst.IntNull.ToString())
            {
                ht.Add("ClassSysNo", Convert.ToInt32(hidClassSysNo.Value.Trim()));
            }
            if(ddlPersonType.SelectedValue != AppConst.IntNull.ToString())
            {
                ht.Add("PersonTypeSysNo", Convert.ToInt32(ddlPersonType.SelectedValue));
            }
            ht.Add("IsAdmin", 0);
            List<DataTable> dtList = new List<DataTable>();
            List<string> workSheetNameList = new List<string>();
            //记录运行时间
            Stopwatch myWatch2 = Stopwatch.StartNew();
            DataSet ds = BasicManager.GetInstance().GetPersonList(1, 10000000, ht);
            myWatch2.Stop();
            _log.Info("导出员工信息列表数据;正式访问数据库查询所有列耗时(毫秒)：" + myWatch2.ElapsedMilliseconds);
            if (Util.HasMoreRow(ds))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("编号");
                dt.Columns.Add("员工姓名");
                dt.Columns.Add("二级部");
                dt.Columns.Add("职能室");
                dt.Columns.Add("员工类型");
                dt.Columns.Add("入职日期");
                dt.Columns.Add("允许登录");
                dt.Columns.Add("状态");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow newDr = dt.NewRow();
                    newDr["编号"] = Convert.ToString(dr["SysNo"]);
                    newDr["员工姓名"] = Convert.ToString(dr["Name"]);
                    newDr["二级部"] = Convert.ToString(dr["OrganName"]);
                    newDr["职能室"] = Convert.ToString(dr["FunctionInfo"]);
                    newDr["员工类型"] = Convert.ToString(dr["TypeName"]);
                    newDr["入职日期"] = Convert.ToString(dr["EntryDate"]);
                    newDr["允许登录"] = AppEnum.GetDescription(typeof(AppEnum.YNStatus),Convert.ToInt32(dr["IsLogin"])) ;
                    newDr["状态"] = AppEnum.GetDescription(typeof(AppEnum.BiStatus),Convert.ToInt32(dr["Status"])) ;;
                    dt.Rows.Add(newDr);
                }
                workSheetNameList.Add("员工信息列表");
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
                ExcelHelper eh = new ExcelHelper(dtList, workSheetNameList, "员工信息列表导出(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ")");
                eh.WriteMultiWorkSheetExcelToClient();
            }
            catch (Exception ex)
            {
                myWatch3.Stop();
                _log.Info("导出员工信息列表数据;写入到Excel耗时(毫秒)：" + myWatch3.ElapsedMilliseconds);
                _log.Error(string.Format("导出员工信息列表数据出现异常，异常信息：{0} ;异常详情：{1}", ex.Message, ex));
                Assert(lblMsg, ex.Message, -1);
                //txtProductID.Text = ex.Message;
            }
        }
    }
}