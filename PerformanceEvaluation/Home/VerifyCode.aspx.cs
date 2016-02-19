using PerformanceEvaluation.Cmn;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceEvaluation.PerformanceEvaluation.Home
{
    public partial class VerifyCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VerifyCodeImage v = new VerifyCodeImage();
            v.BackgroundColor = Color.FromArgb(255, 255, 255);
            v.Chaos = false;
            string code = v.CreateVerifyCode();                //取随机码
            v.CreateImageOnPage(code, this.Context);        // 输出图片
            Response.Cookies.Add(new HttpCookie("CheckCode", code.ToUpper()));// 使用Cookies取验证码的值
        }
    }
}