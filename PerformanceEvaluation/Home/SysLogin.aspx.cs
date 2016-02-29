using log4net;
using PerformanceEvaluation.Cmn;
using PerformanceEvaluation.PerformanceEvaluation.Biz;
using PerformanceEvaluation.PerformanceEvaluation.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceEvaluation.PerformanceEvaluation.Home
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private ILog _log = log4net.LogManager.GetLogger(typeof(WebForm1));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
            btnLogin.Attributes.Add("onkeydown", "if(event.keyCode==13){document.all.btnLogin.focus();event.keyCode=13;return true;}");
        }

        private void Assert(Label lbl, string msg, int status)
        {
            if (status <= 0)
            {
                lbl.Text = "<font color=red>" + msg + "</font>";
            }
            else
            {
                lbl.Text = "<font color=blue>" + msg + "</font>";
            }
        }

        private bool checkForm()
        {
            if (string.IsNullOrEmpty(txtUserID.Text.Trim()))
            {
                Assert(lblMessage, "用户名不可为空", -1);
                return false;
            }
            if (string.IsNullOrEmpty(txtPwd.Text.Trim()))
            {
                Assert(lblMessage, "密码不可为空", -1);
                return false;
            }
            if (Request.Cookies["CheckCode"] == null)
            {
                Assert(lblMessage, "您的浏览器设置已被禁用 Cookies，您必须设置浏览器允许使用 Cookies 选项后才能使用本系统。", -1);
                return false;
            }
            if (String.Compare(Request.Cookies["CheckCode"].Value, txtCode.Text.ToString().Trim(), true) != 0)
            {
                Assert(lblMessage, "对不起，验证码错误！", -1);
                return false;
            }
            return true;
        }

        //登录
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkForm())
                {
                    return;
                }
                int userSysNo;
                if (!int.TryParse(txtUserID.Text.Trim(), out userSysNo))
                {
                    Assert(lblMessage, "无此用户", -1);
                    return;
                }
                PersonInfoEntity oUser = BasicManager.GetInstance().LoadUser(userSysNo);
                if (oUser == null || oUser.IsLogin != (int)AppEnum.YNStatus.Yes)
                {
                    Assert(lblMessage, "无此用户", -1);
                    return;
                }
                if (oUser.Status != (int)AppEnum.BiStatus.Valid)
                {
                    Assert(lblMessage, "用户状态无效", -1);
                    return;
                }
                if (oUser.LoginPwd.ToUpper() == CommonFunctions.md5(txtPwd.Text.Trim() + AppConst.KEY_MD5_MIS) && oUser.Status == (int)AppEnum.BiStatus.Valid)
                {
                    SessionInfo session = new SessionInfo();
                    session.IPAddress = CommonFunctions.GetClientIP(Request);
                    if (oUser.IsAdmin == (int)AppEnum.YNStatus.Yes)
                    {
                        if (oUser.ParentPersonSysNo == 99999)
                        {
                            oUser.UserType = 3;//公司老大
                        }
                        else
                        {
                            oUser.UserType = 2;//绩效管理员
                        }
                    }
                    else
                    {
                        oUser.UserType = 1;//普通用户
                    }
                    oUser.EJBAdmin = 0;
                    OrganEntity oe = BasicManager.GetInstance().GetEJBOrgan(oUser.SysNo, (int)AppEnum.OrganType.EJB);
                    if (oe != null && oe.SysNo != AppConst.IntNull)
                    {
                        //二级部管理人员
                        oUser.EJBAdmin = 1;
                    }
                    session.User = oUser;
                    if (oUser != null)
                    {

                        List<Custom_Sys_MenuEntity> menuList = GetMenuList();
                        if (menuList != null)
                        {
                            session.menuList = menuList;
                        }
                        //UserManager userUM = new UserManager();
                        //userUM.UserEntity = oUser;
                        //userUM.LoadPrivilege();
                        //session.PrivilegeHt = userUM.UserPrivilegeDic;
                    }
                    Session["LoginSession"] = session;
                    Response.Redirect("~/Home/Default.aspx", true);
                }
                else
                {
                    Assert(lblMessage, "用户名或密码错误", -1);
                    return;
                }
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("登录出现异常，异常信息：{0} ;异常详情：{1}", ex.Message, ex));
                Assert(lblMessage, "登录出现异常：" + ex.Message, -1);
            }
        }

        //获取菜单列表
        public List<Custom_Sys_MenuEntity> GetMenuList()
        {
            List<Custom_Sys_MenuEntity> menuList = new List<Custom_Sys_MenuEntity>();
            Custom_Sys_MenuEntity mA = new Custom_Sys_MenuEntity();
            mA.SysNo = 1;
            mA.MenuName = "日常管理";
            mA.Status = 0;
            menuList.Add(mA);
            Custom_Sys_MenuEntity mAA = new Custom_Sys_MenuEntity();
            mAA.SysNo = 2;
            mAA.M1SysNo = 1;
            mAA.MenuName = "绩效管理";
            mAA.Status = 0;
            menuList.Add(mAA);
            Custom_Sys_MenuEntity mAA1 = new Custom_Sys_MenuEntity();
            mAA1.SysNo = 3;
            mAA1.M1SysNo = 1;
            mAA1.M2SysNo = 2;
            mAA1.MenuName = "绩效历史查询";
            mAA1.MenuLink = "../Basic/PerformanceHistory.aspx";
            mAA1.Status = 0;
            menuList.Add(mAA1);
            Custom_Sys_MenuEntity mAA2 = new Custom_Sys_MenuEntity();
            mAA2.SysNo = 4;
            mAA2.M1SysNo = 1;
            mAA2.M2SysNo = 2;
            mAA2.MenuName = "当期绩效评分";
            mAA2.MenuLink = "../Basic/PerformanceRating.aspx";
            mAA2.Status = 0;
            menuList.Add(mAA2);
            Custom_Sys_MenuEntity mAA3 = new Custom_Sys_MenuEntity();
            mAA3.SysNo = 5;
            mAA3.M1SysNo = 1;
            mAA3.M2SysNo = 2;
            mAA3.MenuName = "员工信息管理";
            mAA3.MenuLink = "../Basic/PersonSearch.aspx";
            mAA3.Status = 0;
            menuList.Add(mAA3);
            //Custom_Sys_MenuEntity mAA4 = new Custom_Sys_MenuEntity();
            //mAA4.SysNo = 6;
            //mAA4.M1SysNo = 1;
            //mAA4.M2SysNo = 2;
            //mAA4.MenuName = "商户查询4";
            //mAA4.MenuLink = "../Basic/PerformanceRating.aspx";
            //mAA4.Status = 0;
            //menuList.Add(mAA4);
            return menuList;
        }
    }
}