using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PerformanceEvaluation.Cmn
{
    /// <summary>
    /// ���÷�����
    /// </summary>
    public static class CommonFunctions
    {
        public static string md5(string str, int code)
        {
            string ret = "";
            if (code == 16) //16λmd5���ܣ�ȡ32λ���ܵ�9~25�ַ��� 
            {
                ret = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToUpper().Substring(8, 16);
            }
            if (code == 32) //32λ���� 
            {
                ret = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToUpper();
            }
            return ret;
        }

        /// <summary>
        /// DEC ���ܹ���
        /// </summary>
        /// <param name="pToEncrypt">�����ܵ��ַ���</param>
        /// <param name="sKey">��Կ��ֻ֧��8���ֽڵ���Կ��</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string Encode(string pToEncrypt)
        {
            string sKey = AppConst.KEY_64;
            //�������ݼ��ܱ�׼(DES)�㷨�ļ��ܷ����ṩ���� (CSP) �汾�İ�װ����
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);��//�������ܶ������Կ��ƫ����
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);�� //ԭ��ʹ��ASCIIEncoding.ASCII������GetBytes����

            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);//���ַ����ŵ�byte������

            MemoryStream ms = new MemoryStream();//������֧�ִ洢��Ϊ�ڴ������
            //���彫���������ӵ�����ת������
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //�����Ѿ�����˰Ѽ��ܺ�Ľ���ŵ��ڴ���ȥ

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /**/
        /// <summary>
        /// DEC ���ܹ���
        /// </summary>
        /// <param name="pToDecrypt">�����ܵ��ַ���</param>
        /// <param name="sKey">��Կ��ֻ֧��8���ֽڵ���Կ��ͬǰ��ļ�����Կ��ͬ��</param>
        /// <returns>���ر����ܵ��ַ���</returns>
        public static string Decode(string pToDecrypt)
        {
            string sKey = AppConst.KEY_64;
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);��//�������ܶ������Կ��ƫ��������ֵ��Ҫ�������޸�
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                //����StringBuild����createDecryptʹ�õ��������󣬱���ѽ��ܺ���ı����������
                StringBuilder ret = new StringBuilder();

                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception Ex)
            {
                return "";
            }
        }

        public static string StringFilter(string inputstr)
        {
            string ret;
            ret = inputstr;
            return ret;
        }

        public static string CutStr(string s, int l)
        {
            string temp = s;
            if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= l)
            {
                return temp;
            }
            for (int i = temp.Length; i >= 0; i--)
            {
                temp = temp.Substring(0, i);
                if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= l - 3)
                {
                    return temp + "...";
                }
            }
            return "";
        }

        #region ���ֵ
        static private Random ra = new Random();
        public static int ThrowRandom(int min, int max)
        {
            return ra.Next(min, max + 1);
        }

        public static string CutDateString(DateTime dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(dt.Year.ToString().Substring(2)).Append("-")
                .Append(dt.Month.ToString().Length == 2 ? dt.Month.ToString() : "0" + dt.Month.ToString()).Append("-")
                .Append(dt.Day.ToString().Length == 2 ? dt.Day.ToString() : "0" + dt.Day.ToString());
            return sb.ToString();
        }
        #endregion

        #region ƴ������ĸ

        static public string getSpells(string input)
        {
            int len = input.Length;
            string reVal = "";
            for (int i = 0; i < len; i++)
            {
                reVal += getSpell(input.Substring(i, 1));
            }
            return reVal;
        }

        static public string getSpell(string cn)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cn);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "?";
            }
            else return cn;
        }
        #endregion

        public static string ParamToMd5(string[] keys, string[] values, string pass)
        {
            StringBuilder md5sb = new StringBuilder();
            for (int i = 0; i < keys.Length; i++)
            {
                md5sb.Append(keys[i]).Append("=").Append(values[i]);
            }
            md5sb.Append(pass);
            return md5(md5sb.ToString(), 32);
        }

        #region ��������ַ���
        public static string GenerateRandomString(int Length)
        {
            char[] constant = { '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(constant.Length);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(constant.Length - 1)]);
            }
            return newRandom.ToString();
        }

        public static string GenerateRandomCouponString(int Length)
        {
            char[] constant = {'0','1','2', '3', '4', '5', '6', '7', '8', '9'};
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(constant.Length);
            Guid gid= Guid.NewGuid();
            Random rd = new Random(gid.GetHashCode());
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(constant.Length)]);
            }
            return newRandom.ToString();
        }

        public static string GenerateRandomSpells(int Length)
        {
            char[] constant = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(constant.Length);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(constant.Length - 1)]);
            }
            return newRandom.ToString();
        }

        public static string GenerateRandomNumber(int Length)
        {
            char[] constant = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(constant.Length);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(constant.Length - 1)]);
            }
            return newRandom.ToString();
        }
        #endregion

        public static string[] GetHtmlImageUrlList(string sHtmlText)
        {
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // ����ƥ����ַ���
            MatchCollection matches = regImg.Matches(sHtmlText);

            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // ȡ��ƥ�����б�
            foreach (Match match in matches)
            {
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            }
            return sUrlList;
        }

        public static string FilterUrlPort(string input)
        {
            if (AppConfig.IsFilterPort.ToUpper() == "TRUE")
            {
                Regex r = new Regex(@"://[^/]+?(?<port>:\d+)?/",
                RegexOptions.Compiled);
                string port = r.Match(input).Result("${port}");
                if (port != "")
                {
                    input = input.Replace(port, "");
                }
            }
            return input;
        }

        public static byte[] StringToHexbytes(string str)
        {
            str = str.Replace(" ", "");
            if ((str.Length % 2) != 0)
            {
                str += "";
            }
            byte[] bytes = new byte[str.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
            }
            return bytes;
        }
        /// <summary>
        /// ���ַ�����ʽ��Id�б�ת��ΪList<int>��ʽ
        /// </summary>
        /// <param name="strList">�ַ�����ʽ��Id�б�</param>
        /// <param name="listSeparator">�ָ���</param>
        /// <returns></returns>
        public static List<int> GetIntListFromString(string strList, char listSeparator)
        {
            List<int> list = new List<int>();

            string[] temp = strList.Split(new char[] { listSeparator });

            foreach (string str in temp)
            {
                int n;
                if (int.TryParse(str, out n) == false)
                    continue;

                list.Add(n);
            }

            return list;
        }

        /// <summary>
        /// ��int�б�ת��Ϊ�ַ����б�
        /// </summary>
        /// <param name="list">int�б�</param>
        public static string GetStringFromIntList(List<int> list)
        {
            string strList = string.Empty;

            foreach (int id in list)
            {
                strList += (strList.Length == 0 ? "" : ",") + id.ToString();
            }

            return strList;
        }

        /// ������������������add by alan 2007/05/17------------------------
        /// <summary>
        /// ת��ǵĺ���(DBC case)
        /// 
        /// �����ַ���
        /// ����ַ���
        ///
        ///ȫ�ǿո�Ϊ12288����ǿո�Ϊ32
        ///�����ַ����(33-126)��ȫ��(65281-65374)�Ķ�Ӧ��ϵ�ǣ������65248
        /// </summary>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }


        /// <summary>
        /// Get Client IP from HttpRequest Variables;
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetClientIP(HttpRequest request)
        {
            if (request != null && request.ServerVariables["REMOTE_ADDR"] != null && String.IsNullOrEmpty(Convert.ToString(request.ServerVariables["REMOTE_ADDR"])) == false)
            {
                return request.ServerVariables["REMOTE_ADDR"].ToString().Trim();
            }
            else
            {
                return String.Empty;
            }
        }
        /// <summary>
        /// ��ȡ�ͻ���IP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetIp(HttpRequest request)
        {
            string ip = request.Headers["x-forwarded-for"];
            if (ip == null || ip.Length == 0)
            {
                ip = request.Headers["Proxy-Client-IP"];
            }
            if (ip == null || ip.Length == 0)
            {
                ip = request.Headers["WL-Proxy-Client-IP"];
            }
            if (ip == null || ip.Length == 0)
            {
                ip = request.UserHostAddress;
            }
            return ip;
        }
        public class EnumItem
        {
            private object m_key;
            private object m_value;

            public object Key
            {
                get { return m_key; }
                set { m_key = value; }
            }

            public object Value
            {
                get { return m_value; }
                set { m_value = value; }
            }

            public EnumItem(object _key, object _value)
            {
                m_key = _key;
                m_value = _value;
            }
        }

        /// <summary>
        /// ���ö��������������ȫ������б�������"All"(Value: -1)��
        /// </summary>
        /// <param name="enumType">ö�ٵ�����</param>
        /// <returns></returns>
        public static List<EnumItem> GetEnumItems(Type enumType)
        {
            return GetEnumItems(enumType, false);
        }

        /// <summary>
        /// ���ö��������������ȫ������б�����"All"��
        /// </summary>
        /// <param name="enumType">ö�ٵ�����</param>
        /// <returns></returns>
        public static List<EnumItem> GetEnumItemsWithAll(Type enumType)
        {
            return GetEnumItems(enumType, true);
        }

        /// <summary>
        /// ���ö��������������ȫ������б�
        /// </summary>
        /// <param name="enumType">ö�ٵ�����</param>
        /// <param name="withAll">�Ƿ����"All"</param>
        /// <returns></returns>
        public static List<EnumItem> GetEnumItems(Type enumType, bool withAll)
        {
            List<EnumItem> list = new List<EnumItem>();

            if (enumType.IsEnum != true)
            {
                // ����ö������
                throw new InvalidOperationException();
            }

            // ���� All ѡ��
            if (withAll == true)
                list.Add(new EnumItem(AppConst.IntNull, "All"));

            // �������Description��������Ϣ
            Type typeDescription = typeof(DescriptionAttribute);

            // ���ö�ٵ��ֶ���Ϣ����Ϊö�ٵ�ֵʵ������һ��static���ֶε�ֵ��
            System.Reflection.FieldInfo[] fields = enumType.GetFields();

            // ���������ֶ�
            foreach (FieldInfo field in fields)
            {
                // ���˵�һ������ö��ֵ�ģ���¼����ö�ٵ�Դ����
                if (field.FieldType.IsEnum == false)
                    continue;

                // ͨ���ֶε����ֵõ�ö�ٵ�ֵ
                int value = (int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
                string text = string.Empty;

                // �������ֶε������Զ������ԣ�����ֻ����Description����
                object[] arr = field.GetCustomAttributes(typeDescription, true);
                if (arr.Length > 0)
                {
                    // ��ΪDescription�Զ������Բ������ظ�������ֻȡ��һ��
                    DescriptionAttribute aa = (DescriptionAttribute)arr[0];

                    // ������Ե�����ֵ
                    text = aa.Description;
                }
                else
                {
                    // ���û��������������ô����ʾӢ�ĵ��ֶ���
                    text = field.Name;
                }
                list.Add(new EnumItem(value, text));
            }
            return list;
        }

        /// <summary>
        /// ���ö��ֵ��Ӧ��������Ϣ��
        /// </summary>
        /// <param name="enumType">ö������</param>
        /// <param name="value">ö��ֵ(int����)</param>
        /// <returns></returns>
        public static string GetEnumValueDescription(Type enumType, int value)
        {
            List<EnumItem> list = GetEnumItems(enumType);
            foreach (EnumItem item in list)
            {
                if (Convert.ToInt32(item.Key) == value)
                    return item.Value.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// ���ö��ֵ��Ӧ��������Ϣ��������λ��ö�٣���
        /// </summary>
        /// <param name="enumType">ö������</param>
        /// <param name="value">ö��ֵ(int����)</param>
        /// <returns></returns>
        public static string GetFlagEnumValueDescription(Type enumType, int value)
        {
            List<EnumItem> list = GetEnumItems(enumType);
            string descrip = string.Empty;

            foreach (EnumItem item in list)
            {
                if ((Convert.ToInt32(item.Key) & value) > 0)
                    descrip += (descrip.Length > 0 ? "," : "") + item.Value.ToString();
            }

            if (descrip.Length == 0)
                return "��";

            return descrip;
        }

        /// <summary>
        /// �ж��Ƿ����ID�ĸ�ʽ(��Ȼ��)
        /// </summary>
        /// <param name="number">Id Number</param>
        /// <returns></returns>
        public static bool IsIdNumber(string number)
        {
            throw new Exception("Not Finished.");
        }
        //��Ǯ��ڼ�����,Ϊyyyy-mm-dd��ʽ
        public static ArrayList GetFastCashSession()
        {
            ArrayList alFastCashSession = new ArrayList();

            ArrayList al1 = new ArrayList();
            al1.Add("2007-02-07");
            al1.Add("2007-02-17");
            ArrayList al2 = new ArrayList();
            al2.Add("2007-02-26");
            al2.Add("2007-03-14");

            alFastCashSession.Add(al1);
            alFastCashSession.Add(al2);

            return alFastCashSession;
        }
        /// <summary>
        /// �ж��Ƿ�ע���Ǯ�ʻ������̳ǵȼ�  
        /// Created By Shadow | 2007-02-27
        /// </summary>
        /// <returns></returns>
        public static bool IsRegFastCashPromotionRank()
        {
            bool flag = false;
            DateTime dateFrom = new DateTime(2007, 3, 1, 13, 30, 0);
            DateTime dateTo = new DateTime(2007, 4, 23, 13, 30, 0);
            //DateTime dateFrom = new DateTime(2007, 3, 1);
            //DateTime dateTo = new DateTime(2007, 4, 23, 13, 30, 0);
            //�ж��Ƿ���ע���Ǯ�����û�����Ļ�ڼ���
            if (DateTime.Now >= dateFrom && DateTime.Now <= dateTo)
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// ת�� ProductName ����� ����˫ ���ţ�
        /// ���Ƴ� HTML ��ǩ��
        /// Add By Teracy
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetFormatNameForImgAlt(string productName)
        {
            return Util.RemoveHtmlTag(productName.Replace("'", "&rsquo;").Replace("\"", "&quot;"));
        }
        /// <summary>
        /// �û�����@���ŵ��滻(����ɫ)
        /// Add By Teracy
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static string FormatCustomerId_Black(string UserName)
        {
            return UserName.Replace("@", "<img src='/WebResources/images/Products/@2.gif' border=0>");

        }
        /// <summary>
        /// �û�����@���ŵ��滻(����ɫ)
        /// Add By Teracy
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static string FormatCustomerId_Orange(string UserName)
        {
            return UserName.Replace("@", "<img src='/WebResources/images/Products/@1.gif' border=0>");
        }

        public static bool Contain(string value, string subString, char split)
        {
            if (value == null || value == string.Empty)
                return false;
            string[] list = value.Split(split);
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == subString)
                    return true;
            }
            return false;
        }

        public static DataTable GetNewDataTable(DataTable dt, string condition, string Order)
        {
            DataTable newdt = new DataTable();
            newdt = dt.Clone();
            DataRow[] dr = dt.Select(condition);
            for (int i = 0; i < dr.Length; i++)
            {
                newdt.ImportRow((DataRow)dr[i]);
            }
            newdt.DefaultView.Sort = Order;// " SysNo ASC";
            return newdt;//���صĲ�ѯ���
        }

        //2010-12-14
        public static DataTable LinqToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();
            // column names    
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;
            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow    
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public static string md5(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToUpper();
        }

        /// <summary>   
        /// �õ��ַ����ĳ��ȣ�һ��������2���ַ�   
        /// </summary>   
        /// <param name="str">�ַ���</param>   
        /// <returns>�����ַ�������</returns>   
        public static int GetStringLength(string str)
        {
            if (str.Length == 0) return 0;

            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
            }
            return tempLen;
        }

        /// <summary>
        /// ȡ��ҳURL��ַ
        /// </summary>
        /// <returns></returns>
        public static string GetUrl(HttpRequest Request)
        {
            string strTemp = "";
            
            
            if (Request.ServerVariables["HTTPS"] == "off")
            {
                strTemp = "http://";
            }
            else
            {
                strTemp = "https://";
            }

            strTemp = strTemp + Request.ServerVariables["SERVER_NAME"];

            //if (Request.ServerVariables["SERVER_PORT"] != "80")
            //{
            //    strTemp = strTemp + ":" + Request.ServerVariables["SERVER_PORT"];
            //}

            strTemp = strTemp + Request.ServerVariables["URL"];

            if (Request.QueryString != null)
            {
                strTemp = strTemp + Request.Url.Query;
            }

            return strTemp;
        }

        public static UserControl LoadUserControl(string UserControlPath, Page pg, params object[] constructorParameters)
        {
            List<Type> constParamTypes = new List<Type>();
            foreach (object constParam in constructorParameters)
            {
                constParamTypes.Add(constParam.GetType());
            }

            UserControl ctl = pg.LoadControl(UserControlPath) as UserControl;

            // Find the relevant constructor
            ConstructorInfo constructor = ctl.GetType().BaseType.GetConstructor(constParamTypes.ToArray());

            //And then call the relevant constructor
            if (constructor == null)
            {
                throw new MemberAccessException("The requested constructor was not found on : " + ctl.GetType().BaseType.ToString());
            }
            else
            {
                constructor.Invoke(ctl, constructorParameters);
            }

            // Finally return the fully initialized UC
            return ctl;
        }

        /// <summary>
        /// ���ɷ��͵�APIECard����ˮ��
        /// </summary>
        /// <returns></returns>
        public static string OrderNumber()
        {
            return DateTime.Now.ToString("yyMMdd") + GenerateRandomNumber(4);
        }

        /// <summary>
        /// ��Ч��
        /// </summary>
        /// <param name="dateFormat">���ڸ�ʽ</param>
        /// <returns></returns>
        public static string TheEndDate(string dateFormat)
        {
            return DateTime.Now.AddYears(int.Parse(AppConfig.APIECardKeepYears)).ToString(dateFormat);
        }

        /// <summary>
        /// jsonתDataSet
        /// </summary>
        /// <param name="json">json�ַ���</param>
        /// <returns></returns>
        public static DataSet JsonToDataSet(string json)
        {
            try
            {
                JArray jarr = JArray.Parse(json);
                DataTable dt = new DataTable();
                for (int i = 0; i < jarr.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    JObject job = JObject.Parse(jarr[i].ToString());
                    foreach (JProperty jp in job.Children())
                    {
                        if (!dt.Columns.Contains(jp.Name))
                        {
                            dt.Columns.Add(jp.Name);
                        }
                        dr[jp.Name] = jp.Value.ToString().Replace("\"", "");
                    }
                    dt.Rows.Add(dr);
                }
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// У���Ƿ���ȷ���ֻ�����
        /// </summary>
        /// <param name="mobileNumber">�ֻ�����</param>
        /// <returns></returns>
        public static bool isMobile(string mobileNumber)
        {
            string partten = @"(^0{0,1}1[3|4|5|6|7|8|9][0-9]{9}$)";
            if (System.Text.RegularExpressions.Regex.IsMatch(mobileNumber, partten))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ���ڸ�ʽ��
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateString(string date)
        {
            if (date.Length == 8)
            {
                return date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
            }
            return "2000-01-01";
        }

        /// <summary>
        /// ȡ��ҳURL��ַ
        /// </summary>
        /// <returns></returns>
        public static string GetUrl(HttpRequest Request, bool ishttps)
        {
            string strTemp = "";

            if (ishttps)
            {

                strTemp = "https://";
            }
            else
            {
                strTemp = "http://";
            }

            //if (Request.ServerVariables["HTTPS"] == "off")
            //{
            //    strTemp = "http://";
            //}
            //else
            //{
            //    strTemp = "https://";
            //}

            strTemp = strTemp + Request.ServerVariables["SERVER_NAME"];

            //�Ƿ���˶˿�
            if (!string.IsNullOrEmpty(AppConfig.IsFilterPort) && AppConfig.IsFilterPort.ToUpper() == "FALSE")
            {
                if (Request.ServerVariables["SERVER_PORT"] != "80")
                {
                    strTemp = strTemp + ":" + Request.ServerVariables["SERVER_PORT"];
                    //LogManagement.getInstance().WriteTrace("strTempֵ:" + strTemp, "", "");
                }
            }
            strTemp = strTemp + Request.ServerVariables["URL"];
            //LogManagement.getInstance().WriteTrace("strTempֵ2:" + strTemp, "", "");
            if (Request.QueryString != null)
            {
                strTemp = strTemp + Request.Url.Query;
            }
            return strTemp;
        }
        /// <summary>
        /// ��Unixʱ���ת��ΪDateTime����ʱ��
        /// </summary>
        /// <param name="d">double������</param>
        /// <returns></returns>
        public static DateTime ConvertIntDateTime(double d)
        {
            DateTime time = DateTime.MinValue;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            time = startTime.AddSeconds(d);
            return time;
        }
        /// <summary>
        ///  ��DateTimeʱ���ʽת��ΪUnixʱ�����ʽ
        /// </summary>
        /// <param name="time">ʱ��</param>
        /// <returns></returns>
        public static double ConvertDateTimeInt(DateTime time)
        {
            double intResult = 0;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            intResult = (time - startTime).TotalSeconds;
            return intResult;
        }
    }
}