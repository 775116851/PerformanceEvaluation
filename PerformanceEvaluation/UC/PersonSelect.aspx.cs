using PerformanceEvaluation.Cmn;
using PerformanceEvaluation.PerformanceEvaluation.Biz;
using PerformanceEvaluation.PerformanceEvaluation.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceEvaluation.PerformanceEvaluation.UC
{
    public partial class PersonSelect : PageBase
    {
        protected int PageIndex = 1;
        protected int PageSize = 20;
        protected int PageCount = 0;
        protected int MaxPages = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRep1();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindRep1();
        }

        protected void BindRep1()
        {
            Hashtable ht = new Hashtable();
            if (!string.IsNullOrEmpty(txtPersonSysNo.Text.Trim()))
            {
                ht.Add("PersonSysNo", txtPersonSysNo.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtPersonName.Text.Trim()))
            {
                ht.Add("Name", txtPersonName.Text.Trim());
            }
            ht.Add("Status", (int)AppEnum.BiStatus.Valid);
            PageSize = AspNetPager1.PageSize;
            PageIndex = AspNetPager1.CurrentPageIndex;
            DataSet ds = BasicManager.GetInstance().GetPersonList(PageIndex, PageSize, ht);
            if (ds != null)
            {

                AspNetPager1.RecordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                //查询总页数
                int pageNum = AspNetPager1.RecordCount % AspNetPager1.PageSize == 0 ? AspNetPager1.RecordCount / AspNetPager1.PageSize : (AspNetPager1.RecordCount / AspNetPager1.PageSize) + 1;
                if (PageIndex > pageNum && pageNum != 0)//当前页大于总页数
                {
                    AspNetPager1.CurrentPageIndex = pageNum;
                    PageSize = AspNetPager1.PageSize;
                    PageIndex = AspNetPager1.CurrentPageIndex;
                    ds = BasicManager.GetInstance().GetPersonList(PageIndex, PageSize, ht);
                    AspNetPager1.RecordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                }
                else if (AspNetPager1.RecordCount == 0 && pageNum == 0)
                {
                    AspNetPager1.CurrentPageIndex = 0;
                }
                PageIndex = AspNetPager1.CurrentPageIndex;
                PageCount = AspNetPager1.RecordCount;
                MaxPages = pageNum;
                Rep1.DataSource = ds.Tables[0];
                Rep1.DataBind();
            }
            else
            {
                PageCount = 0;
                MaxPages = 0;
                Rep1.DataSource = null;
                Rep1.DataBind();
            }

        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindRep1();
        }
    }
}