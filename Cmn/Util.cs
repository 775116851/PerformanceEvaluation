using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.IO;

using System.Data.OleDb;
using System.Xml;
using System.Net;

namespace PerformanceEvaluation.Cmn
{
    /// <summary>
    /// Summary description for Util.
    /// </summary>
    public class Util
    {
        private Util()
        {
        }

        public static string[] ChineseNum = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };

        public static bool IsDate(string date)
        {
            try
            {
                DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsZipCode(string zip)
        {
            // 避免全角字符
            try { int.Parse(zip); }
            catch { return false; }

            if (IsNaturalNumber(zip) && zip.Length == 6)
                return true;
            else
                return false;
        }

        public static bool IsMoney(string money)
        {
            try
            {
                Decimal.Parse(money);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool HasMoreRow(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool HasMoreRow(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        //整理字符串到安全格式
        public static string SafeFormat(string strInput)
        {
            if (string.IsNullOrEmpty(strInput) == true)
                return string.Empty;

            strInput = strInput.Replace("--", "");
            strInput = strInput.Replace(";", "");
            strInput = strInput.Replace("/*", "");
            strInput = strInput.Replace("*/", "");
            strInput = strInput.Replace("xp_", "");
            strInput = strInput.Replace("'", "");
            return strInput.Trim();
        }

        public static string ToSqlString(string paramStr)
        {
            return "'" + SafeFormat(paramStr) + "'";
        }

        public static string ToSqlLikeString(string paramStr)
        {
            return "'%" + SafeFormat(paramStr) + "%'";
        }
        public static string ToSqlLikeStringR(string paramStr)
        {
            return "'" + SafeFormat(paramStr) + "%'";
        }
        /// <summary>
        /// 传入的参数必须是'yyyy-MM-dd' 格式. 不另外检查
        /// </summary>
        /// <param name="paramDate"></param>
        /// <returns></returns>
        public static string ToSqlEndDate(string paramDate)
        {
            return ToSqlString(paramDate + " 23:59:59");
        }


        public static string TrimNull(Object obj)
        {
            if (obj is System.DBNull)
            {
                return AppConst.StringNull;
            }
            else
            {
                return obj.ToString().Trim();
            }
        }

        public static int TrimIntNull(Object obj)
        {
            if (obj is System.DBNull)
            {
                return AppConst.IntNull;
            }
            else
            {
                return Int32.Parse(obj.ToString());
            }
        }

        public static decimal TrimDecimalNull(Object obj)
        {
            if (obj is System.DBNull)
            {
                return AppConst.DecimalNull;
            }
            else
            {
                return decimal.Parse(obj.ToString());
            }
        }
        public static DateTime TrimDateNull(Object obj)
        {
            if (obj is System.DBNull)
            {
                return AppConst.DateTimeNull;
            }
            else
            {
                return DateTime.Parse(obj.ToString());
            }
        }
        public static string GetMD5(String str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToUpper();
            //			byte[] data = Encoding.Unicode.GetBytes(str);
            //			MD5 md = new MD5CryptoServiceProvider();
            //			byte[] result = md.ComputeHash(data);
            //			return Encoding.Unicode.GetString(result);
        }

        public static string GetSHA1(String str)
        {
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(str.ToString());
            SHA1 sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        // Function to test for Positive Integers.  
        public static bool IsNaturalNumber(String strNumber)
        {
            Regex objNotNaturalPattern = new Regex("[^0-9]");
            Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");
            return !objNotNaturalPattern.IsMatch(strNumber) &&
                objNaturalPattern.IsMatch(strNumber);
        }

        // Function to test for Positive Integers with zero inclusive  
        public static bool IsWholeNumber(String strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }

        // Function to Test for Integers both Positive & Negative  

        public static bool IsInteger(String strNumber)
        {
            Regex objNotIntPattern = new Regex("[^0-9-]");
            Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");
            return !objNotIntPattern.IsMatch(strNumber) && objIntPattern.IsMatch(strNumber);
        }
        public static bool IsNatureIneger(String strNumber)
        {
            //Regex objNotIntPattern = new Regex(@"^(0|([1-9]\d*))$");
            //Regex objIntPattern = new Regex(@"^-(0|([1-9]\d*))$");
            return Regex.IsMatch(strNumber, @"^(0|([1-9]\d*))$") || Regex.IsMatch(strNumber, @"^-(0|([1-9]\d*))$");

        }
        // Function to Test for Positive Number both Integer & Real 

        public static bool IsPositiveNumber(String strNumber)
        {
            Regex objNotPositivePattern = new Regex("[^0-9.]");
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            return !objNotPositivePattern.IsMatch(strNumber) &&
                objPositivePattern.IsMatch(strNumber) &&
                !objTwoDotPattern.IsMatch(strNumber);
        }

        // Function to test whether the string is valid number or not
        public static bool IsNumber(String strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
                !objTwoDotPattern.IsMatch(strNumber) &&
                !objTwoMinusPattern.IsMatch(strNumber) &&
                objNumberPattern.IsMatch(strNumber);
        }

        // Function To test for Alphabets. 
        public static bool IsAlpha(String strToCheck)
        {
            Regex objAlphaPattern = new Regex("[^a-zA-Z]");
            return !objAlphaPattern.IsMatch(strToCheck);
        }
        // Function to Check for AlphaNumeric.
        public static bool IsAlphaNumeric(String strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }
        public static bool IsEmailAddress(string strEmailAddress)
        {
            Regex objNotEmailAddress = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return objNotEmailAddress.IsMatch(strEmailAddress);
        }
        /// <summary>
        ///check for InternetURL         
        /// </summary>
        /// <param name="strURL"></param>
        /// <returns></returns>
        public static bool IsInternetURL(string strURL)
        {
            Regex objInternetURL = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            Regex objInternetUrl = new Regex(@"([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            Regex objUrl = new Regex("(" + objInternetURL + ")|(" + objInternetUrl + ")");
            return objUrl.IsMatch(strURL);
        }
        // Function Format Money 
        public static decimal ToMoney(string moneyStr)
        {
            return decimal.Round(decimal.Parse(moneyStr), 2);
        }
        public static decimal ToMoney(decimal moneyValue)
        {
            return decimal.Round(moneyValue, 2);
        }
        /// <summary>
        /// 舍去金额的分,直接舍去,非四舍五入
        /// </summary>
        /// <param name="moneyValue"></param>
        /// <returns></returns>
        public static decimal TruncMoney(decimal moneyValue)
        {
            int tempAmt = Convert.ToInt32(moneyValue * 100) % 10;
            moneyValue = (decimal)((moneyValue * 100 - tempAmt) / 100m);
            return moneyValue;
        }
        /// <summary>
        /// 判断是否手机号码
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static bool IsCellNumber(string cell)
        {
            if (string.IsNullOrEmpty(cell) == true)
                return false;

            try
            {
                // 验证为数字，防止全角字符
                Convert.ToInt64(cell);

                return Regex.IsMatch(cell, @"^1[0-9]\d{9}$");
            }
            catch
            {
                return false;
            }
        }

        public static void ToExcel(System.Web.UI.WebControls.DataGrid DataGrid2Excel, string FileName, string Title)
        {
            ToExcel(DataGrid2Excel, FileName, Title, "");
        }

        /// <summary>
        /// Renders the html text before the datagrid.
        /// </summary>
        /// <param name="writer">A HtmlTextWriter to write html to output stream</param>
        private static void FrontDecorator(HtmlTextWriter writer)
        {
            writer.WriteFullBeginTag("HTML");
            writer.WriteFullBeginTag("Head");
            //			writer.RenderBeginTag(HtmlTextWriterTag.Style);
            //			writer.Write("<!--");
            //			
            //			StreamReader sr = File.OpenText(CurrentPage.MapPath(MY_CSS_FILE));
            //			String input;
            //			while ((input=sr.ReadLine())!=null) 
            //			{
            //				writer.WriteLine(input);
            //			}
            //			sr.Close();
            //			writer.Write("-->");
            //			writer.RenderEndTag();
            writer.WriteEndTag("Head");
            writer.WriteFullBeginTag("Body");
        }

        /// <summary>
        /// Renders the html text after the datagrid.
        /// </summary>
        /// <param name="writer">A HtmlTextWriter to write html to output stream</param>
        private static void RearDecorator(HtmlTextWriter writer)
        {
            writer.WriteEndTag("Body");
            writer.WriteEndTag("HTML");
        }

        public static DataTable ToTable(DataTable ds, System.Web.UI.WebControls.DataGrid myGrid)
        {
            for (int i = 0; i < ds.Columns.Count; i++)
            {
                DataColumn dc = ds.Columns[i];
                bool notexist = true;
                foreach (System.Web.UI.WebControls.DataGridColumn mc in myGrid.Columns)
                {
                    if ((mc as System.Web.UI.WebControls.BoundColumn) == null)
                        continue;
                    if ((mc as System.Web.UI.WebControls.BoundColumn).DataField.ToLower().Equals(dc.ColumnName.ToLower()))
                    {
                        notexist = false;
                        if (mc.Visible)
                            dc.ColumnName = mc.HeaderText;
                        else
                        {
                            ds.Columns.Remove(dc);
                            i--;
                        }
                        break;
                    }
                }
                if (notexist)
                {
                    ds.Columns.Remove(dc);
                    i--;
                }
            }
            return ds;
        }

        public static void ToExcel(System.Web.UI.WebControls.DataGrid DataGrid2Excel, string FileName, string Title, string Head)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);

            FrontDecorator(hw);
            if (Title != "")
                hw.Write(Title + "<br>");
            if (Head != "")
                hw.Write(Head + "<br>");

            DataGrid2Excel.EnableViewState = false;
            DataGrid2Excel.RenderControl(hw);

            RearDecorator(hw);

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.Buffer = true;
            response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            response.ContentType = "application/ms-excel";
            response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
            response.Charset = "gb2312";
            response.Write(sw.ToString());
            response.End();
        }

        public static void ToExcel(DataTable dt, string FileName)
        {
            System.Web.UI.WebControls.DataGrid dgTemp = new System.Web.UI.WebControls.DataGrid();
            dgTemp.DataSource = dt;
            dgTemp.DataBind();
            ToExcel(dgTemp, FileName, "", "");
        }

        public static void Swap(ref int x, int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }
        public static void Swap(ref decimal x, decimal y)
        {
            decimal temp = x;
            x = y;
            y = temp;
        }
        public static void Swap(ref string x, string y)
        {
            string temp = x;
            x = y;
            y = temp;
        }

        #region ChineseMoney
        private static string getSmallMoney(string moneyValue)
        {
            int intMoney = Convert.ToInt32(moneyValue);
            if (intMoney == 0)
            {
                return "";
            }
            string strMoney = intMoney.ToString();
            int temp;
            StringBuilder strBuf = new StringBuilder(10);
            if (strMoney.Length == 4)
            {
                temp = Convert.ToInt32(strMoney.Substring(0, 1));
                strMoney = strMoney.Substring(1, strMoney.Length - 1);
                strBuf.Append(ChineseNum[temp]);
                if (temp != 0)
                    strBuf.Append("仟");
            }
            if (strMoney.Length == 3)
            {
                temp = Convert.ToInt32(strMoney.Substring(0, 1));
                strMoney = strMoney.Substring(1, strMoney.Length - 1);
                strBuf.Append(ChineseNum[temp]);
                if (temp != 0)
                    strBuf.Append("佰");
            }
            if (strMoney.Length == 2)
            {
                temp = Convert.ToInt32(strMoney.Substring(0, 1));
                strMoney = strMoney.Substring(1, strMoney.Length - 1);
                strBuf.Append(ChineseNum[temp]);
                if (temp != 0)
                    strBuf.Append("拾");
            }
            if (strMoney.Length == 1)
            {
                temp = Convert.ToInt32(strMoney);
                strBuf.Append(ChineseNum[temp]);
            }
            return strBuf.ToString();
        }

        public static string GetChineseMoney(decimal moneyValue)
        {
            string result = "";
            if (moneyValue == 0)
                return "零";

            if (moneyValue < 0)
            {
                moneyValue *= -1;
                result = "负";
            }
            ///			int intMoney = Convert.ToInt32(Util.TruncMoney(moneyValue)*100); 
            ///			Update By Cindy 2006.07.06发票打印大写金额不截断“分”
            string strMoney = Math.Floor(moneyValue * 100).ToString();
            int moneyLength = strMoney.Length;
            StringBuilder strBuf = new StringBuilder(100);
            if (moneyLength > 14)
            {
                throw new Exception("Money Value Is Too Large");
            }

            //处理亿部分
            if (moneyLength > 10 && moneyLength <= 14)
            {
                strBuf.Append(getSmallMoney(strMoney.Substring(0, strMoney.Length - 10)));
                strMoney = strMoney.Substring(strMoney.Length - 10, 10);
                strBuf.Append("亿");
            }

            //处理万部分
            if (moneyLength > 6)
            {
                strBuf.Append(getSmallMoney(strMoney.Substring(0, strMoney.Length - 6)));
                strMoney = strMoney.Substring(strMoney.Length - 6, 6);
                strBuf.Append("万");
            }

            //处理元部分
            if (moneyLength > 2)
            {
                strBuf.Append(getSmallMoney(strMoney.Substring(0, strMoney.Length - 2)));
                strMoney = strMoney.Substring(strMoney.Length - 2, 2);
                strBuf.Append("元");
            }

            //处理角、分处理分
            if (Convert.ToInt32(strMoney) == 0)
            {
                strBuf.Append("整");
            }
            else
            {
                if (moneyLength > 1)
                {
                    int intJiao = Convert.ToInt32(strMoney.Substring(0, 1));
                    strBuf.Append(ChineseNum[intJiao]);
                    if (intJiao != 0)
                    {
                        strBuf.Append("角");
                    }
                    strMoney = strMoney.Substring(1, 1);
                }

                int intFen = Convert.ToInt32(strMoney.Substring(0, 1));
                if (intFen != 0)
                {
                    strBuf.Append(ChineseNum[intFen]);
                    strBuf.Append("分");
                }
            }
            string temp = strBuf.ToString();
            while (temp.IndexOf("零零") != -1)
            {
                strBuf.Replace("零零", "零");
                temp = strBuf.ToString();
            }

            strBuf.Replace("零亿", "亿");
            strBuf.Replace("零万", "万");
            strBuf.Replace("亿万", "亿");

            return result + strBuf.ToString();
        }
        #endregion

        public static string DataTableToExcel(DataTable dt, string excelName)
        {
            if (dt == null)
            {
                return "DataTable不能为空";
            }

            int rows = dt.Rows.Count;
            int cols = dt.Columns.Count;
            StringBuilder sb;

            if (rows == 0)
            {
                return "没有数据";
            }

            sb = new StringBuilder();

            string physicPath = AppConfig.ErrorLogFolder;
            if (!System.IO.Directory.Exists(physicPath))
            {
                System.IO.Directory.CreateDirectory(physicPath);
            }
            string strFileName = physicPath + excelName;

            if (System.IO.File.Exists(strFileName))
                System.IO.File.Delete(strFileName);

            string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFileName + ";Extended Properties=Excel 8.0;";


            //生成创建表的脚本
            sb.Append("CREATE TABLE ");
            sb.Append(dt.TableName + " ( ");

            for (int i = 0; i < cols; i++)
            {
                string oleColumnType = dt.Columns[i].DataType.Name;

                if (dt.Columns[i].DataType == System.Type.GetType("System.Decimal"))
                    oleColumnType = "decimal";
                else if (dt.Columns[i].DataType == System.Type.GetType("System.Int32")
                    || dt.Columns[i].DataType == System.Type.GetType("System.Int")
                    )

                    oleColumnType = "int";
                else
                {
                    oleColumnType = String.Format("VarChar({0})", 100);
                }

                if (i < cols - 1)
                    sb.Append(string.Format("{0} " + oleColumnType + ",", dt.Columns[i].ColumnName));
                else
                    sb.Append(string.Format("{0} " + oleColumnType + ")", dt.Columns[i].ColumnName));
            }

            using (OleDbConnection objConn = new OleDbConnection(connString))
            {
                OleDbCommand objCmd = new OleDbCommand();
                objCmd.Connection = objConn;

                objCmd.CommandText = sb.ToString();

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //LogManagement.getInstance().WriteException(e, "在Excel中创建表", "Cmn/Util");
                    return "在Excel中创建表失败，错误信息：" + e.Message;
                }

                #region 生成插入数据脚本
                sb.Remove(0, sb.Length);
                sb.Append("INSERT INTO ");
                sb.Append(dt.TableName + " ( ");

                for (int i = 0; i < cols; i++)
                {
                    if (i < cols - 1)
                        sb.Append(dt.Columns[i].ColumnName + ",");
                    else
                        sb.Append(dt.Columns[i].ColumnName + ") values (");
                }

                for (int i = 0; i < cols; i++)
                {
                    if (i < cols - 1)
                        sb.Append("@" + dt.Columns[i].ColumnName + ",");
                    else
                        sb.Append("@" + dt.Columns[i].ColumnName + ")");
                }
                #endregion


                //建立插入动作的Command
                objCmd.CommandText = sb.ToString();
                OleDbParameterCollection param = objCmd.Parameters;

                for (int i = 0; i < cols; i++)
                {
                    param.Add(new OleDbParameter("@" + dt.Columns[i].ColumnName, OleDbType.VarChar));
                }

                //遍历DataTable将数据插入新建的Excel文件中
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < param.Count; i++)
                    {
                        param[i].Value = row[i];
                    }

                    objCmd.ExecuteNonQuery();
                }

                return "数据已成功导入Excel";
            }//end using
        }

        #region		Added by SeanQu 2006/05/08

        /// <summary>
        /// 判断是否为合法的数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidNumber(string value)
        {
            return Regex.IsMatch(value, @"^\d*$");
        }


        /// <summary>
        /// 将一个新的字符串添加到旧字符串的的最前面，并且将旧字符串中含有与新字符串相同字符串的去掉，
        /// 分隔符为自定义
        /// </summary>
        /// <param name="insertString"></param>
        /// <param name="oldString"></param>
        /// <param name="compartChar"></param>
        /// <returns></returns>
        public static string SortWithInsertNewString(string insertString, string oldString, char compartChar)
        {
            string[] oldStringList = oldString.Split(new char[] { compartChar });
            string newString = "";
            ArrayList newStringList = new ArrayList();

            newStringList.Add(insertString);

            for (int i = 0; i < oldStringList.Length; i++)
            {
                if (oldStringList[i] != insertString)
                {
                    newStringList.Add(oldStringList[i]);
                }
            }

            for (int i = 0; i < newStringList.Count; i++)
            {
                if (newString == "")
                {
                    newString = newStringList[i].ToString();
                }
                else
                {
                    newString += compartChar.ToString() + newStringList[i].ToString();
                }
                if (i == 9)
                {
                    break;
                }
            }
            return newString;
        }

        /// <summary>
        /// Added by seanqu 2006/06/07
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetAbsoluteFilePath(string filePath)
        {
            string file = filePath;
            if (!filePath.Substring(1, 1).Equals(":")
                && !filePath.StartsWith("\\"))
            {
                file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            }

            return file.Replace("/", "\\");
        }

        #endregion


        public static string RemoveHtmlTag(string str)
        {
            //Regex regex  = new Regex(@"<([\s-\S][^>]*)?>");
            //Regex regex2 = new Regex(@"(\w(\.|,|\/))");

            //return regex2.Replace(regex.Replace(str,""),"");

            Regex reg = new Regex(@"<\/*[^<>]*>");
            return reg.Replace(str, "");
        }
        public static string GetLastName(string str)
        {
            string[] names = str.Split('_');
            string lastName = str;
            if (names.Length <= 1)
                return lastName;
            lastName = "";
            for (int i = 1; i < names.Length; i++)
            {
                if (lastName.Length != 0)
                    lastName = lastName + "_";
                lastName = lastName + names[i];
            }
            return lastName;
        }
        public static string GetInQueryString(string str)
        {
            if (str.Trim().Length == 0)
                return str.Trim();

            string[] str1 = Regex.Split(str, @"\.");
            StringBuilder sb = new StringBuilder();
            foreach (string tmp in str1)
            {
                if (tmp.Trim().Length != 0 && Util.IsInteger(tmp.Trim()))
                {
                    sb.Append(tmp.Trim()).Append(",");
                }
            }
            if (sb.Length == 0)
                return "";
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        //将XmlDocument转为string edit by xiaocp 
        public static string XmlDocumentToString(ref XmlDocument doc)
        {
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);

            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();

            return xmlString;
        }

        public static int SwapRound(double dbValue)
        {
            int round = (int)(dbValue * 10 % 10) > 4 ? (int)dbValue + 1 : (int)dbValue;
            return round;
        }

        public static int SwapRound(decimal dbValue)
        {
            int round = (int)(dbValue * 10 % 10) > 4 ? (int)dbValue + 1 : (int)dbValue;
            return round;
        }

        public static int Rounding(decimal dbValue)
        {
            int round;
            decimal dbValueHalf = dbValue / 2;
            if (dbValueHalf % 2 > 0)
            {
                round = ((int)(dbValueHalf / 10) * 10 + 10);
            }
            else
            {
                round = ((int)(dbValueHalf / 10) * 10);
            }
            return round;
        }

        static readonly DateTime changeDate = new DateTime(2011, 11, 1);
        public static decimal GetChildrenFuelTax(decimal FuelTax)
        {
            if (DateTime.Now < changeDate)
            {
                return MathRound(FuelTax / 2, -1);
            }
            int fuelTax = Convert.ToInt32(FuelTax);
            int i = (fuelTax / 2) % 10;
            return fuelTax / 2 - i;
        }

        //public static decimal GetChildrenFuelTax(decimal FuelTax)
        //{
        //    return MathRound(FuelTax / 2, -1);
        //}

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="n">四舍五入位数，负数表示倒数整数位数</param>
        /// <returns></returns>
        public static decimal MathRound(decimal d1, int n)
        {
            if (n >= 0)
            {
                return decimal.Round(d1, n, MidpointRounding.AwayFromZero);
            }
            else
            {
                decimal d = decimal.Round(d1 / (decimal)Math.Pow(10, -1 * n), 0, MidpointRounding.AwayFromZero);
                return d * (decimal)Math.Pow(10, -1 * n);
            }
        }

        public static string ReadHtmFile(string filePath)
        {
            string templet = "";

            if (filePath.Contains("http:"))
            {
                HttpWebResponse myRes;
                HttpWebRequest myReq;
                Stream m_stream;
                StreamReader m_reader;
                myReq = (HttpWebRequest)WebRequest.Create(filePath);
                myReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.5; Windows NT 4.0)";

                try
                {
                    myRes = (HttpWebResponse)myReq.GetResponse();
                    m_stream = myRes.GetResponseStream();
                }
                catch
                { return ""; }
                m_reader = new StreamReader(m_stream, Encoding.GetEncoding("gb2312"));
                StringBuilder m_strb = new StringBuilder();
                string tmpstr;
                while ((tmpstr = m_reader.ReadLine()) != null)
                {
                    m_strb.Append(tmpstr + "\r\n");
                }
                templet = m_strb.ToString();
            }
            else
            {
                FileStream aFile;
                try
                {
                    aFile = new FileStream(@filePath, FileMode.Open, FileAccess.Read);
                }
                catch
                {
                    return "";
                }
                StreamReader sr = new StreamReader(aFile, System.Text.Encoding.GetEncoding("gb2312"));

                templet = sr.ReadToEnd();
                sr.Close();
            }
            return templet;
        }

        public static bool IsSafeHtml(string html)
        {
            bool flag = true;
            Regex regex1 = new Regex(@"(<script[\s\S]+</script *>|<iframe[\s\S]+</iframe *>|<frameset[\s\S]+</frameset *>|<html[\s\S]+</html *>|<header[\s\S]+</header *>|
<body[\s\S]+</body *>|<link[\s\S]+</link *>|<object[\s\S]+</object *>|<embed[\s\S]+</embed *>|<form[\s\S]+</form *>)", RegexOptions.IgnoreCase);
            Regex regex2 = new Regex(@" href *= *[\s\S]*script *:", RegexOptions.IgnoreCase);
            Regex regex3 = new Regex(@" on[\s\S]*=", RegexOptions.IgnoreCase);
            if (regex1.IsMatch(html) || regex2.IsMatch(html) || regex3.IsMatch(html))
            {
                flag = false;
            }
            return flag;
        }

        public static string ToSafeHtml(string input)
        {
            if (input == null)
            {
                return null;
            }
            else
            {
                //return HttpContext.Current.Server.HtmlEncode(input.Replace(":", "："));
                return System.Web.HttpUtility.HtmlEncode(input.Replace(":", "："));
            }
        }

        /// <summary>
        /// 身份证验证
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns></returns>
        public static bool CheckIDCard(string Id)
        {
            Id = Id.Trim();
            if (Id.Length == 18)
            {
                bool check = CheckIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                bool check = CheckIDCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 18位身份证验证
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns></returns>
        private static bool CheckIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }

        /// <summary>
        /// 15位身份证验证
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns></returns>
        private static bool CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }

        /// <summary>
        /// 检查是否图片
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Boolean IsImage(Stream stream)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Des解密
        /// </summary>
        /// <param name="decryptString">解密字符串</param>
        /// <param name="decryptKey">解密密码</param>
        /// <returns></returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                DCSP.Key = rgbKey;
                DCSP.IV = rgbKey;
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(), CryptoStreamMode.Write);

                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        #region 过滤IntNull,DecimalNull,StringNull的ToString方法
        public static string ToString(int value)
        {
            return value == AppConst.IntNull ? string.Empty : value.ToString();
        }
        public static string ToString(int value, string formmater)
        {
            return value == AppConst.IntNull ? string.Empty : string.Format(formmater, value);
        }

        public static string ToString(decimal value)
        {
            return value == AppConst.DecimalNull ? string.Empty : value.ToString();
        }
        public static string ToString(decimal value, string formatter)
        {
            return value == AppConst.DecimalNull ? string.Empty : string.Format(formatter, value);
        }

        public static string ToString(string value)
        {
            return value == AppConst.StringNull ? string.Empty : value.ToString();
        }
        public static string ToString(string value, string formatter)
        {
            return value == AppConst.StringNull ? string.Empty : string.Format(formatter, value);
        }

        public static string ToString(float value)
        {
            return value == AppConst.FloatNull ? string.Empty : value.ToString();
        }
        public static string ToString(float value, string formatter)
        {
            return value == AppConst.FloatNull ? string.Empty : string.Format(formatter, value);
        }

        public static string ToString(double value)
        {
            return value == AppConst.DoubleNull ? string.Empty : value.ToString();
        }
        public static string ToString(double value, string formatter)
        {
            return value == AppConst.DoubleNull ? string.Empty : string.Format(formatter, value);
        }

        public static string ToString(DateTime value)
        {
            return value == AppConst.DateTimeNull ? string.Empty : value.ToString();
        }
        public static string ToString(DateTime value, string formatter)
        {
            return value == AppConst.DateTimeNull ? string.Empty : string.Format(formatter, value);
        }

        #endregion

        public static string GetHttpRequestUrl(HttpRequest request)
        {
            string hostPort = request.Url.Authority;
            if (!string.IsNullOrEmpty(AppConfig.UrlPort))
            {
                hostPort = request.Url.Host + ":" + AppConfig.UrlPort;
            }

            return request.Url.Scheme + "://" + hostPort + request.Url.AbsolutePath;
        }

        /// <summary>
        /// 过滤特俗字符 2014-06-11 wq
        /// </summary>
        /// <param name="sJson"></param>
        /// <returns></returns>
        public static string JsonFilter(string sJson)
        {
            if (string.IsNullOrEmpty(sJson))
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < sJson.Length; i++)
            {
                char c = sJson.ToCharArray()[i];
                switch (c)
                {
                    case '"':
                        sb.Append("\"");
                        break;
                    case '\'':
                        sb.Append("\\'");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '/':
                        sb.Append("\\/");
                        break;
                    //   case '\b': 
                    //      sb.Append("\\b"); 
                    //   break; 
                    //   case '\f': 
                    //      sb.Append("\\f"); 
                    //   break; 
                    //   case '\n': 
                    //     sb.Append("\\n"); 
                    //  break; 
                    //  case '\r': 
                    //     sb.Append("\\r"); 
                    //  break; 
                    //  case '\t': 
                    //     sb.Append("\\t"); 
                    //  break; 

                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }

        public static void ToTxt(string txt, string filename)
        {
            if (txt != "")
            {
                System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
                Response.Clear();
                Response.Buffer = true;
                //Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ".txt");
                //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文
                Response.ContentType = "text/plain";//设置输出文件类型为txt文件。 

                System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
                Response.Write(txt);
                Response.End();
            }
        }
    }
}
