using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluation.Cmn
{
    public class AppConst
    {
        #region 系统中判断未赋值的判断 
        public const string StringNull = "StringNull";
        public const int IntNull = -9999;
        public const decimal DecimalNull = -9999;
        public const float FloatNull = -9999;  //Add By Teracy
        public const float DoubleNull = -9999;  //Add By Teracy 
        public const int OtherGroup = -9000;//商品分组 '更多'的sysno
        public static DateTime DateTimeNull = DateTime.Parse("1900-01-01");
        public static DateTime DateTimeDelete = DateTime.Parse("1900-01-02");
        #endregion

        /// <summary>
        /// 指示更新时需将该int类型的数据库值清空
        /// </summary>
        public const int IntDelete = -9998;


        public const string AllSelectString = "--- 全部 ---";
        public const string PleaseSelectString = "--- 请选择 ---";
        public const string DecimalToInt = "#########0"; //用于point的显示，一般来说currentprice应该没有分。
        public const string DecimalFormat = "#########0.00";
        public const string DecimalFormatWithGroup = "#,###,###,##0.00";
        public const string IntFormatWithGroup = "#,###,###,##0";
        public const string DecimalFormatWithCurrency = "￥#########0.00";
        public const string DateFormat = "yyyy-MM-dd";
        public const string DateFormatLong = "yyyy-MM-dd HH:mm:ss";
        public const string DecimalFormatForPayRate = "#########0.000";//payrate支持小数点后三位
        public const int APIDefaultUserSysNo = -1000;//用于代表API接口当前系统用户 
        public const string KEY_64 = "Ayq4nJae";
        /// <summary>
        /// 前后台MD5加密的密钥
        /// </summary>
        public const string KEY_MD5_MIS = "E7w#lI8v";
        public const string KEY_MD5_WEB = "Lo9*T3vM";
        //用于显示信息重复
        public const int InfoRepeat = -1;
        //用于DataGrid中每页的记录数
        public const int PageSize = 20;
        //用于DataGrid中每页按钮的数目
        public const int PageButtonCount = 5;

        /// <summary>
        /// 邮件短信多个参数间分隔符 如：1|@&$|2|@&$|3
        /// </summary>
        public const string EmailSMSValueSeparator = "|@&$|";

        public const long WeixinImgSizeMax = 1024 * 1024 * 10;
        //积分变更日志验证码
        public const string KEY_Point_LogCheck = "Point123";

        //热门关键字内容搜素
        public const string HotSearchKeyWord = "HotSearchKeyWord";

        //广告区域配置
        public const string AdvArea = "AdvArea";
    }
}
