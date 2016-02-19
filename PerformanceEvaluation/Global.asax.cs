using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace PerformanceEvaluation.PerformanceEvaluation
{
    public class Global : System.Web.HttpApplication
    {
        private ILog _log = log4net.LogManager.GetLogger(typeof(Global));
        protected void Application_Start(object sender, EventArgs e)
        {
            _log.Info("1111111");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception erroy = Server.GetLastError();
            string err = "出错页面是：" + Request.Url.ToString() + Environment.NewLine;
            err += "异常信息：" + erroy.Message + Environment.NewLine;
            if (erroy.InnerException != null)
            {
                err += "InnerException：" + erroy.InnerException.Message + Environment.NewLine;
            }
            err += "Source:" + erroy.Source + Environment.NewLine;
            err += "StackTrace:" + erroy.StackTrace + Environment.NewLine;
            _log.Error(err);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}