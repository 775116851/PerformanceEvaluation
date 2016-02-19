using log4net;
using PerformanceEvaluation.PerformanceEvaluation.Info;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceEvaluation.PerformanceEvaluation.Code
{
    public class PageBase : Page
    {
        private ILog _log = log4net.LogManager.GetLogger(typeof(PageBase));
        protected SessionInfo sInfo { get { return (Session["LoginSession"] as SessionInfo); } }
        private SessionInfo _LoginSession = null;
        public SessionInfo LoginSession
        {
            get
            {
                SessionInfo sessionLogin = Session["LoginSession"] as SessionInfo;
                return (Session["LoginSession"] as SessionInfo);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (CheckAuthentication() == false)
            {
                return;
            }
        }

        protected bool CheckAuthentication()
        {
            bool result = false;
            _LoginSession = Session["LoginSession"] as SessionInfo;
            if (_LoginSession == null)
            {
                Response.Redirect("~/Home/SysLogin.aspx", true);
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        protected void Assert(Label lbl, string msg, int status)
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

        protected void PreventRepeatSubmit(int sysNo, string paramName)
        {
            if (Request.Headers["Accept"] == "*/*")
            {
                if (Request.Url.ToString().IndexOf('?') < 0)
                {
                    Response.Redirect(Request.Url.ToString() + "?" + paramName + "=" + sysNo, true);
                }
                else
                {
                    Response.Redirect(Request.Url.ToString(), true);
                }
            }
        }
        protected void PreventRepeatSubmit(int sysNo)
        {
            if (Request.Headers["Accept"] == "*/*")
            {
                if (Request.Url.ToString().IndexOf('?') < 0)
                {
                    Response.Redirect(Request.Url.ToString() + "?SysNo=" + sysNo, true);
                }
                else
                {
                    Response.Redirect(Request.Url.ToString(), true);
                }
            }
        }
        protected void CheckPath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        protected Boolean IsImage(Stream stream)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                return true;
            }
            catch
            {
                return false;
            }
        }
        protected bool CheckAccess(int privilegeSysNo)
        {
            //if (LoginSession != null)
            //{
            //    Dictionary<int, Sys_PrivilegeEntity> ht = LoginSession.PrivilegeHt;
            //    if (ht == null || ht.Count == 0) return false;
            //    if (ht.ContainsKey((int)AppEnum.Privilege.Organ_Administrator)) // 如果是全权用户
            //    {
            //        return true; // 全权用户默认返回True
            //    }
            //    if (!ht.ContainsKey(privilegeSysNo)) return false;

            //    return true;
            //}
            //else
            //{
                return false;
            //}
        }

        //权限验证
        protected bool CheckAccess(int privilegeSysNo, bool isRedirect, Label lbl)
        {
            if (LoginSession != null)
            {
                if (CheckAccess(privilegeSysNo))
                {
                    return true;
                }
                if (isRedirect)
                {
                    Server.Transfer("~/SysManage/SysPrivilegeNoAllow.aspx?s=" + privilegeSysNo, true);
                }
                if (lbl != null)
                {
                    Assert(lbl, "您没有权限进行此项操作，请申请权限****** " + privilegeSysNo + "", -1);
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}