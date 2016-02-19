using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Data;

namespace PerformanceEvaluation.Cmn
{
    public class AppEnum
    {
        #region 工具函数
        public static SortedList GetStatus(System.Type t)
        {
            SortedList list = new SortedList();

            Array a = Enum.GetValues(t);
            for (int i = 0; i < a.Length; i++)
            {
                string enumName = a.GetValue(i).ToString();
                int enumKey = (int)System.Enum.Parse(t, enumName);
                string enumDescription = GetDescription(t, enumKey);
                list.Add(enumKey, enumDescription);
            }
            return list;
        }

        public static string GetEnumsStr(Type enumName)
        {
            StringBuilder sb = new StringBuilder();
            // get enum fileds
            FieldInfo[] fields = enumName.GetFields();
            bool isFirst = true;

            sb.Append("[");
            foreach (FieldInfo field in fields)
            {
                if (!field.FieldType.IsEnum)
                {
                    continue;
                }
                // get enum value
                int value = (int)enumName.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
                string text = field.Name;
                string description = string.Empty;
                object[] array = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (array.Length > 0)
                {
                    description = ((DescriptionAttribute)array[0]).Description;
                }
                else
                {
                    description = ""; //none description,set empty
                }
                //add to list


                if (isFirst)
                {
                    isFirst = false;
                }
                else
                { sb.Append(","); }


                sb.Append("{Name:'" + description + "'");
                sb.Append(",");
                sb.Append("Value:'" + value + "'");
                sb.Append("}");
            }
            sb.Append("]");

            return sb.ToString();
        }

