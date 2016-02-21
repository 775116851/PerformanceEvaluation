using log4net;
using PerformanceEvaluation.Cmn;
using PerformanceEvaluation.PerformanceEvaluation.Biz;
using PerformanceEvaluation.PerformanceEvaluation.Code;
using PerformanceEvaluation.PerformanceEvaluation.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceEvaluation.PerformanceEvaluation.Home
{
    public partial class ChangePassword : PageBase
    {
        private ILog _log = log4net.LogManager.GetLogger(typeof(ChangePassword));
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool checkForm()
        {
            if (txtNewPwd.Value.Trim() == "")
            {
                Assert(lblMessage, "新密码不可为空", -1);
                return false;
            }
            if (txtNewPwd.Value.Trim() != txtConfirmNewPwd.Value.Trim())
            {
                Assert(lblMessage, "新密码两次输入不相同", -1);
                return false;
            }
            Regex objNotWholePattern = new Regex(@"^(?![^a-zA-Z]+$)(?!\D+$).{6,16}$");
            if (!objNotWholePattern.IsMatch(txtNewPwd.Value.Trim()))
            {
                Assert(lblMessage, "新密码必须含字母数字且不少于6位,不长于16位", -1);
                return false;
            }
            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!checkForm())
            {
                return;
            }
            PersonInfoEntity oUser = LoginSession.User;
            if (oUser == null)
            {
                Assert(lblMessage, "用户不存在", -1);
                return;
            }
            if (oUser.LoginPwd.ToUpper() == Util.GetMD5(txtOldPwd.Value.Trim() + AppConst.KEY_MD5_MIS).ToUpper())
            {
                try
                {
                    PersonInfoEntity model = new PersonInfoEntity();
                    model.SysNo = oUser.SysNo;
                    model.LoginPwd = Util.GetMD5(txtNewPwd.Value.Trim() + AppConst.KEY_MD5_MIS);
                    model.LastUpdateTime = DateTime.Now;
                    model.LastUpdateUserSysNo = oUser.SysNo;
                    BasicManager.GetInstance().SaveUser(model);
                    Assert(lblMessage, "修改密码成功!", 1);
                }
                catch (Exception exp)
                {
                    _log.Error(string.Format("新密码设置失败,失败信息:{0} ;失败详情:{1}",exp.Message,exp));
                    Assert(lblMessage, "新密码设置失败，请联系管理员", -1);
                    //LogManagement.getInstance().WriteException(exp.ToString());
                }
            }
            else
            {
                Assert(lblMessage, "旧密码输入不正确", -1);
            }
        }
    }
}