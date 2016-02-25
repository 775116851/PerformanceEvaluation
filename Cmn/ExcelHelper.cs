using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Collections.Generic;
using CarlosAg.ExcelXmlWriter;
namespace allinpay.Mall.Cmn
{
    /**/
    /// <summary>
    /// ExcelBook 导出功能主类 的摘要说明
    /// </summary>
    public class ExcelHelper
    {
        #region Fields (11)

        private string _Author = "";
        private Workbook _book = new Workbook();
        private List<ExcelColumnCollection> _columnNamesCollection = new List<ExcelColumnCollection>();
        private string _Company = "";
        private DataTable _dataTable = null;
        private bool _isAutoFitWidth = true;
        private string _LastAuthor = "";
        private SortedList<string, int> _maxLengthOfField = new SortedList<string, int>();
        private Page _page = null;
        private string _title = "";
        private string _Version = "11.6408";

        private bool IsSettlement = false;//结算报表固定列宽高
        private SortedList<string, int> _LengthOfSettlement = new SortedList<string, int>();//结算报表固定列宽高
        /// <summary>
        /// 数据表集合 2013-9-29
        /// </summary>
        private List<DataTable> _dataTables = null;
        /// <summary>
        /// sheet名集合
        /// </summary>
        private List<string> _SheetNames = null;

        #endregion Fields

        #region Constructors (1)

