using PerformanceEvaluation.Cmn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceEvaluation.PerformanceEvaluation.UC
{
    public partial class DropDown : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BindDataTable(DataTable dt, string valueName, string textName, bool isAll)
        {
            ddlEnum.Items.Clear();
            ddlEnum.DataSource = dt;
            ddlEnum.DataValueField = valueName;
            ddlEnum.DataTextField = textName;
            ddlEnum.DataBind();
            if (isAll)
                ddlEnum.Items.Insert(0, new ListItem(AppConst.AllSelectString, AppConst.IntNull.ToString()));
        }

        public void BindStatus(System.Type t, bool isAll)
        {
            ddlEnum.Items.Clear();
            ddlEnum.DataSource = AppEnum.GetStatus(t);
            ddlEnum.DataValueField = "key";
            ddlEnum.DataTextField = "value";
            ddlEnum.DataBind();
            if (isAll)
                ddlEnum.Items.Insert(0, new ListItem(AppConst.AllSelectString, AppConst.IntNull.ToString()));
        }

        public void BindSelect(System.Type t)
        {
            ddlEnum.Items.Clear();
            ddlEnum.DataSource = AppEnum.GetStatus(t);
            ddlEnum.DataValueField = "key";
            ddlEnum.DataTextField = "value";
            ddlEnum.DataBind();

            ddlEnum.Items.Insert(0, new ListItem(AppConst.PleaseSelectString, AppConst.IntNull.ToString()));
        }

        public void InsertItem(int Index, ListItem newItem)
        {
            ddlEnum.Items.Insert(Index, newItem);
        }

        public void RemoveItemByIndex(int IndexNo)
        {
            if (IndexNo < ddlEnum.Items.Count)
            {
                ddlEnum.Items.RemoveAt(IndexNo);
            }
        }

        public void RemoveItemByValue(int ddlValue)
        {
            foreach (ListItem item in ddlEnum.Items)
            {
                if (item.Value == ddlValue.ToString())
                {
                    ddlEnum.Items.Remove(item);
                    break;
                }
            }
        }

        public int KeyValue
        {
            get
            {
                return Convert.ToInt32(ddlEnum.SelectedItem.Value);
            }
            set
            {
                ddlEnum.SelectedIndex = ddlEnum.Items.IndexOf(ddlEnum.Items.FindByValue(value.ToString()));
            }
        }

        public bool Enabled
        {
            set
            {
                ddlEnum.Enabled = value;
            }
        }

        public bool AutoPostBack
        {
            get
            {
                return ddlEnum.AutoPostBack;
            }
            set
            {
                ddlEnum.AutoPostBack = value;
            }
        }

        public Unit Width
        {
            get
            {
                return ddlEnum.Width;
            }
            set
            {
                ddlEnum.Width = value;
            }
        }

        public string onClientChange
        {
            set { ddlEnum.Attributes.Add("onchange", value); }
        }

        //定义一个委托
        public delegate void userEvent(object sender, EventArgs arg);
        //

        public event userEvent SelectedIndexChanged;
        protected void ddlEnum_onSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedIndexChanged != null)
                this.SelectedIndexChanged(this, e);
        }
    }
}