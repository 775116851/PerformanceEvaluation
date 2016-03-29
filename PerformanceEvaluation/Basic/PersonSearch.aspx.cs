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
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        //导入数据
        protected void btnUploadCode_Click(object sender, EventArgs e)
        {
            if (FileUpload1.FileBytes.LongLength > 0)
            {
                string filenName = FileUpload1.FileName;
                string fileExt = System.IO.Path.GetExtension(filenName);
                if (fileExt == ".xls" || fileExt == ".xlsx")
                {
                    //int productSysNo = sysNo;
                    string fileDateTimeName = DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh")
                         + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss") + DateTime.Now.ToString("fff");
                    string preFileName = System.IO.Path.GetFileNameWithoutExtension(filenName);
                    preFileName = preFileName.Replace(preFileName, fileDateTimeName);
                    string filePath = Server.MapPath("~/file/") + preFileName + fileExt;
                    if (!Directory.Exists(Server.MapPath("~/file")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/file"));
                    }
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    FileUpload1.SaveAs(filePath);
                    try
                    {
                        Tuple<bool,List<PersonInfoEntity>,string> result = LoadListFromExcel(filePath);
                        if(result.Item1 == false)
                        {
                            File.Delete(filePath);
                            Assert(lblMsg, result.Item3, -1);
                            return;
                        }
                        if(result.Item2 == null || result.Item2.Count <= 0)
                        {
                            File.Delete(filePath);
                            Assert(lblMsg, "读取Excel为空或模版列名不对!" + result.Item3, -1);
                            return;
                        }
                        Tuple<int, string> rMessage = BasicManager.GetInstance().ImportPersonInfo(result.Item2);
                        if (rMessage.Item1 == -1)
                        {
                            File.Delete(filePath);
                            Assert(lblMsg, "员工信息导入失败!" + rMessage.Item2 + "_" + result.Item3, -1);
                            return;
                        }
                        else
                        {
                            Assert(lblMsg, "员工信息导入成功!成功导入" + rMessage.Item1 + "条数据;" + result.Item3, 1);
                        }

                        //DataSet set = LoadDataFromExcel(filePath);
                        //if (!Util.HasMoreRow(set))
                        //{
                        //    File.Delete(filePath);
                        //    Assert(lblMsg, "读取Excel为空或模版列名不对!", -1);
                        //    return;
                        //}
                        //DataTable table = set.Tables[0];
                        //table.Columns[0].ColumnName = "Code";
                        //if (table.Rows.Count > 1000)
                        //{
                        //    File.Delete(filePath);
                        //    Assert(lblMsg, "商品串码每次仅限导入一千条，请分批导入!", -1);
                        //    return;
                        //}
                        //DateTime dtTime = DateTime.Now;
                        //List<Product_CodeEntity> listCode = new List<Product_CodeEntity>();
                        //for (int i = 0; i < table.Rows.Count; i++)
                        //{
                        //    DataRow drCode = table.Rows[i];
                        //    if (!(drCode["Code"] == null || drCode["Code"].ToString().Trim().Length == 0))
                        //    {
                        //        Product_CodeEntity pce = new Product_CodeEntity();
                        //        pce.Code = Convert.ToString(drCode["Code"]).Trim();
                        //        pce.ProductSysNo = productSysNo;
                        //        pce.Status = (int)AppEnum.ProductCodeStatus.UnUse;
                        //        pce.CreateTime = dtTime;
                        //        pce.CreateUser = LoginSession.User.SysNo;
                        //        listCode.Add(pce);
                        //    }
                        //    else
                        //    {
                        //        table.Rows.RemoveAt(i);
                        //        i--;
                        //    }
                        //}
                        //if (listCode.Count <= 0)
                        //{
                        //    File.Delete(filePath);
                        //    Assert(lblMsg, "读取的Excel为空!", -1);
                        //    return;
                        //}
                        ////判断excel中是否存在相同数据
                        //var qOne = (from p in listCode group p by p.Code into g where g.Count() > 1 select new { Code = g.Key }).ToList();
                        //if (qOne.Count > 0)
                        //{
                        //    StringBuilder sbError = new StringBuilder();
                        //    sbError.Append("导码失败!Excel中存在以下重复串码：");
                        //    foreach (var m in qOne)
                        //    {
                        //        sbError.Append(m.Code).Append(";");
                        //    }
                        //    File.Delete(filePath);
                        //    Assert(lblMsg, sbError.ToString(), -1);
                        //    return;
                        //}
                        //Tuple<int, string> rMessage = ProductManager.GetInstance().ImportProductCode(listCode);
                        //if (rMessage.Item1 == -1)
                        //{
                        //    File.Delete(filePath);
                        //    Assert(lblMsg, "导码失败!" + rMessage.Item2, -1);
                        //    return;
                        //}
                        //else
                        //{
                        //    string note = "";
                        //    note = "AccountQty:由" + product.Product_Inventory.AccountQty + "到" + product.Product_Inventory.AccountQty + rMessage.Item1 + "";
                        //    LogManager.GetInstance().AddLogForNote(productSysNo, (int)AppEnum.LogType.Product_ProductInventory_Update, (int)AppEnum.LogWriteType.Update, LoginSession, note);
                        //    BindRep1();
                        //    Assert(lblMsg, "导码成功!成功导入" + rMessage.Item1 + "条数据", 1);
                        //}
                    }
                    catch (Exception ex)
                    {
                        File.Delete(filePath);
                        Assert(lblMsg, ex.Message, -1);
                        return;
                    }
                }
                else
                {
                    Assert(lblMsg, "请选择后缀名是.xls、.xlsx的文件", -1);
                    return;
                }
            }
            else
            {
                Assert(lblMsg, "请先选择需要导入员工信息的excel文件", -1);
                return;
            }
        }

        /// <summary>
        /// 访问文件(Excel)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private DataSet LoadDataFromExcel(string filePath)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
            if (filePath.EndsWith(".xlsx"))
            {
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1'";
            }
            using (OleDbConnection OleConn = new OleDbConnection(strConn))
            {
                OleConn.Open();
                String sql = "SELECT * FROM [更新表单$] ";
                OleDbDataAdapter OleDaExcel = new OleDbDataAdapter(sql, OleConn);
                DataSet OleDsExcle = new DataSet();
                OleDaExcel.Fill(OleDsExcle, "Sheet1");
                OleConn.Close();
                if (Util.HasMoreRow(OleDsExcle))
                {
                    if (OleDsExcle.Tables[0].Rows.Count <= 2)
                    {

                    }
                    OleDsExcle.Tables[0].Rows.RemoveAt(0); //第一行是标题
                    OleDsExcle.Tables[0].Rows.RemoveAt(0); //第一行是标题
                    int rows = OleDsExcle.Tables[0].Rows.Count;
                    for (int i = 0; i < rows; i++)
                    {
                        String account = OleDsExcle.Tables[0].Rows[i][0].ToString().Trim();
                        String regex = @"([+-]?)(\d\.\d{1,})[Ee]([+-])(\d+)";
                        if (Regex.IsMatch(account, regex))
                        {
                            string err = string.Format("第{0}行串码{1}格式不正确", i + 2, account);
                            throw new Exception(err);
                        }
                    }
                }
                return OleDsExcle;
            }
        }

        private Tuple<bool,List<PersonInfoEntity>,string> LoadListFromExcel(string filePath)
        {
            DateTime dtTime = DateTime.Now;
            StringBuilder sbError = new StringBuilder();
            List<PersonInfoEntity> list = new List<PersonInfoEntity>();
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
            if (filePath.EndsWith(".xlsx"))
            {
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1'";
            }
            try
            {
                using (OleDbConnection OleConn = new OleDbConnection(strConn))
                {
                    OleConn.Open();
                    String sql = "SELECT * FROM [更新表单$] WHERE F1 IS NOT NULL ";
                    OleDbDataAdapter OleDaExcel = new OleDbDataAdapter(sql, OleConn);
                    DataSet OleDsExcle = new DataSet();
                    OleDaExcel.Fill(OleDsExcle, "Sheet1");
                    OleConn.Close();
                    if (Util.HasMoreRow(OleDsExcle))
                    {
                        if (OleDsExcle.Tables[0].Rows.Count <= 1)
                        {
                            return Tuple.Create<bool, List<PersonInfoEntity>, string>(false, null, "读取Excel为空或模版列名不对2!");
                        }
                        OleDsExcle.Tables[0].Rows.RemoveAt(0); //第一行是标题
                        int rows = OleDsExcle.Tables[0].Rows.Count;
                        int i = 2;
                        foreach (DataRow dr in OleDsExcle.Tables[0].Rows)
                        { 
                            i++;
                            PersonInfoEntity pie = new PersonInfoEntity();
                            string czbz = Convert.ToString(dr[0]).Trim();
                            if (string.IsNullOrEmpty(czbz) || (czbz != "D" && czbz != "U" && czbz != "A"))
                            {
                                sbError.Append("Excel文档第" + i + "行数据有误，操作标志为空或无效，该行已被过滤;");
                                continue;
                            }
                            pie.CZBZ = czbz;
                            string sysNo = Convert.ToString(dr[1]).Trim();
                            int iSysNo = AppConst.IntNull;
                            if (!string.IsNullOrEmpty(sysNo) && int.TryParse(sysNo, out iSysNo))
                            {
                                pie.SysNo = iSysNo;
                            }
                            else
                            {
                                if (czbz == "D" || czbz == "U")
                                {
                                    sbError.Append("Excel文档第" + i + "行数据有误，操作标志为修改或删除时编号为空或无效，该行已被过滤;");
                                    continue;
                                }
                            }
                            string name = Convert.ToString(dr[2]).Trim();
                            if (!string.IsNullOrEmpty(name))
                            {
                                pie.Name = name;
                            }
                            else
                            {
                                continue;
                            }
                            string ejbName = Convert.ToString(dr[3]).Trim();
                            if (!string.IsNullOrEmpty(ejbName))
                            {
                                pie.EJBName = ejbName;
                            }
                            string znsName = Convert.ToString(dr[4]).Trim();
                            if (!string.IsNullOrEmpty(znsName))
                            {
                                pie.ZNSName = znsName;
                            }
                            string ygnxName = Convert.ToString(dr[5]).Trim();
                            if (!string.IsNullOrEmpty(ygnxName))
                            {
                                pie.YGNXName = ygnxName;
                            }
                            string birthDate = Convert.ToString(dr[6]).Trim();
                            DateTime iBirthDate = AppConst.DateTimeNull;
                            if (!string.IsNullOrEmpty(birthDate) && DateTime.TryParse(birthDate, out iBirthDate))
                            {
                                pie.BirthDate = iBirthDate;
                            }
                            string entryDate = Convert.ToString(dr[7]).Trim();
                            DateTime iEntryDate = AppConst.DateTimeNull;
                            if (!string.IsNullOrEmpty(entryDate) && DateTime.TryParse(entryDate, out iEntryDate))
                            {
                                pie.EntryDate = iEntryDate;
                            }
                            string positiveDate = Convert.ToString(dr[8]).Trim();
                            DateTime iPositiveDate = AppConst.DateTimeNull;
                            if (!string.IsNullOrEmpty(positiveDate) && DateTime.TryParse(positiveDate, out iPositiveDate))
                            {
                                pie.PositiveDate = iPositiveDate;
                            }
                            string outDate = Convert.ToString(dr[9]).Trim();
                            DateTime iOutDate = AppConst.DateTimeNull;
                            if (!string.IsNullOrEmpty(outDate) && DateTime.TryParse(outDate, out iOutDate))
                            {
                                pie.OutDate = iOutDate;
                            }
                            string qQ = Convert.ToString(dr[10]).Trim();
                            if (!string.IsNullOrEmpty(qQ))
                            {
                                pie.QQ = qQ;
                            }
                            string mobilePhone = Convert.ToString(dr[11]).Trim();
                            if (!string.IsNullOrEmpty(mobilePhone))
                            {
                                pie.MobilePhone = mobilePhone;
                            }
                            string email = Convert.ToString(dr[12]).Trim();
                            if (!string.IsNullOrEmpty(email))
                            {
                                pie.Email = email;
                            }
                            string note = Convert.ToString(dr[13]).Trim();
                            if (!string.IsNullOrEmpty(note))
                            {
                                pie.Note = note;
                            }
                            pie.LastUpdateTime = dtTime;
                            pie.LastUpdateUserSysNo = LoginSession.User.SysNo;
                            list.Add(pie);
                        }
                    }
                    return Tuple.Create<bool, List<PersonInfoEntity>, string>(true, list, sbError.ToString());
                }
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("导入文件在读取excel时出现异常，异常信息:{0} ;异常详情:{1}", ex.Message, ex));
                return Tuple.Create<bool, List<PersonInfoEntity>, string>(false, null, ex.Message);
            }
        }

        protected void linkbtnDown_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/Images/员工信息导入模板.xls");
            string fileExt = System.IO.Path.GetExtension(path);
            if (fileExt == ".xls")
            {
                DownTemplate(path);
            }
        }

        private void DownTemplate(string path)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            Response.Clear();
            Response.ClearHeaders();
            Response.Buffer = false;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(System.IO.Path.GetFileName(path)));
            Response.AppendHeader("Content-Length", fi.Length.ToString());
            Response.WriteFile(path);
            Response.Flush();
            Response.End();
        }
    }
}