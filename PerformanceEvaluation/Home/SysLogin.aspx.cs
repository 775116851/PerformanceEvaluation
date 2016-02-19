using PerformanceEvaluation.Cmn;
using PerformanceEvaluation.PerformanceEvaluation.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceEvaluation.PerformanceEvaluation.Home
{
    public partial class SysLogin : System.Web.UI.Page
    {
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
            if (!checkForm())
            {
                return;
            }
            SessionInfo session = new SessionInfo();
            session.IPAddress = CommonFunctions.GetClientIP(Request);
            //session.User = oUser;
            //if (oUser != null)
            //{
            //    UserManager userUM = new UserManager();
            //    userUM.UserEntity = oUser;
            //    userUM.LoadPrivilege();
            //    session.PrivilegeHt = userUM.UserPrivilegeDic;
            //}
            Session["LoginSession"] = session;
            Response.Redirect("~/Home/Default.aspx", true);
        }

    }
}