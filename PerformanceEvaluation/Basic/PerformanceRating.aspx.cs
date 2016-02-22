using PerformanceEvaluation.Cmn;
using PerformanceEvaluation.PerformanceEvaluation.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceEvaluation.PerformanceEvaluation.Basic
{
    public partial class PerformanceRating : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitUi();
            }
        }

        private void InitUi()
        {
            ddlPF.BindStatus(typeof(AppEnum.YNStatus),true);
        }
    }
}