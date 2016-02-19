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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (LoginSession != null && LoginSession.User != null)
                //{

                //    IList<Custom_Sys_MenuEntity> menulist = Session["MenuList"] as IList<Custom_Sys_MenuEntity>;
                //    if (menulist == null)
                //    {
                //        menulist = UserManager.GetInstance().GetMenuList(LoginSession.User.SysNo, (int)AppEnum.PlatformType.Organ);
                //        if (menulist != null)
                //        {
                //            Session["MenuList"] = menulist;
                //        }
                //    }

                //    if (menulist != null)
                //    {
                //        MenuListStr = Cmn.Util.JsonFilter(JsonConvert.SerializeObject(menulist));//2014-06-11 wq
                //    }

                //    if (LoginSession.User.UserName != AppConst.StringNull)
                //    {
                //        UserName = LoginSession.User.UserName;
                //    }
                //}
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
    }
}