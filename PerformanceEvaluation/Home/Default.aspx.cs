using log4net;
using Newtonsoft.Json;
using PerformanceEvaluation.Cmn;
using PerformanceEvaluation.PerformanceEvaluation.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceEvaluation.PerformanceEvaluation.Home
{
    public partial class Default : PageBase
    {
        public string MenuListStr = string.Empty;
        public string UserName = string.Empty;
        private ILog _log = log4net.LogManager.GetLogger(typeof(Default));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (LoginSession != null && LoginSession.User != null)
                    {

                        //IList<Custom_Sys_MenuEntity> menulist = Session["MenuList"] as IList<Custom_Sys_MenuEntity>;
                        //if (menulist == null)
                        //{
                        //    menulist = UserManager.GetInstance().GetMenuList(LoginSession.User.SysNo, (int)AppEnum.PlatformType.Organ);
                        //    if (menulist != null)
                        //    {
                        //        Session["MenuList"] = menulist;
                        //    }
                        //}

                        //if (menulist != null)
                        //{
                        //    MenuListStr = Cmn.Util.JsonFilter(JsonConvert.SerializeObject(menulist));
                        //}

                        if (LoginSession.menuList != null)
                        {
                            MenuListStr = Cmn.Util.JsonFilter(JsonConvert.SerializeObject(LoginSession.menuList));
                        }

                        if (LoginSession.User.Name != AppConst.StringNull)
                        {
                            UserName = LoginSession.User.Name;
                        }
                    }
                }
                else
                {
                    if (hidAction.Value == "Logout")
                    {
                        Session.Clear();
                        Response.Redirect("~/Home/SysLogin.aspx", true);
                        //LogoutBySSO();
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("登录出现异常，异常信息：{0} ;异常详情：{1}",ex.Message,ex));
            }
        }
    }
}