        /// <summary>
        /// 检查类型名是否在当前类型中存在
        /// </summary>
        /// <param name="t"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsCodeInExistsType(System.Type t, string code)
        {
            SortedList list = new SortedList();
            Array a = Enum.GetValues(t);
            for (int i = 0; i < a.Length; i++)
            {
                string enumName = a.GetValue(i).ToString();
                if (enumName == code)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 加载枚举类型名称
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static DataTable GetEnumKeyName(System.Type t)
        {
            SortedList list = new SortedList();
            Array a = Enum.GetValues(t);
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add("text", System.Type.GetType("System.String"));
            dt.Columns.Add("value", System.Type.GetType("System.String"));

            for (int i = 0; i < a.Length; i++)
            {
                string enumName = a.GetValue(i).ToString();
                string enumKeyName = System.Enum.Parse(t, enumName).ToString();
                int enumKey = (int)System.Enum.Parse(t, enumName);
                string enumDescription = GetDescription(t, enumKey);
                dr = dt.NewRow();
                dr[0] = enumDescription;
                dr[1] = enumKeyName;
                //ddl.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(enumDescription,enumKeyName));
                dt.Rows.Add(dr);

            }
            return dt;
        }

        private static string GetName(System.Type t, object v)
        {
            try
            {
                return Enum.GetName(t, v);
            }
            catch
            {
                return "UNKNOWN";
            }
        }

        /// <summary>
        /// 返回指定枚举类型的指定值的描述
        /// </summary>
        /// <param name="t">枚举类型</param>
        /// <param name="v">枚举值</param>
        /// <returns></returns>
        public static string GetDescription(System.Type t, object v)
        {
            try
            {
                FieldInfo fi = t.GetField(GetName(t, v));
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return (attributes.Length > 0) ? attributes[0].Description : GetName(t, v);
            }
            catch
            {
                return "UNKNOWN";
            }
        }

        public static string _GetDescription(System.Type t, object v)
        {
            try
            {
                FieldInfo fi = t.GetField(GetName(t, v));
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return (attributes.Length > 0) ? attributes[0].Description : GetName(t, v);
            }
            catch
            {
                return "UNKNOWN";
            }
        }
        #endregion


        //二值状态
        //请登记使用者 user, role, category1/2/3, categoryattribute, manufacturer
        #region BiStatus 0, -1
        //===========================================
        public enum BiStatus : int
        {
            [Description("有效")]
            Valid = 0,
            [Description("无效")]
            InValid = -1,
            [Description("删除")]
            Delete = -2,

        }
        public static SortedList GetBiStatus()
        {
            return GetStatus(typeof(BiStatus));
        }
        public static string GetBiStatus(object v)
        {
            return GetDescription(typeof(BiStatus), v);
        }
        //--------------------------------------------
        #endregion

        #region 地区类型
        public enum AreaType : int
        {
            [Description("省")]
            Province = 0,
            [Description("市")]
            City = 1,
            [Description("区")]
            District = 2
        }
        public static SortedList GetAreaType()
        {
            return GetStatus(typeof(AreaType));
        }
        public static string GetAreaType(object v)
        {
            return GetDescription(typeof(AreaType), v);
        }
        #endregion

        #region Yes/No 1, 0
        //===========================================
        public enum YNStatus : int
        {
            [Description("否")]
            No = 0,
            [Description("是")]
            Yes = 1


        }

        public static SortedList GetYNStatus()
        {
            return GetStatus(typeof(YNStatus));
        }
        public static string GetYNStatus(object v)
        {
            return GetDescription(typeof(YNStatus), v);
        }
        //--------------------------------------------
        #endregion

        //权限表. 
        #region Privilege
        //===============================================
        public enum Privilege : int
        {
            #region IPP 权限
            //系统管理
            [Description("运营-全权用户")]
            IPP_Administrator = 100,
            [Description("运营-角色权限配置")]
            IPP_RolePrivilegeSet = 101,
            [Description("运营-角色菜单配置")]
            IPP_RoleMenuSet = 102,
            [Description("运营-用户角色配置")]
            IPP_UserRoleSet = 103,
            [Description("运营-添加修改用户")]
            IPP_UserOpt = 104,
            [Description("运营-查询用户")]
            IPP_UserSearch = 105,
            [Description("运营-查看用户权限")]
            IPP_UserPrivilegeSearch = 106,
            [Description("运营-添加修改角色")]
            IPP_RoleOpt = 107,
            [Description("运营-查询角色")]
            IPP_RoleSearch = 108,
            [Description("运营-查询权限")]
            IPP_PrivilegeSearch = 109,
            [Description("运营-添加修改菜单")]
            IPP_MenuOpt = 110,
            [Description("运营-查询菜单")]
            IPP_MenuSearch = 111,
            [Description("运营-添加修改API用户")]
            IPP_APIUserOpt = 112,
            [Description("运营-查询API用户")]
            IPP_APIUserSearch = 113,
            [Description("运营-添加修改API接口方法")]
            IPP_APIMethodOpt = 114,
            [Description("运营-查询API接口方法")]
            IPP_APIMethodSerach = 115,
            [Description("运营-查询日志")]
            IPP_LogSearch = 116,
            [Description("运营-添加/修改/删除配置")]
            IPP_ConfigOpt = 117,
            [Description("运营-查询配置")]
            IPP_ConfigSearch = 118,
            [Description("运营-添加修改区域")]
            IPP_AreaOpt = 119,
            [Description("运营-查询区域")]
            IPP_AreaSearch = 120,
            [Description("运营-添加修改行业类别")]
            IPP_VCategoryOpt = 121,
            [Description("运营-查询行业类别")]
            IPP_VCategorySearch = 122,
            [Description("运营-添加修改行业类别属性")]
            IPP_VCategoryPropertyOpt = 123,
            [Description("运营-查询行业类别属性")]
            IPP_VCategoryPropertySearch = 124,
            [Description("运营-添加修改商品类别")]
            IPP_PCategoryOpt = 125,
            [Description("运营-查询商品类别")]
            IPP_PCategorySearch = 126,
            [Description("运营-添加修改商品类别属性")]
            IPP_PCategoryPropertyOpt = 127,
            [Description("运营-查询商品类别属性")]
            IPP_PCategoryPropertySearch = 128,
            [Description("运营-三级类别属性模板配置")]
            IPP_PCategoryTemplateSet = 129,
            [Description("运营-一级机构-销售渠道配置")]
            IPP_OrganSaleChannelSet = 130,


            [Description("运营-查询商户")]
            IPP_VendorSearch = 131,
            [Description("运营-查询商品")]
            IPP_ProductSearch = 132,
            [Description("运营-查询商品渠道")]
            IPP_ProductChannelSearch = 133,
            [Description("运营-设置商品渠道")]
            IPP_ProductChannelSet = 134,
            [Description("运营-下架商品")]
            IPP_ProductOffSale = 135,
            [Description("运营-查询订单")]
            IPP_OrderSearch = 136,
            [Description("运营-添加修改机构")]
            IPP_OrganOpt = 137,
            [Description("运营-查询机构")]
            IPP_OrganSearch = 138,
            [Description("运营-添加修改销售渠道")]
            IPP_OrganSaleChannelOpt = 139,
            [Description("运营-查询销售渠道")]
            IPP_OrganSaleChannelSearch = 140,
            [Description("运营-API角色权限控制")]
            IPP_APIRoleSet = 141,
            [Description("运营-商户激活注销")]
            IPP_Vendor_Active = 142,
            [Description("运营-重置并发送密码")]
            IPP_ResetPwd = 143,
            [Description("运营-商户导出")]
            IPP_VendorExport = 144,
            [Description("运营-商品导出")]
            IPP_ProductExport = 145,
            [Description("运营-查询支付手续费率")]
            IPP_BasicFeeRateSearch = 148,
            [Description("运营-添加修改支付手续费率")]
            IPP_BasicFeeRateSave = 147,
            [Description("运营-查询API角色")]
            IPP_APIRoleSearch = 149,
            [Description("运营-添加修改API角色")]
            IPP_APIRoleOpt = 150,
            #endregion

            #region Organ 权限

            [Description("机构-全权用户")]
            Organ_Administrator = 1000,
            [Description("机构-角色权限配置")]
            Organ_RolePrivilegeSet = 1001,
            [Description("机构-角色菜单配置")]
            Organ_RoleMenuSet = 1002,
            [Description("机构-用户角色配置")]
            Organ_UserRoleSet = 1003,
            [Description("机构-添加修改用户")]
            Organ_UserOpt = 1004,
            [Description("机构-查询用户")]
            Organ_UserSearch = 1005,
            [Description("机构-查看用户权限")]
            Organ_UserPrivilegeSearch = 1006,
            [Description("机构-添加修改角色")]
            Organ_RoleOpt = 1007,
            [Description("机构-查询角色")]
            Organ_RoleSearch = 1008,
            [Description("机构-查询权限")]
            Organ_PrivilegeSearch = 1009,
            [Description("机构-查询商户")]
            Organ_VendorSearch = 1010,
            [Description("机构-添加修改商户")]
            Organ_VendorOpt = 1011,
            [Description("机构-创建商户初始用户")]
            Organ_VendorUserCreate = 1012,
            [Description("机构-设置商户商品类别")]
            Organ_VCategorySet = 1013,
            [Description("机构-设置商户相册视频")]
            Organ_VendorPhotoSet = 1014,
            [Description("机构-设置商户属性")]
            Organ_VendorPropertySet = 1015,
            [Description("机构-查询商品")]
            Organ_ProductSearch = 1016,
            [Description("机构-添加修改商品")]
            Organ_ProductOpt = 1017,
            [Description("机构-提报商品")]
            Organ_ProductReport = 1018,
            [Description("机构-审核商品")]
            Organ_ProductAudit = 1019,
            [Description("机构-经理审核商品")]
            Organ_ProductManagerAudit = 1020,
            [Description("机构-下架商品")]
            Organ_ProductOffSale = 1021,
            [Description("机构-查询订单")]
            Organ_OrderSearch = 1022,
            [Description("机构-取消订单")]
            Organ_OrderCancel = 1023,
            [Description("机构-取消订单并退款")]
            Organ_OrderRefund = 1024,
            [Description("机构-订单发货")]
            Organ_OrderOutStock = 1025,
            [Description("机构-团购提货")]
            Organ_OrderConsume_Tuan = 1026,
            [Description("机构-门票验证")]
            Organ_OrderConsume_Ticket = 1027,
            [Description("机构-查询退换货")]
            Organ_RMASearch = 1028,
            [Description("机构-添加修改退换货")]
            Organ_RMAOpt = 1029,
            [Description("机构-导出退换货申请")]
            Organ_RMAExport = 1030,
            [Description("机构-退换货单件处理(收货)")]
            Organ_RMAReceiveGoods = 1031,
            [Description("机构-退换货单件处理(退货/换货/拒绝)")]
            Organ_RMADeal = 1032,
            [Description("机构-退换货单件处理(发货/签收)")]
            Organ_RMAOutStock = 1033,
            [Description("机构-查询广告位配置")]
            Organ_AdvSearch = 1034,
            [Description("机构-添加修改广告位配置")]
            Organ_AdvOpt = 1035,
            [Description("机构-查询新闻公告")]
            Organ_NewsSearch = 1036,
            [Description("机构-添加修改新闻公告")]
            Organ_NewsOpt = 1037,
            [Description("机构-查询前台特定区域配置")]
            Organ_OnlineSearch = 1038,
            [Description("机构-添加修改前台特定区域配置")]
            Organ_OnlineOpt = 1039,
            [Description("机构-查询会员评论")]
            Organ_CustomerCommentSearch = 1040,
            [Description("机构-审核会员评论")]
            Organ_CustomerCommentOpt = 1041,
            [Description("机构-查询机构信息")]
            Organ_OrganSearch = 1042,
            [Description("机构-添加修改机构信息")]
            Organ_OrganOpt = 1043,
            [Description("机构-查询机构快递")]
            Organ_OrganExpressSearch = 1044,
            [Description("机构-添加修改机构快递")]
            Organ_OrganExpressOpt = 1045,
            [Description("机构-查询机构会员")]
            Organ_CustomerSearch = 1046,
            [Description("机构-添加机构会员")]
            Organ_CustomerOpt = 1047,
            [Description("机构-审核机构会员")]
            Organ_CustomerAudit = 1048,
            [Description("机构-重置并发送会员登录密码")]
            Organ_CustomerPwdSend = 1049,
            [Description("机构-查询网上支付记录")]
            Organ_NetPaySearch = 1050,
            [Description("机构-添加网上支付记录")]
            Organ_NewPayAdd = 1051,
            [Description("机构-审核/作废网上支付记录")]
            Organ_NewPayAduit = 1052,
            [Description("机构-查询财务收退款记录")]
            Organ_IncomeSearch = 1053,
            [Description("机构-审核财务收退款记录")]
            Organ_IncomeAudit = 1054,
            [Description("机构-查询财务收结算记录")]
            Organ_SettlementPromotionSearch = 1055,
            [Description("机构-导入/导出/审核财务结算记录")]
            Organ_SettlementPromotionOpt = 1056,
            [Description("机构-查询交易流水报表")]
            Organ_ReportOrderConsume = 1057,
            [Description("机构-查询交易明细报表")]
            Organ_ReportOrderDetail = 1058,
            [Description("机构-查询结算订单报表")]
            Organ_ReportOrderSettment = 1059,
            [Description("机构-查询按活动项目查询报表")]
            Organ_ReportOrderSearchByPromotion = 1060,
            [Description("机构-订单添加")]
            Organ_OrderAdd = 1061,
            [Description("机构-购物商品结算查询")]
            Organ_SettlementShoppingSearch = 1062,
            [Description("机构-商户激活注销")]
            Organ_Vendor_Active = 1063,
            [Description("机构-商户审核")]
            Organ_Vendor_Audit = 1064,
            [Description("机构二次开卡")]
            Organ_CreateCard = 1065,
            [Description("机构-重置并发送密码")]
            Organ_ResetPwd = 1066,
            [Description("机构-查询商户销售明细报表")]
            Organ_OrdersCount_Query = 1067,
            [Description("机构-导出商户销售明细报表")]
            Organ_OrdersCount_Query_Export = 1068,
            [Description("机构-查询商户统计报表")]
            Organ_VendorsCount_Query = 1069,
            [Description("机构-导出商户统计报表")]
            Organ_VendorsCount_Query_Export = 1070,
            [Description("机构-查询拓展结算报表")]
            Organ_SettlementCount_Query = 1071,
            [Description("机构-导出拓展结算报表")]
            Organ_SettlementCount_Query_Export = 1072,
            [Description("机构-查询商品订单结算报表")]
            Organ_GoodsSettleCount_Query = 1073,
            [Description("机构-导出商品订单结算报表")]
            Organ_GoodsSettleCount_Query_Export = 1074,
            [Description("机构-查询销售佣金报表")]
            Organ_CommissionCount_Query = 1075,
            [Description("机构-导出销售佣金报表")]
            Organ_CommissionCount_Query_Export = 1076,
            [Description("机构-导出财务收退款记录")]
            Organ_ExportIncomeSearch = 1077,
            [Description("机构-设定商品分组")]
            Organ_VendorGroup_Set = 1078,
            [Description("机构-查询商品分组")]
            Organ_VendorGroup_Query = 1079,


            [Description("机构-商户导出")]
            Organ_VendorExport = 1080,
            [Description("机构-约惠一清报表下载")]
            Organ_YueHuiReportDownload = 1081,
            [Description("机构-收款自动核对结果查询")]
            Organ_FinanceAutoConfirm = 1082,
            [Description("机构-收款自动核对结果下载")]
            Organ_FinanceAutoConfirmDownload = 1083,
            [Description("机构-活动类商品结算订单查询")]
            Organ_FinancePromotionOrderSearch = 1084,
            [Description("机构-活动类商品结算订单下载")]
            Organ_FinancePromotionOrderDownload = 1085,
            [Description("机构-商户统计查询")]
            Organ_MSVendorSearch = 1086,
            [Description("机构-商户统计下载")]
            Organ_MSVendorDownload = 1087,
            [Description("机构-渠道分类统计查询")]
            Organ_MSSaleChannelSearch = 1088,
            [Description("机构-渠道分类统计下载")]
            Organ_MSSaleChannelDownload = 1089,
            [Description("机构-行业分类统计查询")]
            Organ_MSCategorySearch = 1090,
            [Description("机构-行业分类统计下载")]
            Organ_MSCategoryDownload = 1091,
            [Description("机构-功能分类统计查询")]
            Organ_MSFuncTypeSearch = 1092,
            [Description("机构-功能分类统计下载")]
            Organ_MSFuncTypeDownload = 1093,
            [Description("机构-基本情况统计查询")]
            Organ_MSBaseInfoSearch = 1094,
            [Description("机构-基本情况统计下载")]
            Organ_MSBaseInfoDownload = 1095,
            [Description("机构-会员统计查询")]
            Organ_MSCustomerSearch = 1096,
            [Description("机构-会员统计下载")]
            Organ_MSCustomerDownload = 1097,
            [Description("机构-订单明细导出")]
            Organ_OrderMXExport = 1098,
            [Description("机构-删除商户")]
            Organ_Vendor_Delete = 1099,
            [Description("机构-商品排序设置")]
            Organ_ProductSortSet = 1100,
            [Description("机构-缴费查询")]
            Organ_PayOrderSearch = 1101,
            [Description("机构-订单强行短信")]
            Organ_OrderForceSMS = 1102,
            [Description("机构-公告管理")]
            Organ_AnnManage = 1103,
            [Description("机构-查询销售汇总报表")]
            Organ_OrdersSummary_Query = 1104,
            [Description("机构-导出销售汇总报表")]
            Organ_OrdersSummary_Query_Export = 1105,

            [Description("机构-查询活动过期订单结算报表")]
            Organ_PromotionOverdue_Query = 1106,
            [Description("机构-导出活动过期订单结算报表")]
            Organ_PromotionOverdue_Query_Export = 1107,
            [Description("机构-导出活动过期订单清算报表")]
            Organ_ExportPromotionOverdue = 1108,
            [Description("机构-添加修改抵用券")]
            Organ_CouponAdd = 1110,
            [Description("机构-消息推送管理")]
            Organ_PushManagement = 1109,
            [Description("机构-查询优惠券")]
            Organ_CouponQuery = 1111,
            [Description("机构-生成优惠券")]
            Organ_CouponGenerate = 1112,
            [Description("机构-优惠券明细查询")]
            Organ_CouponCodeDetailQuery = 1113,
            [Description("机构-批次明细查询")]
            Organ_CouponBatchDetailQuery = 1114,
            [Description("机构-导出生成优惠券数据")]
            Organ_CouponGenerateExport = 1115,
            [Description("机构-导出优惠券明细数据")]
            Organ_CouponCodeDetailQueryExport = 1116,
            [Description("机构-导出批次明细数据")]
            Organ_CouponBatchQueryExport = 1117,
            [Description("机构-优惠券作废")]
            Organ_CouponCodeInValid = 1118,
            [Description("机构-批次作废")]
            Organ_CouponBatchInValid = 1119,

            [Description("机构-查询订餐易订单结算报表")]
            Organ_OuterAppSettlement_Query = 1120,
            [Description("机构-导出订餐易订单结算报表")]
            Organ_OuterAppSettlement_Export = 1121,
            [Description("机构-导出订餐易订单清算报表")]
            Organ_OuterAppSettlement_Export_Summary = 1122,
            [Description("机构-清算订餐易订单")]
            Organ_OuterAppSettlement_Settle = 1123,
            [Description("机构-删除商品")]
            Organ_ProductDelete = 1124,
            [Description("机构-活动专区管理")]
            Organ_ActivityManagement = 1127,
            [Description("机构-修改库存")]
            Organ_UpdateProductInventory = 1125,
            [Description("机构-分配商户API角色")]
            Organ_APIRole = 1126,

            [Description("机构-手工对账")]
            Organ_CheckWithETS = 1128,
            [Description("机构-微信营销活动添加")]
            Organ_WeixinActivityAdd = 1129,

            [Description("机构-客户积分等级添加")]
            Organ_CustomerRankAdd = 1130,
            [Description("机构-积分活动添加")]
            Organ_PointActivityRuleAdd = 1131,
            [Description("机构-积分活动提交")]
            Organ_PointActivityRuleSubmit = 1132,
            [Description("机构-积分活动审核")]
            Organ_PointActivityRuleCommit = 1133,
            [Description("机构-积分活动审核不通过")]
            Organ_PointActivityRuleCommitCancel = 1134,
            [Description("机构-积分活动作废")]
            Organ_PointActivityRuleCancel = 1135,

            [Description("机构-积分基本规则添加")]
            Organ_PointCategoryRuleAdd = 1136,
            [Description("机构-积分基本规则提交")]
            Organ_PointCategoryRuleSubmit = 1137,
            [Description("机构-积分基本规则审核")]
            Organ_PointCategoryRuleCommit = 1138,
            [Description("机构-积分基本规则审核不通过")]
            Organ_PointCategoryRuleCommitCancel = 1139,
            [Description("机构-积分基本规则作废")]
            Organ_PointCategoryRuleCancel = 1140,
            [Description("机构-客户积分使用规则添加")]
            Organ_BasicRuleAdd = 1141,
            [Description("机构-积分活动查询")]
            Organ_PointActivityRuleSearch = 1142,
            [Description("机构-积分规则查询")]
            Organ_PointBasicRuleSearch = 1143,
            [Description("机构-会员查询")]
            Organ_UniteCustomerSearch = 1144,
            [Description("机构-会员导出")]
            Organ_UniteCustomerExport = 1145,
            [Description("机构-会员编辑")]
            Organ_UniteCustomerUpdate = 1146,
            [Description("机构-积分变更查询")]
            Organ_PointRequestSearch = 1147,
            [Description("机构-积分变更保存")]
            Organ_PointRequestSave = 1148,
            [Description("机构-积分变更审核")]
            Organ_PointRequestAudit = 1149,
            [Description("机构-积分明细查询")]
            Organ_PointSearch = 1150,
            [Description("机构-强制退款")]
            Organ_CompelRefund = 1151,
            [Description("机构-会员投票活动查询")]
            Organ_ActivityVoteSearch = 1152,
            [Description("机构-会员投票活动添加修改")]
            Organ_ActivityVoteEdit = 1153,
            [Description("机构-商品导出")]
            Organ_ProductExport = 1154,
            [Description("机构-积分变更作废")]
            Organ_PointRequestAbandon = 1155,
            [Description("机构-会员设置有效无效")]
            Organ_Customer_Active = 1156,

            [Description("机构-机构优惠券使用明细查询")]
            Organ_CouponDetailSearch = 1157,
            [Description("机构-机构优惠券使用明细导出")]
            Organ_CouponDetailExport = 1158,

            [Description("机构-导入/导出/审核任我游财务结算记录")]
            Organ_RWYSettlementPromotionOpt = 1159,
            [Description("机构-查询任我游财务收结算记录")]
            Organ_RWYSettlementPromotionSearch = 1160,
            [Description("机构-任我游商品结算订单查询")]
            Organ_RWYFinancePromotionOrderSearch = 1161,
            [Description("机构-任我游商品结算订单下载")]
            Organ_RWYFinancePromotionOrderDownload = 1162,

            [Description("机构-查询积分统计报表")]
            Organ_PointCountReportSearch = 1163,
            [Description("机构-导出积分统计报表")]
            Organ_PointCountReportExport = 1164,
            [Description("机构-查询积分抵值明细报表")]
            Organ_PointValueReportSearch = 1165,
            [Description("机构-导出积分抵值明细报表")]
            Organ_PointValueReportExport = 1166,

            [Description("机构-消费验证明细报表查询")]
            Organ_ConsumeDetailsSearch = 1167,
            [Description("机构-消费验证明细报表导出")]
            Organ_ConsumeDetailsExport = 1168,
            [Description("机构-商户排序设置")]
            Organ_VendorSortSet = 1169,

            [Description("机构-商户评论")]
            Organ_VendorCommentSearch = 1170,
            [Description("机构-商品评论")]
            Organ_ProductCommentSearch = 1171,



            [Description("机构-查询商户合作")]
            Organ_VendorCooperateSearch = 1173,
            [Description("机构-查询商户合作角色配置")]
            Organ_RoleCooperateAdd = 1174,

            [Description("机构-查询商户佣金分润设置")]
            Organ_CommissionSpilt_Query = 1175,
            [Description("机构-设置商户佣金分润")]
            Organ_CommissionSpilt_Set = 1176,

            [Description("机构-佣金对账报表查询")]
            Organ_Commission_Query = 1177,
            [Description("机构-佣金对账报表导出")]
            Organ_Commission_Export = 1178,
            [Description("机构-生活缴费查询")]
            Organ_LifesOrderSearch = 1179,
            [Description("机构-生活缴费导出")]
            Organ_LifesOrderExport = 1180,
            [Description("机构-生活缴费操作")]
            Organ_LifesOrderOpt = 1181,
            [Description("机构-手机充值查询")]
            Organ_MobileOrderSearch = 1182,
            [Description("机构-手机充值操作")]
            Organ_MobileOrderOpt = 1183,
            [Description("机构-手机充值导出")]
            Organ_MobileOrderExport = 1184,


            [Description("机构-秒杀保存")]
            Organ_Sales_CutDownAdd = 1185,//秒杀保存
            [Description("机构-秒杀提交")]
            Organ_Sales_CutDownSubMit = 1186,//秒杀提交
            [Description("机构-秒杀审核")]
            Organ_Sales_CutDownCommit = 1187,//秒杀审核
            [Description("机构-秒杀作废")]
            Organ_Sales_CutDownCancel = 1188,//秒杀作废
            [Description("机构-秒杀审核作废")]
            Organ_Sales_CutDownCommitCancel = 1189,//秒杀审核作废
            [Description("机构-秒杀查询")]
            Organ_Sales_CutDownSearch = 1190,//秒杀查询
            [Description("机构-购物商品添加")]
            Organ_GeneralProductAdd = 1191,//购物商品添加
            [Description("机构-属性模板新增修改查询")]
            Organ_VendorProductPropertyTemplate = 1192,////属性模板新增修改查询
            [Description("机构-热门搜索查询")]
            Organ_HotKeySearch = 1193,
            [Description("机构-热门搜索添加修改")]
            Organ_HotKeyOpt = 1194,

            [Description("机构-万能活动编辑")]
            Organ_ActivitiesEdit = 1195,
            [Description("机构-万能活动添加")]
            Organ_ActivitiesAdd = 1196,
            [Description("机构-万能活动查询")]
            Organ_ActivitiesSearch = 1197,
            [Description("机构-万能活动删除")]
            Organ_ActivitiesDelete = 1198,

            [Description("机构-万能活动模块编辑")]
            Organ_ActivitiesTemplateEdit = 1199,
            [Description("机构-万能活动模块添加")]
            Organ_ActivitiesTemplateAdd = 1200,
            [Description("机构-万能活动模块查询")]
            Organ_ActivitiesTemplateSearch = 1201,
            [Description("机构-万能活动模块删除")]
            Organ_ActivitiesTemplateDelete = 1202,

            [Description("机构-APP模块设置管理编辑")]
            Organ_App_TemplateEdit = 1203,
            [Description("机构-APP模块设置管理添加")]
            Organ_App_TemplateAdd = 1204,
            [Description("机构-APP模块设置管理查询")]
            Organ_App_TemplateSearch = 1205,

            [Description("机构-保存运费模版")]
            Organ_ExpressTemplate_Save = 1206,
            [Description("机构-删除运费模版")]
            Organ_ExpressTemplate_Delete = 1207,
            [Description("机构-查询运费模版")]
            Organ_ExpressTemplate_Search = 1208,
            [Description("机构-保存运费规则")]
            Organ_ExpressRule_Save = 1209,

            [Description("机构-查询商户自提")]
            Organ_PickUpAddressSearch = 1210,
            [Description("机构-添加修改商户自提")]
            Organ_PickUpAddressAdd = 1211,
            [Description("机构-删除商户自提")]
            Organ_PickUpAddressDelete = 1212,
            [Description("机构-商户自提禁用")]
            Organ_PickUpAddressInvalid = 1213,
            [Description("机构-商户自提启用")]
            Organ_PickUpAddressValid = 1214,

            [Description("机构-查看订单消费串码")]
            Organ_Order_Codes = 1215,
            [Description("机构-发送订单消费券码")]
            Organ_Order_SendCouponCodes = 1216,
            
            [Description("机构-第三方消费验证明细报表查询")]
            Organ_ThirdConsumeDetailsSearch = 1217,
            [Description("机构-第三方消费验证明细报表导出")]
            Organ_ThirdConsumeDetailsExport = 1218,

            [Description("合并支付订单查询")]
            Organ_MergeOrderSearch = 1219,
            #endregion


            #region Vendor 权限
            [Description("商户-全权用户")]
            Vendor_Administrator = 500,
            [Description("商户-查询商户平台用户")]
            Vendor_UserSearch = 501,
            [Description("商户-添加修改商户平台用户")]
            Vendor_UserAddForVendor = 502,
            [Description("商户-查询商户平台角色")]
            Vendor_RoleSearch = 503,
            [Description("商户-添加修改商户平台角色")]
            Vendor_RoleAddForVendor = 504,
            [Description("商户-用户角色配置")]
            Vendor_UserRoleSet = 505,
            [Description("商户-角色权限配置")]
            Vendor_RolePrivilegeSet = 506,
            [Description("商户-角色菜单配置")]
            Vendor_RoleMenuSet = 507,
            [Description("商户-查看商户用户权限")]
            Vendor_UserPrivilegeSearch = 508,
            [Description("商户-重置密码")]
            Vendor_UserResetPassword = 712,
            [Description("商户-查询商品")]
            Vendor_ProductSearch = 600,
            [Description("商户-添加修改商品")]
            Vendor_ProductOpt = 601,
            [Description("商户-提报商品")]
            Vendor_ProductReport = 602,
            [Description("商户-下架商品")]
            Vendor_ProductOffSale = 603,
            [Description("商户-编辑商品类别")]
            Vendor_ProductCategory_Edit = 604,

            [Description("商户-查询订单")]
            Vendor_Order_Query = 700,
            [Description("商户-取消订单")]
            Vendor_Order_Cancel = 701,
            [Description("商户-取消订单并退款")]
            Vendor_Order_CancelAndRefund = 702,
            [Description("商户-订单发货")]
            Vendor_Order_Delivery = 703,

            [Description("商户-团购提货")]
            Vendor_GroupBuy_TakeGoods = 704,
            [Description("商户-门票验证")]
            Vendor_Ticket_Validation = 705,

            [Description("商户-查询退换货")]
            Vendor_ReturnedGoods_Query = 706,
            [Description("商户-添加修改退换货")]
            Vendor_ReturnedGoods_Edit = 707,
            [Description("商户-导出退换货申请")]
            Vendor_ReturnedGoods_Apply_Export = 708,
            [Description("商户-退换货单件处理(收货)")]
            Vendor_ReturnedGoods_TakeGoods = 709,
            [Description("商户-退换货单件处理(退货/换货/拒绝)")]
            Vendor_ReturnedGoods_Reject = 710,
            [Description("商户-退换货单件处理(发货/签收)")]
            Vendor_ReturnedGoods_DeliveryOrTake = 711,
            [Description("商户-重置并发送密码")]
            Vendor_ResetPwd = 712,
            [Description("商户-查询销售明细统计")]
            Vendor_OrdersCount_Query = 713,
            [Description("商户-导出销售明细统计")]
            Vendor_OrdersCount_Query_Export = 714,
            [Description("商户-查询结算统计表")]
            Vendor_SettlementCount_Query = 715,
            [Description("商户-导出结算统计表")]
            Vendor_SettlementCount_Query_Export = 716,
            [Description("商户-设定商品分组")]
            Vendor_VendorGroup_Set = 717,
            [Description("商户-查询商品分组")]
            Vendor_VendorGroup_Query = 718,
            [Description("商户-订单明细导出")]
            Vendor_OrderMXExport = 719,
            [Description("商户-商品排序设置")]
            Vendor_ProductSortSet = 720,
            [Description("商户-查询财务收退款记录")]
            Vendor_Income_Query = 721,
            [Description("商户-导出财务收退款记录")]
            Vendor_Income_Query_Export = 722,
            [Description("商户-商品删除")]
            Vendor_ProductDelete = 723,
            [Description("商户-中信银行订单导出")]
            Vendor_ZXOrderExport = 729,
            [Description("商户-查询商户自提")]
            Vendor_VendorPickUpAddressSearch = 724,
            [Description("商户-添加修改商户自提")]
            Vendor_VendorPickUpAddressAdd = 725,
            [Description("商户-删除商户自提")]
            Vendor_VendorPickUpAddressDelete = 726,
            [Description("商户-商户自提禁用")]
            Vendor_VendorPickUpAddressInvalid = 727,
            [Description("商户-商户自提启用")]
            Vendor_VendorPickUpAddressValid = 728,
            [Description("商户-添加修改普通购物商品")]
            Vendor_ProductOptGeneral = 730,
            [Description("商户-添加修改活动类商品")]
            Vendor_ProductOptPromotion = 731,
            [Description("商户-查询运费模版")]
            Vendor_VendorExpressTemplateSearch = 736,
            [Description("商户-添加修改运费模版")]
            Vendor_VendorExpressTemplateAdd = 737,
            [Description("商户-删除运费模版")]
            Vendor_VendorExpressTemplateDelete = 738,
            [Description("商户-保存运费规则")]
            Vendor_VendorExpressRuleSave = 739,

            [Description("商户-微信营销活动添加")]
            Vendor_WeixinActivityAdd = 732,
            [Description("商户-微信营销活动历史查看")]
            Vendor_WeixinActivityList = 733,
            [Description("商户-店铺营销活动添加")]
            Vendor_ActivityAdd = 734,
            [Description("商户-店铺营销活动历史查看")]
            Vendor_ActivityList = 735,
            [Description("商户-查询销售汇总统计")]
            Vendor_OrdersSummary_Query = 740,
            [Description("商户-导出销售汇总统计")]
            Vendor_OrdersSummary_Query_Export = 741,
            [Description("商户-客服查询商品")]
            Vendor_CustomProudct_Query = 742,
            [Description("商户-客服查询订单")]
            Vendor_CustomOrder_Query = 743,
            [Description("商户-客服修改订单价格")]
            Vendor_CustomOrder_Update = 744,

            [Description("商户-查询电子会员卡汇总统计")]
            Vendor_ECardSummary_Query = 745,
            [Description("商户-导出电子会员卡汇总统计")]
            Vendor_ECardSummary_Query_Export = 746,
            [Description("商户-查询电子会员卡明细统计")]
            Vendor_ECardDetails_Query = 747,
            [Description("商户-导出电子会员卡明细统计")]
            Vendor_ECardDetails_Query_Export = 748,
            [Description("商户-商户商品属性模板")]
            Vendor_VendorProductPropertyTemplate = 749,

            [Description("商户-查询活动订单过期结算统计")]
            Vendor_PromotionOverdue_Query = 750,
            [Description("商户-导出活动订单过期结算统计")]
            Vendor_PromotionOverdue_Query_Export = 751,
            [Description("商户-商品属性添加修改")]
            Vendor_ProductProperty = 752,

            [Description("商户-查询微信关键词回复")]
            Vendor_WeiXinReply_Query = 753,
            [Description("商户-添加微信关键词回复")]
            Vendor_WeiXinReply_Add = 754,
            [Description("商户-删除微信关键词回复")]
            Vendor_WeiXinReply_Delete = 755,
            [Description("商户-添加修改首页模板")]
            Vendor_HomePageTemplate_Save = 757,

            [Description("商户-修改库存")]
            Vendor_UpdateProductInventory = 756,

            [Description("商户-修改商户信息")]
            Vendor_UpdateVendor = 758,

            [Description("商户-订单验证码重发")]
            Vendor_OrderForceSMS = 759,
            [Description("商户-查询验证终端门店统计")]
            Vendor_ConsumeVerifyBranch_Query = 760,
            [Description("商户-导出验证终端门店统计")]
            Vendor_ConsumeVerifyBranch_Export = 761,

            [Description("商户-查询消费验证明细报表")]
            Vendor_ConsumeDetails_Query = 762,
            [Description("商户-导出消费验证明细报表")]
            Vendor_ConsumeDetails_Export = 763,
            [Description("商户-商户微信用户绑定取消")]
            Vendor_WeiXinCancel = 764,

            [Description("商户-外部结算报表导出")]
            Vendor_OutAppSettlement_Export = 765,
            [Description("商户-外部结算报表查询")]
            Vendor_OutAppSettlement_Query = 766,
            

            #endregion
        }
        public static SortedList GetPrivilege()
        {
            return GetStatus(typeof(Privilege));
        }

        public static string GetPrivilege(object v)
        {
            return GetDescription(typeof(Privilege), v);
        }
        //------------------------------------------------------
        #endregion

        //操作日志名称
        #region Log Type
        //===========================================
        public enum LogType : int
        {
            //xx_xx_xx
            //模块_子模块_操作
            //------------------------------------------------------------------------------------sys 10
            #region IPP
            [Description("增加用户")]
            IPP_Sys_User_Add = 101010,
            [Description("更新用户")]
            IPP_Sys_User_Update = 101011,
            [Description("用户登录")]
            IPP_Sys_User_Login = 101012,
            [Description("增加角色")]
            IPP_Sys_Role_Add = 101110,
            [Description("更新角色")]
            IPP_Sys_Role_Update = 101111,
            [Description("增加用户角色关系")]
            IPP_Sys_UserRole_Add = 101210,
            [Description("删除用户角色关系")]
            IPP_Sys_UserRole_Delete = 101212,
            [Description("增加角色权限关系")]
            IPP_Sys_RolePrivilege_Add = 101310,
            [Description("删除角色权限关系")]
            IPP_Sys_RolePrivilege_Delete = 101312,
            [Description("添加系统配置")]
            IPP_Sys_Configuration_Add = 101313,
            [Description("修改系统配置")]
            IPP_Sys_Configuration_Update = 101314,
            [Description("删除系统配置")]
            IPP_Sys_Configuration_Del = 101315,
            [Description("用户密码修改")]
            IPP_Sys_UserPwd_Change = 101316,
            [Description("用户忘记密码")]
            IPP_Sys_UserPwd_Forget = 101317,
            [Description("商户平台用户密码修改")]
            IPP_Vendor_UserPwd_Change = 101318,
            [Description("商户平台用户忘记密码")]
            IPP_Vendor_UserPwd_Forget = 101319,
            [Description("增加用户")]
            IPP_Vendor_User_Add = 101320,
            [Description("更新用户")]
            IPP_Vendor_User_Update = 101321,
            [Description("用户登录")]
            IPP_Vendor_User_Login = 101322,
            [Description("添加一级机构-销售机构配置")]
            IPP_Sys_Organ_SaleOrgan_Add = 101323,
            [Description("修改一级机构-销售机构配置")]
            IPP_Sys_Organ_SaleOrgan_Update = 101324,

            [Description("增加会员")]
            IPP_Basic_Customer_Add = 113001,
            [Description("修改会员")]
            IPP_Basic_Customer_Update = 113002,
            [Description("发送会员密码")]
            IPP_Basic_Customer_SendPwd = 113003,

            [Description("增加地区")]
            IPP_Basic_Area_Add = 116010,
            [Description("更新地区")]
            IPP_Basic_Area_Update = 116011,
            [Description("增加类别")]
            IPP_Basic_Category_Add = 116012,
            [Description("更新类别")]
            IPP_Basic_Category_Update = 116013,
            [Description("增加类别-属性")]
            IPP_Basic_Category_Property_Add = 116014,
            [Description("更新类别-属性")]
            IPP_Basic_Category_Property_Update = 116015,
            [Description("增加属性")]
            IPP_Basic_Property_Add = 116016,
            [Description("更新属性")]
            IPP_Basic_Property_Update = 116017,
            [Description("增加属性值")]
            IPP_Basic_Property_Value_Add = 116018,
            [Description("更新属性值")]
            IPP_Basic_Property_Value_Update = 116019,
            [Description("增加商品类别")]
            IPP_Basic_PCategory_Add = 116020,
            [Description("更新商品类别")]
            IPP_Basic_PCategory_Update = 116021,
            [Description("增加商品属性")]
            IPP_Basic_PProperty_Add = 116022,
            [Description("更新商品属性")]
            IPP_Basic_PProperty_Update = 116023,
            [Description("增加商品属性值")]
            IPP_Basic_PProperty_Value_Add = 116024,
            [Description("更新商品属性值")]
            IPP_Basic_PProperty_Value_Update = 116025,

            [Description("插入前台展示区域产品")]
            IPP_Online_List_Insert = 117031,
            [Description("删除前台展示区域产品")]
            IPP_Online_List_Delete = 117032,
            [Description("添加/更新/删除新闻公告")]
            IPP_Online_News_Opt = 117033,
            [Description("添加/更新/删除小类产品推荐")]
            IPP_Online_CategoryRecommend_Opt = 117034,

            [Description("添加机构信息")]
            IPP_Organ_Add = 118011,
            [Description("更新机构信息")]
            IPP_Organ_Update = 118012,
            [Description("添加机构用户")]
            IPP_Organ_User_Add = 118013,
            [Description("修改机构用户")]
            IPP_Organ_User_Update = 118014,
            [Description("用户登录")]
            IPP_Organ_User_Login = 118015,
            [Description("机构用户忘记密码")]
            IPP_Organ_UserPwd_Forget = 118016,
            [Description("机构用户密码修改")]
            IPP_Organ_UserPwd_Change = 118017,
            [Description("修改渠道支付方式")]
            IPP_SaleChannel_Payment_Save = 118018,

            [Description("通过活动审核")]
            IPP_Product_AuditPass = 119014,
            [Description("下架活动")]
            IPP_Product_OffSale = 119016,
            [Description("下架活动但有效")]
            IPP_Product_OffSaleValid = 119017,


            [Description("添加万能活动")]
            IPP_Activities_Add = 120016,
            [Description("修改万能活动")]
            IPP_Activities_Update = 120017,
            [Description("添加万能活动模板")]
            IPP_ActivitiesTemplate_Add = 120018,
            [Description("修改万能活动模板")]
            IPP_ActivitiesTemplate_Update = 120019,

            [Description("机构-添加万能活动")]
            Organ_Activities_Add = 120020,
            [Description("机构-修改万能活动")]
            Organ_Activities_Update = 120021,
            [Description("机构-添加万能活动模板")]
            Organ_ActivitiesTemplate_Add = 120022,
            [Description("机构-修改万能活动模板")]
            Organ_ActivitiesTemplate_Update = 120023,


            [Description("投诉管理发送回复短信")]
            IPP_Complaint_SendCustomerMsg = 130001,
            [Description("投诉管理发送回复邮件")]
            IPP_Complaint_SendEmail = 130002,
            [Description("添加投诉")]
            IPP_Complaint_Add = 130003,


            [Description("添加菜单")]
            IPP_Sys_Menu_Add = 140001,
            [Description("修改菜单")]
            IPP_Sys_Menu_Update = 140002,
            [Description("一级机构-销售渠道添加")]
            IPP_Organ_SaleChannel_Add = 130005,
            [Description("一级机构-销售渠道修改")]
            IPP_Organ_SaleChannel_Update = 130006,
            [Description("一级机构-销售渠道修改")]
            IPP_Sys_RolePrivilege_Update = 130007,

            [Description("重置用户密码")]
            IPP_Reset_Password = 130008,
            [Description("激活商户")]
            IPP_Vendor_Active = 130009,
            [Description("注销商户")]
            IPP_Vendor_Deactive = 130010,

            [Description("添加支付手续费率")]
            IPP_BasicFeeRate_Add = 130011,
            [Description("修改支付手续费率")]
            IPP_BasicFeeRate_Update = 130012,

            #endregion

            #region Organ

            [Description("添加活动信息")]
            Organ_Product_Add = 219011,
            [Description("更新活动信息")]
            Organ_Product_Update = 219012,
            [Description("提报活动信息")]
            Organ_Product_Request = 219013,
            [Description("通过活动审核")]
            Organ_Product_AuditPass = 219014,
            [Description("退回活动审核")]
            Organ_Product_AuditReturn = 219015,
            [Description("下架活动")]
            Organ_Product_OffSale = 219016,
            [Description("下架活动但有效")]
            Organ_Product_OffSaleValid = 219017,
            [Description("添加活动主图片")]
            Organ_Product_Photo_Add = 219018,
            [Description("添加活动主图片(小图)")]
            Organ_Product_Photo_Small_Add = 219019,

            [Description("添加订单")]
            Organ_Order_Add = 221001,
            [Description("修改订单")]
            Organ_Order_Update = 221002,
            [Description("查询订单")]
            Organ_Order_Search = 221003,
            [Description("取消订单")]
            Organ_Order_Cancel = 221004,
            [Description("订单退款")]
            Organ_Order_Refund = 221005,
            [Description("订单消费")]
            Organ_Order_Consume = 221006,
            [Description("消费退款")]
            Organ_Order_Consume_Refund = 221007,
            [Description("订单发货")]
            Organ_Order_OutStock = 221008,
            [Description("发送订单密码短信")]
            Organ_Order_SendPwd = 221009,
            [Description("订单签收")]
            Organ_Order_Sign = 221010,
            [Description("订单退款商户审核")]
            Organ_Order_VendorVerify = 221011,

            [Description("添加网上支付记录")]
            Organ_NetPay_Add = 222001,
            [Description("审核网上支付记录")]
            Organ_NetPay_Verify = 222002,
            [Description("查询网上支付记录")]
            Organ_NetPay_Search = 222003,
            [Description("作废网上支付记录")]
            Organ_NetPay_Delete = 222004,
            [Description("活动财务确认单结算")]
            Organ_Income_Confirm = 222005,

            [Description("退换货签收")]
            Organ_RMADealRevice = 222006,
            [Description("退换货退货")]
            Organ_RMADealRefund = 222007,
            [Description("退换货换货")]
            Organ_RMADealChange = 222008,
            [Description("退换货发货")]
            Organ_RMADealOutStock = 222009,
            [Description("退换货新增")]
            Organ_RMA_Add = 222010,
            [Description("退换货强行签收")]
            Organ_RMA_Signin = 222011,
            [Description("购物财务确认单结算")]
            Organ_ShoppingIncome_Confirm = 222012,
            [Description("拒绝退换货")]
            Organ_RMA_DealRefuce = 222013,
            [Description("拒绝收货")]
            Organ_RMA_DealRefuceRevice = 222014,

            [Description("结算财务收退款")]
            Organ_Income_Settlement = 223002,
            [Description("作废财务收退款")]
            Organ_Income_Abandon = 223003,

            [Description("编辑商户微信信息")]
            Organ_Vendor_WeiXin_Edit = 224001,
            [Description("添加商户初始用户")]
            Organ_Vendor_User_Add = 224002,
            [Description("添加供应商信息")]
            Organ_Vendor_Add = 224003,
            [Description("更新供应商信息")]
            Organ_Vendor_Update = 224004,
            [Description("更新供应商视频文件链接")]
            Organ_Vendor_Video_Update = 224005,
            [Description("添加供应商相册图片")]
            Organ_Vendor_Photo_Add = 224006,
            [Description("移动供应商相册图片")]
            Organ_Vendor_Photo_Move = 224007,
            [Description("删除供应商相册图片")]
            Organ_Vendor_Photo_Delete = 224008,
            [Description("编辑商户活动业务信息")]
            Organ_Vendor_PromitionBiz_Edit = 224009,
            [Description("编辑商户结算信息")]
            Organ_Vendor_BalanceInfo_Edit = 224010,

            [Description("添加机构用户角色信息")]
            Organ_Sys_UserRole_Add = 224011,
            [Description("更新机构用户角色信息")]
            Organ_Sys_UserRole_Update = 224012,
            [Description("添加机构角色权限信息")]
            Organ_Sys_RolePrivilege_Add = 224013,
            [Description("添加机构用户信息")]
            Organ_Sys_User_Add = 224014,
            [Description("更新机构用户信息")]
            Organ_Sys_User_Update = 224015,
            [Description("添加机构角色信息")]
            Organ_Sys_Role_Add = 224016,
            [Description("更新机构角色信息")]
            Organ_Sys_Role_Update = 224017,
            [Description("分配机构角色菜单")]
            Organ_Sys_RoleMenu_Save = 224018,

            [Description("商户审核")]
            Organ_Vendor_Audit = 224019,
            [Description("激活商户")]
            Organ_Vendor_Active = 224020,
            [Description("注销商户")]
            Organ_Vendor_Deactive = 224021,
            [Description("编辑商户任我游信息")]
            Organ_Vendor_RWY_Edit = 224022,


            [Description("设置广告位信息")]
            Organ_Online_AdvOpt_Add = 225001,
            [Description("设置新闻公告信息")]
            Organ_Online_NewsOpt_Add = 225002,
            [Description("设置广告位信息信息")]
            Organ_Online_TemOpt_Add = 225003,
            [Description("添加商户属性")]
            Organ_Vendor_Property_Value_Add = 225011,
            [Description("修改商户属性")]
            Organ_Vendor_Property_Value_Update = 225012,
            [Description("删除广告位信息")]
            Organ_Online_AdvOpt_Delete = 225013,
            [Description("删除版块区商品信息")]
            Organ_Online_TemOpt_Delete = 225014,
            [Description("删除新闻公告信息")]
            Organ_Online_NewsOpt_Delete = 225015,

            [Description("机构平台-添加机构")]
            Organ_Organ_Add = 226001,
            [Description("机构平台-更新机构")]
            Organ_Organ_Update = 226002,
            [Description("添加机构快递信息")]
            Organ_Organ_Express_Add = 226003,
            [Description("更新机构快递信息")]
            Organ_Organ_Express_Update = 226004,
            [Description("更新机构商品类型")]
            Organ_Vendor_ProductCategory_Save = 226005,
            [Description("机构平台-添加机构会员")]
            Organ_OrganCustomer_Add = 226006,
            [Description("机构平台-更新机构会员")]
            Organ_OrganCustomer_Update = 226007,
            [Description("机构平台-修改密码")]
            Organ_UserPwd_Change = 226008,
            [Description("机构平台-添加商品分组")]
            Organ_VendorGroup_Add = 226009,
            [Description("机构平台-修改商品分组")]
            Organ_VendorGroup_Update = 226010,
            [Description("机构平台-删除商户")]
            Organ_Vendor_Delete = 226011,
            [Description("机构平台-添加抵用券")]
            Organ_Coupon_Add = 226012,
            [Description("机构平台-生成优惠券")]
            Organ_CouponBatchCode_Generate = 226013,
            [Description("机构平台-优惠券作废")]
            Organ_CouponCode_InValid = 226014,
            [Description("机构平台-批次作废")]
            Organ_CouponBatch_InValid = 226015,
            [Description("机构平台-更新抵用券")]
            Organ_Coupon_Edit = 226016,
            [Description("机构平台-客户等级修改")]
            Organ_CustomerRankAdd = 226017,
            [Description("机构平台-积分活动添加")]
            Organ_PointActivityAdd = 226018,
            [Description("机构平台-积分活动修改")]
            Organ_PointActivityEdit = 226019,
            [Description("机构平台-积分活动提交")]
            Organ_PointActivitySubmit = 226020,
            [Description("机构平台-积分活动审核")]
            Organ_PointActivityCommit = 226021,
            [Description("机构平台-积分活动审核不通过")]
            Organ_PointActivityCommitCancel = 226022,
            [Description("机构平台-积分活动作废")]
            Organ_PointActivityCancel = 226023,
            [Description("机构平台-积分基础规则添加/修改")]
            Organ_Point_BasicRuleAdd = 226024,

            [Description("机构平台-积分活动添加")]
            Organ_PointCategoryRuleAdd = 226025,
            [Description("机构平台-积分活动修改")]
            Organ_PointCategoryRuleEdit = 226026,
            [Description("机构平台-积分活动提交")]
            Organ_PointCategoryRuleSubmit = 226027,
            [Description("机构平台-积分活动审核")]
            Organ_PointCategoryRuleCommit = 226028,
            [Description("机构平台-积分活动审核不通过")]
            Organ_PointCategoryRuleCommitCancel = 226029,
            [Description("机构平台-积分活动作废")]
            Organ_PointCategoryRuleCancel = 226030,
            [Description("机构平台-积分变更保存")]
            Organ_PointRequestSave = 226031,
            [Description("机构平台-积分变更提交")]
            Organ_PointRequestConfirm = 226032,
            [Description("机构平台-积分变更审核通过")]
            Organ_PointLogAudit = 226033,
            [Description("机构平台-积分变更审核不通过")]
            Organ_PointLogNoAudit = 226034,
            [Description("机构平台-会员编辑")]
            Organ_UniteCustomerSave = 226035,
            [Description("机构平台-积分变更作废")]
            Organ_PointRequestAbandon = 226036,
            [Description("机构平台-会员设置为有效")]
            Organ_Customer_Valid = 226037,
            [Description("机构平台-会员设置为无效")]
            Organ_Customer_InValid = 226038,
            [Description("机构平台-热门搜索添加")]
            Organ_HotKey_Add = 226039,
            [Description("机构平台-热门搜索修改")]
            Organ_HotKey_Update = 226040,


            [Description("机构平台-保存运费模版")]
            Organ_ExpressTemplate_Save = 226041,
            [Description("机构平台-删除运费模版")]
            Organ_ExpressTemplate_Delete = 226042,
            [Description("机构平台-查询运费模版")]
            Organ_ExpressTemplate_Search = 226043,
            [Description("机构平台-保存运费规则")]
            Organ_ExpressRule_Save = 226044,

            [Description("机构平台-查询商户自提")]
            Organ_PickUpAddressSearch = 226045,
            [Description("机构平台-添加修改商户自提")]
            Organ_PickUpAddressAdd = 226046,
            [Description("机构平台-添加修改商户自提")]
            Organ_PickUpAddressUpdate = 226047,
            [Description("机构平台-删除商户自提")]
            Organ_PickUpAddressDelete = 226048,
            [Description("机构平台-商户自提禁用")]
            Organ_PickUpAddressInvalid = 226049,
            [Description("机构平台-商户自提启用")]
            Organ_PickUpAddressValid = 226050,


            [Description("机构平台-APP模块设置管理编辑")]
            Organ_App_TemplateEdit = 226051,
            [Description("机构平台-APP模块设置管理添加")]
            Organ_App_TemplateAdd = 226052,
            #endregion

            #region Vendor
            [Description("添加活动信息")]
            Vendor_Product_Add = 319001,
            [Description("更新活动信息")]
            Vendor_Product_Update = 319002,
            [Description("提报活动信息")]
            Vendor_Product_Request = 319003,
            [Description("通过活动审核")]
            Vendor_Product_AuditPass = 319004,
            [Description("退回活动审核")]
            Vendor_Product_AuditReturn = 319005,
            [Description("下架活动")]
            Vendor_Product_OffSale = 319006,
            [Description("下架活动但有效")]
            Vendor_Product_OffSaleValid = 319007,
            [Description("添加活动主图片")]
            Vendor_Product_Photo_Add = 319008,
            [Description("添加活动主图片")]
            Vendor_Product_Photo_Small_Add = 319019,
            [Description("添加商户用户")]
            Vendor_User_Add = 319020,
            [Description("更新商户用户")]
            Vendor_User_Update = 319021,
            [Description("添加商户角色")]
            Vendor_Role_Add = 319022,
            [Description("更新商户角色")]
            Vendor_Role_Update = 319023,
            [Description("设置商户用户角色")]
            Vendor_UserRole_Set = 319024,
            [Description("分配商户角色权限")]
            Vendor_RolePrivilege_Set = 319025,
            [Description("分配商户角色菜单")]
            Vendor_RoleMenu_Set = 319026,
            [Description("重置密码")]
            Vendor_Password_Reset = 319027,

            [Description("退换货签收")]
            Vendor_RMADealRevice = 322006,
            [Description("退换货退货")]
            Vendor_RMADealRefund = 322007,
            [Description("退换货换货")]
            Vendor_RMADealChange = 322008,
            [Description("退换货发货")]
            Vendor_RMADealOutStock = 322009,
            [Description("退换货新增")]
            Vendor_RMA_Add = 322010,
            [Description("退换货强行签收")]
            Vendor_RMA_Signin = 322011,
            [Description("修改订单")]
            Vendor_Order_Update = 321002,
            [Description("查询订单")]
            Vendor_Order_Search = 321003,
            [Description("取消订单")]
            Vendor_Order_Cancel = 321004,
            [Description("订单退款")]
            Vendor_Order_Refund = 321005,
            [Description("订单消费")]
            Vendor_Order_Consume = 321006,
            [Description("消费退款")]
            Vendor_Order_Consume_Refund = 321007,
            [Description("订单发货")]
            Vendor_Order_OutStock = 321008,
            [Description("发送订单密码短信")]
            Vendor_Order_SendPwd = 321009,
            [Description("订单签收")]
            Vendor_Order_Sign = 321010,
            [Description("拒绝退换货")]
            Vendor_RMA_DealRefuce = 322013,
            [Description("拒绝收货")]
            Vendor_RMA_DealRefuceRevice = 322014,

            [Description("商户平台-修改密码")]
            Vendor_UserPwd_Change = 322015,

            [Description("商户平台-修改商品分组")]
            Vendor_VendorGroup_Update = 322016,
            [Description("商户平台-添加商品分组")]
            Vendor_VendorGroup_Add = 322017,
            [Description("添加商户自提")]
            Vendor_PickUpAddress_Add = 322018,
            [Description("更新商户自提")]
            Vendor_PickUpAddress_Update = 322019,
            [Description("保存运费模版")]
            Vendor_ExpressTemplate_Save = 322020,
            [Description("删除运费模版")]
            Vendor_ExpressTemplate_Delete = 322021,
            [Description("保存运费规则")]
            Vendor_ExpressRule_Save = 322022,
            [Description("微信营销活动添加")]
            Vendor_Activity_Add = 322023,
            [Description("微信营销活动修改")]
            Vendor_Activity_Update = 322024,
            [Description("微信营销活动归档")]
            Vendor_Activity_Record = 322025,
            [Description("微客服修改订单金额")]
            Vendor_Custom_OrderAmt_Update = 322026,
            [Description("微信营销活动添加")]
            Vendor_WeixinActivity_Add = 322027,
            [Description("微信营销活动修改")]
            Vendor_WeixinActivity_Update = 322028,
            [Description("微信营销活动归档")]
            Vendor_WeixinActivity_Record = 322029,

            [Description("删除微信关键词回复")]
            Vendor_WeixinReply_Delete = 322030,
            [Description("添加微信关键词回复")]
            Vendor_WeixinReply_Add = 322031,
            [Description("添加修改首页模板")]
            Vendor_HomePageTemplage_Add = 322032,
            [Description("更新微信活动")]
            Organ_WeixinActivity_Update = 322033,
            [Description("微信营销活动添加")]
            Organ_WeixinActivity_Add = 322034,
            [Description("商户后台修改商户信息")]
            Vendor_Vendor_Update = 322035,

            [Description("订单退款商户审核")]
            Vendor_Order_VendorVerify = 322036,
            #endregion

        }
        #endregion
        public static SortedList GetLogType()
        {
            return GetStatus(typeof(LogType));
        }
        public static string GetLogType(object v)
        {
            return GetDescription(typeof(LogType), v);
        }
        //--------------------------------------------


        //积分
        #region 积分原因种类
        //===========================================
        public enum PointLogType : int
        {

            [Description("购物获得积分")]
            GetShopping = 1,

            [Description("评论获得积分")]
            GetComment = 2,

            [Description("活动获得积分")]
            GetPromotion = 3,

            [Description("人工调整积分")]
            GetManual = 5,

            [Description("积分消费")]
            PayShopping = 6,

            [Description("退还奖励的积分")]
            PayReturnAward = 7,

            [Description("退还消费的积分")]
            PayReturnShopping = 8,
        }
        public static SortedList GetPointLogType()
        {
            return GetStatus(typeof(PointLogType));
        }
        public static string GetPointLogType(object v)
        {
            return GetDescription(typeof(PointLogType), v);
        }
        //--------------------------------------------
        #endregion

        #region 积分日志状态
        //===========================================
        public enum PointLogStatus : int
        {

            [Description("有效")]
            Valid = 0,

            [Description("已冻结")]
            Frozen = 1,

            [Description("已退还")]
            Returned = 2,
            [Description("已作废")]
            Invalid = 3,

        }
        public static SortedList GetPointLogStatus()
        {
            return GetStatus(typeof(PointLogStatus));
        }
        public static string GetPointLogStatus(object v)
        {
            return GetDescription(typeof(PointLogStatus), v);
        }
        //--------------------------------------------
        #endregion


        //三值状态
        //请登记使用者
        #region TriStatus point, email, sms，RMA退货入库入其他产品时的审核状态。
        //===========================================
        public enum TriStatus : int
        {
            [Description("Abandon")]
            Abandon = -1,
            [Description("Origin")]
            Origin = 0,
            [Description("Handled")]
            Handled = 1

        }
        public static SortedList GetTriStatus()
        {
            return GetStatus(typeof(TriStatus));
        }
        public static string GetTriStatus(object v)
        {
            return GetDescription(typeof(TriStatus), v);
        }
        //--------------------------------------------
        #endregion

        #region 客户性别 0, 1
        //===========================================
        public enum Gendor : int
        {
            [Description("男")]
            Male = 1,
            [Description("女")]
            Female = 0

        }
        public static SortedList GetGendor()
        {
            return GetStatus(typeof(Gendor));
        }
        public static string GetGendor(object v)
        {
            return GetDescription(typeof(Gendor), v);
        }
        //--------------------------------------------
        #endregion


        //供应商类型
        #region Vendor Type
        //===============================================
        public enum VendorType : int
        {

            [Description("生产商")]
            Manufacturer = 0,
            [Description("经销商")]
            Distributor = 1,
            [Description("代理")]
            Agent = 2,
            [Description("零售商")]
            Retailer = 3,
            [Description("快递团")]
            Express = 4,
            [Description("服务团")]
            Service = 5,
            [Description("其他")]
            Other = 6

        }
        public static SortedList GetVendorType()
        {
            return GetStatus(typeof(VendorType));
        }

        public static string GetVendorType(object v)
        {
            return GetDescription(typeof(VendorType), v);
        }
        //------------------------------------------------------
        #endregion

        #region OnlineListArea
        //===============================================
        public enum OnlineListArea : int
        {

            [Description("首页正在团购")]
            HomePage_TuanGou = 1,
            [Description("首页热门约惠")]
            HomePage_HotHui = 2,
            [Description("首页今日秒杀")]
            HomePage_MiaoSha = 3,
            [Description("首页美食餐饮知名商家")]
            HomePage_Floor1Vendor = 4,
            [Description("首页美食餐饮人气排行")]
            HomePage_Floor1Rank = 5,
            [Description("F1首页美食餐饮产品展示")]
            HomePage_Floor1Product = 6,

            [Description("首页丽人知名商家")]
            HomePage_Floor2Vendor = 7,
            [Description("首页丽人人气排行")]
            HomePage_Floor2Rank = 8,
            [Description("F2首页丽人产品展示")]
            HomePage_Floor2Product = 9,

            [Description("首页文化娱乐知名商家")]
            HomePage_Floor3Vendor = 10,
            [Description("首页文化娱乐人气排行")]
            HomePage_Floor3Rank = 11,
            [Description("F3首页文化娱乐产品展示")]
            HomePage_Floor3Product = 12,

            [Description("首页运动健康知名商家")]
            HomePage_Floor4Vendor = 13,
            [Description("首页运动健康人气排行")]
            HomePage_Floor4Rank = 14,
            [Description("F4首页运动健康产品展示")]
            HomePage_Floor4Product = 15,

            [Description("首页教育培训知名商家")]
            HomePage_Floor5Vendor = 16,
            [Description("首页教育培训人气排行")]
            HomePage_Floor5Rank = 17,
            [Description("F5首页教育培训产品展示")]
            HomePage_Floor5Product = 18,

            [Description("分类页热门推荐")]
            CategoryProductPage_HotProduct = 19,

            [Description("新闻页热门推荐")]
            NewsPage_HotProduct = 20,
            [Description("新闻页知名商家")]
            NewsPage_HotVendor = 21,
            [Description("商品详情页推荐")]
            ProductDetail_Product = 22,
            [Description("APP首页身边")]
            APPIndex_Product = 23,
        }
        public static SortedList GetOnlineListArea()
        {
            return GetStatus(typeof(OnlineListArea));
        }

        public static string GetOnlineListArea(object v)
        {
            return GetDescription(typeof(OnlineListArea), v);
        }
        //------------------------------------------------------
        #endregion

        #region 新闻公告类型
        public enum NewsType : int
        {
            [Description("活动")]
            Activity = 1,
            [Description("预告")]
            PreActivity = 2,
            [Description("公告")]
            Annountce = 3,
            [Description("通知")]
            Notice = 4,
            [Description("帮助中心")]
            Help = 5,

        }
        public static SortedList GetNewsType()
        {
            return GetStatus(typeof(NewsType));
        }
        public static string GetNewsType(object v)
        {
            return GetDescription(typeof(NewsType), v);
        }
        #endregion

        #region 文件存储类型
        public enum AdvSaveType : int
        {
            [Description("Html文件存储")]
            FileSave = 1,
            [Description("图片存储")]
            DatabaseSave = 2,
        }
        public static SortedList GetAdsSaveType()
        {
            return GetStatus(typeof(AdvSaveType));
        }
        public static string GetAdsSaveType(object v)
        {
            return GetDescription(typeof(AdvSaveType), v);
        }
        #endregion

        #region 是否链接
        public enum IsOutLink
        {
            [Description("否")]
            IsNotLink = 0,
            [Description("是")]
            IsOutLink = 1,
        }
        #endregion

        #region 网页上显示的信息类型(严重级别)

        /// <summary>
        /// 网页上显示的信息类型(严重级别)
        /// </summary>
        public enum ErrorMessageLevel
        {
            [Description("信息")]
            Information,
            [Description("警告")]
            Warning,
            [Description("错误")]
            Error,
        }
        #endregion

        /// <summary>
        /// Topic 的状态
        /// </summary>
        /// 
        public enum CommentStatus
        {
            [Description("普通")]
            Normal = 0,
            [Description("已回复")]
            Replyed = 1,
            [Description("已作废")]
            Abandon = 2,
        }



        #region 密码验证类型(商城和O2O的密码加密方式不一致0为商城方式，1为O2O方式)
        /// <summary>
        /// ValidateType  的状态  密码验证类型
        /// </summary>
        /// 
        public enum ValidateType : int
        {
            [Description("商城方式")]
            Business = 0,
            [Description("O2O方式")]
            O2O = 1,

        }
        #endregion




        /// <summary>
        /// Topic Reply 的状态
        /// </summary>
        public enum CommentReplyStatus
        {
            [Description("普通")]
            Normal = 0,
            [Description("已作废")]
            Abandon = 1,
        }

        /// <summary>
        /// Topic 类型
        /// </summary>
        public enum CommentType
        {
            [Description("签到点评")]
            NotBuyComment = 1,
            [Description("消费点评")]
            BuyComment = 2,
        }

        public static SortedList GetCommentType()
        {
            return GetStatus(typeof(CommentType));
        }
        public static string GetCommentType(object v)
        {
            return GetDescription(typeof(CommentType), v);
        }

        public enum CommentUpdateType
        {
            [Description("屏蔽评论")]
            AbandonTopic,
            [Description("撤销屏蔽评论")]
            CancelAbandonTopic,
            [Description("置顶评论")]
            TopicSetTop,
            [Description("设置精华")]
            TopicSetDigset,
            [Description("取消置顶")]
            TopicCancelTop,
            [Description("取消精华")]
            TopicCancelDigset,
            [Description("精华显示")]
            DigsetShow,
            [Description("精华屏蔽")]
            DigsetHid,
            [Description("已处理/无需回复")]
            HasReviewed,
            [Description("未阅读处理")]
            NotHasReviewed,
        }

        /// <summary>
        /// Property Web Display Style Type
        /// </summary>
        public enum PropertyWebDisplayStyle
        {
            [Description("Property")]
            Property = 0,
            [Description("Value")]
            Value = 1,
            [Description("Property + Value")]
            PropertyValue = 2,
            [Description("Value + Property")]
            ValueProperty = 3,
        }

        #region 财务销售收款单 状态
        public enum IncomeStatus : int
        {
            [Description("人工作废")]
            AbandonManually = -2,
            [Description("系统作废")]
            Abandon = -1,
            [Description("原始")]
            Origin = 0,
            [Description("确认")]
            Confirmed = 1,
            [Description("结算")]
            Settlement = 2
        }
        public static SortedList GetIncomeStatus()
        {
            return GetStatus(typeof(IncomeStatus));
        }
        public static string GetSOIncomeStatus(object v)
        {
            return GetDescription(typeof(IncomeStatus), v);
        }

        #endregion

        #region Complaint 投诉来源。评论0、邮箱1、电话2、传真3、媒体4、银行5
        public enum ComplaintSourceType : int
        {
            [Description("评论")]
            Comments = 1,
            [Description("邮箱")]
            Mailbox = 2,
            [Description("电话")]
            Phone = 3,
            [Description("传真")]
            Fax = 4,
            [Description("媒体")]
            Media = 5,
            [Description("银行")]
            Bank = 6,
            [Description("在线客服")]
            Online = 7,
        }
        public static SortedList GetComplaintSourceType()
        {
            return GetStatus(typeof(ComplaintSourceType));
        }
        public static string GetComplaintSourceType(object v)
        {
            return GetDescription(typeof(ComplaintSourceType), v);
        }
        #endregion

        #region Complaint_Master 投诉类别。投诉产品0、投诉物流配送1、投诉售后2、投诉客户服务3、投诉供应商4、投诉商城5、投诉其他6
        public enum ComplaintType : int
        {
            [Description("产品相关")]
            Product = 1,
            [Description("订单发货与配送")]
            Logistics = 2,
            [Description("订购相关问题")]
            Aftermarket = 3,
            [Description("退换货问题")]
            Service = 4,
            [Description("取消或者修改订单")]
            Supplier = 5,
            [Description("投诉与建议")]
            Mall = 6,
            [Description("投诉电话客服")]
            Other = 7,
            [Description("其它问题")]
            Others = 8,
        }
        public static SortedList GetComplaintType()
        {
            return GetStatus(typeof(ComplaintType));
        }
        public static string GetComplaintType(object v)
        {
            return GetDescription(typeof(ComplaintType), v);
        }
        #endregion

        #region Complaint_Master 投诉状态。已作废-1、待处理0、处理中1、处理完毕2
        public enum ComplaintStatus : int
        {
            [Description("已作废")]
            Cancel = -1,
            [Description("待处理")]
            Waiting = 0,
            [Description("处理中")]
            Dealing = 1,
            [Description("处理完毕")]
            Effect = 2,
        }
        public static SortedList GetComplaintStatus()
        {
            return GetStatus(typeof(ComplaintStatus));
        }
        public static string GetComplaintStatus(object v)
        {
            return GetDescription(typeof(ComplaintStatus), v);
        }
        #endregion

        #region Complaint_Master 投诉类别(严重状态)。未知0、一般1、严重2
        public enum ComplaintLevel : int
        {
            [Description("未知")]
            Unknown = 0,
            [Description("一般")]
            General = 1,
            [Description("严重")]
            Serious = 2,
        }
        public static SortedList GetComplaintLevel()
        {
            return GetStatus(typeof(ComplaintLevel));
        }
        public static string GetComplaintLevel(object v)
        {
            return GetDescription(typeof(ComplaintLevel), v);
        }
        #endregion

        #region Complaint_Master 责任归属部门。运营1、技术2、招商3、供应商4
        public enum ComplaintResponsibleDept : int
        {
            [Description("运营")]
            Operation = 1,
            [Description("技术")]
            Technology = 2,
            [Description("招商")]
            Merchant = 3,
            [Description("供应商")]
            Supplier = 4,
        }
        public static SortedList GetComplaintResponsibleDept()
        {
            return GetStatus(typeof(ComplaintResponsibleDept));
        }
        public static string GetComplaintResponsibleDept(object v)
        {
            return GetDescription(typeof(ComplaintResponsibleDept), v);
        }
        #endregion

        #region Complaint_Master 投诉回复方式。电话1、邮件2
        public enum ComplaintReplySourceType : int
        {
            [Description("电话")]
            Phone = 1,
            [Description("邮件")]
            Email = 2,
        }
        public static SortedList GetComplaintReplySourceType()
        {
            return GetStatus(typeof(ComplaintReplySourceType));
        }
        public static string GetComplaintReplySourceType(object v)
        {
            return GetDescription(typeof(ComplaintReplySourceType), v);
        }
        #endregion

        #region 销售单取消原因
        public enum SOCancelType : int
        {

        }
        public static SortedList GetSOCancelType()
        {
            return GetStatus(typeof(SOCancelType));
        }
        public static string GetSOCancelType(object v)
        {
            return GetDescription(typeof(SOCancelType), v);
        }
        #endregion

        #region CookieName
        public enum CookieName : byte
        {
            [Description("xykscCookie")]
            XYKSC,
            [Description("GiftCartCookie")]
            GiftCartCookie,
            [Description("InvoiceNo")]
            InvoiceNo,
            [Description("YTPackageNo")]
            YTPackageNo,
            [Description("STPackageNo")]
            STPackageNo,
            [Description("EMSPackageNo")]
            EMSPackageNo,
            [Description("SFPackageNo")]
            SFPackageNo
        }
        public static SortedList GetCookieName()
        {
            return GetStatus(typeof(CookieName));
        }
        public static string GetCookieName(object v)
        {
            return GetDescription(typeof(CookieName), v);
        }
        #endregion

        #region 万能活动模板的活动类型 1 万能活动，2 品牌馆
        public enum ActiveType : int
        {
            [Description("万能活动")]
            Activities = 1,
            [Description("品牌馆")]
            Brand = 2,
        }
        public static SortedList GetActiveType()
        {
            return GetStatus(typeof(ActiveType));
        }
        public static string GetActiveType(object v)
        {
            return GetDescription(typeof(ActiveType), v);
        }
        #endregion

        #region 广告位
        public enum AdvArea
        {
            [Description("首页首屏顶部")]
            indexTopAdv = 1,
            [Description("首页首屏中部")]
            indexMidAdv = 2,
        }
        public static SortedList GetAdvArea()
        {
            return GetStatus(typeof(AdvArea));
        }
        public static string GetAdvArea(object v)
        {
            return GetDescription(typeof(AdvArea), v);
        }
        #endregion

        #region 区域配置类型
        public enum OnLineAreaType
        {
            [Description("商户")]
            Vendor = 1,
            [Description("商品")]
            Product = 2,
        }
        public static SortedList GetOnLineAreaType()
        {
            return GetStatus(typeof(OnLineAreaType));
        }
        public static string GetOnLineAreaType(object v)
        {
            return GetDescription(typeof(OnLineAreaType), v);
        }
        #endregion

        #region 用户类型 1运营平台用户，2机构用户, 3商户平台用户, 4注册会员
        public enum UserType : int
        {
            [Description("运营平台用户")]
            Operator = 1,
            [Description("机构用户")]
            Organ = 2,
            [Description("商户平台用户")]
            Vendor = 3,
            [Description("注册会员")]
            Customer = 4,
        }
        #endregion

        #region 角色类型 1运营平台角色,2机构平台角色，3商户平台角色
        public enum RoleType : int
        {
            [Description("运营平台角色")]
            Operator = 1,
            [Description("机构平台角色")]
            Organ = 2,
            [Description("商户平台角色")]
            Vendor = 3,
        }
        #endregion

        #region 角色类型 1机构平台角色，2商户平台角色
        public enum APIRoleType : int
        {
            [Description("机构平台")]
            Organ = 1,
            [Description("商户平台")]
            Vendor = 2,
        }
        #endregion

        #region 权限类型 1运营，2机构 3商户
        public enum PrivilegeType : int
        {
            [Description("运营平台")]
            Operator = 1,
            [Description("机构平台")]
            Organ = 2,
            [Description("商户平台")]
            Vendor = 3,
        }
        #endregion

        #region 星级 1一星，2二星 3三星 4四星 5五星
        public enum StarLevel : int
        {
            [Description("一星")]
            One = 1,
            [Description("二星")]
            Two = 2,
            [Description("三星")]
            Three = 3,
            [Description("四星")]
            Four = 4,
            [Description("五星")]
            Five = 5,
        }
        #endregion

        #region 商户层级 1一级，2二级 3三级
        public enum VendorLevel : int
        {
            [Description("一级")]
            One = 1,
            [Description("二级")]
            Two = 2,
            [Description("三级")]
            Three = 3,
            [Description("四级")]
            Four = 4,
            [Description("五级")]
            Five = 5,
        }
        #endregion

        #region 保存数据的类型 1添加，2更新
        public enum SaveDataType : int
        {
            [Description("添加")]
            Add = 1,
            [Description("更新")]
            Update = 2,
        }
        #endregion

        #region 机构层级 1一级，2二级 3三级 4四级 5五级
        public enum OrganLevel : int
        {
            [Description("一级")]
            One = 1,
            [Description("二级")]
            Two = 2,
            [Description("三级")]
            Three = 3,
            [Description("四级")]
            Four = 4,
            [Description("五级")]
            Five = 5,
        }
        #endregion

        #region 机构类型 1销售机构 2拓展机构 3销售+拓展机构
        public enum OrganType : int
        {
            [Description("销售")]
            Sale = 1,
            [Description("拓展")]
            Expand = 2,
            [Description("销售+拓展")]
            SaleAndExpand = 3,
        }
        #endregion

        #region 商户文件类型 1图片 2视频
        public enum VendorFileType : int
        {
            [Description("图片")]
            Photo = 1,
            [Description("视频")]
            Video = 2,
        }
        #endregion

        #region 会员来源 1平台注册 2微博 3QQ 4支付宝 5其他
        public enum CustomerSourceType : int
        {
            [Description("平台")]
            Platform = 1,
            [Description("微博")]
            MicroBlog = 2,
            [Description("QQ")]
            Tencent = 3,
            [Description("支付宝")]
            Alipay = 4,
            [Description("其他")]
            Other = 5,
        }
        #endregion

        #region 会员等级 1普通会员 2青铜会员 3白银会员 4黄金会员
        public enum CustomerRankType : int
        {
            [Description("普通会员")]
            Ordinary = 1,
            [Description("青铜会员")]
            Bronze = 2,
            [Description("白银会员")]
            Silver = 3,
            [Description("黄金会员")]
            Gold = 4,
        }

        public static SortedList GetCustomerRankType()
        {
            return GetStatus(typeof(CustomerRankType));
        }
        public static string GetCustomerRankType(object v)
        {
            return GetDescription(typeof(CustomerRankType), v);
        }
        #endregion

        #region 会员等级所需消费值 1普通会员 2青铜会员 3白银会员 4黄金会员
        public enum CustomerRankConsume : int
        {
            [Description("0")]
            Ordinary = 1,
            [Description("800")]
            Bronze = 2,
            [Description("3000")]
            Silver = 3,
            [Description("10000")]
            Gold = 4,
        }

        public static SortedList GetCustomerRankConsume()
        {
            return GetStatus(typeof(CustomerRankConsume));
        }
        public static string GetCustomerRankConsume(object v)
        {
            return GetDescription(typeof(CustomerRankConsume), v);
        }
        #endregion

        #region 短信状态 0待发送 1已发送 -1发送失败
        public enum SmsStatus : int
        {
            [Description("待发送")]
            WaitSend = 0,
            [Description("已发送")]
            Success = 1,
            [Description("发送失败")]
            Fail = -1,
        }
        #endregion


        #region 活动状态 1未提报 2待审核 3已上架 4下架有效 5已下架 -1审核未通过
        public enum ProductStatus : int
        {
            [Description("未提报")]
            NoRequest = 1,
            [Description("待平台审核")]
            WaitPlatAudit = 7,
            [Description("待审核")]
            WaitAudit = 2,
            [Description("已上架")]
            OnSale = 3,
            [Description("下架有效")]
            OffSaleValid = 4,
            [Description("已下架")]
            OffSale = 5,
            [Description("二级审核")]
            LeaderAudit = 6,
            [Description("审核未通过")]
            NoPass = -1,
        }
        #endregion

        public enum ProductPriceStatus : int
        {
            [Description("未提报")]
            NoRequest = 1,
            [Description("已上架")]
            OnSale = 3,
            [Description("下架有效")]
            OffSaleValid = 4,
            [Description("已下架")]
            OffSale = 5,
        }
        public enum ProductPriceRequestStatus : int
        {
            [Description("未提报")]
            NoRequest = 1,
            [Description("待二级审核")]
            WaitPlatAudit = 7,
            [Description("待一级审核")]
            WaitAudit = 2,
            [Description("已上架")]
            OnSale = 3,
            [Description("审核未通过")]
            NoPass = -1,
            [Description("作废")]
            Cancel = -2,
        }
        #region 删除商品
        public enum ProductDeleteStatus : int
        {
            [Description("删除")]
            Delete = -99,

        }
        #endregion

        #region 活动类型 1约惠 2团购 3优惠券 4计次 5积分兑换 6银行积分抵现
        public enum PromotionType : int
        {
            [Description("约惠")]
            Yuehui = 1,
            [Description("团购")]
            Tuan = 2,
            [Description("优惠券")]
            Coupon = 3,
            [Description("计次")]
            Jici = 4,
            [Description("积分兑换")]
            Point = 5,
            [Description("银行积分抵现")]
            DiXian = 6,
            [Description("旅游")]
            Traveling = 8,
        }
        public static string GetPromotionType(object v)
        {
            return GetDescription(typeof(PromotionType), v);
        }
        #endregion

        #region 评分 1差 2一般 3好 4很好 5非常好
        public enum VendorCommentScore : int
        {
            [Description("差")]
            Good0 = 0,
            [Description("一般")]
            Good1 = 1,
            [Description("好")]
            Good2 = 2,
            [Description("很好")]
            Good3 = 3,
            [Description("非常好")]
            Good4 = 4,
        }
        public static string GetVendorCommentScore(object v)
        {
            return GetDescription(typeof(VendorCommentScore), v);
        }
        #endregion

        #region 活动状态 1线上 2线下 3线上+线下
        public enum PromotionPrePayType : int
        {
            [Description("线上")]
            OnLine = 1,
            [Description("线下")]
            OffLine = 2,
            [Description("线上+线下")]
            OnLineAndOffLine = 3,
        }
        #endregion

        #region PromotionShowType 活动前台显示类型
        public enum PromotionShowType : int
        {
            [Description("(约惠)全单折扣")]
            Yuehui1 = 11,
            [Description("(约惠)满额立减")]
            Yuehui2 = 12,
            [Description("(团购)普通")]
            TuanGou1 = 21,
            [Description("(团购)电子票务")]
            TuanGou2 = 22,
            [Description("(团购)卡")]
            TuanGou3 = 23,
            [Description("(团购)抵用券")]
            TuanGou4 = 24,
            [Description("(团购)非验证票")]
            TuanGou5 = 25,
            [Description("(团购)旅游")]
            TuanGou6 = 26,
            [Description("(团购)第三方接入")]
            ThirdAccess=27,
            [Description("(优惠券)满减优惠券")]
            Coupon1 = 31,
            [Description("(计次)计次")]
            Jici = 41,
            [Description("(积分兑换)")]
            Jifen1 = 51,
            [Description("(银行积分抵现)")]
            BankJifen1 = 61,
            [Description("(普通商品)餐饮")]
            OrdinaryGoods1 = 71,
            [Description("(普通商品)购物")]
            OrdinaryShoppingGoods = 72,
            [Description("景点门票")]
            AttractionsTickets = 81,

        }
        public static string GetPromotionShowType(object v)
        {
            return GetDescription(typeof(PromotionShowType), v);
        }
        #endregion

        #region Email状态 0待发送 1已发送 -1发送失败
        public enum EmailStatus : int
        {
            [Description("待发送")]
            WaitSend = 0,
            [Description("已发送")]
            Success = 1,
            [Description("发送失败")]
            Fail = -1,
        }
        #endregion

        #region Email优先级 1高 2中 3低
        public enum EmailPriority : int
        {
            [Description("高")]
            Low = 1,
            [Description("中")]
            Mid = 2,
            [Description("低")]
            High = 3,
        }
        #endregion

        #region 商户相册图片的显示位置 1商户相册 2商户banner 3商户Logo
        public enum VendorPhotoType : int
        {
            [Description("商户相册")]
            Album = 1,
            [Description("商户banner")]
            Banner = 2,
            [Description("商户Logo")]
            Logo = 3,
        }
        #endregion

        #region 创建订单结果 0全部成功 1部分成功 -1全部失败
        public enum CreateOrderResultStatus : int
        {
            [Description("全部成功")]
            AllSuccess = 0,
            [Description("部分成功")]
            PartSuccess = 1,
            [Description("全部失败")]
            AllFail = -1,
        }
        #endregion

        #region 订单状态 0待支付，1待消费，2已消费，3已过期，4退款中，5已退款，6待发货，7已发货,8已签收,9已退签,-1已取消,-2退款失败,-3开卡中
        public enum OrderStatus : int
        {
            [Description("待支付")]
            WaitPay = 0,
            [Description("待消费")]
            WaitConsume = 1,
            [Description("已消费")]
            Consumed = 2,
            [Description("已过期")]
            Expired = 3,
            [Description("退款中")]
            Refunding = 4,
            [Description("已退款")]
            Refunded = 5,
            [Description("待发货")]
            WaitOutStock = 6,
            [Description("已发货")]
            AlreadyOutStock = 7,
            [Description("已签收")]
            AlreadySignin = 8,
            [Description("已退签")]
            BackToSign = 9,
            [Description("已取消")]
            Cancel = -1,
            [Description("退款失败")]
            RefundFail = -2,
            [Description("开卡中")]
            CardCancel = -3,
            [Description("待商户审核")]
            WaitVendorVerify = -4,
        }

        public static string GetOrderStatus(object v)
        {
            return GetDescription(typeof(OrderStatus), v);
        }
        #endregion

        #region 订单类型 1约惠 2团购 3优惠券 4计次 5积分兑换 6银行积分抵现 7普通商品  8卡券分销
        public enum OrderType : int
        {
            [Description("约惠")]
            Yuehui = 1,
            [Description("团购")]
            Tuan = 2,
            [Description("优惠券")]
            Coupon = 3,
            [Description("计次")]
            Jici = 4,
            [Description("积分兑换")]
            Point = 5,
            [Description("银行积分抵现")]
            DiXian = 6,
            [Description("普通商品")]
            OrdinaryGoods = 7,
            [Description("任我游")]
            Traveling = 8,
        }

        public static string GetOrderType(object v)
        {
            return GetDescription(typeof(OrderType), v);
        }
        #endregion

        #region 消费类型 1提货 2现金消费 3银行特惠 4银行积分兑换 5银行积分抵现
        public enum ConsumeType : int
        {
            [Description("提货")]
            PickUp = 1,
            [Description("现金消费")]
            CashConsume = 2,
            [Description("银行特惠")]
            Special = 3,
            [Description("银行积分兑换")]
            Point = 4,
            [Description("银行积分抵现")]
            DiXian = 5,
            [Description("普通商品")]
            OrdinaryGoods = 6,
        }

        public static SortedList GetConsumeType()
        {
            return GetStatus(typeof(ConsumeType));
        }
        public static string GetConsumeType(object v)
        {
            return GetDescription(typeof(ConsumeType), v);
        }

        #endregion

        #region 支付记录来源 1前台 2后台
        public enum NetPaySource : int
        {
            [Description("前台")]
            WEB = 1,
            [Description("后台")]
            MIS = 2,
            [Description("自动")]
            Auto = 3,
        }
        #endregion

        #region 支付记录状态 0待审核 1已审核 -1已作废
        public enum NetPayStatus : int
        {
            [Description("待审核")]
            WaitVerify = 0,
            [Description("已审核")]
            Verified = 1,
            [Description("已作废")]
            Abandon = -1,
        }
        #endregion

        #region 短信类型 1串码短信 2非串码短信
        public enum SmsType : int
        {
            [Description("串码短信")]
            CodeEncrypt = 1,
            [Description("非串码短信")]
            CodeUnEncrypt = 2,
        }
        #endregion

        #region API接口返回代码 停用
        public enum APIResultCode : int
        {
            [Description("无效的数据格式")]
            InvalidData = 10001,
            [Description("版本号错误")]
            VersionError = 10002,
            [Description("加密类型错误")]
            EncryptTypeError = 10003,
            [Description("无效的请求时间")]
            RequestTimeError = 10004,
            [Description("用户或签名错误")]
            UserError = 10005,
            [Description("接口名错误")]
            InterfaceNameError = 10006,
            [Description("无接口使用权限")]
            PrivilegeError = 10007,
            [Description("一般的业务逻辑错误")]
            GeneralBusinessError = 99998,
            [Description("未知错误，请联系相关人员")]
            UnknownError = 99999,
            [Description("鉴权查询成功")]
            OrderQuerySuccess = 20100,
            [Description("消费成功")]
            OrderConsumeSuccess = 20101,
            [Description("消费冲正成功")]
            OrderReverseSuccess = 20102,
            [Description("消费撤销成功")]
            ConsumeCancelSuccess = 20103,
            [Description("消费撤销冲正成功")]
            ConsumeCancelReverseSuccess = 20104,
            [Description("消费退货成功")]
            ConsumeRefundSuccess = 20105,
            [Description("消费退货冲正成功")]
            ConsumeRefundReverseSuccess = 20106,

            [Description("消费码已使用")]
            ConsumePwdUsed = 20110,
            [Description("消费码不正确")]
            ConsumePwdError = 20111,
            [Description("卡号不匹配")]
            CardNoError = 20112,
            [Description("消费类型错误")]
            ConsumeTypeError = 20113,
            [Description("优惠规则不匹配")]
            CouponRuleError = 20120,
            [Description("请求日期错误")]
            ReqDataError = 20121,
            [Description("请求时间错误")]
            ReqTimeError = 20122,
            [Description("请求流水号错误")]
            ReqSeqIdError = 20123,
            [Description("请求终端号错误")]
            ReqTermError = 20124,
            [Description("O2O平台商户号和POS系统商户号不能同时为空")]
            ReqMerOrExtMerError = 20125,
            [Description("消费金额错误或不能为空")]
            ConsumeAmtError = 20126,
            [Description("消费积分错误或不能为空")]
            ConsumePointError = 20127,
            [Description("POS系统商户号错误")]
            ReqMerError = 20128,
            [Description("O2O平台商户号错误")]
            ExtMerError = 20129,
            [Description("POS系统商户号或O2O平台商户号验证不通过")]
            VendorIDorPosIDError = 20130,
            [Description("消费类型与订单不匹配")]
            ConsumeTypeNotMatch = 20131,
            [Description("操作失败,业务尚未定义")]
            BusinessNotExsit = 20132,
            [Description("操作失败,订单处于非待消费状态")]
            OrderNotWaitConsume = 20133,
            [Description("请求流水号已存在")]
            ReqSeqIdExsit = 20134,
            [Description("找不到该流水号的消费记录")]
            ReqSeqIdNotExsit = 20135,
            [Description("活动尚未开始或已过期")]
            NotInPromotionTime = 20136,
            [Description("实付金额不等于应付金额")]
            ConsumeRealAmtError = 20137,
            [Description("消费数量错误")]
            ConsumeQtyError = 20138,
            [Description("剩余的可消费数量不足")]
            ConsumeQtyNotEnough = 20139,
            [Description("原消费请求流水号错误")]
            OrgSeqIdError = 20140,
            [Description("该消费已冲正,不支持退款")]
            ConsumeAlreadyReverse = 20141,
            [Description("该消费已撤销或退货,不支持退款")]
            ConsumeAlreadyCancelOrRefund = 20142,
            [Description("非刷卡或现金消费,不支持退款")]
            ConsumeNotCardOrCash = 20143,
        }
        public static SortedList GetAPIResultCode()
        {
            return GetStatus(typeof(APIResultCode));
        }
        public static string GetAPIResultCode(object v)
        {
            return GetDescription(typeof(APIResultCode), v);
        }
        #endregion

        #region API接入方应用类型 1 POS鉴权消费应用 2 手机客户端应用
        public enum APIAppType : int
        {
            /// <summary>
            /// POS鉴权消费应用
            /// </summary>
            [Description("POS鉴权消费应用")]
            Order = 1,
            /// <summary>
            /// 手机客户端应用
            /// </summary>
            [Description("手机客户端应用")]
            APP = 2,
        }
        public static SortedList GetAPIAppType()
        {
            return GetStatus(typeof(APIAppType));
        }
        public static string GetAPIAppType(object v)
        {
            return GetDescription(typeof(APIAppType), v);
        }
        #endregion

        #region POS接入通道 1 POS通道一 2 POS通道二
        public enum FromPosNo : int
        {
            /// <summary>
            /// POS通道一
            /// </summary>
            [Description("POS通道一")]
            One = 1,
            /// <summary>
            /// POS通道二
            /// </summary>
            [DescriptionAttribute("POS通道二")]
            Two = 2,
        }
        public static SortedList GetFromPosNo()
        {
            return GetStatus(typeof(FromPosNo));
        }
        public static string GetFromPosNo(object v)
        {
            return GetDescription(typeof(FromPosNo), v);
        }
        #endregion

        #region 日志记录等级 0 不记录 1 记录核心日志 2记录全部日志
        public enum LogLevel : int
        {
            /// <summary>
            /// 不记录
            /// </summary>
            [Description("不记录")]
            NoRecord = 0,
            /// <summary>
            /// 记录核心日志
            /// </summary>
            [Description("记录核心日志")]
            PartRecord = 1,
            /// <summary>
            /// 记录全部日志
            /// </summary>
            [Description("记录全部日志")]
            AllRecord = 2,
        }
        public static SortedList GetLogLevel()
        {
            return GetStatus(typeof(LogLevel));
        }
        public static string GetLogLevel(object v)
        {
            return GetDescription(typeof(LogLevel), v);
        }
        #endregion

        #region API加密类型
        public enum APIEncryptType : int
        {
            [Description("MD5")]
            MD5 = 1,
        }
        public static SortedList GetAPIEncryptType()
        {
            return GetStatus(typeof(APIEncryptType));
        }
        public static string GetAPIEncryptType(object v)
        {
            return GetDescription(typeof(APIEncryptType), v);
        }
        #endregion

        #region API请求类型 1 消费 2 消费冲正 3 消费撤销 4 消费撤销冲正 5 消费退货 6 消费退货冲正
        public enum APIReqType : int
        {
            /// <summary>
            /// 消费
            /// </summary>
            [Description("消费")]
            Consume = 1,
            /// <summary>
            /// 消费冲正
            /// </summary>
            [Description("消费冲正")]
            Reverse = 2,
            /// <summary>
            /// 消费撤销
            /// </summary>
            [Description("消费撤销")]
            ConsumeCancel = 3,
            /// <summary>
            /// 消费撤销冲正
            /// </summary>
            [Description("消费撤销冲正")]
            ConsumeCancelReverse = 4,
            /// <summary>
            /// 消费退货
            /// </summary>
            [Description("消费退货")]
            ConsumeRefund = 5,
            /// <summary>
            /// 消费退货冲正
            /// </summary>
            [Description("消费退货冲正")]
            ConsumeRefundReverse = 6,
        }

        public static SortedList GetAPIReqType()
        {
            return GetStatus(typeof(APIReqType));
        }
        public static string GetAPIReqType(object v)
        {
            return GetDescription(typeof(APIReqType), v);
        }

        #endregion

        #region 订单消费验证类型 1 POS验证 2 WEB验证 3客服验证
        public enum OrderVerifyType : int
        {
            [Description("POS验证")]
            POS = 1,
            [Description("WEB验证")]
            Vendor = 2,
            [Description("客服验证")]
            MIS = 3,
        }
        #endregion

        #region 消费券状态 0 有效 1 已使用 -1 无效
        public enum OrderItemStatus : int
        {
            [Description("有效")]
            Valid = 0,
            [Description("已使用")]
            Used = 1,
            [Description("无效")]
            UnValid = -1,
        }
        #endregion

        #region 结算周期 0日结 1周结 3月结 4季结 5半月结
        public enum SettlementPeriodUnit : int
        {
            [Description("日结")]
            Day = 0,
            [Description("周结")]
            Week = 1,
            [Description("月结")]
            Month = 3,
            [Description("季结")]
            Season = 4,
            [Description("半月结")]
            HalfMonth = 5,
        }
        #endregion

        #region 财务收款单类型 1 线上收款单 2 线下收款单 3 退款单  4 普通商品退换货退款单 5 普通商品取消退款单 6 外部应用退款单
        public enum IncomeType : int
        {
            [Description("线上收款单")]
            OnlineIncome = 1,
            [Description("线下收款单")]
            OfflineIncome = 2,
            [Description("退款单")]
            Refund = 3,
            [Description("普通商品退换货退款单")]
            GoodsRefund = 4,
            [Description("普通商品取消退款单")]
            GoodsCancelRefund = 5,
            [Description("外部应用退款单")]
            OuterAppRefund = 6,
        }

        public static string GetIncomeType(object v)
        {
            return GetDescription(typeof(IncomeType), v);
        }

        #endregion

        #region 商户平台用户角色类型 1 管理员 2 操作员
        public enum VendorUserRoleType : int
        {
            [Description("管理员")]
            Admin = 1,
            [Description("操作员")]
            Optor = 2,
        }
        #endregion


        #region 机构平台用户角色类型 1 管理员 2 操作员
        public enum OrganUserRoleType
        {
            [Description("管理员")]
            Admin = 1,
            [Description("操作员")]
            Optor = 2,
        }
        #endregion


        #region 短信接口返回值
        public enum SMSResult
        {
            [Description("同一条短信中手机号不能有相同")]   //自己定义的错误
            MobileRepeat = -2,
            [Description("发送异常")]   //自己定义的错误
            Exception = -1,
            [Description("发送成功")]
            Success = 0,
            [Description("接入编号不存在")]
            ApiIdNotExist = 1,
            [Description("帐号或密码错误")]
            AccountPasswordError = 2,
            [Description("手机号码格式不正确")]
            PhoneNumFormatError = 3,
            [Description("发送时间格式不正确")]
            SendTimeError = 4,
            [Description("验签失败")]
            SignError = 5,
            [Description("系统错误")]
            SysError = 99,
        }
        public static SortedList GetSMSResult()
        {
            return GetStatus(typeof(SMSResult));
        }
        public static string GetSMSResult(object v)
        {
            return GetDescription(typeof(SMSResult), v);
        }
        #endregion

        #region APP接口返回代码
        public enum APIResultCode_App : int
        {
            [Description("接口调用成功")]
            Success = 10000,
            [Description("无效的数据格式")]
            InvalidData = 10001,
            [Description("版本号错误")]
            VersionError = 10002,
            [Description("加密类型错误")]
            EncryptTypeError = 10003,
            [Description("无效的请求时间")]
            RequestTimeError = 10004,
            [Description("用户或签名错误")]
            UserError = 10005,
            [Description("接口名错误")]
            InterfaceNameError = 10006,
            [Description("无接口使用权限")]
            PrivilegeError = 10007,
            [Description("一般的业务逻辑错误")]
            GeneralBusinessError = 99998,
            [Description("未知错误，请联系相关人员")]
            UnknownError = 99999,

            [Description("手机格式不正确")]
            MobileError = 20001,
            [Description("会员密码不正确")]
            CustomerPwdError = 20002,
            [Description("手机号码已存在")]
            MobilePhoneExsited = 20003,
            [Description("短信发送失败")]
            SendSMSFailed = 20004,
            [Description("验证码不正确")]
            VerifyCodeError = 20005,
            [Description("验证码已过期")]
            VerifyCodeExpired = 20006,
            [Description("会员不存在")]
            CustomerNotExsit = 20007,
            [Description("活动不存在")]
            PromotionNotExsit = 20008,
            [Description("活动搜索结果为空")]
            PromotionSearchNoData = 20009,
            [Description("会员密码不能为空")]
            CustomerPwdEmpty = 20010,
            [Description("城市编号不能为空且要求为正整数")]
            CitySysNoError = 20011,
            [Description("类别编号要求正整数")]
            CategorySysNoError = 20012,
            [Description("活动类型填写格式不正确或不存在")]
            PromotionTypeError = 20013,
            [Description("排序方式填写不正确")]
            SortTypeError = 20014,
            [Description("分页数要求为正整数")]
            PageIndexError = 20015,
            [Description("每页显示个数要求为整数")]
            PageSizeError = 20016,
            [Description("活动编号不能为空且要求为正整数")]
            PromotionSysNoError = 20017,
            [Description("会员编号不能为空且要求为正整数")]
            CustomerSysNoError = 20018,
            [Description("购买数量填写不正确")]
            BuyQtyError = 20019,
            [Description("购买单价填写不正确")]
            BuyPriceError = 20020,
            [Description("购买总价填写不正确")]
            TotalAmountError = 20021,
            [Description("购买单价与实际单价不符,下单取消")]
            BuyPriceNotEqualCurrentPrice = 20022,
            [Description("订单编号填写不正确")]
            OrderIDError = 20023,
            [Description("订单不存在")]
            OrderNotExsit = 20024,
            [Description("库存不足")]
            InventoryNotEnough = 20025,
            [Description("活动尚未开始")]
            PromotionNotStart = 20026,
            [Description("活动已结束")]
            PromotionAlreadyEnd = 20027,
            [Description("活动不存在或不处于上架状态")]
            PromotionNotOnSale = 20028,
            [Description("非团购订单不支持退款")]
            OrderCannotRefund = 20029,
            [Description("该活动不支持退款")]
            PromotioinCannotRefund = 20030,
            [Description("订单不处于待消费状态,不能退款")]
            OrderNotWaitConsume = 20031,
            [Description("区域编号要求正整数")]
            AreaSysNoError = 20032,
            [Description("订单搜索结果为空")]
            OrderSearchNoData = 20033,
            [Description("小于订单的起购数量")]
            LowerMinQty = 20034,
            [Description("大于订单的限购数量")]
            HigherMaxQty = 20035,
        }
        public static SortedList GetAPIResultCode_App()
        {
            return GetStatus(typeof(APIResultCode_App));
        }
        public static string GetAPIResultCode_App(object v)
        {
            return GetDescription(typeof(APIResultCode_App), v);
        }
        #endregion

        #region APP活动搜索排序方式
        public enum AppSearchSortType
        {
            [Description("购买人数")]
            BuyQty = 1,
            [Description("价格从低到高")]
            PriceLowToHigh = 2,
            [Description("价格从高到低")]
            PriceHighToLow = 3,
            [Description("最新发布")]
            New = 4,
            [Description("即将结束")]
            End = 5,
            [Description("附近优先")]
            Near = 6,
        }


        public static SortedList GetAppSearchSortType()
        {
            return GetStatus(typeof(AppSearchSortType));
        }
        public static string GetAppSearchSortType(object v)
        {
            return GetDescription(typeof(AppSearchSortType), v);
        }
        #endregion

        #region 普通商品列表排序方式
        public enum ProductGeneralListSortType
        {
            [Description("购买人数")]
            BuyQty = 1,
            [Description("价格从低到高")]
            PriceLowToHigh = 2,
            [Description("价格从高到低")]
            PriceHighToLow = 3,
            [Description("最新发布")]
            New = 4,
            [Description("即将结束")]
            End = 5,
        }
        #endregion

        #region 客户类型 1普通会员 2导游
        public enum CustomerType : int
        {
            [Description("普通会员")]
            Customer = 1,
            [Description("导游")]
            TourGuide = 2,
        }
        #endregion

        #region 客户状态 -2审核不通过 -1无效 0有效 1待审核
        public enum CustomerStatus : int
        {
            [Description("审核不通过")]
            NoPass = -2,
            [Description("无效")]
            UnValid = -1,
            [Description("有效")]
            Valid = 0,
            [Description("待审核")]
            WaitConfirm = 1,
        }
        #endregion

        #region 类别状态 -1无效 0有效 1有效但不在前台显示
        public enum CategoryStatus : int
        {
            [Description("无效")]
            UnValid = -1,
            [Description("有效")]
            Valid = 0,
            [Description("有效但不在前台显示")]
            ValidNotShow = 1,
        }
        #endregion

        #region 类别状态 -1无效 0有效 1有效但不在前台显示
        public enum PropertyIsValid : int
        {
            [Description("无效")]
            UnValid = 0,
            [Description("有效")]
            Valid = 1
        }
        #endregion

        #region 电子会员卡
        /// <summary>
        /// 会员等级
        /// </summary>
        public enum CustomerLevel : int
        {
            [Description("一级")]
            OneLevel = 1,
            [Description("二级")]
            TwoLevel = 2,
            [Description("三级")]
            ThreeLevel = 3,
            [Description("四级")]
            FourLevel = 4,
            [Description("五级")]
            FiveLevel = 5,
        }

        /// <summary>
        /// 卡状态
        /// </summary>
        public enum CardStatus : int
        {
            [Description("正常")]
            Normal = 1,
            [Description("冻结")]
            Freeze = 2,
        }

        /// <summary>
        /// 卡状态按钮TEXT
        /// </summary>
        public enum CardStatusButtonText : int
        {
            [Description("冻结")]
            Freeze = 0,
            [Description("解冻")]
            UnFreeze = 3,
        }

        /// <summary>
        /// 卡状态匹配接口状态
        /// </summary>
        public enum CardStatusForAPI : int
        {
            [Description("正常")]
            Normal = 0,
            [Description("冻结")]
            Freeze = 3,
        }

        /// <summary>
        /// 充值状态
        /// </summary>
        public enum RechargeStatus : int
        {
            [Description("充值申请")]
            RechargeSubmit = 0,
            [Description("充值确认")]
            RechargeConfirm = 1,
            [Description("充值审核不通过")]
            RechargeRefuse = -1,
        }

        /// <summary>
        /// API充值状态
        /// </summary>
        public enum APIRechargeStatus : int
        {
            [Description("充值申请处理中")]
            RechargeSubmit = 4,
            [Description("充值申请成功")]
            RechargeSubmitOK = 5,
            [Description("充值确认处理中")]
            RechargeConfirm = 7,
            [Description("充值确认成功")]
            RechargeConfirmOK = 8,
            [Description("充值失败")]
            RechargeRefuse = -1,
            [Description("充值完成")]
            RechargeDone = 1,
        }

        /// <summary>
        /// API充值类型
        /// </summary>
        public enum APIRechargeTypes : int
        {
            [Description("单卡激活")]
            ECardActive = 0,
            [Description("卡号段激活")]
            ECardListActive = 1,
            [Description("单卡充值")]
            ECardRecharge = 2,
            [Description("卡号段充值")]
            ECardListRecharge = 3,
            [Description("文件充值")]
            FileRecharge = 4,
        }

        /// <summary>
        /// 客户来源
        /// </summary>
        public enum CustomerFrom : int
        {
            [Description("商户后台开卡")]
            ManagementPortal = 1,
            [Description("通商惠门户购卡")]
            WebPortal = 2,
            [Description("通商惠消费")]
            WebConsume = 3,
            [Description("微信客户端注册")]
            Weixin = 4,
        }

        /// <summary>
        /// 交易类型
        /// </summary>
        public enum DealType
        {
            [Description("消费")]
            Consume = 0,
            [Description("消费撤销")]
            ConsumeCancel = 1,
        }

        /// <summary>
        /// 交易状态
        /// </summary>
        public enum DealStatus : int
        {
            [Description("挂起")]
            Hold = 0,
            [Description("失败")]
            Fail = 1,
            [Description("成功")]
            OK = 2,
            [Description("已冲正")]
            Check = 3,
            [Description("已取消")]
            Cancel = 4,
        }

        #endregion

        #region 积分类型 1平台积分 2机构积分
        public enum PointFromType : int
        {
            [Description("平台积分")]
            Platform = 1,
            [Description("机构积分")]
            Organ = 2,
        }
        #endregion

        #region 邮件短信整合

        #region xml业务命名短信邮件

        public enum EmailSMSXmlBusinessName : int
        {
            [Description("价格举报")]
            ShoppingPriceReport = 0,
            [Description("网上支付记录审核后给客户发送的订单待发货的提示短信")]
            ShoppingAuditNetPay = 1,
            [Description("前台会员密码找回")]
            ShoppingGetPWD = 2,
            [Description("客户投诉回复")]
            ShoppingReplyComplaint = 3,
            [Description("供应商发货，短信提醒客户")]
            ShoppingSendGoods = 4,
            [Description("注册成功")]
            ShoppingRegister = 5,
            [Description("注册送优惠券")]
            ShoppingRegisterPromotion = 6,
            [Description("手机号码验证")]
            ShoppingValidationCellPhone = 7,
            [Description("邮箱验证")]
            ShoppingValidationEmail = 10,
            [Description("后台服务异常(MallService,MIS等)")]
            MisServiceError = 11,
            [Description("支付到9588多次失败，发Email通知客服")]
            MisPayTo9588Fail = 12,
            [Description("商旅酒店预警")]
            MisHotelWarning = 13,
            [Description("商旅机票预警")]
            MisTicketsWarning = 14,
            [Description("订单支付验证后，提醒商家发货")]
            MisSendGoodsWarning = 15,
            [Description("取消签收提醒供货商")]
            MisCancelSignIn = 16,
            [Description("缺货预警通知")]
            MisInventoryWarning = 17,
            [Description("订单支付后,发送密码短信,失败后提醒技术人员")]
            MisChangePWDForServiceOrderError = 18,
            [Description("发送服务团商户密码")]
            SendServiceVendorPWD = 19,
            [Description("24小时未发货")]
            MisSendGoodsMonitor24 = 20,
            [Description("48小时未发货")]
            MisSendGoodsMonitor48 = 21,
            [Description("发送行程单信息失败提醒")]
            MisSendSingleTripFail = 22,
            [Description("机票消息通知出票")]
            TripOutTicket = 23,
            [Description("机票消息通知订单改期")]
            TripDateChanged = 24,
            [Description("酒店订单提醒")]
            TripConfirmHotelOrder = 25,
            [Description("酒店房间已满提醒")]
            TripRoomFull = 26,
            [Description("酒店取消订单")]
            TripCancelHotelOrder = 27,
            [Description("商旅酒店前台提交订单确认")]
            TripSubmitHotelOrder = 28,
            [Description("生活缴费常付账单提醒短信")]
            LifeRemind = 29,
            [Description("客户订单支付成功自动审核未通过")]
            MisOrderPayAccessNoVerify = 30
        }

        public static SortedList GetEmailSMSXmlBusinessName()
        {
            return GetStatus(typeof(EmailSMSXmlBusinessName));
        }

        public static string GetEmailSMSXmlBusinessName(object v)
        {
            return GetDescription(typeof(EmailSMSXmlBusinessName), v);
        }

        #endregion

        #region 短信邮件xml文件名称

        public enum EmailSMSXmlFileName : int
        {
            [Description("前台购物")]
            Shopping = 0,
            [Description("Mis后台，业务和技术人员")]
            Mis = 1,
            [Description("商旅机票酒店")]
            Trip = 2,
            [Description("生活缴费")]
            Life = 3,
        }

        public static SortedList GetEmailSMSXmlFileName()
        {
            return GetStatus(typeof(EmailSMSXmlFileName));
        }

        public static string GetEmailSMSXmlFileName(object v)
        {
            return GetDescription(typeof(EmailSMSXmlFileName), v);
        }

        #endregion

        #endregion

        #region 商品类型(1电子 2实物 3电子+实物)
        public enum ProductType : int
        {
            [Description("电子")]
            Virtue = 1,
            [Description("实物")]
            Entity = 2,
            [Description("电子+实物")]
            VEntity = 3,
        }
        #endregion

        #region 结算单状态
        /// <summary>
        /// 结算单状态
        /// </summary>
        public enum FinanceSettlementStatus
        {
            //[Description("人工作废")]
            //AbandonManually = -2,
            //[Description("系统作废")]
            //AbandonSystem = -1,
            [Description("原始")]
            Origin = 0,
            [Description("已结算")]
            Settled = 1,
        }
        public static SortedList GetFinanceSettlementStatus()
        {
            return GetStatus(typeof(FinanceSettlementStatus));
        }
        public static string GetFinanceSettlementStatus(object v)
        {
            return GetDescription(typeof(FinanceSettlementStatus), v);
        }
        #endregion

        #region API接口返回代码(新开放平台)
        public enum APIResCode : int
        {
            [Description("调用成功")]
            Success = 1001,

            [Description("参数缺失")]
            ArgumentNull = 1101,
            [Description("参数无效")]
            ArgumentInvalid = 1102,
            [Description("接口名无效")]
            MethodInvalid = 1103,
            [Description("接口已停用")]
            MethodNoUsed = 1104,
            [Description("接口无权限")]
            MethodNoPrivilege = 1105,
            [Description("用户无权限")]
            UserNoPrivilege = 1106,
            [Description("业务逻辑错误")]
            LogicError = 1107,
            [Description("外部逻辑错误")]
            OutBizErrpr = 1997,
            [Description("业务逻辑错误")]
            BusinessError = 1998,
            [Description("未知错误，请联系相关人员")]
            UnknownError = 1999,

            //下面是逻辑返回码
            #region 逻辑错误

            #region 基础类
            [Description("查询类型有误")]
            SearchTypeError = 1201,
            [Description("投票项有误")]
            ActivityVoteTargetSysNoError = 1202,
            #endregion

            #region 会员类
            [Description("更新类型错误")]
            UpdateTypeError = 2001,
            [Description("会员编号不能为空")]
            CustomerSysnoError = 2002,
            [Description("随机码格式错误")]
            RandCodeError = 2003,
            [Description("修改密码错误")]
            UpdatePwdError = 2004,
            [Description("更新类型错误")]
            CustomerUpdateTypeError = 2005,

            [Description("商品不存在")]
            ProductNotExsit = 2007,
            [Description("密码解析失败")]
            PassworkNotExsit = 2008,
            [Description("原始密码解析失败")]
            OrginalPwdResolveError = 2009,
            [Description("新密码解析失败")]
            NewPwdResolveError = 2010,
            [Description("收货地址名称格式错误")]
            ReceiveTitleError = 2011,
            [Description("操作类型格式错误")]
            ModifyTypeError = 2012,
            [Description("配送信息编号格式错误")]
            ReceiveSysNoError = 2013,
            [Description("配送方式不存在")]
            ReceiveInfoSearchNoData = 2014,
            [Description("商户公众账号格式错误")]
            WXIDSearchError = 2015,
            [Description("会员已存在")]
            CustomerVendorExists = 2016,
            [Description("会员不存在")]
            CustomerVendorNotExists = 2017,
            [Description("会员无效")]
            CustomerVendorError = 2018,
            [Description("会员无效")]
            CustomerOrganError = 2019,
            [Description("验证码错误")]
            AuthCodeError = 2020,
            [Description("投票编号错误")]
            ActivityVoteSysNoError = 2021,
            [Description("会员消息编号错误")]
            PushCustomerSysNoError = 2022, 
            [Description("状态错误")]
            StatusError = 2023,
            [Description("会员消息错误")]
            PushManagementError = 2024,
            #endregion

            #region 商户类
            [Description("商户不存在")]
            VendorInformationSearchNoData = 4001,
            [Description("微信公众号首页编号错误")]
            WeixinHomeinfoSysNoError = 4012,
            [Description("营销活动编号错误")]
            ActivitySysNoError = 4013,
            [Description("未提供位置信息")]
            LongitudeOrLatitudeError = 4014,
            [Description("关键词错误")]
            KeyWordError = 4015,
            #endregion

            #region 商品类
            [Description("商品编号不能为空")]
            ProductSysnoError = 5001,
            #endregion

            #region 订单类
            [Description("无效的数据格式")]
            InvalidData = 7001,
            [Description("请求日期错误")]
            ReqDataError = 7002,
            [Description("请求时间错误")]
            ReqTimeError = 7003,
            [Description("请求流水号错误")]
            ReqSeqIdError = 7004,
            [Description("请求终端号错误")]
            ReqTermError = 7005,
            [Description("消费类型错误")]
            ConsumeTypeError = 7006,
            [Description("O2O平台商户号和POS系统商户号不能同时为空")]
            ReqMerOrExtMerError = 7007,
            [Description("POS系统商户号错误")]
            ReqMerError = 7008,
            [Description("O2O平台商户号错误")]
            ExtMerError = 7009,
            [Description("消费金额错误或不能为空")]
            ConsumeAmtError = 7010,
            [Description("消费积分错误或不能为空")]
            ConsumePointError = 7011,
            [Description("消费码不正确")]
            ConsumePwdError = 7012,
            [Description("卡号不匹配")]
            CardNoError = 7013,
            [Description("原消费请求流水号错误")]
            OrgSeqIdError = 7014,
            [Description("消费数量错误")]
            ConsumeQtyError = 7015,
            [Description("需指定团购活动编号")]
            ProductSysNoNull = 7016,
            [Description("团购类型格式错误或不能为空")]
            PromotionShowTypeError = 7017,
            [Description("活动商品不存在")]
            PromotionProductNotExsit = 7018,
            [Description("POS系统商户号或O2O平台商户号验证不通过")]
            VendorIDorPosIDError = 7019,
            [Description("操作失败,业务尚未定义")]
            BusinessNotExsit = 7020,
            [Description("消费类型与订单不匹配")]
            ConsumeTypeNotMatch = 7021,
            [Description("请求流水号已存在")]
            ReqSeqIdExsit = 7022,
            [Description("微信号格式不正确")]
            WXOpenIdError = 7023,
            [Description("商户号格式不正确")]
            VendorIDError = 7024,
            [Description("预约类型格式不正确")]
            BookTypeError = 7025,
            [Description("商品格式不正确")]
            ProductItemListError = 7026,
            [Description("商品号不正确")]
            ProductSysNoError = 7027,
            [Description("就餐人数格式不正确")]
            DinnerCountError = 7028,
            [Description("桌号格式不正确")]
            TableNoError = 7029,
            [Description("实付金额或打折金额不能同时为空")]
            ConsumeRealAmtDiscountAmtError = 7030,
            [Description("排序方式填写不正确")]
            SortTypeError = 7031,
            [Description("分页数要求为正整数")]
            PageIndexError = 7032,
            [Description("每页显示个数要求为整数")]
            PageSizeError = 7033,
            [Description("商品类别不能为空且要求为正整数")]
            ProductTypeSysNoError = 7034,
            [Description("订单状态错误")]
            OrderStatusError = 7035,
            [Description("会员编号和微信编号不能同时为空")]
            CustomerIDAndWXIDNotNull = 7036,
            [Description("创建订单出错")]
            CreatePromotionOrderError = 7037,
            [Description("绑定类型错误")]
            DiscountBindTypeError = 7038,
            [Description("售卖类型错误")]
            SaleTypeError = 7039,
            [Description("支付方式错误")]
            PayMentError = 7040,
            [Description("客户号或者微信号必须有一个")]
            CustomerSysNoAndCustomerOpenID = 7041,
            [Description("微信号必须传入手机号")]
            CustomerOpenIDError = 7042,
            [Description("需指定销售机构及销售渠道编号")]
            OrganSysNoAndSaleChannelSysNoNull = 7043,

            [Description("购买的商品未绑定卡品牌")]
            ProductNotBindCard = 7046,
            [Description("购买的商品未绑定开卡机构")]
            ProductNotBindCardOrgan = 7047,

            [Description("订单价格发生变化,下单取消")]
            ProductAmtChange = 7049,
            [Description("缺少配送方式")]
            NeedExpressNotExsit = 7050,
            [Description("商品单价计算错误")]
            PriceIsNull = 7051,
            [Description("快递信息不正确")]
            NeedExpressError = 7052,
            [Description("客户信息不存在")]
            CustomerModelNotExsit = 7053,
            [Description("约惠需要选择订单绑定方式")]
            PromotionOrderTypeError = 7054,
            [Description("购买数量小于最低起购数量")]
            BuyQtyLessThanMinQty = 7055,
            [Description("购买数量大于最大限购数量")]
            BuyQtyMoreThanMaxQty = 7056,
            [Description("购物信息不能为空")]
            ShoppingInfoError = 7057,
            [Description("缺少快递方式")]
            ExpressTypeError = 7058,
            [Description("商品不处于上架状态或多个商品不属于同一个商户")]
            PromotionStatusOrPromotionVendorError = 7059,
            [Description("商品明细中必须为同一类型的商品")]
            PromotionDetailTypeNotSameError = 7060,
            [Description("商品明细中的商品不能来自多个商户")]
            PromotionDetailFromNotSameVendorError = 7061,
            [Description("短信内容创建失败，请检查短信配置！")]
            CreateMessageError = 7062,
            [Description("订餐信息不能为空")]
            DinnerInfoError = 7063,
            [Description("未知的点餐类型")]
            DinnerTypeError = 7064,
            [Description("需指定活动编号")]
            NeedPromotionSysNoError = 7065,
            [Description("卡产品已经过期")]
            CardNotEnabledError = 7066,

            [Description("未找到记录")]
            OrderNotExistsError = 7068,
            [Description("二维码格式错误")]
            QRCodeError = 7069,
            [Description("请您先绑定银行卡")]
            UserHaveNoCardError = 7070,
            [Description("用户外部编号错误")]
            CustomerOutIDError = 7071,
            [Description("卡不允许充值，本次只允许购买一张")]
            CardNotEnabledError1 = 7072,
            [Description("卡不允许充值，已购买过，不允许交易")]
            CardNotEnabledError2 = 7073,
            [Description("本次交易金额不在卡允许的最小最大充值金额范围内")]
            CardNotEnabledError3 = 7074,
            [Description("交易完成后超过了卡的最大允许金额")]
            CardNotEnabledError4 = 7075,
            [Description("应用类型错误")]
            AppTypeError = 7076,
            [Description("订单金额不一致")]
            OrderAmtEqualError = 7077,
            [Description("运费模版不存在")]
            OrderAreaTempletError = 7078,
            [Description("优惠券错误")]
            CouponError = 7079,
            [Description("取消订单错误")]
            CancelOrderError = 7080,
            [Description("订单取消并退款错误")]
            RefundOrderError = 7081,
            #endregion

            #endregion

            //下面是业务返回码
            #region 业务错误

            #region 基础类
            [Description("行业类别列表为空")]
            VCategoryListSearchNoData = 12001,
            [Description("机构首页广告列表为空")]
            AppAdvSearchNoData = 12002,
    
            #endregion

            #region 会员类
            [Description("密码格式错误")]
            PwdError = 12003,
            [Description("会员登录失败")]
            CustomerLoginError = 12004,
            [Description("原始密码不能为空")]
            OrginalLoginPwdError = 12005,
            [Description("新密码不能为空")]
            NewLoginPwdError = 12006,
            [Description("原始支付密码不能为空")]
            OrginalPayPwdError = 12007,
            [Description("新支付密码不能为空")]
            NewPayPwdError = 12008,

            [Description("会员不存在")]
            CustomerNotExsit = 12009,
            [Description("用户未注册")]
            CustomerNotRegister = 12010,
            [Description("手机号或密码错误")]
            CustomerPhoneOrPwdError = 12011,
            [Description("手机号码已存在")]
            CustomerPhoneExsit = 12012,
            [Description("原始密码错误")]
            OrginalPwdError = 12013,
            [Description("会员商品已存在")]
            CustomerProductExsit = 12014,
            [Description("推荐人姓名错误")]
            AcademicReferenceError = 12015,
            [Description("抱歉，您已经投票一次，感谢您的关注！")]
            ActivityVoteTargetExists = 12016,
            [Description("投票失败")]
            AddActivityVoteTargetError = 12017,
            [Description("抱歉，投票活动已经结束，感谢您的参与！")]
            ActivityVoteExpireError = 12018,


            [Description("优惠券编号格式错误")]
            CouponSysNoError = 12019,
            [Description("优惠券不存在")]
            CouponNotExists = 12020,
            [Description("优惠券已过期")]
            CouponNotOnTime = 12021,
            [Description("您已领取优惠券")]
            CouponAlreadyReceived = 12022,
            [Description("优惠券已经发放完毕")]
            CouponAlreadyDistribution = 12023,
            [Description("发放优惠券活动尚未开始")]
            CouponNotOnTimeBefore = 12024,
            #endregion

            #region 商户类
            [Description("未找到微信账号对应的商户编号")]
            WXIDSearchNoData = 14001,
            [Description("微信ID或者商户ID至少填一项")]
            WXOpenIdVendorIDError = 14002,
            [Description("商户没有对应的订单")]
            WXOpenIdSearchNoDataError = 14003,
            [Description("用户ID或密码错误")]
            UserPwdError = 14004,
            [Description("商户不存在")]
            VendorError = 14005,
            [Description("用户ID与商户号不匹配")]
            UserIDToVendorError = 14006,
            [Description("用户ID已存在")]
            UserIDExist = 14007,
            [Description("商户用户已绑定")]
            VendorUserIDExist = 14008,
            [Description("用户ID错误")]
            UserIDError = 14009,
            [Description("密码错误")]
            VendorPwdError = 14010,
            [Description("查询状态格式错误")]
            AllStatusError = 14011,
            [Description("首页信息无数据")]
            WeixinHomeInfoSearchNoData = 14012,
            [Description("合作商户名称错误")]
            CooperateNameError = 14013,
            [Description("合作商户手机号错误")]
            CooperatePhoneError = 14014,
            [Description("合作商户Email地址错误")]
            CooperateEmailError = 14015,
            #endregion

            #region 商品类
            [Description("商品不存在")]
            GeneralProductNotExsit = 15001,
            [Description("库存不足")]
            InventoryNotEnough = 15002,
            [Description("活动尚未开始")]
            PromotionNotStart = 15003,
            [Description("活动已结束")]
            PromotionAlreadyEnd = 15004,
            [Description("活动不存在或不处于上架状态")]
            PromotionNotOnSale = 15005,
            [Description("活动类型填写格式不正确或不存在")]//下面是活动类
            PromotionTypeError = 15006,
            [Description("活动编号不能为空且要求为正整数")]
            PromotionSysNoError = 15007,
            #endregion

            #region 订单类
            [Description("消费码已使用")]
            ConsumePwdUsed = 17001,
            [Description("剩余的可消费数量不足")]
            ConsumeQtyNotEnough = 17002,
            [Description("操作失败,订单处于非待消费状态")]
            OrderNotWaitConsume = 17003,
            [Description("活动尚未开始或已过期")]
            NotInPromotionTime = 17004,
            [Description("该消费已冲正,不支持退款")]
            ConsumeAlreadyReverse = 17005,
            [Description("非刷卡或现金消费,不支持退款")]
            ConsumeNotCardOrCash = 17006,
            [Description("该消费已撤销或退货,不支持退款")]
            ConsumeAlreadyCancelOrRefund = 17007,
            [Description("找不到该流水号的消费记录")]
            ReqSeqIdNotExsit = 17008,
            [Description("实付金额不等于应付金额")]
            ConsumeRealAmtError = 17009,
            [Description("手机格式不正确")]
            MobileError = 17010,
            [Description("购买数量不正确")]
            ProductQty = 17011,
            [Description("小于订单的起购数量")]
            LowerMinQty = 17012,
            [Description("大于订单的限购数量")]
            HigherMaxQty = 17013,
            [Description("订单不存在")]
            OrderNotExsit = 17014,
            [Description("非团购订单不支持退款")]
            OrderCannotRefund = 17015,
            [Description("该活动不支持退款")]
            PromotioinCannotRefund = 17016,
            [Description("商品列表搜索结果为空")]
            ProductSearchNoData = 17017,
            [Description("商品详情搜索结果为空")]
            ProductDetailSearchNoData = 17018,
            [Description("商品类别搜索结果为空")]
            ProductTypeSearchNoData = 17019,
            [Description("订单号不能为空")]
            OrderIDError = 17020,
            [Description("购买单价不正确")]
            ProductPrice = 17021,
            [Description("购买总价不能为空")]
            TotalAmtNotNull = 17022,
            [Description("订单不存在")]
            OrderDetailSearchNoData = 17023,
            [Description("购买单价与实际单价不符,下单取消")]
            BuyPriceNotEqualCurrentPrice = 17024,
            [Description("银行卡与用户尚未绑定")]
            CardNoUserNotBindError = 17025,
            [Description("订单未发货，请您在商户服务平台进行发货")]
            OrderStatusOutStockError = 17026,
            [Description("只有商家送货和到店自提才可以签收")]
            SignExpressTypeError = 17027,
            [Description("订单未发货，消费开始日期错误")]
            ConsumeStartTimeError = 17028,
            [Description("消费结束日期错误")]
            ConsumeEndTimeError = 17029,
            [Description("快递类型错误")]
            ExpressTypeNotDefineError = 17030,
            [Description("收货区域错误")]
            ReceiveAreaError = 17031,
            [Description("收货地址错误")]
            ReceiveAddressError = 17032,
            [Description("总价错误")]
            TotalAmtError = 17033,
            [Description("收货人名称错误")]
            ReceiveNameError = 17034,
            [Description("创建起始时间格式错误")]
            CreateStartTimeError = 17035,
            [Description("创建截止时间格式错误")]
            CreateEndTimeError = 17036,
            [Description("活动商品与订单类型不一致")]
            OrderTypeAndProductError = 17037,
            [Description("活动商品对应的商户不存在")]
            VendorAndProductError = 17038,

            #region 这些是创建订单里的
            [Description("商品不能为空")]
            PromotionNotNull = 17039,
            [Description("手机号不正确")]
            MobilePhoneError = 17040,
            [Description("客户配送信息不全")]
            CustomerNeedExpressError = 17041,
            [Description("客户积分不足，不能兑换该产品")]
            CustomerPointNotExsit = 17042,
            [Description("不能重复下单，请先消费")]
            RepeatOrder = 17043,
            #endregion

            [Description("消费金额未达到满额立减要求")]
            CouponConsumeAmtError = 17044,

            [Description("购买的商品不是现售的商品")]
            ProductNotCashSales = 17045,
            [Description("商品不存在或未上架")]
            ProductSysNoExsit = 17046,
            [Description("该手机号已注册过此会员卡，请勿重复注册")]
            CreateCardError = 17047,
            [Description("库存不足")]
            InventoryLack = 17048,
            [Description("该商户不支持该运货方式")]
            VendorExpressError = 17049,
            [Description("快递送货、商家送货不应选择自提点")]
            VendorPickUpEmpty = 17050,
            [Description("自提地点有误")]
            VendorPickUpError = 17051,
            [Description("该地区不支持送货")]
            ExpressAreaError = 17052,
            [Description("商品必须只属于一个商户")]
            OnlyOneVendorError = 17053,
            [Description("运费计算有误")]
            ExpressAmtError = 17054,
            [Description("产品参数不正确")]
            ProductArgsError = 17055,
            [Description("验证失败，此优惠券无效")]
            CouponError1 = 17056,
            [Description("用户已经设置支付密码")]
            AllreadySetPayPwd = 17057,

            [Description("积分功能关闭")]
            CustomerPointEndError = 17058,
            [Description("客户不存在")]
            CustomerError = 17059,
            [Description("该机构未设置积分抵现金")]
            OrganPointError = 17060,
            [Description("积分不足")]
            CustomerPointEnoughError = 17061,
            [Description("传入商品列表数据有误")]
            ProductListError = 17062,
            [Description("单商品总价有误")]
            ProductPriceError = 17063,
            [Description("积分操作异常")]
            CustomerPointError = 17064,
            [Description("订购人姓名错误")]
            BuyerNameError = 17065,
            [Description("游玩时间输入有误")]
            VisitStarTimeError = 17066,
            [Description("游客信息错误")]
            OrderVisitorListError = 17067,
            [Description("评论已存在")]
            CommentExists = 17068,
            [Description("没有权限评论")]
            OrderForCommentError = 17069,
            [Description("评论内容错误")]
            CommentContentError = 17070,
            [Description("评论分数有误")]
            ScoreError = 17071,
            [Description("评论失败")]
            AddCommentError = 17072,

            [Description("城市输入有误")]
            CityIDError = 17073,
            [Description("费用类型错误")]
            PayTypeError = 17074,
            [Description("省份输入有误")]
            ProvinceIDError = 17075,

            [Description("重复支付")]
            MobilePayRepeatError = 17076,
            [Description("创建订单失败")]
            CreateOrderError = 17077,
            [Description("尚未开始秒杀")]
            CreateOrderMiaoShaError = 17078,
            #endregion


            #endregion


        }
        public static SortedList GetAPIResCode()
        {
            return GetStatus(typeof(APIResCode));
        }
        public static string GetAPIResCode(object v)
        {
            return GetDescription(typeof(APIResCode), v);
        }
        #endregion

        #region 统一同步会员返回代码
        public enum SyncCustomerResCode : int
        {
            [Description("操作成功")]
            Success = 0000,
            [Description("该帐号不存在")]
            Error1 = 0001,
            [Description("该帐号已注册，已激活")]
            Error2 = 0002,
            [Description("该帐号已注册，未激活")]
            Error3 = 0003,
            [Description("该帐号已注册，已冻结废弃")]
            Error4 = 0004,
            [Description("该帐号目前不是未激活状态，无法激活")]
            Error5 = 0005,
            [Description("该帐号已存在，无法绑定")]
            Error6 = 0006,
            [Description("该用户编号不存在")]
            Error7 = 0007,
            [Description("登录密码不正确")]
            Error8 = 0008,
            [Description("支付密码不正确")]
            Error9 = 0009,
            [Description("密码错误,请重新输入")]
            Error10 = 0010,
            [Description("密码连续错误次数超过5次，如需立即登录请找回登录密码，或3个小时后重新尝试登录")]
            Error11 = 0011,
            [Description("密码应为6—20位长度字符")]
            Error12 = 0012,
            [Description("密码不能为连续字符")]
            Error13 = 0013,
            [Description("密码不能为全部相同字符")]
            Error14 = 0014,
            [Description("登录密码与支付密码不能相同")]
            Error15 = 0015,
            [Description("该用户已冻结")]
            Error16 = 0016,
            [Description("该刷卡器序列号未被任何用户绑定")]
            Error17 = 0017,
            [Description("该刷卡器序列号已被其他用户绑定")]
            Error18 = 0018,
            [Description("该刷卡器序列号已被该用户绑定")]
            Error19 = 0019,
            [Description("验证码不正确")]

            Error20 = 1001,
            [Description("验证码已过期")]
            Error21 = 1002,
            [Description("该用户没有签约银行卡")]
            Error22 = 2001,
            [Description("该用户没有符合条件的订单")]
            Error23 = 3001,
            [Description("该订单不存在")]
            Error24 = 3002,
            [Description("业务码不存在")]
            Error25 = 9001,
            [Description("交易码不存在")]
            Error26 = 9002,
            [Description("操作码不存在")]
            Error27 = 9003,
            [Description("系统错误")]
            Error28 = 9999,
            [Description("支付密码应为6位数字")]
            Error29 = 0031,
        }

        public static SortedList GetSyncCustomerResCode()
        {
            return GetStatus(typeof(SyncCustomerResCode));
        }

        public static string GetSyncCustomerResCode(object v)
        {
            return GetDescription(typeof(SyncCustomerResCode), v);
        }
        #endregion

        #region 统一会员缴费消费项目
        public enum PaySaleProject : int
        {
            [Description("水费")]
            SFXM = 01,
            [Description("电费")]
            DFXM = 02,
            [Description("煤气")]
            MQXM = 03,
            [Description("固话宽带")]
            GHKD = 04,
            [Description("有线电视")]
            YXDS = 06,
            [Description("手机充值")]
            SJCZ = 10001,
            [Description("水费(山西)")]
            SFSX = 11,
            [Description("电费(山西)")]
            DFSX = 12,
            [Description("煤气(山西)")]
            MQSX = 13,
            [Description("固话宽带(山西)")]
            GHSX = 14,
            [Description("有线电视(山西)")]
            YXSX = 16,
            [Description("水费(新兴-全国通道)")]
            SFXX = 21,
            [Description("电费(新兴-全国通道)")]
            DFXX = 22,
            [Description("煤气(新兴-全国通道)")]
            MQXX = 23,
            [Description("固话宽带(新兴-全国通道)")]
            GHXX = 24,
            [Description("数字电视(新兴-全国通道)")]
            SZXX = 26,
        }
        #endregion

        #region 统一会员缴费状态
        public enum PayOrderStatus : int
        {
            [Description("新建订单")]
            XJDD = 1,
            [Description("支付中")]
            ZFZ = 2,
            [Description("支付失败")]
            ZFSB = 3,
            [Description("支付成功,销帐中")]
            ZFCGXZ = 4,
            [Description("销帐成功")]
            XZCG = 5,
            [Description("销帐失败")]
            XZSB = 6,
        }
        #endregion

        /// <summary>
        /// 扣减渠道枚举
        /// </summary>
        public enum ConsumeChannel
        {
            /// <summary>
            /// 通商惠
            /// </summary>
            tongshanghui = 1,
            /// <summary>
            /// 增值
            /// </summary>
            appreciation = 2
        }

        #region 微电商

        #region 预订类型
        /// <summary>
        /// 结算单状态
        /// </summary>
        public enum BookType
        {
            [Description("预订")]
            Booking = 1,
            [Description("现场")]
            Scene = 2,
        }
        public static SortedList GetBookType()
        {
            return GetStatus(typeof(BookType));
        }
        public static string GetBookType(object v)
        {
            return GetDescription(typeof(BookType), v);
        }
        #endregion

        #region 掌上生活
        /// <summary>
        /// 会员密码更新类型
        /// </summary>
        public enum CustomerPwdUpdateType
        {
            [Description("更新登录密码")]
            UpdateloginPwd = 1,
            [Description("找回登录密码")]
            RetrievePwd = 2,
            [Description("更新支付密码")]
            UpdatePayPwd = 3,
            [Description("找回支付密码")]
            RetrievePayPwd = 4,
            [Description("修改支付密码")]
            ReSetPayPwd = 5,
        }
        public static SortedList GetCustomerPwdUpdateType()
        {
            return GetStatus(typeof(CustomerPwdUpdateType));
        }
        public static string GetCustomerPwdUpdateType(object v)
        {
            return GetDescription(typeof(CustomerPwdUpdateType), v);
        }


        #endregion

        #endregion

        #region 微信链接类型
        public enum WeiXinLinkType : int
        {
            //[Description("团购")]
            //Tuan = 1,
            //[Description("预订点菜")]
            //Reservations = 2,
            [Description("订餐易")]
            Site = 3,
            [Description("微电商(商户简介)")]
            VendorSummary = 4,
            [Description("微电商(购物)")]
            Shopping = 5,
            [Description("微信息")]
            MicroInfo = 6,
            [Description("微特卖")]
            MicroSpecialSell = 7,
            [Description("其他")]
            Other = 99,
        }
        public static SortedList GetWeiXinLinkType()
        {
            return GetStatus(typeof(WeiXinLinkType));
        }
        public static string GetWeiXinLinkType(object v)
        {
            return GetDescription(typeof(WeiXinLinkType), v);
        }
        #endregion

        #region 商品应用类型 1 通商惠活动 2餐饮(微电商) 3购物 4卡券分销
        public enum AppType : int
        {
            [Description("通商惠活动")]
            Promotion = 1,
            [Description("餐饮(微电商)")]
            Dinner = 2,
            [Description("购物")]
            Shopping = 3,
            [Description("卡券分销")]
            Card = 4,
            [Description("订餐易")]
            OuterApp = 5,
            [Description("第三方应用")]
            OuterAppBoKa = 6,
            [Description("旅游")]
            Traveling = 8,
        }
        #endregion

        #region 商品类型(1电子 2实物 3电子+实物)
        public enum GeneralProductType : int
        {
            [Description("餐饮(微电商)")]
            Dinner = 2,
            [Description("购物")]
            Shopping = 3,

        }
        #endregion

        /// <summary>
        /// 机构销售渠道枚举
        /// </summary>
        public enum OrganSaleChannel
        {
            [Description("Web网站")]
            WebSite = 1,
            [Description("App客户端")]
            AppClient = 2,
            [Description("微信公众号")]
            WeixinPublicAccount = 3,
        }

        public enum ThirdChannel
        {
            [Description("京东")]
            JD=1,
        }

        /// <summary>
        /// ajax操作类型
        /// </summary>
        public enum EnumAjaxAction
        {
            /// <summary>
            /// 添加机构
            /// </summary>
            AddOrgan = 1,
            /// <summary>
            /// 更新机构
            /// </summary>
            UpdateOrgan = 2,
            /// <summary>
            /// 查询机构列表
            /// </summary>
            QueryOrganList = 3,
            /// <summary>
            /// 查询销售渠道列表
            /// </summary>
            QueryOrganSaleChannelList = 4,
            /// <summary>
            /// 添加机构销售渠道
            /// </summary>
            AddOrganSaleChannel = 5,
            /// <summary>
            /// 修改机构销售渠道
            /// </summary>
            UpdateOrganSaleChannel = 6,

            /// <summary>
            /// 查询商户列表
            /// </summary>
            QueryVendorList = 7,
            /// <summary>
            /// 查询机构快递列表
            /// </summary>
            QueryOrganExpressList = 8,
            /// <summary>
            /// 添加机构快递
            /// </summary>
            AddOrganExpress = 9,
            /// <summary>
            /// 更新机构快递
            /// </summary>
            UpdateOrganExpress = 10,
            /// <summary>
            /// 查询商户所在机构的销售渠道列表
            /// </summary>
            QuerySaleChannelListForVendor = 11,
            /// <summary>
            /// 保存商户商品种类
            /// </summary>
            SaveVendorProductCategory = 12,
            /// <summary>
            /// 获取上级机构层级
            /// </summary>
            GetParentOrganLevel = 13,
            /// <summary>
            /// 获取商户评论项
            /// </summary>
            GetVendorCommentItems = 14,
            /// <summary>
            /// 查询支付方式列表
            /// </summary>
            QueryPaymentList = 15,
            /// <summary>
            /// 保存渠道支付方式设置
            /// </summary>
            SaveSaleChannelPayment = 16,
            /// <summary>
            /// 激活商户
            /// </summary>
            ActiveVendor = 17,
            /// <summary>
            /// 注销商户
            /// </summary>
            DeactiveVendor = 18,
            /// <summary>
            /// 审核商户
            /// </summary>
            AuditVendor = 19,
            /// <summary>
            /// 删除商户
            /// </summary>
            DeleteVendor = 20,
            /// <summary>
            /// 同步上级商户信息
            /// </summary>
            GetParentVendorInfo = 21,
            

        }

        /// <summary>
        /// 商户佣金结算方式
        /// </summary>
        public enum SettlementType
        {
            [Description("收支两条线")]
            DoubleLine = 1,
            [Description("轧差")]
            ZhaCha = 2,
        }

        /// <summary>
        /// 商户计费方式
        /// </summary>
        public enum ProductCommCalcType
        {
            [Description("佣金制")]
            YongJin = 1,
            [Description("差价制")]
            ChaJia = 2,

        }

        /// <summary>
        /// 商户计费方式
        /// </summary>
        public enum CommCalcType
        {
            [Description("佣金制")]
            YongJin = 1,
            [Description("差价制")]
            ChaJia = 2,
            [Description("佣金制+差价制")]
            Both = 3,
        }

        public enum ProductMainStatus
        {
            [Description("待提报")]
            NoRequest = 1,
            [Description("已提报")]
            Request = 2,
            [Description("已上架")]
            Sale = 3,
            [Description("已下架")]
            OffSale = 4,
        }

        /// <summary>
        /// 商户结算方式
        /// </summary>
        public enum BalanceType
        {
            [Description("T+0")]
            T0 = 1,
            [Description("T+1")]
            T1 = 2,
            [Description("T+2")]
            T2 = 3,
            [Description("T+3")]
            T3 = 4
        }

        /// <summary>
        /// 平台类型
        /// </summary>
        public enum PlatformType
        {
            [Description("管理平台")]
            Manager = 1,
            [Description("机构平台")]
            Organ = 2,
            [Description("商户平台")]
            Vendor = 3
        }

        #region 菜单类型
        /// <summary>
        /// 菜单类型
        /// </summary>
        public enum MenuType
        {
            [Description("管理平台(IPP)")]
            Manager = 1,
            [Description("机构平台(Organ)")]
            Organ = 2,
            [Description("商户平台(Vendor)")]
            Vendor = 3
        }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public enum PageOperationType
        {
            [Description("只读")]
            ReadOnly = 1,
            [Description("可以修改")]
            CanWrite = 0
        }

        public static SortedList GetMenuType()
        {
            return GetStatus(typeof(ActiveType));
        }
        public static string GetMenuType(object v)
        {
            return GetDescription(typeof(ActiveType), v);
        }
        #endregion

        #region 机构用户来源
        public enum OrganUserCreateFrom
        {
            [Description("管理平台")]
            Manager = 1,
            [Description("机构平台")]
            Organ = 2,
            [Description("商户平台")]
            Vendor = 3,
        }
        #endregion


        #region 快递100 订阅状态
        #region 快递推送状态 ：无效-1、未订阅0、已订阅1、中止2、正常签收3、退签4
        public enum ExpressPushStatus
        {
            [Description("无效")]
            Invalid = -1,
            [Description("未订阅")]
            NoSubscribe = 0,
            [Description("已订阅")]
            Subscribe = 1,
            [Description("中止")]
            Abort = 2,
            [Description("正常签收")]
            SignIn = 3,
            [Description("退签")]
            BackToSign = 4,
        }
        #endregion
        public enum ExpressAPIBackState
        {
            [Description("在途，快件处于运输过程中")]
            InTransit = 0,
            [Description("揽件，快件已由快递公司揽收")]
            Took = 1,
            [Description("疑难，快递100无法解析的状态，或者是需要人工介入的状态，比方说收件人电话错误")]
            Difficult = 2,
            [Description("签收,正常签收")]
            SignIn = 3,
            [Description("退签,货物退回发货人并签收")]
            BackToSign = 4,
            [Description("派件,货物正在进行派件")]
            Send = 5,
            [Description("退回,货物正处于返回发货人的途中")]
            Back = 6,
            [Description("转投,货物已转交给其他快递公司代为投递")]
            SwitchTo = 7,
        }
        public enum CheckStatus
        {
            [Description("快递签收未确认")]
            UnConfirmed = 0,
            [Description("快递确认签收")]
            Confirmed = 1,
        }
        public enum KeyType
        {
            [Description("订单表")]
            OrderMaster = 1,
            [Description("退换货明细表")]
            RMA_Item = 2,
        }
        #endregion

        #region RMA 申请类型:退货1，换货2，维修3
        public enum RMARequestType : int
        {
            [Description("退货")]
            Return = 1,
            [Description("换货")]
            Change = 2,
            //[Description("维修")]
            //Repair = 3,
        }
        public static SortedList GetRMARequestType()
        {
            return GetStatus(typeof(RMARequestType));
        }
        public static string GetRMARequestType(object v)
        {
            return GetDescription(typeof(RMARequestType), v);
        }
        #endregion

        #region RMA 申请原因:质量问题1、运输损坏2、产品不满意3、未按时送达4、客户原因5、有偿维修6、免费维修7、其它8
        public enum RMAReason : int
        {
            [Description("质量问题")]
            Quality = 1,
            [Description("运输损坏")]
            Damage = 2,
            [Description("产品不满意")]
            Product = 3,
            [Description("未按时送达")]
            Delivery = 4,
            [Description("客户原因")]
            Customer = 5,
            //[Description("有偿维修")]
            //Paid = 6,
            //[Description("免费维修")]
            //Free = 7,
            [Description("其它")]
            Other = 8,
        }
        public static SortedList GetRMAReason()
        {
            return GetStatus(typeof(RMAReason));
        }
        public static string GetRMAReason(object v)
        {
            return GetDescription(typeof(RMAReason), v);
        }
        #endregion

        #region RMA 主表状态:已作废-1，待处理0，处理中1，已处理2
        public enum RMAMainStatus : int
        {
            [Description("已作废")]
            Cancel = -1,
            [Description("待处理")]
            Waiting = 0,
            [Description("处理中")]
            Dealing = 1,
            [Description("已处理")]
            Effect = 2,
        }
        public static SortedList GetRMAMainStatus()
        {
            return GetStatus(typeof(RMAMainStatus));
        }
        public static string GetRMAMainStatus(object v)
        {
            return GetDescription(typeof(RMAMainStatus), v);
        }
        #endregion

        #region RMA Item表状态:拒绝退换货-2，已作废-1，待收货0，已收货1，已检验2，已发货3，已签收4，已申请退款5
        public enum RMAItemStatus : int
        {
            [Description("拒绝退换货")]
            Refuse = -2,
            [Description("已作废")]
            Cancel = -1,
            [Description("待收货")]
            Waiting = 0,
            [Description("已收货")]
            Receive = 1,
            [Description("已检验")]
            Check = 2,
            [Description("已发货")]
            Send = 3,
            [Description("已签收")]
            Signin = 4,
            [Description("已申请退款")]
            Return = 5,
            [Description("已退签")]
            BackToSign = 6,
        }
        public static SortedList GetRMAItemStatus()
        {
            return GetStatus(typeof(RMAItemStatus));
        }
        public static string GetRMAItemStatus(object v)
        {
            return GetDescription(typeof(RMAItemStatus), v);
        }
        #endregion

        #region RMA 处理类型:退货1，换货2，维修3，拒绝4
        public enum RMADealType : int
        {
            [Description("退货")]
            Return = 1,
            [Description("换货")]
            Change = 2,
            //[Description("维修")]
            //Repair = 3,
            [Description("拒绝")]
            Refuse = 4,
        }
        public static SortedList GetRMADealType()
        {
            return GetStatus(typeof(RMADealType));
        }
        public static string GetRMADealType(object v)
        {
            return GetDescription(typeof(RMADealType), v);
        }
        #endregion

        /// <summary>
        /// 快递方式
        /// </summary>
        public enum ExpressType
        {
            [Description("快递送货")]
            ByExpress = 1,
            [Description("商家送货")]
            ByVendor = 2,
            [Description("到店自提")]
            ByCustomerSelf = 3,
            [Description("送货上门")]
            ByOrganSend = 4,
        }

        public static string GetExpressType(object v)
        {
            return GetDescription(typeof(ExpressType), v);
        }

        #region 折扣绑定方式 1、手机绑定，2、卡绑定
        public enum DiscountBindType
        {
            [Description("手机绑定")]
            MobilePhoneBind = 1,
            [Description("卡绑定")]
            CardBind = 2,
        }
        public static string GetDiscountBindType(object v)
        {
            return GetDescription(typeof(DiscountBindType), v);
        }
        #endregion

        public enum ProductSaleType
        {
            [Description("现售")]
            CashSales = 1,
            [Description("预售")]
            Booking = 2,
            [Description("纯展示")]
            Showing = 3,
        }

        public enum CreateOrderError
        {
            #region Tuan
            [Description("通商惠活动信息不能为空")]
            error1 = 1,
            [Description("客户号或者微信号必须有一个")]
            error2 = 2,
            [Description("微信号必须传入手机号")]
            error3 = 3,
            [Description("手机号不正确")]
            error4 = 4,
            [Description("需指定团购活动编号")]
            error5 = 5,
            [Description("需指定销售机构及销售渠道编号")]
            error6 = 6,
            [Description("活动商品不存在或未上架")]
            error7 = 7,
            [Description("购买的商品不是现售的商品")]
            error8 = 8,
            [Description("购买的商品未绑定卡品牌")]
            error9 = 9,
            [Description("购买的商品未绑定开卡机构")]
            error10 = 10,
            [Description("活动尚未开始")]
            error11 = 11,
            [Description("活动已结束")]
            error12 = 12,
            [Description("库存不足")]
            error13 = 13,
            [Description("购买数量小于最低起购数量")]
            error14 = 14,
            [Description("购买数量大于最大限购数量")]
            error15 = 15,
            [Description("订单价格发生变化,下单取消")]
            error16 = 16,
            [Description("缺少配送方式")]
            error17 = 17,
            [Description("快递信息不正确")]
            error18 = 18,
            [Description("客户配送信息不全")]
            error19 = 19,
            [Description("商品单价计算有问题")]
            error20 = 20,
            [Description("客户信息不存在")]
            error21 = 21,
            [Description("客户积分不足，不能兑换该产品")]
            error22 = 22,
            [Description("购物信息不能为空")]
            error23 = 23,
            [Description("商户不存在")]
            error24 = 24,
            [Description("缺少客户信息")]
            error25 = 25,
            [Description("需指定销售机构及销售渠道编号")]
            error26 = 26,
            [Description("缺少支付方式")]
            error27 = 27,
            [Description("缺少快递方式")]
            error28 = 28,
            [Description("商品不处于上架状态或多个商品不属于同一个商户")]
            error29 = 29,
            [Description("数量必须大于0")]
            error30 = 30,
            [Description("商品明细中必须为同一类型的商品")]
            error31 = 31,
            [Description("商品明细中的商品不能来自多个商户")]
            error32 = 32,
            [Description("短信内容创建失败，请检查短信配置！")]
            error33 = 33,
            [Description("订餐信息不能为空")]
            error34 = 34,
            [Description("菜品列表为空")]
            error35 = 35,
            [Description("未知的点餐类型")]
            error36 = 36,
            [Description("就餐人数必须大于0")]
            error37 = 37,
            [Description("桌号不能为空")]
            error38 = 38,
            [Description("需指定活动编号")]
            error39 = 39,
            [Description("约惠需要选择订单绑定方式")]
            error40 = 40,
            [Description("不能重复下单，请先消费")]
            error41 = 41,

            [Description("卡产品已经过期")]
            error42 = 42,
            [Description("您已经不能再开卡了")]
            error43 = 43,
            [Description("未找到记录")]
            error44 = 44,
            [Description("卡不允许充值，本次只允许购买一张")]
            error45 = 45,
            [Description("卡不允许充值，已购买过，不允许交易")]
            error46 = 46,
            [Description("本次交易金额不在卡允许的最小最大充值金额范围内")]
            error47 = 47,
            [Description("交易完成后超过了卡的最大允许金额")]
            error48 = 48,
            [Description("订单金额不能为零")]
            error49 = 49,
            [Description("订单号不存在")]
            error50 = 50,
            [Description("订单金额不一致")]
            error51 = 51,
            [Description("手机号不一致")]
            error52 = 52,
            [Description("应用类型不正确")]
            error53 = 53,
            [Description("该商户不支持该运货方式")]
            error54 = 54,
            [Description("非快递自提不能选择自提点")]
            error55 = 55,
            [Description("自提点无效")]
            error56 = 56,
            [Description("该地区不支持送货")]
            error57 = 57,
            [Description("运费模版不存在")]
            error58 = 58,
            [Description("产品参数不正确")]
            error59 = 59,
            [Description("抵用券不可用")]
            error60 = 60,

            
            [Description("积分功能关闭")]
            error61 = 61,
            [Description("客户不存在")]
            error62 = 62,
            [Description("该机构未设置积分抵现金")]
            error63 = 63,
            [Description("积分不足")]
            error64 = 64,
            [Description("传入商品列表数据有误")]
            error65 = 65,
            [Description("单商品总价有误")]
            error66 = 66,
            [Description("积分操作异常")]
            error67 = 67,
            [Description("创建订单失败")]
            error68 = 68,
            [Description("该商品没有进行秒杀")]
            error69 = 69,
            #endregion
        }

        public static string GetCreateOrderError(object v)
        {
            return GetDescription(typeof(CreateOrderError), v);
        }

        /// <summary>
        /// 商户状态:有效，无效，待审核，审核不通过
        /// </summary>
        public enum VendorStatus : int
        {
            [Description("无效")]
            Invalid = -1,
            [Description("有效")]
            Valid = 0,
            [Description("待审核")]
            WaitAudit = 1,
            [Description("审核不通过")]
            AuditFailure = 2,
        }

        /// <summary>
        /// 图片类型 BigRectangle:475*355   SmallRectangle:180*130  SmallRectangle2:110*80  BigSquare:200*200  MiddleSquare:100*100  SmallSquare:80*80
        /// </summary>
        public enum PictureType
        {
            [Description("475*355")]
            BigRectangle = 1,
            [Description("180*130")]
            SmallRectangle = 2,
            [Description("110*80")]
            SmallRectangle2 = 3,
            [Description("200*200")]
            BigSquare = 4,
            [Description("140*140")]
            MiddleSquare = 5,
            [Description("80*80")]
            SmallSquare = 6
        }

        /// <summary>
        /// 1、新增 2、修改 3、删除
        /// </summary>
        public enum ModifyType
        {
            [Description("新增")]
            Add = 1,
            [Description("修改")]
            Update = 2,
            [Description("删除")]
            Delete = 3
        }

        /// <summary>
        /// 商户前端版本
        /// </summary>
        public enum FrontVersion
        {
            [Description("Version1.0")]
            V1 = 1,
            [Description("Version1.1竖版")]
            V11 = 11,
            [Description("Version1.1横版")]
            V11Horizontal = 12,
            [Description("Version2.0竖版")]
            V2 = 20,
            [Description("Version2.0横版")]
            V2Horizontal = 21
        }
        /// <summary>
        /// 商品串码状态
        /// </summary>
        public enum ProductCodeStatus : int
        {
            [Description("已使用")]
            Use = 1,
            [Description("未使用")]
            UnUse = 0,
            [Description("无效")]
            InValid = -1
        }
        /// <summary>
        /// 商品券码
        /// </summary>
        public enum ProductQuanStatus : int
        { 
            [Description("已售出")]
            Sale=1,
            [Description("未售出")]
            UnSale=0
        }

        public enum OrderCancelReasonType : int
        {
            [Description("无法联系")]
            Contact = 1,
            [Description("余额不足")]
            Balance = 2,
            [Description("重复订单")]
            Repeat = 3,
            [Description("测试订单")]
            Test = 4,
            [Description("价格原因")]
            Price = 5,
            [Description("缺货")]
            Stock = 6,
            [Description("后悔购买")]
            Regret = 7,
            [Description("其他")]
            Other = 8
        }
        /// <summary>
        /// 账户类型
        /// </summary>
        public static class ACCOUNT_TYPE
        {
            public const string 贷记卡 = "00";
            public const string 借记卡 = "01";
            public const string 准贷记卡 = "02";
            /// <summary>
            /// 获取卡类型
            /// </summary>
            /// <param name="cardTypeCode"></param>
            /// <returns></returns>
            public static string GetAccountType(string cardTypeCode)
            {
                foreach (FieldInfo fi in typeof(ACCOUNT_TYPE).GetFields())
                {
                    if (fi.GetValue(null).ToString() == cardTypeCode)
                    {
                        return fi.Name;
                    }
                }
                return string.Empty;
            }
        }

        public static SortedList GetOrderCancelReasonType()
        {
            return GetStatus(typeof(OrderCancelReasonType));
        }

        public static string GetOrderCancelReasonType(object v)
        {
            return GetDescription(typeof(OrderCancelReasonType), v);
        }

        public enum prdtType : int
        {
            [Description("通用(现金)")]
            money = 1,
            [Description("未使用")]
            jifen = 2,
            [Description("次数")]
            cishu = 3
        }

        public enum Customer_From
        {
            [Description("商户会员")]
            VendorCustomer = 1,
            [Description("机构会员")]
            OrganCustomer = 2
        }


        public enum AttetionLogType
        {
            [Description("关注")]
            Attention = 1,
            [Description("关注")]
            CancleAttention = 2
        }

        public static string GetPrdtType(object v)
        {
            return GetDescription(typeof(prdtType), v);
        }

        #region VendorPickUpAddressStatus 0, -1,-2
        //===========================================
        public enum VendorPickUpAddressStatus : int
        {
            [Description("有效")]
            Valid = 0,
            [Description("无效")]
            InValid = -1,
            [Description("删除")]
            Delete = -2

        }
        public static SortedList GetVendorPickUpAddressStatus()
        {
            return GetStatus(typeof(VendorPickUpAddressStatus));
        }
        public static string GetVendorPickUpAddressStatus(object v)
        {
            return GetDescription(typeof(VendorPickUpAddressStatus), v);
        }
        //--------------------------------------------
        #endregion

        public enum ExpressFreightType : int
        {
            [Description("免运费")]
            Free = 1,
            [Description("定额运费")]
            Normal = 2,
            [Description("分区运费")]
            Region = 3
        }

        public enum ExpressStatus : int
        {
            [Description("有效")]
            Valid = 0,
            [Description("无效")]
            InValid = -1,
            [Description("删除")]
            Delete = -2
        }
        public enum CountExpressFreightType : int
        {
            [Description("订单运费统一为该笔订单各商品运费的最大值")]
            Max = 1,
            [Description("订单运费统一为该笔订单各商品运费的总和")]
            Sum = 2,
            [Description("订单运费统一为该笔订单各商品运费的最小值")]
            Min = 3,
        }
        public static SortedList GetCountExpressFreightType()
        {
            return GetStatus(typeof(CountExpressFreightType));
        }
        public static string GetCountExpressFreightType(object v)
        {
            return GetDescription(typeof(CountExpressFreightType), v);
        }
        public enum ActivityType
        {
            [Description("商品营销")]
            ProductMarking = 1,
        }
        public enum CreateSettlementType
        {
            [Description("团购消费结算")]
            OrderConsumed = 1,
            [Description("普通购物结算")]
            GeneralSettlement = 2,
            [Description("团购非验证票结算")]
            TuanGou5Settlement = 3,
            [Description("过期非退款结算")]
            TuanConsumeOver = 4,
            [Description("团购卡结算")]
            TuanGou3Settlement = 5,
            [Description("订餐易结算")]
            OuterAppSettlement = 6,
            [Description("任我游门票结算")]
            TickectSettlement = 7,
            [Description("京东尚娃结算")]
            TuanJDSettlement=8,
        }

        public enum OutletsATMSearchType : int
        {
            [Description("网点")]
            Outlets = 1,
            [Description("ATM")]
            ATM = 2
        }

        public enum VendorWXReplyKeyType : int
        {
            [Description("店铺入口")]
            ShopInterface = 1,
            [Description("自定义")]
            Custom = 2,
        }


        #region 优惠券创建枚举
        public enum CouponSource : int
        {
            [Description("机构")]
            ByOrgan = 1,
            [Description("商户")]
            ByVendor = 2,
        }
        //优惠券结算方式：0 线下，1 线上
        public enum CouponSettleType : int
        {
            [Description("线下")]
            SettleOffline = 0,
            [Description("线上")]
            SettleOnline = 1,
        }
        //1是全场，2是活动分类，3是购物分类（3暂无），4是商品
        public enum CouponUseRange : int
        {
            [Description("全场")]
            All = 1,
            [Description("绑定分类")]
            V_Category = 2,
            [Description("绑定商品")]
            Product = 4,
        }

        public enum CouponType : int
        {
            [Description("线下发放")]
            SendBySelf = 1,
            [Description("线上领取")]
            SendByOnline = 2,
            [Description("注册送券")]
            SendByRegister = 3,
            [Description("满额送券")]
            SendByFulfilled = 4,
           
        }

        public enum CoupopCodeBiStatus : int
        {
            [Description("有效")]
            Valid = 0,
            [Description("无效")]
            InValid = -1,
            [Description("已使用")]
            Used = 1,

        }
        #endregion


        #region 首页模板设置枚举
        public enum HomePageTemplateType : int
        {
            [Description("最新上架")]
            OnSale = 1,
            [Description("分类推荐")]
            Group = 2,
        }
        #endregion
        public enum SendStatus : int
        {
            [Description("未发送")]
            NotSend = 0,
            [Description("已发送")]
            Send = 1
        }

        //活动专区 活动类型
        public enum OrganActivityType : int
        {
            [Description("特惠专区")]
            SpecialZone = 1,
            [Description("普通促销")]
            GeneralMarketing = 2,
        }

        #region 积分
        //证件类型(1身份证)
        public enum IDType : int
        {
            [Description("身份证")]
            PID = 1,
        }

        //积分类型()
        public enum PointType : int
        {
            [Description("注册奖励")]
            Register = 1,
            [Description("人工变更")]
            Update = 2,
            [Description("创建订单扣减积分")]
            OrderCreate = 3,
            [Description("取消订单返还积分")]
            OrderCancel = 4,
            [Description("消费奖励积分")]
            ConsumeMax = 5,
            [Description("消费冲正扣减奖励积分")]
            ConsumeMin = 6,
            [Description("退款返还扣减积分")]
            Return = 7,
            [Description("退款扣减奖励积分")]
            ReturnGetPoint = 8,
            [Description("订单签收获得积分")]
            OrderSign = 9,
            [Description("作废指定积分操作")]
            CancelPointLog = 10,
        }

        //应用系统类型(1传统商城,2通商惠活动3通商惠购物)
        public enum SysType : int
        {
            [Description("传统商城")]
            WebBussiness = 1,
            [Description("通商惠活动")]
            O2OPromotion = 2,
            [Description("通商惠购物")]
            O2OGeneral = 3,
        }

        //状态(0待提报,1待审核,2已确认,-1已作废,-2已驳回，-3已过期)
        public enum PointRequestStatus : int
        {
            [Description("待提报")]
            WaitRequest = 0,
            [Description("待审核")]
            WaitAudit = 1,
            [Description("已确认")]
            Audited = 2,
            [Description("已作废")]
            Abandon = -1,
            [Description("已驳回")]
            NoPass = -2,
            [Description("已过期")]
            Expire = -3,
        }
       
        #endregion

        public enum BasicRuleType : int 
        {
            [Description("积分兑换")]
            PointsExchange = 1,
            [Description("注册成功")]
            Register = 2,
        }
        public enum ApplyType : int
        {
            [Description("全场")]
            All = 1,
            [Description("绑定分类")]
            V_Category = 2,
            [Description("绑定商品")]
            Product = 3,
        }


        public enum MovieOrderStatus : int
        {
            [Description("未支付")]
            NoPay = -1,
            [Description("支付失败")]
            Fail = 0,
            [Description("支付成功")]
            PaySuccess = 1,
            [Description("出票中")]
            TicketDealing = 2,
            [Description("出票成功")]
            TicketSuccess = 3,
            [Description("停止写票失败")]
            StopTicketFail = 4,
            [Description("停止写票成功")]
            StopTicketSuccess = 5,

        }

        public static string GetMovieOrderStatus(object v)
        {
            return GetDescription(typeof(MovieOrderStatus), v);
        }

        //团购商品分期
        public enum ProductPromotionPeriod : int
        {
            [Description("一期")]
            One = 1,
            [Description("三期")]
            Three = 3,
            [Description("六期")]
            Six = 6,
            [Description("九期")]
            Nine = 9,
            [Description("十二期")]
            Twelve = 12,
            [Description("十五期")]
            Fifteen = 15,
            [Description("十八期")]
            Eighteen = 18,
            [Description("二十一期")]
            TwentyOne = 21,
            [Description("二十四期")]
            TwentyFour = 24,
            [Description("三十六期")]
            ThirtySix = 36,
        }
        public enum VendorLogStatus : int
        {
            [Description("未审阅")]
            NotAudit = 0,
            [Description("已审阅")]
            Audited = 1,
        }
        public enum VendorLogType : int
        {
            [Description("微信")]
            WeiXin = 0,
            [Description("结算")]
            Balance = 1,
        }
        public enum PointResult : int
        {
            [Description("保存数据异常")]
            error0 = 0,
            [Description("积分功能关闭")]
            error1 = 1,
            [Description("客户不存在")]
            error2 = 2,
            [Description("该机构未设置积分抵现金")]
            error3 = 3,
            [Description("积分不足")]
            error4 = 4,
            [Description("传入商品列表数据有误")]
            error5 = 5,
            [Description("单商品总价有误")]
            error6 = 6,
            
        }
        #region -4:已退款，-3: 退款中 ，-2: 审核未通过，-1: 已取消,0 :待审核 ，1:待支付 ， 2: 待发货(待消费) ， 3: 已发货， 4: 已签收（已消费）
        public enum WebOrderStatus : int
        {
            [Description("已退款")]
            Refunded = -4,
            [Description("退款中")]
            Refunding = -3,
            [Description("审核未通过")]
            NoAudit = -2,
            [Description("已取消")]
            Cancel = -1,
            [Description("待审核")]
            WaitAudit = 0,
            [Description("待支付")]
            WaitPay = 1,
            [Description("待发货(待消费)")]
            WaitOutStock = 2,
            [Description("已发货")]
            AlreadyOutStock = 3,
            [Description("已签收（已消费）")]
            AlreadySignin = 4,
        }
        #endregion

        #region 特殊扣佣代码，目前仅针对大连银行
        public enum SpecialCommissionCode : int
        {
            [Description("H001")]
            H001 = 0,
            [Description("H002")]
            H002 = 1,
            [Description("H003")]
            H003 = 2,
            [Description("H004")]
            H004 = 3,
            [Description("H005")]
            H005 = 4,
            [Description("H006")]
            H006 = 5,
            [Description("H007")]
            H007 = 6,
            [Description("H008")]
            H008 = 7,
            [Description("H009")]
            H009 = 8,
            [Description("H010")]
            H010 = 9,
            [Description("H011")]
            H011 = 10,
            [Description("H012")]
            H012 = 11,
            [Description("H013")]
            H013 = 12,
            [Description("H014")]
            H014 = 13,
            [Description("H015")]
            H015 = 14,
            [Description("H016")]
            H016 = 15,
            [Description("H017")]
            H017 = 16,
            [Description("H018")]
            H018 = 17,
            [Description("H019")]
            H019 = 18,
            [Description("H020")]
            H020 = 19,
            [Description("H021")]
            H021 = 20,
            [Description("H022")]
            H022 = 21,
            [Description("H023")]
            H023 = 22,
            [Description("H024")]
            H024 = 23,
            [Description("H025")]
            H025 = 24,
            [Description("H026")]
            H026 = 25,
            [Description("H027")]
            H027 = 26,
            [Description("H028")]
            H028 = 27,
            [Description("H029")]
            H029 = 28,
            [Description("H030")]
            H030 = 29,
        }
        public static SortedList GetSpecialCommissionCode()
        {
            return GetStatus(typeof(SpecialCommissionCode));
        }
        public static string GetSpecialCommissionCode(object v)
        {
            return GetDescription(typeof(SpecialCommissionCode), v);
        }
        #endregion

        #region 任我游相关枚举
        //商户类型
        public enum VendorTypeRWY : int
        {
            [Description("普通商户")]
            Default = 1,
            [Description("景点商户")]
            ScenicSpots = 2,
        }
        //门票类型
        public enum TicketTypeRWY : int
        {
            [Description("景点")]
            ScenicSpots = 1,
            [Description("温泉")]
            HotSpring = 2,
            [Description("游泳馆")]
            Natatorium = 3,
            [Description("戏水")]
            Water = 4,
            [Description("运动")]
            Movement = 5,
        }
        public enum StarRWY : int
        {
            [Description("未定义")]
            Zero = 0,
            [Description("A")]
            One = 1,
            [Description("AA")]
            Two = 2,
            [Description("AAA")]
            Three = 3,
            [Description("AAAA")]
            Four = 4,
            [Description("AAAAA")]
            Five = 5,
        }
        #endregion

        //限购方式
        public enum RestrictionType : int
        {
            [Description("会员ID限购")]
            CustomerId = 0,
            [Description("会员ID与手机号限购")]
            CustomerMobilePhone = 1,
        }

        #region 当前应用类型 1惠动民生 2掌尚达银 3鑫动青岛 4兰花汇 5新晋商（亿汇通） 6掌运生活（鑫源商城）
        public enum APPName
        {
            [Description("空")]
            None = 0,
            [Description("惠动民生")]
            HDMS = 1,
            [Description("掌尚达银")]
            BODL = 2,
            [Description("鑫动青岛")]
            XDQD = 3,
            [Description("兰花汇")]
            LanHH = 4,
            [Description("新晋商")]
            YiHT = 5,
            [Description("掌运生活")]
            XinYuan = 6,
          
        }
        #endregion

        #region 库存计数类型 (0支付减库存， 1下单减库存)
        public enum InventoryMode : int
        {
            [Description("支付减库存")]
            ZhiFu = 0,
            [Description("下单减库存")]
            XiaDan = 1,
        }
        #endregion

        public enum CheckIncomeResult
        {
            [Description("通商惠平台成功，ETS无支付记录")]
            OnlyTSH = 0,
            [Description("ETS平台成功，通商惠无支付记录")]
            OnlyETS = 1,
            [Description("金额不一致")]
            AmtDiff = 2,
        }

        public enum ConfirmForBankResult
        {
            [Description("通商惠有记录，支付系统无记录")]
            OnlyTSH = 0,
            [Description("支付系统有记录，通商惠无记录")]
            OnlyETS = 1,
            [Description("金额不一致")]
            AmtDiff = 2,
            [Description("金额一致")]
            AmtSame = 3,
        }
        public enum VendorCooprateSource
        { 
            [Description("商户合作来源WEB")]
            Web = 1,
            [Description("商户合作来源APP")]
            App = 2
        }
        public enum CouponCustomerStatus
        {
            [Description("有效")]
            Valid = 0,
            [Description("无效")]
            InValid = -1,
            [Description("已使用")]
            Used = 1,
        }

        public enum JDCouponCodeStatus
        {
            [Description("未使用")]
            UnUsed=0,
            [Description("已使用")]
            Used=1,
            [Description("已撤销")]
            Cancel=2,
            [Description("使用中")]
            Using=3,
            [Description("已过期")]
            Expired=4,
        }

        #region 生活缴费

        #region 生活缴费类型

        public enum LifeType : int
        {
            [Description("水费")]
            Water = 0,
            [Description("电费")]
            Electricity = 1,
            [Description("燃气费")]
            Gas = 2,
            [Description("有线电视费")]
            CableTV = 3,
            [Description("固话宽带费")]
            FixedLineBroadband = 4,
            [Description("话费")]
            HuaFei = 5,
        }

        public static SortedList GetLifeType()
        {
            return GetStatus(typeof(LifeType));
        }

        public static string GetLifeType(object v)
        {
            return GetDescription(typeof(LifeType), v);
        }
        #endregion


       


        #region 订单状态类型

        public enum LifeOrderType : int
        {
            [Description("待支付")]
            WaitPay = 1,
            [Description("已支付，待处理")]
            PayedProcessing = 2,
            [Description("处理中")]
            Processing = 3,
            [Description("缴费成功")]
            PaymentSuccessfully = 4,
            [Description("缴费失败")]
            PaymentFail = 5,
        }

        public static SortedList GetLifeOrderType()
        {
            return GetStatus(typeof(LifeOrderType));
        }

        public static string GetLifeOrderType(object v)
        {
            return GetDescription(typeof(LifeOrderType), v);
        }
        #endregion



       

        #region 规则汇总-销账

        public enum LifeWriteoffType : int
        {
            [Description("正常情况，备用字段一个都不填，适合大部分单位")]
            BakOne = 1,
            [Description("备用1-查询的备用1 备用2-返回记录的备用1 备用3-查询时的备用2")]
            BakTwo = 2,
            [Description("如果是分账序号:备用1-billkey 备用2-2 备用3-返回记录的备用1")]
            BakThree = 3,
            [Description("如果是条形码：备用2-1")]
            BakFour = 4,
            [Description("如果是户号缴费: 备用1-billkey，备用3-查询时的备用3")]
            BakFive = 5,
            [Description("其他情况: 备用2-查询的备用2，备用4-返回记录的备用2")]
            BakSix = 6,
            [Description("contractNo留空，customername填contractNo")]
            BakSeven = 7,
            [Description("备用1-缴费金额(pay_amount,先查询出来的要缴费金额)，以分表示")]
            BakEight = 8,
            [Description("备用1-返回记录的备用1 备用2-查询时的备用2")]//上海燃气，跟踪查询时的备用2和返回固定值是否一致
            BakNine = 9,

        }

        public static SortedList GetLifeWriteoffType()
        {
            return GetStatus(typeof(LifeWriteoffType));
        }
        #region 生活缴费

       

        #region 水电煤缴费类型(与水电煤type值相同)
        /// <summary>
        /// 水电煤缴费类型(与水电煤type值相同)
        /// </summary>
        public enum LifePayType : int
        {
            /// <summary>
            /// 水费
            /// </summary>
            [Description("缴水费")]
            WaterPay = 21,
            /// <summary>
            /// 电费
            /// </summary>
            [Description("缴电费")]
            ElectricityPay = 22,
            /// <summary>
            /// 燃气费，煤
            /// </summary>
            [Description("缴燃气费")]
            GasPay = 23,
            /// <summary>
            /// 有线电视费
            /// </summary>
            [Description("缴有线电视费")]
            CableTVPay = 26,
            /// <summary>
            /// 固话宽带费
            /// </summary>
            [Description("缴固话宽带费")]
            FixedLineBroadbandPay = 24,

        }

        public static SortedList GetLifePayType()
        {
            return GetStatus(typeof(LifePayType));
        }

        public static string GetLifePayType(object v)
        {
            return GetDescription(typeof(LifePayType), v);
        }
        #endregion




        #region 订单状态类型

        public enum LifesOrderType : int
        {
            [Description("未支付")]
            WaitPay = 1,
            [Description("已支付")]
            PayedProcessing = 2,
            [Description("处理中")]
            Processing = 3,
            [Description("缴费成功")]
            PaymentSuccessfully = 4,
            [Description("缴费失败")]
            PaymentFail = 5,
            [Description("已取消")]
            HaveCancel = 6,
            [Description("已退款")]
            HaveRefund = 7,
        }

        public static SortedList GetLifesOrderType()
        {
            return GetStatus(typeof(LifesOrderType));
        }

        public static string GetLifesOrderType(object v)
        {
            return GetDescription(typeof(LifesOrderType), v);
        }
        #endregion

        #region 规则汇总-查询

        public enum LifeSearchType : int
        {
            [Description("1个参数情况（如用电卡号,电话号码等）")] //1
            ParaOne = 1,
            [Description("2个参数情况(如条形码，金额)")] //2
            ParaTwo = 2,
            [Description("2个参数情况(如供暖用户号,付款单位)")] //2
            ParaTwoHeating = 3,
            [Description("多选3个参数情况(如类型，条形码,金额)")] //3
            ParaThree = 4,
            [Description("多选4个参数情况(如类型，分账序号，密码，查询日期)")] //4
            ParaFour = 5,
            [Description("多选2个参数情况(如类型，条形码))")] //2
            ParaTwoMultiple = 6,
            [Description("多选3个参数情况(如类型,户号，查询年月)")] //3
            ParaThreeMultiple = 7,
        }

        public static SortedList GetLifeSearchType()
        {
            return GetStatus(typeof(LifeSearchType));
        }

        public static string GetLifeSearchType(object v)
        {
            return GetDescription(typeof(LifeSearchType), v);
        }
        #endregion





        #region 规则汇总-验证

        public enum LifeValidationType : int
        {
            [Description("无规则，默认都会做不能为空验证")]
            None = 0,
            [Description("最低缴费金额为20元")]
            MoneyTwenty = 1,
            [Description("长度必须为10位")]
            LengthTen = 2,
            [Description("缴费时间段: 2：00-22：00")]
            WriteOffTimeTwo = 3,
            [Description("缴费时间段:7：00-22：00")]
            WriteOffTimeSeven = 4,
            [Description("必须输入8或11位电话号码")]
            LengthMustEightOrEleven = 5,
            [Description("号码前面必须加上0351")]
            NeedAreaCode = 6,
            [Description("固话号码前要加区号. 时间段:0：00-24：00（除周二、四及每月5日22：00至次日0:0外)")]
            TimeCode = 7,
            [Description("11位手机号码")]
            LengthEleven = 8,
            [Description("必须为8位固话号码")]
            LengthEight = 9,
            [Description("用于如果是条形码，会有一段说明文字，其它没有")]
            NeedDescription = 10,
            [Description("金额格式不正确")]
            IsMoney = 11,
            [Description("长度必须为11位")]
            IsEleven = 12,

        }

        public static SortedList GetLifeValidationType()
        {
            return GetStatus(typeof(LifeValidationType));
        }

        public static string GetLifeValidationType(object v)
        {
            return GetDescription(typeof(LifeValidationType), v);
        }
        #endregion

        

        

        

        

        

        #region 预付卡返回格式类型

        public enum LifeRemindType : int
        {
            [Description("不提醒")]
            None = 0,
            [Description("短信")]
            CellPhone = 1,
            [Description("邮件")]
            Email = 2,
        }

        public static SortedList GetLifeRemindType()
        {
            return GetStatus(typeof(LifeRemindType));
        }

        public static string GetLifeRemindType(object v)
        {
            return GetDescription(typeof(LifeRemindType), v);
        }
        #endregion

        

        #endregion
        

        

        public static string GetLifeWriteoffType(object v)
        {
            return GetDescription(typeof(LifeWriteoffType), v);
        }
        #endregion

        #region 水电煤缴费提醒返回类型

        public enum LifesRemindType : int
        {
            [Description("未设置")]
            None = 0,
            [Description("短信")]
            CellPhone = 1,
            [Description("邮件")]
            Email = 2,
        }

        public static SortedList GetLifesRemindType()
        {
            return GetStatus(typeof(LifesRemindType));
        }

        public static string GetLifesRemindType(object v)
        {
            return GetDescription(typeof(LifesRemindType), v);
        }
        #endregion

        

        #region 规则汇总-输入类型

        public enum LifeInputType : int
        {
            [Description("文本框")]
            TextBox = 1,
            [Description("下拉框")]
            DropDownList = 2,
            [Description("密码框")]
            Password = 3,
            [Description("日期")]
            DateTime = 4,
        }

        public static SortedList GetLifeInputType()
        {
            return GetStatus(typeof(LifeInputType));
        }

        public static string GetLifeInputType(object v)
        {
            return GetDescription(typeof(LifeInputType), v);
        }
        #endregion

        #region 规则汇总-金额是否编辑

        public enum LifePaymentType : int
        {
            [Description("大于(>)")]
            Greaterthan = 0,
            [Description("大于等于(>=)")]
            GreaterThanOrEqual = 1,
            [Description("等于(=)")]
            Equal = 2,
            [Description("小于等于(<=)")]
            LessThanOrEqual = 3,
            [Description("小于(<)")]
            Less = 4,
        }

        public static SortedList GetLifePaymentType()
        {
            return GetStatus(typeof(LifePaymentType));
        }

        public static string GetLifePaymentType(object v)
        {
            return GetDescription(typeof(LifePaymentType), v);
        }
        #endregion

        #region 预付卡返回格式类型

        public enum FormatType : int
        {
            [Description("XML格式")]
            xml = 1,
            [Description("JSON格式")]
            json = 2
        }

        public static SortedList GetFormatType()
        {
            return GetStatus(typeof(FormatType));
        }

        public static string GetFormatType(object v)
        {
            return GetDescription(typeof(FormatType), v);
        }
        #endregion

        #region 预付卡查询,销帐返回说明

        public enum LifeRspCodeType : int
        {
            [Description("查询成功")]
            S0000 = 1,
            [Description("缴费成功")]
            W0000 = 2,
            [Description("缴费超时")]
            SX12 = 3,
            [Description("暂不支持")]
            DE12 = 4,
            [Description("系统繁忙,请稍后再试")]
            DE11 = 5,
            [Description("该户号不存在")]
            DE10 = 6,
            [Description("超过限定金额")]
            DE09 = 7,
            [Description("系统繁忙,请稍后再试")]
            DE07 = 8,
            [Description("暂时无法缴费")]
            DE06 = 9,
            [Description("超过缴费时间")]
            DE05 = 10,
            [Description("超过受理期,不予受理,请至缴费单位缴费")]
            DE04 = 11,
            [Description("系统繁忙,请稍后再试")]
            DE03 = 12,
            [Description("未出账或已经缴纳,暂时无需缴费")]
            DE02 = 13,
            [Description("未出账或已经缴纳,暂时无需缴费")]
            DE01 = 14,
            [Description("尚未开通此业务")]
            NP04 = 15,
            [Description("系统繁忙,请稍后再试")]
            NP03 = 16,
            [Description("系统繁忙,请稍后再试")]
            NP02 = 17,
            [Description("系统繁忙,请稍后再试")]
            NP01 = 18,
            [Description("登记交易流水出错")]
            SS02 = 19,
            [Description("系统数据出错")]
            SS01 = 20,
            [Description("金额应>=30,<=2000,金额以10元递增")]
            HZ01 = 21,
            [Description("查询超时")]
            TOUT = 22,
            [Description("缴费权限未开通")]
            CPNY = 23,
            [Description("未出账或已经缴纳,暂时无需缴费")]
            DJ02 = 24,

            //浦发接口返回说明 
            [Description("系统繁忙,请稍后再试")]
            SP01 = 25,
            [Description("系统繁忙,请稍后再试")]
            SP02 = 26,
            [Description("系统繁忙,请稍后再试")]
            SP03 = 27,
            [Description("尚未开通此业务")]
            SP04 = 28,
            [Description("系统繁忙,请稍后再试")]
            SP05 = 29,
            [Description("未出账或已经缴纳,暂时无需缴费")]
            DJ01 = 30,
            //[Description("未出账或已经缴纳,暂时无需缴费")]
            //DJ02 = 31,
            [Description("系统繁忙,请稍后再试")]
            DJ03 = 32,
            [Description("超过受理期，请联系公共事业单位")]
            DJ04 = 33,
            [Description("超过公共事业单位受理时间，请在有效时间段内缴费")]
            DJ05 = 34,
            [Description("暂时无法缴费,请联系公共事业单位")]
            DJ06 = 35,
            [Description("系统繁忙,请稍后再试")]
            DJ07 = 36,
            [Description("预付费交易金额不足")]
            DJ08 = 37,
            [Description("超过限定金额")]
            DJ09 = 38,
            [Description("该户号不存在")]
            DJ10 = 39,
            [Description("系统繁忙,请稍后再试")]
            DJ11 = 40,
            [Description("该用户不支持网上缴费，请联系公共事业单位")]
            DJ12 = 41,
            [Description("输入参数有误")]
            DJ13 = 42,
            [Description("自定义信息")]
            DJ14 = 43,
            [Description("无此扣款账户")]
            EG06 = 44,
            [Description("扣款失败")]
            EG21 = 45,
            [Description("该前台流水已扣款")]
            EG90 = 46,
            [Description("预付费交易金额不足")]
            DE08 = 47,
            [Description("金额不符合规则")]
            DE13 = 48,
            [Description("卡过期或状态无效，请联系公共事业单位！")]
            DE14 = 49,
            [Description("卡为副卡或不存在，请联系公共事业单位！")]
            DE15 = 50,
            [Description("密码错误，请重新输入！")]
            DE16 = 51,


        }

        public static SortedList GetLifeRspCodeType()
        {
            return GetStatus(typeof(LifeRspCodeType));
        }

        public static string GetLifeRspCodeType(object v)
        {
            return GetDescription(typeof(LifeRspCodeType), v);
        }
        #endregion

        #region 预付卡返回格式类型

        public enum LifeQueryBackType : int
        {
            [Description("一般的返回情况，适合大多数收费单位")]
            One = 0,
            [Description("特殊情况,如太原自来水")]
            Two = 1
        }

        public static SortedList GetLifeQueryBackType()
        {
            return GetStatus(typeof(LifeQueryBackType));
        }

        public static string GetLifeQueryBackType(object v)
        {
            return GetDescription(typeof(LifeQueryBackType), v);
        }
        #endregion



        #region 规则汇总-输入提示

        public enum LifeInputPrompt : int
        {
            [Description("因条形码每月改变，常付账单内容将不记录条形码。")]
            Barcode = 0,
            [Description("")]
            StringNull = 1,
        }

        public static SortedList GetLifeInputPrompt()
        {
            return GetStatus(typeof(LifeInputPrompt));
        }

        public static string GetLifeInputPrompt(object v)
        {
            return GetDescription(typeof(LifeInputPrompt), v);
        }
        #endregion

        #endregion
        #region 手机充值订单状态;
        public enum MobilePayStatus : int
        {
            [Description("未支付")]
            New = 1,
            [Description("已支付")]
            Payed = 2,
            [Description("处理中")]
            Dealing = 3,
            [Description("充值成功")]
            Success = 4,
            [Description("已取消")]
            Canceled = -2,
            [Description("充值失败")]
            Failed = -1,
            [Description("已退款")]
            Refunded = -96

        }
        public static SortedList GetMobliePayStatus()
        {
            return GetStatus(typeof(MobilePayStatus));
        }
        public static string GetMobliePayStatus(object v)
        {
            return GetDescription(typeof(MobilePayStatus), v);
        }
      

        #endregion
     
        //来源：1机构，2商户
        public enum Resource
        {
            [Description("机构")]
            Organ = 1,
            [Description("商户")]
            Vendor = 2,
        }

        #region 文件上传类型 1Html文件
        public enum FileUploadType : int
        {
            [Description("Html广告位文件")]
            AdvHtml = 1,
        }
        #endregion

        #region 消息类型：1：图文信息 2：消息链接
        public enum PushType
        {
            [Description("图文信息")]
            GraphicInformation = 1,
            [Description("消息链接")]
            NewsLinks = 2,
        }

        #endregion PushCustomer表状态 -1删除 0未读 1已读
        public enum PushStatus
        {
            [Description("删除")]
            Delete = -1,
            [Description("未读")]
            Unread = 0,
            [Description("已读")]
            Read = 1,
        }
        #region OrderPay 0：原始1：已支付
        public enum OrderPay
        {
            [Description("原始")]
            UnPay = 0,
            [Description("已支付")]
            Pay = 1,
        }
        #endregion

        #region 秒杀抢购类型

        public enum CutType:int
        {
            [Description("抢购")]
            QiangGou = 1,
            [Description("秒杀")]
            MiaoSha = 2,

        }
        public static SortedList GetCutType()
        {
            return GetStatus(typeof(CutType));
        }
        #endregion

        #region 支付记录状态 0待支付 1已支付 -1已作废
        public enum OrderPayStatus : int
        {
            [Description("待支付")]
            WaitPay = 0,
            [Description("已支付")]
            Payed = 1,
            [Description("已作废")]
            Abandon = -1,
        }
        #endregion

    }
}


