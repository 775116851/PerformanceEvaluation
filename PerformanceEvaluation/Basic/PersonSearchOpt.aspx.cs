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
        bool isTJ = true;
        protected int SysNo
        {
            set { ViewState["SysNo"] = value; }
            get { return Convert.ToInt32(ViewState["SysNo"]); }
        }
        //查看用户
        protected int OptType
        {
            set { ViewState["OptType"] = value; }
            get { return Convert.ToInt32(ViewState["OptType"]); }
        }
        private ILog _log = log4net.LogManager.GetLogger(typeof(PersonSearchOpt));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(LoginSession.User.UserType != 2)
                {
                    btnSave.Visible = false;
                    btnSave.Enabled = false;
                    Assert(lblMsg, "非绩效管理员不允许操作", -1);
                    return;
                }
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
                if (Request.QueryString["OptType"] != null && Request.QueryString["OptType"].Trim() != "")
                {
                    OptType = Convert.ToInt32(Request.QueryString["OptType"].ToString());
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

            if (SysNo != AppConst.IntNull)
            {
                btnSave.Visible = false;
                btnSave.Enabled = false;
                if (OptType == 1)
                {
                    btnSave.Enabled = true;
                    btnSave.Visible = true;
                }
                PersonInfoEntity pie = BasicManager.GetInstance().LoadUser(SysNo);
                if (pie == null || pie.SysNo == AppConst.IntNull)
                {
                    Assert(lblMsg, "员工信息不存在", -1);
                    return;
                }
                if (pie.IsAdmin == (int)AppEnum.YNStatus.Yes)
                {
                    Assert(lblMsg, "不允许修改管理员", -1);
                    return;
                }
                txtPersonName.Text = pie.Name;
                if (pie.OrganSysNo != AppConst.IntNull)
                {
                    ddlOrgan.SelectedValue = pie.OrganSysNo.ToString();
                    //职能室列表
                    Dictionary<int, OrganEntity> classList = BasicManager.GetInstance().GetClassList(pie.OrganSysNo);
                    if (classList != null && classList.Count != 0)
                    {
                        for (int i = 0; i < classList.Count; i++)
                        {
                            ddlClass.Items.Add(new ListItem(classList.Values.ElementAt(i).FunctionInfo, classList.Values.ElementAt(i).SysNo.ToString()));
                        }
                    }
                    ddlClass.SelectedValue = pie.ClassSysNo.ToString();
                    hidClassSysNo.Value = pie.ClassSysNo.ToString();
                }
                ddlPersonType.SelectedValue = pie.PersonTypeSysNo.ToString();
                if (pie.ParentPersonSysNo != AppConst.IntNull)
                {
                    hdnParentPersonSysNo.Value = pie.ParentPersonSysNo.ToString();
                    PersonInfoEntity pE = BasicManager.GetInstance().LoadUser(pie.ParentPersonSysNo);
                    if (pE != null && pE.SysNo != AppConst.IntNull)
                    {
                        txtParentPersonName.Value = pE.Name;
                    }
                }
                txtSkillCategory.Text = pie.SkillCategory;
                ddlGender.KeyValue = pie.Gender;
                txtMobilePhone.Text = pie.MobilePhone;
                txtTelPhone.Text = pie.TelPhone;
                txtQQ.Text = pie.QQ;
                txtEmail.Text = pie.Email;
                if(pie.BirthDate != AppConst.DateTimeNull)
                {
                    textBirthDate.Value = pie.BirthDate.ToString("yyyy-MM-dd");
                }
                if(pie.EntryDate != AppConst.DateTimeNull)
                {
                    textEntryDate.Value = pie.EntryDate.ToString("yyyy-MM-dd");
                }
                if (pie.PositiveDate != AppConst.DateTimeNull)
                {
                    textPositiveDate.Value = pie.PositiveDate.ToString("yyyy-MM-dd");
                }
                if (pie.OutDate != AppConst.DateTimeNull)
                {
                    textOutDate.Value = pie.OutDate.ToString("yyyy-MM-dd");
                }
                ddlIsLogin.KeyValue = pie.IsLogin;
                ddlStatus.KeyValue = pie.Status;
                txtNote.Text = pie.Note;
            }


        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtPersonName.Text.Trim()))
            {
                Assert(lblMsg, "请输入员工姓名", -1);
                return;
            }
            if(ddlOrgan.SelectedValue == AppConst.IntNull.ToString())
            {
                Assert(lblMsg, "请选择所属机构", -1);
                return;
            }
            if(ddlPersonType.SelectedValue == AppConst.IntNull.ToString())
            {
                Assert(lblMsg, "请选择员工类型", -1);
                return;
            }
            if(ddlIsLogin.KeyValue == AppConst.IntNull)
            {
                Assert(lblMsg, "请选择是否允许登录", -1);
                return;
            }
            if (ddlStatus.KeyValue == AppConst.IntNull)
            {
                Assert(lblMsg, "请选择状态", -1);
                return;
            }
            DateTime dtTime = DateTime.Now;
            PersonInfoEntity model = new PersonInfoEntity();
            model.SysNo = SysNo;
            if (SysNo == AppConst.IntNull)
            {
                
                model.CreateTime = dtTime;
                model.CreateUserSysNo = LoginSession.User.SysNo;
                model.IsAdmin = (int)AppEnum.YNStatus.No;
                model.LoginPwd = "9577C930E002DFE330CEFAFBA8DF82DE";//初始密码 12345a
            }
            model.OrganSysNo = Convert.ToInt32(ddlOrgan.SelectedValue);
            if (!string.IsNullOrEmpty(hidClassSysNo.Value.Trim()) && hidClassSysNo.Value != AppConst.IntNull.ToString())
            {
                model.ClassSysNo = Convert.ToInt32(hidClassSysNo.Value.Trim());
            }
            model.Name = txtPersonName.Text.Trim();
            model.PersonTypeSysNo = Convert.ToInt32(ddlPersonType.SelectedValue);
            if (!string.IsNullOrEmpty(textBirthDate.Value.Trim()))
            {
                model.BirthDate = Convert.ToDateTime(textBirthDate.Value.Trim());
            }
            if (!string.IsNullOrEmpty(textEntryDate.Value.Trim()))
            {
                model.EntryDate = Convert.ToDateTime(textEntryDate.Value.Trim());
            }
            if (!string.IsNullOrEmpty(textPositiveDate.Value.Trim()))
            {
                model.PositiveDate = Convert.ToDateTime(textPositiveDate.Value.Trim());
            }
            if (!string.IsNullOrEmpty(textOutDate.Value.Trim()))
            {
                model.OutDate = Convert.ToDateTime(textOutDate.Value.Trim());
            }
            model.Status = ddlStatus.KeyValue;
            if (!string.IsNullOrEmpty(txtSkillCategory.Text.Trim()))
            {
                model.SkillCategory = txtSkillCategory.Text.Trim();
            }
            if (!string.IsNullOrEmpty(hdnParentPersonSysNo.Value.Trim()) && hdnParentPersonSysNo.Value != AppConst.IntNull.ToString())
            {
                model.ParentPersonSysNo = Convert.ToInt32(hdnParentPersonSysNo.Value.Trim());
            }
            if (!string.IsNullOrEmpty(txtMobilePhone.Text.Trim()))
            {
                model.MobilePhone = txtMobilePhone.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtTelPhone.Text.Trim()))
            {
                model.TelPhone = txtTelPhone.Text.Trim();
            }
            if (ddlGender.KeyValue != AppConst.IntNull)
            {
                model.Gender = ddlGender.KeyValue;
            }
            if (!string.IsNullOrEmpty(txtQQ.Text.Trim()))
            {
                model.QQ = txtQQ.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                model.Email = txtEmail.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtNote.Text.Trim()))
            {
                model.Note = txtNote.Text.Trim();
            }
            model.IsLogin = ddlIsLogin.KeyValue;
            model.LastUpdateTime = dtTime;
            model.LastUpdateUserSysNo = LoginSession.User.SysNo;
            try
            {
                if (isTJ == true)
                {
                    isTJ = false;
                    int result = BasicManager.GetInstance().SaveUser(model);
                    if (model.SysNo == AppConst.IntNull)
                    {
                        SysNo = result;
                        Assert(lblMsg, "添加员工信息成功!", 1);
                    }
                    else
                    {
                        Assert(lblMsg, "修改员工信息成功!", 1);
                    }
                    isTJ = true;
                    //职能室列表
                    Dictionary<int, OrganEntity> classList = BasicManager.GetInstance().GetClassList(model.OrganSysNo);
                    if (classList != null && classList.Count != 0)
                    {
                        for (int i = 0; i < classList.Count; i++)
                        {
                            ddlClass.Items.Add(new ListItem(classList.Values.ElementAt(i).FunctionInfo, classList.Values.ElementAt(i).SysNo.ToString()));
                        }
                    }
                    ddlClass.SelectedValue = model.ClassSysNo.ToString();
                    hidClassSysNo.Value = model.ClassSysNo.ToString();
                }
                isTJ = true;
            }
            catch (Exception ex)
            {
                isTJ = true;
                Assert(lblMsg, "数据保存失败!" + ex.Message, -1);
            }
            isTJ = true;
        }
    }
}