        /**/
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dt">DataTable形式的数据源</param>
        /// <param name="title">Excel显示标题</param>
        public ExcelHelper(DataTable dt, string title)
        {
            Page page = (Page)HttpContext.Current.Handler;
            if (dt == null)
            {
                throw new Exception("数据源为空");
            }
            _dataTable = dt;
            _title = title;
            _page = page;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dts">DataTable集合形式的数据源</param>
        /// <param name="sheetNames">sheet名称集合</param>
        /// <param name="title">Excel显示标题</param>
        public ExcelHelper(List<DataTable> dts, List<string> sheetNames, string title)
        {
            Page page = (Page)HttpContext.Current.Handler;
            if (dts == null || dts.Count == 0)
            {
                throw new Exception("数据源为空");
            }
            _dataTables = dts;
            _title = title;
            _page = page;
            _SheetNames = sheetNames;
        }
        #endregion Constructors

        #region Properties (5)

        public string Author
        {
            get { return _Author; }
            set { _Author = value; }
        }

        public string Company
        {
            get { return _Company; }
            set { _Company = value; }
        }

        /**/
        /// <summary>
        /// 列宽是否自适应
        /// </summary>
        public bool IsAutoFitWidth
        {
            get { return _isAutoFitWidth; }
            set { _isAutoFitWidth = value; }
        }

        public string LastAuthor
        {
            get { return _LastAuthor; }
            set { _LastAuthor = value; }
        }

        public string Version
        {
            get { return _Version; }
            set { _Version = value; }
        }

        #endregion Properties

        #region Methods (10)

        // Public Methods (5) 

        /**/
        /// <summary>
        /// 添加标题行 集合
        /// </summary>
        /// <param name="exColumnCollection"></param>
        public void AddColumnNamesCollection(ExcelColumnCollection exColumnCollection)
        {
            _columnNamesCollection.Add(exColumnCollection);
        }

        /**/
        /// <summary>
        /// 清除标题行集合
        /// </summary>
        public void ClearColumnNamesCollection()
        {
            _columnNamesCollection.Clear();
        }

        /**/
        /// <summary>
        /// 以GridView的Head为标题
        /// </summary>
        /// <param name="row">GridView表头行对象</param>
        public void SetColumnNameFromGridViewHeadRow(GridViewRow row)
        {
            ExcelColumnCollection excelcols = new ExcelColumnCollection();
            _columnNamesCollection.Add(excelcols);
            foreach (TableCell cell in row.Cells)
            {
                excelcols.Add(new ExcelColumn(cell.Text));
            }
        }

        /**/
        /// <summary>
        /// 向客户端发送Excel下载文档数据
        /// </summary>
        public void WriteExcelToClient()
        {
            WriteExcelToClient(null);
        }
        /// <summary>
        /// 输出包含多个worksheet的excel
        /// 2013-9-29
        /// </summary>
        /// <param name="sheetNames"></param>
        /// <param name="downloadFileName"></param>
        public void WriteMultiWorkSheetExcelToClient(string downloadFileName = "")
        {
            string fileName = downloadFileName;
            if (fileName == "")
            {
                fileName = string.IsNullOrEmpty(downloadFileName) ? (string.IsNullOrEmpty(_title) ? "noname" : _title) : downloadFileName;
            }
            if (_dataTables == null || _dataTables.Count == 0)
            {
                throw new Exception("数据源为空");
            }
            InitializeBook(_book);
            SetStyles(_book.Styles);
            SetMultiSheets();
            _book.Save(_page.Response.OutputStream);
            _page.Response.AppendHeader("Content-Disposition", "Attachment; FileName=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8) + ".xls;");
            _page.Response.ContentEncoding = System.Text.Encoding.UTF8;
            _page.Response.Charset = "UTF-8";
            _page.Response.Flush();
            _page.Response.End();
        }

        /**/
        /// <summary>
        /// 向客户端发送Excel下载文档数据
        /// </summary>
        /// <param name="downloadFileName">下载时显示的文件名称</param>
        public void WriteExcelToClient(string downloadFileName)
        {
            string fileName = string.IsNullOrEmpty(downloadFileName) ? (string.IsNullOrEmpty(_title) ? "noname" : _title) : downloadFileName;

            InitializeBook(_book);
            SetStyles(_book.Styles);
            SetSheels(_book.Worksheets);

            _book.Save(_page.Response.OutputStream);
            _page.Response.AppendHeader("Content-Disposition", "Attachment; FileName=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8) + ".xls;");
            _page.Response.ContentEncoding = System.Text.Encoding.UTF8;
            _page.Response.Charset = "UTF-8";
            _page.Response.Flush();
            _page.Response.End();
        }
        // Private Methods (5) 

        /**/
        /// <summary>
        /// 获取字段最大宽度函数
        /// </summary>
        /// <param name="oldLength">原来长度</param>
        /// <param name="str">当前字符串</param>
        /// <returns>最大值</returns>
        private int GetMaxLength(int oldLength, string str)
        {
            if (str == null) str = "";
            byte[] bs = System.Text.Encoding.Default.GetBytes(str.Trim());
            int newLength = bs.Length;
            if (oldLength > newLength)
                return oldLength;
            else
                return newLength;
        }

        #region 自动换行
        /// <summary>
        /// 截取指定长度字符串-防止双字节字符
        /// </summary>
        /// <param name="str">截取源字符串</param>
        /// <param name="oldIndex">截取长度</param>
        /// <returns></returns>
        int GetSubIndex(string str, int oldIndex)
        {
            if (oldIndex <= 0)
                return 0;
            if (System.Text.Encoding.Default.GetBytes(str).Length <= oldIndex)
                return oldIndex;
            System.Text.RegularExpressions.Regex doublestr = new System.Text.RegularExpressions.Regex("[^\x00-\xff]");//双字节字符
            string temp = str;
            int j = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                if (doublestr.IsMatch(temp.Substring(i, 1)))
                {
                    j += 2;
                }
                else
                {
                    j++;
                }
                if (j == oldIndex)
                    return j;
                else if (j > oldIndex)
                    return oldIndex - 1;
            }
            return oldIndex;
        }
        private int GetHLine(int height, string value)
        {
            //Regex cn = new Regex("[\u4e00-\u9fa5]+");//正则表达式 表示汉字范围
            System.Text.RegularExpressions.Regex doublestr = new System.Text.RegularExpressions.Regex("[^\x00-\xff]");//双字节字符
            List<string> lst = new List<string>();
            string TMPstring = string.Empty;
            for (int i = 0; i < value.Length; i++)
            {
                string tmp = value.Substring(i, 1);
                if (doublestr.IsMatch(tmp))
                {
                    if (!string.IsNullOrEmpty(TMPstring))
                    {
                        lst.Add(TMPstring);
                        TMPstring = "";
                    }
                    lst.Add(tmp);
                }
                else
                {
                    if (tmp == " " || tmp == "-")
                    {
                        if (TMPstring != "")
                        {
                            lst.Add(TMPstring);
                            TMPstring = "";
                        }
                        lst.Add(tmp);

                    }
                    else
                    {
                        TMPstring += tmp;
                    }
                }
            }
            if (TMPstring != "")
            {
                lst.Add(TMPstring);
                TMPstring = "";
            }

            int h = 0;
            string ssstmp = "";
            foreach (string ss in lst)
            {
                int nLength = System.Text.Encoding.Default.GetBytes(ssstmp).Length * 6;
                if (nLength > height)
                {
                    if (nLength % height == 0)
                    {
                        h += nLength / height;
                        ssstmp = "";
                    }
                    else
                    {
                        h += nLength / height;
                        int nIndex = GetSubIndex(ssstmp, (nLength / height) * (height / 6));
                        ssstmp = System.Text.Encoding.Default.GetString(System.Text.Encoding.Default.GetBytes(ssstmp), nIndex, System.Text.Encoding.Default.GetBytes(ssstmp).Length - nIndex);
                        if (System.Text.Encoding.Default.GetBytes(ssstmp).Length * 6 == height)//前一行缩进一位,后一行可能刚好满一行
                        {
                            h++;
                            ssstmp = "";
                        }
                    }
                }
                if (doublestr.IsMatch(ss))
                {
                    ssstmp += ss;
                }
                else if (ss == "-" || ss == " ")
                {
                    if (h == 0 && ssstmp == "" && ss == "-")//第一个字符为'-'时,前面需加单引号'
                    {
                        ssstmp = "'";
                    }
                    ssstmp += ss;
                }
                else
                {
                    if (ssstmp == "")
                    {
                        ssstmp = ss;
                    }
                    else
                    {
                        if (ssstmp.Substring(ssstmp.Length - 1) == "-" || ssstmp.Substring(ssstmp.Length - 1) == " ")
                        {
                            if ((System.Text.Encoding.Default.GetBytes(ssstmp).Length * 6 + System.Text.Encoding.Default.GetBytes(ss).Length * 6) > height)
                            {
                                h++;
                                Console.WriteLine(ssstmp);
                                ssstmp = ss;
                            }
                            else
                            {
                                ssstmp += ss;
                            }
                        }
                        else
                        {
                            ssstmp += ss;
                        }
                    }
                }

            }
            int _nLength = System.Text.Encoding.Default.GetBytes(ssstmp).Length * 6;
            if (_nLength > height)
            {
                if (_nLength % height == 0)
                {
                    h += _nLength / height;
                    ssstmp = "";
                }
                else
                {
                    h += (_nLength / height) + 1;
                    int nIndex = GetSubIndex(ssstmp, (_nLength / height) * (height / 6));
                    ssstmp = System.Text.Encoding.Default.GetString(System.Text.Encoding.Default.GetBytes(ssstmp), nIndex, System.Text.Encoding.Default.GetBytes(ssstmp).Length - nIndex);
                    if (System.Text.Encoding.Default.GetBytes(ssstmp).Length * 6 == height)//前一行缩进一位,后一行可能刚好满一行
                    {
                        h++;
                        ssstmp = "";
                    }
                }
            }
            else if (ssstmp != "")
                h++;
            return h;
        }
#endregion

        /**/
        /// <summary>
        /// 初始化Excel Workbook
        /// </summary>
        /// <param name="book">book</param>
        /// 
        private void InitializeBook(Workbook book)
        {
            book.Properties.Author = Author;
            book.Properties.LastAuthor = LastAuthor;
            book.Properties.Created = DateTime.Now;
            book.Properties.Company = Company;
            book.Properties.Version = Version;
            book.ExcelWorkbook.WindowHeight = 13500;
            book.ExcelWorkbook.WindowWidth = 17100;
            book.ExcelWorkbook.WindowTopX = 360;
            book.ExcelWorkbook.WindowTopY = 75;
            book.ExcelWorkbook.ProtectWindows = false;
            book.ExcelWorkbook.ProtectStructure = false;
        }

        /**/
        /// <summary>
        /// 设置Excel Sheet
        /// </summary>
        /// <param name="sheets">sheets集合</param>
        private void SetSheels(WorksheetCollection sheets)
        {
            Worksheet sheet = sheets.Add("Sheet1");
            SetSingleWorkSheet(sheet);
        }

        private void SetMultiSheets()
        {
            for (int i = 0; i < _dataTables.Count; i++)
            {
                Worksheet sheet = null;
                if (_SheetNames.Count > i)
                {
                    sheet = _book.Worksheets.Add(_SheetNames[i]);
                }
                else
                {
                    sheet = _book.Worksheets.Add("未命名");
                }
                _dataTable = _dataTables[i];
                SetSingleWorkSheet(sheet);
            }
        }

        private void SetSingleWorkSheet(Worksheet sheet)
        {
            sheet.Table.DefaultRowHeight = 14.25F;
            sheet.Table.DefaultColumnWidth = 54F;
            sheet.Table.FullColumns = 1;
            sheet.Table.FullRows = 1;

            // -----------------------------------------------

            WorksheetRow row = null;
            WorksheetCell cell = null;

            #region 大标题
            row = sheet.Table.Rows.Add();
            row.AutoFitHeight = true;
            row.Height = 30;
            cell = row.Cells.Add();
            cell.StyleID = "TitleStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = _title;
            cell.MergeAcross = _dataTable.Columns.Count - 1;
            #endregion

            foreach (DataColumn dc in _dataTable.Columns)//初始化列宽度集合
            {
                _maxLengthOfField[dc.ColumnName] = 0;
            }

            //-----------------------------------------------字段
            #region 字段标题行

            if (_columnNamesCollection.Count != 0)
            {
                for (int i = 0; i < _columnNamesCollection.Count; i++)
                {
                    row = sheet.Table.Rows.Add();
                    row.AutoFitHeight = true;
                    int j = 0;
                    foreach (ExcelColumn ec in _columnNamesCollection[i])
                    {
                        cell = row.Cells.Add();
                        cell.Data.Text = ec.Name;
                        cell.Data.Type = DataType.String;
                        if (i != _columnNamesCollection.Count - 1)
                        {
                            cell.MergeAcross = ec.MergeAcross;
                            cell.StyleID = "FieldStyle";
                        }
                        else//最下层标题行
                        {
                            cell.StyleID = "LastFieldStyle";
                            _maxLengthOfField[_dataTable.Columns[j].ColumnName] = GetMaxLength(_maxLengthOfField[_dataTable.Columns[j++].ColumnName], ec.Name);
                        }
                    }
                }
            }
            else//没有设置标题行 则默认取dt的标题
            {
                row = sheet.Table.Rows.Add();
                row.AutoFitHeight = true;
                foreach (DataColumn dc in _dataTable.Columns)
                {
                    cell = row.Cells.Add();
                    cell.Data.Text = dc.ColumnName;
                    cell.Data.Type = DataType.String;
                    cell.StyleID = "FieldStyle";
                    _maxLengthOfField[dc.ColumnName] = GetMaxLength(_maxLengthOfField[dc.ColumnName], dc.ColumnName);
                }
            }
            #endregion
            // -----------------------------------------------

            #region 数据行
            object dcValueO = null;
            string dcValueS = null;
            foreach (DataRow dr in _dataTable.Rows)
            {
                row = sheet.Table.Rows.Add();
                row.AutoFitHeight = true;
                foreach (DataColumn dc in _dataTable.Columns)
                {
                    dcValueO = dr[dc];
                    if (dcValueO == DBNull.Value)
                    {
                        dcValueS = string.Empty;
                    }
                    else
                    {
                        dcValueS = dcValueO.ToString();
                    }
                    cell = row.Cells.Add();
                    cell.Data.Text = dcValueS;
                    cell.Data.Type = TypeConvert(dc.DataType);
                    cell.StyleID = "DataStyle";
                    if (_isAutoFitWidth || _columnNamesCollection.Count == 0)
                    {
                        _maxLengthOfField[dc.ColumnName] = GetMaxLength(_maxLengthOfField[dc.ColumnName], dcValueS);
                    }
                }
            }
            #endregion
            // -----------------------------------------------

            #region 设置列
            WorksheetColumn column = null;
            if (!_isAutoFitWidth && _columnNamesCollection.Count != 0)
            {
                foreach (ExcelColumn ec in _columnNamesCollection[_columnNamesCollection.Count - 1])
                {
                    column = new WorksheetColumn();
                    column.AutoFitWidth = false;
                    column.Width = ec.Width;
                    sheet.Table.Columns.Add(column);
                }
            }
            else
            {
                foreach (DataColumn dc in _dataTable.Columns)
                {
                    column = new WorksheetColumn();
                    column.AutoFitWidth = false;
                    column.Width = _maxLengthOfField[dc.ColumnName] * 7;
                    sheet.Table.Columns.Add(column);
                }
            }
            #endregion
            //  Options
            // -----------------------------------------------
            sheet.Options.Selected = true;
            sheet.Options.ProtectObjects = false;
            sheet.Options.ProtectScenarios = false;
            sheet.Options.Print.PaperSizeIndex = 9;
            sheet.Options.Print.HorizontalResolution = 300;
            sheet.Options.Print.VerticalResolution = 300;
            sheet.Options.Print.ValidPrinterInfo = true;
        }
        /// <summary>
        /// 普通商品结算报表导出
        /// 2013-9-29
        /// </summary>
        /// <param name="sheetNames"></param>
        /// <param name="downloadFileName"></param>
        public void WriteMultiWorkSheetExcelToClientNormalGoodsSettlement(Dictionary<string, string> dic, string downloadFileName = "", bool isSettlement = false)
        {
            IsSettlement = isSettlement;
            string fileName = downloadFileName;
            if (fileName == "")
            {
                fileName = string.IsNullOrEmpty(downloadFileName) ? (string.IsNullOrEmpty(_title) ? "noname" : _title) : downloadFileName;
            }
            if (_dataTables == null || _dataTables.Count == 0)
            {
                throw new Exception("数据源为空");
            }
            InitializeBook(_book);
            SetStyles(_book.Styles);
            SetMultiSheetsNormalGoodsSettlement(dic);
            _book.Save(_page.Response.OutputStream);
            _page.Response.AppendHeader("Content-Disposition", "Attachment; FileName=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8) + ".xls;");
            _page.Response.ContentEncoding = System.Text.Encoding.UTF8;
            _page.Response.Charset = "UTF-8";
            _page.Response.Flush();
            _page.Response.End();
        }
        /// <summary>
        /// 普通商品结算报表导出
        /// 2013-10-15
        /// </summary>
        private void SetMultiSheetsNormalGoodsSettlement(Dictionary<string, string> dic)
        {
            for (int i = 0; i < _dataTables.Count; i++)
            {
                Worksheet sheet = null;
                if (_SheetNames.Count > i)
                {
                    sheet = _book.Worksheets.Add(_SheetNames[i]);
                }
                else
                {
                    sheet = _book.Worksheets.Add("未命名");
                }
                _dataTable = _dataTables[i];
                SetSingleWorkSheetNormalGoodsSettlement(sheet, dic);
            }
        }
        /// <summary>
        /// 普通商品结算报表导出
        /// 2013-10-15
        /// </summary>
        /// <param name="sheet"></param>
        private void SetSingleWorkSheetNormalGoodsSettlement(Worksheet sheet, Dictionary<string, string> dic)
        {
            sheet.Table.DefaultRowHeight = 14.25F;
            sheet.Table.DefaultColumnWidth = 54F;
            sheet.Table.FullColumns = 1;
            sheet.Table.FullRows = 1;

            // -----------------------------------------------

            WorksheetRow row = null;
            WorksheetCell cell = null;

            #region 大标题
            row = sheet.Table.Rows.Add();
            if (!IsSettlement)
            {
                row.AutoFitHeight = true;
            }
            else
            {
                foreach (DataRow dr in _dataTable.Rows)
                {
                    dr["备注"] = "测试";
                }
                _dataTable.Columns.Add("是否合并结算");
                _dataTable.Columns["备注"].SetOrdinal(_dataTable.Columns.Count-1);
                _dataTable.Columns["是否合并结算"].SetOrdinal(_dataTable.Columns.Count - 2);
                row.Height = 28;
            }
            cell = row.Cells.Add();
            cell.StyleID = "TitleSmallStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = _title;
            cell.MergeAcross = _dataTable.Columns.Count - 1;
            #endregion
            foreach (DataColumn dc in _dataTable.Columns)//初始化列宽度集合
            {
                _maxLengthOfField[dc.ColumnName] = 0;
            }
            if (IsSettlement)
            {
                _LengthOfSettlement["客户号"] = 54;
                _LengthOfSettlement["客户名称"] = 60;
                _LengthOfSettlement["商户编号/分店代码"] = 54;
                _LengthOfSettlement["商户名称"] = 78;
                _LengthOfSettlement["客户银行账号"] = 60;
                _LengthOfSettlement["客户银行开户名称"] = 72;
                _LengthOfSettlement["开户行行号"] = 42;
                _LengthOfSettlement["开户行名称"] = 60;
                _LengthOfSettlement["交易币种"] = 30;
                _LengthOfSettlement["结算金额"] = 72;
                _LengthOfSettlement["清算交易日期"] = 60;
                _LengthOfSettlement["原交易本金"] = 72;
                _LengthOfSettlement["原交易商户手续费"] = 48;
                _LengthOfSettlement["交易叠加产品增值手续费"] = 48;
                _LengthOfSettlement["结算周期"] = 30;
                _LengthOfSettlement["用途"] = 72;
                _LengthOfSettlement["结算方式"] = 36;
                _LengthOfSettlement["归属机构"] = 60;
                _LengthOfSettlement["是否合并结算"] = 36;
                _LengthOfSettlement["备注"] = 30;
            }

            #region 表的上部

            #region 第一行
            row = sheet.Table.Rows.Add();
            //row.AutoFitHeight = true;
            row.Height = IsSettlement ? 28 : 23;

            cell = row.Cells.Add();
            cell.StyleID = "TitleSmallStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "内部机构名称";

            cell = row.Cells.Add();
            cell.StyleID = IsSettlement ? "TitleSmallStyle" : "DataSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "移动业务部";
            cell.MergeAcross = 2;

            cell = row.Cells.Add();
            cell.StyleID = "TitleSmallStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "内部机构代码";

            cell = row.Cells.Add();
            cell.StyleID = IsSettlement ? "TitleSmallStyle" : "DataSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "";

            cell = row.Cells.Add();
            cell.StyleID = "TitleSmallStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "清算交易日期";

            cell = row.Cells.Add();
            cell.StyleID = IsSettlement ? "TitleSmallStyle" : "DataSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = dic["time"];
            cell.MergeAcross = 3;

            cell = row.Cells.Add();
            cell.StyleID = "TitleSmallStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "报表生成日期";

            cell = row.Cells.Add();
            cell.StyleID = IsSettlement ? "TitleSmallStyle" : "DataSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = DateTime.Now.ToString("yyyyMMdd");
            cell.MergeAcross =IsSettlement?5: 4;

            cell = row.Cells.Add();
            cell.StyleID = "TitleSmallStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "";

            cell = row.Cells.Add();
            cell.StyleID = IsSettlement ? "TitleSmallStyle" : "DataSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "";
            #endregion

            #region 第二行
            row = sheet.Table.Rows.Add();
            //row.AutoFitHeight = true;
            row.Height = IsSettlement ? 28 : 23;

            cell = row.Cells.Add();
            cell.StyleID = "TitleSmallStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "业务受理机构名称";
            _maxLengthOfField[_dataTable.Columns[0].ColumnName] = GetMaxLength(_maxLengthOfField[_dataTable.Columns[0].ColumnName], cell.Data.Text);

            cell = row.Cells.Add();
            cell.StyleID = IsSettlement ? "LeftStyle" : "DataSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "支付";
            cell.MergeAcross = 16;

            cell = row.Cells.Add();
            cell.StyleID = IsSettlement ? "TitleSmallStyle" : "DataSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "";
            #endregion

            #region 第三行
            row = sheet.Table.Rows.Add();
            //row.AutoFitHeight = true;
            row.Height = IsSettlement ? 28 : 23;

            cell = row.Cells.Add();
            cell.StyleID = "TitleSmallStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "业务受理机构代码";

            cell = row.Cells.Add();
            cell.StyleID = IsSettlement ? "TitleSmallStyle" : "DataSmallFontStyle";
            cell.Data.Type = DataType.Number;
            cell.Data.Text = "48212900";

            for (int i = 0; i < _dataTable.Columns.Count - 2; i++)
            {
                cell = row.Cells.Add();
                cell.StyleID = IsSettlement ? "TitleSmallStyle" : "DataSmallFontStyle";
                cell.Data.Type = DataType.String;
                cell.Data.Text = "";
            }
            #endregion

            #endregion

            //-----------------------------------------------字段
            #region 字段标题行

            if (_columnNamesCollection.Count != 0)
            {
                for (int i = 0; i < _columnNamesCollection.Count; i++)
                {
                    row = sheet.Table.Rows.Add();
                    //row.AutoFitHeight = true;
                    row.Height = IsSettlement ? 36 : 23;
                    int j = 0;
                    foreach (ExcelColumn ec in _columnNamesCollection[i])
                    {
                        cell = row.Cells.Add();
                        cell.Data.Text = ec.Name;
                        cell.Data.Type = DataType.String;
                        if (i != _columnNamesCollection.Count - 1)
                        {
                            cell.MergeAcross = ec.MergeAcross;
                            cell.StyleID = "TitleSmallStyle";
                        }
                        else//最下层标题行
                        {
                            cell.StyleID = "LastFieldStyle";
                            _maxLengthOfField[_dataTable.Columns[j].ColumnName] = GetMaxLength(_maxLengthOfField[_dataTable.Columns[j++].ColumnName], ec.Name);
                        }
                    }
                }
            }
            else//没有设置标题行 则默认取dt的标题
            {
                row = sheet.Table.Rows.Add();
                if (!IsSettlement)
                {
                    row.AutoFitHeight = true;
                }
                else
                {
                    row.Height = 36;
                }
                foreach (DataColumn dc in _dataTable.Columns)
                {
                    cell = row.Cells.Add();
                    cell.Data.Text = dc.ColumnName;
                    cell.Data.Type = DataType.String;
                    cell.StyleID = "TitleSmallStyle";
                    _maxLengthOfField[dc.ColumnName] = GetMaxLength(_maxLengthOfField[dc.ColumnName], dc.ColumnName);
                }
            }
            #endregion
            // -----------------------------------------------

            #region 数据行
            object dcValueO = null;
            string dcValueS = null;
            foreach (DataRow dr in _dataTable.Rows)
            {
                row = sheet.Table.Rows.Add();
                row.AutoFitHeight = true;
                foreach (DataColumn dc in _dataTable.Columns)
                {
                    dcValueO = dr[dc];
                    if (dcValueO == DBNull.Value)
                    {
                        dcValueS = string.Empty;
                    }
                    else
                    {
                        dcValueS = dcValueO.ToString();
                    }
                    cell = row.Cells.Add();
                    cell.Data.Text = dcValueS;
                    cell.Data.Type = TypeConvert(dc.DataType);
                    cell.StyleID = "DataSmallFontStyle";
                    if (_isAutoFitWidth || _columnNamesCollection.Count == 0)
                    {
                        _maxLengthOfField[dc.ColumnName] = GetMaxLength(_maxLengthOfField[dc.ColumnName], dcValueS);
                        if (IsSettlement && _LengthOfSettlement.ContainsKey(dc.ColumnName))
                        {
                            cell.Data.Text = dc.ColumnName == "是否合并结算" ? "否" : dcValueS;
                            int h = 0;
                            int ColumnLength = System.Text.Encoding.Default.GetBytes(dcValueS).Length;//获取字段内容长度
                            if (ColumnLength == 0)
                                ColumnLength = 1;
                            if (_LengthOfSettlement[dc.ColumnName] == 0)
                                _LengthOfSettlement[dc.ColumnName] = 60;
                            //h = ((ColumnLength * 6) % (_LengthOfSettlement[dc.ColumnName]) == 0) ? ((ColumnLength * 6) / _LengthOfSettlement[dc.ColumnName]) * 12 : 12 * (((ColumnLength * 6) / _LengthOfSettlement[dc.ColumnName]) + 1);
                            h = GetHLine(_LengthOfSettlement[dc.ColumnName], cell.Data.Text) * 12;
                            row.AutoFitHeight = false;
                            row.Height = h > row.Height ? h : row.Height;
                        }
                    }
                }
            }
            #endregion
            // -----------------------------------------------

            #region 表的尾部，合计行和签名
            row = sheet.Table.Rows.Add();
            if (!IsSettlement)
            {
                row.AutoFitHeight = true;
            }
            else
            {
                row.Height = 36;
            }

            cell = row.Cells.Add();
            cell.StyleID =IsSettlement?"TotalStyle": "TitleSmallStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "总计";
            cell.MergeAcross = 7;

            cell = row.Cells.Add();
            cell.StyleID = "DataSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "";
            for (int i = 9; i < _dataTable.Columns.Count; i++)
            {
                if (_dataTable.Columns[i].ColumnName == "结算金额")
                {
                    cell = row.Cells.Add();
                    cell.StyleID = IsSettlement ? "RightStyle" : "DataSmallFontStyle";
                    cell.Data.Type = TypeConvert(_dataTable.Columns[i].DataType);
                    cell.Data.Text = dic["settleAmtCount"];
                }
                else if (_dataTable.Columns[i].ColumnName == "原交易本金")
                {
                    cell = row.Cells.Add();
                    cell.StyleID = IsSettlement ? "RightStyle" : "DataSmallFontStyle";
                    cell.Data.Type = TypeConvert(_dataTable.Columns[i].DataType);
                    cell.Data.Text = dic["totalAmtCount"];
                }
                else if (_dataTable.Columns[i].ColumnName == "原交易商户手续费")
                {
                    cell = row.Cells.Add();
                    cell.StyleID = IsSettlement ? "RightStyle" : "DataSmallFontStyle";
                    cell.Data.Type = TypeConvert(_dataTable.Columns[i].DataType);
                    cell.Data.Text = dic["yjAmtCount"];
                }
                else if (_dataTable.Columns[i].ColumnName == "交易叠加产品增值手续费")
                {
                    cell = row.Cells.Add();
                    cell.StyleID = IsSettlement ? "RightStyle" : "DataSmallFontStyle";
                    cell.Data.Type = TypeConvert(_dataTable.Columns[i].DataType);
                    cell.Data.Text = dic["feeCount"];
                }
                else
                {
                    cell = row.Cells.Add();
                    cell.StyleID = IsSettlement ? "RightStyle" : "DataSmallFontStyle";
                    cell.Data.Type = TypeConvert(_dataTable.Columns[i].DataType);
                    cell.Data.Text = "";
                }
            }

            row = sheet.Table.Rows.Add();
            if (!IsSettlement)
                row.AutoFitHeight = true;
            else
                row.Height = 24;

            //cell = row.Cells.Add();
            //cell.StyleID = "LastFieldSmallFontStyle";
            //cell.Data.Type = DataType.String;
            //cell.Data.Text = IsSettlement ? "" : "部门负责人:";

            //cell = row.Cells.Add();
            //cell.StyleID = "LastFieldSmallFontStyle";
            //cell.Data.Type = DataType.String;
            //cell.Data.Text = IsSettlement ? "部门负责人:" : "";
            cell = row.Cells.Add();
            cell.StyleID = "LastFieldSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "部门负责人:";
            cell.MergeAcross = 2;

            //cell = row.Cells.Add();
            //cell.StyleID = "LastFieldSmallFontStyle";
            //cell.Data.Type = DataType.String;
            //cell.Data.Text = "";
            //cell = row.Cells.Add();
            //cell.StyleID = "LastFieldSmallFontStyle";
            //cell.Data.Type = DataType.String;
            //cell.Data.Text = "";
            cell = row.Cells.Add();
            cell.StyleID = "LastFieldSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "";
            cell.MergeAcross = 1;

            
            cell = row.Cells.Add();
            cell.StyleID = "LastFieldSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "复核:";

            cell = row.Cells.Add();
            cell.StyleID = "LastFieldSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "";

            cell = row.Cells.Add();
            cell.StyleID = "LastFieldSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "";

            cell = row.Cells.Add();
            cell.StyleID = "LastFieldSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "";
            cell = row.Cells.Add();
            cell.StyleID = "LastFieldSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "制表:";
            cell.MergeAcross = 1;

            cell = row.Cells.Add();
            cell.StyleID = "LastFieldSmallFontStyle";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "";

            if (IsSettlement)
            {
                cell = row.Cells.Add();
                cell.StyleID = "LastFieldSmallFontStyle";
                cell.Data.Type = DataType.String;
                cell.Data.Text = "";
                cell.MergeAcross = 2;

                cell = row.Cells.Add();
                cell.StyleID = "LastFieldSmallFontStyle";
                cell.Data.Type = DataType.String;
                cell.Data.Text = "监督人:";
            }
            #endregion

            #region 设置列
            WorksheetColumn column = null;
            if (!_isAutoFitWidth && _columnNamesCollection.Count != 0)
            {
                foreach (ExcelColumn ec in _columnNamesCollection[_columnNamesCollection.Count - 1])
                {
                    column = new WorksheetColumn();
                    column.AutoFitWidth = false;
                    column.Width = ec.Width;
                    sheet.Table.Columns.Add(column);
                }
            }
            else
            {
                foreach (DataColumn dc in _dataTable.Columns)
                {
                    column = new WorksheetColumn();
                    column.AutoFitWidth = false;
                    column.Width = IsSettlement ? _LengthOfSettlement[dc.ColumnName] : _maxLengthOfField[dc.ColumnName] * 7;
                    sheet.Table.Columns.Add(column);
                }
            }
            #endregion
            //  Options
            // -----------------------------------------------
            sheet.Options.Selected = true;
            sheet.Options.ProtectObjects = false;
            sheet.Options.ProtectScenarios = false;
            sheet.Options.Print.PaperSizeIndex = 9;
            sheet.Options.Print.HorizontalResolution = 300;
            sheet.Options.Print.VerticalResolution = 300;
            sheet.Options.Print.ValidPrinterInfo = true;
        }

        /**/
        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="styles">样式集合</param>
        private void SetStyles(WorksheetStyleCollection styles)
        {
            // -----------------------------------------------
            //  Default
            // -----------------------------------------------
            WorksheetStyle Default = styles.Add("Default");
            Default.Name = "Normal";
            Default.Font.FontName = "宋体";
            Default.Font.Size = 12;
            Default.Alignment.Vertical = StyleVerticalAlignment.Center;
            if (IsSettlement)
                Default.Alignment.WrapText = true;
            // -----------------------------------------------
            //  TitleStyle
            // -----------------------------------------------
            WorksheetStyle TitleStyle = styles.Add("TitleStyle");
            TitleStyle.Font.Bold = true;
            TitleStyle.Font.FontName = "黑体";
            TitleStyle.Font.Size = 14;
            TitleStyle.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            TitleStyle.Alignment.Vertical = StyleVerticalAlignment.Center;
            if (IsSettlement)
            {
                TitleStyle.Alignment.WrapText = true;
                TitleStyle.Font.Size = 10;
            }
            // -----------------------------------------------
            //  TitleSmallStyle
            // -----------------------------------------------
            WorksheetStyle TitleSmallStyle = styles.Add("TitleSmallStyle");
            TitleSmallStyle.Font.Bold = true;
            TitleSmallStyle.Font.FontName = "宋体";
            TitleSmallStyle.Font.Size = 10;
            TitleSmallStyle.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            TitleSmallStyle.Alignment.Vertical = StyleVerticalAlignment.Center;
            TitleSmallStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            TitleSmallStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
            TitleSmallStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            TitleSmallStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            if (IsSettlement)
                TitleSmallStyle.Alignment.WrapText = true;
            // -----------------------------------------------
            //  FieldStyle
            // -----------------------------------------------
            WorksheetStyle FieldStyle = styles.Add("FieldStyle");
            FieldStyle.Font.Bold = true;
            FieldStyle.Font.FontName = "宋体";
            FieldStyle.Font.Size = 12;
            FieldStyle.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            FieldStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            FieldStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
            FieldStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            FieldStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            if (IsSettlement)
                FieldStyle.Alignment.WrapText = true;
            // -----------------------------------------------
            //  LastFieldStyle
            // -----------------------------------------------
            WorksheetStyle LastFieldStyle = styles.Add("LastFieldStyle");
            LastFieldStyle.Font.Bold = true;
            LastFieldStyle.Font.FontName = "宋体";
            LastFieldStyle.Font.Size = 12;
            LastFieldStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            LastFieldStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
            LastFieldStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            LastFieldStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            if (IsSettlement)
                LastFieldStyle.Alignment.WrapText = true;
            // -----------------------------------------------
            //  LastFieldSmallFontStyle
            // -----------------------------------------------
            WorksheetStyle LastFieldSmallFontStyle = styles.Add("LastFieldSmallFontStyle");
            LastFieldSmallFontStyle.Font.Bold = true;
            LastFieldSmallFontStyle.Font.FontName = "宋体";
            LastFieldSmallFontStyle.Font.Size = 10;
            if (IsSettlement)
            {
                LastFieldSmallFontStyle.Alignment.Horizontal = StyleHorizontalAlignment.Center;
                LastFieldSmallFontStyle.Alignment.Vertical = StyleVerticalAlignment.Bottom;
                LastFieldSmallFontStyle.Alignment.WrapText = true;
            }
            // -----------------------------------------------
            //  DataStyle
            // -----------------------------------------------
            WorksheetStyle DataStyle = styles.Add("DataStyle");
            DataStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            DataStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            DataStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            DataStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
            if (IsSettlement)
                DataStyle.Alignment.WrapText = true;
            // -----------------------------------------------
            //  DataSmallFontStyle
            // -----------------------------------------------
            WorksheetStyle DataSmallFontStyle = styles.Add("DataSmallFontStyle");
            DataSmallFontStyle.Font.FontName = "宋体";
            DataSmallFontStyle.Font.Size = 10;
            DataSmallFontStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            DataSmallFontStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            DataSmallFontStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            DataSmallFontStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
            if (IsSettlement)
            {
                DataSmallFontStyle.Alignment.WrapText = true;
            }

            // -----------------------------------------------
            //  TotalStyle
            // -----------------------------------------------
            WorksheetStyle TotalStyle = styles.Add("TotalStyle");
            TotalStyle.Font.Bold = true;
            TotalStyle.Font.FontName = "宋体";
            TotalStyle.Font.Size = 10;
            TotalStyle.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            TotalStyle.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            TotalStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            TotalStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
            TotalStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            TotalStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            TotalStyle.Alignment.WrapText = true;

            // -----------------------------------------------
            //  LeftStyle
            // -----------------------------------------------
            WorksheetStyle LeftStyle = styles.Add("LeftStyle");
            LeftStyle.Font.Bold = true;
            LeftStyle.Font.FontName = "宋体";
            LeftStyle.Font.Size = 10;
            LeftStyle.Alignment.Horizontal = StyleHorizontalAlignment.Left;
            LeftStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            LeftStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
            LeftStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            LeftStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            LeftStyle.Alignment.WrapText = true;

            // -----------------------------------------------
            //  RightStyle
            // -----------------------------------------------
            WorksheetStyle RightStyle = styles.Add("RightStyle");
            RightStyle.Font.Bold = true;
            RightStyle.Font.FontName = "宋体";
            RightStyle.Font.Size = 10;
            RightStyle.Alignment.Horizontal = StyleHorizontalAlignment.Right;
            RightStyle.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            RightStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            RightStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
            RightStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            RightStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            RightStyle.Alignment.WrapText = true;
        }

        /**/
        /// <summary>
        /// 数据类型转换
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private DataType TypeConvert(Type type)
        {
            switch (type.Name)
            {
                case "Decimal":
                case "Double":
                case "Single":
                    return DataType.Number;
                case "Int16":
                case "Int32":
                case "Int64":
                case "SByte":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                    return DataType.Number;
                case "String":
                    return DataType.String;
                case "DateTime":
                    return DataType.String;
                default:
                    return DataType.String;
            }
        }

        #endregion Methods

        #region Nested Classes (2)


        /**/
        /// <summary>
        /// Excel列名ExcelColumn 的摘要说明
        /// </summary>
        public class ExcelColumn
        {
            #region Fields (3)

            //列名
            private int _mergeAcross = 0;
            //默认宽度
            private string _name = string.Empty;
            private int _width = 150;

            #endregion Fields

            #region Constructors (3)

            //跨越字段,合并单元格
            /**/
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="width">列宽</param>
            /// <param name="name">列名</param>
            public ExcelColumn(int width, string name)
            {
                this._width = width;
                this._name = name;
                this._mergeAcross = 0;
            }

            /**/
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="name">列名</param>
            /// <param name="colspan">合并列数</param>
            public ExcelColumn(string name, int colspan)
            {
                this._name = name;
                this._mergeAcross = colspan - 1;
            }

            /**/
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="name">列名</param>
            public ExcelColumn(string name)
            {
                this._name = name;
            }

            #endregion Constructors

            #region Properties (3)

            public int MergeAcross
            {
                get { return _mergeAcross; }
            }

            public string Name
            {
                get { return _name; }
            }

            public int Width
            {
                get { return _width; }
            }

            #endregion Properties
        }
        /**/
        /// <summary>
        /// ExcelColumnCollection 的摘要说明
        /// </summary>
        public class ExcelColumnCollection : System.Collections.CollectionBase
        {
            #region Constructors (1)

            public ExcelColumnCollection()
            {
            }

            #endregion Constructors

            #region Properties (2)

            /**/
            /// <summary>
            /// 获取或设置指定索引处的元素。
            /// </summary>
            /// <param name="index">要获得或设置的元素从零开始的索引。</param>
            /// <returns>指定索引处的元素。</returns>
            /// <exception cref="System.ArgumentOutOfRangeException">index 小于零。
            /// - 或 -
            /// index 等于或大于 Count。
            /// </exception>
            public ExcelColumn this[int index]
            {
                get
                {
                    return (ExcelColumn)List[index];
                }

                set
                {
                    List[index] = value;
                }
            }

            /**/
            /// <summary>
            /// 获取或设置指定 关键字 的元素。
            /// </summary>
            /// <param name="nodeText">要获得或设置的元素的关键字。</param>
            /// <returns>如果在整个 ItemNodeCollection 中找到 关键字 的第一个匹配项，则为该项的元素；否则为 null。</returns>
            /// <exception cref="System.ArgumentException">设置未成功，集合中未找到指定关键字的元素。</exception>
            public ExcelColumn this[string name]
            {
                get
                {
                    ExcelColumn excelColumn;
                    for (int i = 0; i < List.Count; i++)
                    {
                        excelColumn = (ExcelColumn)List[i];

                        if (excelColumn.Name == name)
                        {
                            return excelColumn;
                        }
                    }

                    return null;
                }

                set
                {
                    ExcelColumn excelColumn;
                    for (int i = 0; i < List.Count; i++)
                    {
                        excelColumn = (ExcelColumn)List[i];

                        if (excelColumn.Name == name)
                        {
                            excelColumn = value;
                            return;
                        }
                    }

                    throw new ArgumentException("设置未成功，集合中未找到指定关键字的元素。");
                }
            }

            #endregion Properties

            #region Methods (3)

            // Public Methods (3) 

            /**/
            /// <summary>
            /// 将对象添加到 ExcelColumnCollection 的结尾处。
            /// </summary>
            /// <param name="value">要添加到 ExcelColumnCollection 的末尾处的 <see cref="ExcelColumn"/>。</param>
            /// <returns>ExcelColumnCollection 索引，已在此处添加了 value。</returns>
            public int Add(ExcelColumn value)
            {
                return (List.Add(value));
            }

            /**/
            /// <summary>
            /// 搜索指定的 <see cref="ExcelColumn"/>，并返回整个 ExcelColumnCollection 中第一个匹配项的从零开始的索引。
            /// </summary>
            /// <param name="value">要在 ExcelColumnCollection 中查找的 <see cref="ExcelColumn"/>。</param>
            /// <returns>如果在整个 ExcelColumnCollection 中找到 value 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。</returns>
            public int IndexOf(ExcelColumn value)
            {
                return (List.IndexOf(value));
            }

            /**/
            /// <summary>
            /// 从 RoleCollection 中移除特定对象的第一个匹配项。
            /// </summary>
            /// <param name="value">要从 <see cref="ExcelColumnCollection"/> 移除的 <see cref="ExcelColumn"/>。</param>
            /// 
            /// <exception cref="System.ArgumentException">未在 ExcelColumnCollection 对象中找到 value 参数。</exception>
            /// <exception cref="System.NotSupportedException">ExcelColumnCollection 为只读，或 ExcelColumnCollection 具有固定大小。
            /// </exception>
            public void Remove(ExcelColumn value)
            {
                List.Remove(value);
            }

            #endregion Methods
        }
        #endregion Nested Classes
    }


}