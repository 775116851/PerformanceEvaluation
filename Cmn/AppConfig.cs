using System;
using System.Collections.Generic;

using System.Text;
using System.Configuration;

namespace PerformanceEvaluation.Cmn
{
    public class AppConfig 
    {
        private AppConfig()
        {
            //1
        }

        public static string LoginUrl
        {
            get { return ConfigurationManager.AppSettings["EACUrl"]; }
        }
        #region 数据库链接
        //日志库
        public static string Conn_Log
        {
            get { return ConfigurationManager.ConnectionStrings["Conn_Log"].ToString(); }
        }
        //邮件短信库
        public static string Conn_Mail
        {
            get { return ConfigurationManager.ConnectionStrings["Conn_Mail"].ToString(); }
        }
        //默认读库
        public static string Conn_Default
        {
            get
            {
                ConnectionStringSettings connect = ConfigurationManager.ConnectionStrings["Conn_Default"];
                if (connect != null)
                    return connect.ToString();
                else return string.Empty;
            }
        }
        //客户库
        public static string Conn_Customer
        {
            get { return ConfigurationManager.ConnectionStrings["Conn_Customer"].ToString(); }
        }
        //客户读库
        public static string Conn_Customer_Read
        {
            get { return ConfigurationManager.ConnectionStrings["Conn_Customer_Read"].ToString(); }
        }
        //平台库
        public static string Conn_IPP
        {
            get { return ConfigurationManager.ConnectionStrings["Conn_IPP"].ToString(); }
        }
        //平台读库
        public static string Conn_IPP_Read
        {
            get { return ConfigurationManager.ConnectionStrings["Conn_IPP_Read"].ToString(); }
        }
        //应用库
        public static string Conn_O2O2
        {
            get { return ConfigurationManager.ConnectionStrings["Conn_O2O2"].ToString(); }
        }
        //应用读库
        public static string Conn_O2O2_Read
        {
            get { return ConfigurationManager.ConnectionStrings["Conn_O2O2_Read"].ToString(); }
        }
        #endregion
        public static string FarDeskUser
        {
            get { return ConfigurationManager.AppSettings["FarDeskUser"]; }
        }

        public static string FarDeskPwd
        {
            get { return ConfigurationManager.AppSettings["FarDeskPwd"]; }
        }

        public static int DefaultOrgan
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultOrgan"]); }
        }

        public static string UploadFilePath
        {
            get { return ConfigurationManager.AppSettings["UploadFilePath"]; }
        }

        public static string UploadFileUserPath
        {
            get { return ConfigurationManager.AppSettings["UploadFileUserPath"]; }
        }

        /// <summary>
        /// 掌上生活首页广告轮播位的SyssNo
        /// </summary>
        public static string AppHomeAdvSysNo
        {
            get { return ConfigurationManager.AppSettings["AppHomeAdvSysNo"]; }
        }

        public static string WebResource
        {
            get
            {
                return ConfigurationManager.AppSettings["WebResource"];
            }
        }

        public static string IsFilterPort
        {
            get { return ConfigurationManager.AppSettings["IsFilterPort"]; }
        }

        public static string Domain
        {
            get { return ConfigurationManager.AppSettings["Domain"]; }
        }

        public static string ErrorLogFolder
        {
            get { return ConfigurationManager.AppSettings["ErrorLogFolder"]; }
        }

       
        public static string HomeUrl
        {
            get { return ConfigurationManager.AppSettings["HomeUrl"]; }
        }

        public static string ServerName
        {
            get { return ConfigurationManager.AppSettings["ServerName"]; }
        }

        public static string UserPortblUrl
        {
            get { return ConfigurationManager.AppSettings["UserPortblUrl"]; }
        }

        /// <summary>
        /// 统一会员缴费查询URL
        /// </summary>
        public static string PayPortblUrl
        {
            get { return ConfigurationManager.AppSettings["PayPortblUrl"]; }
        }
        #region 微电商 
        public static string ApiBasePath 
        {
            get { return ConfigurationManager.AppSettings["ApiBasePath"]; } 
        }
        public static string ApiAppKey
        {
            get { return ConfigurationManager.AppSettings["AppKey"]; }
        }
        public static string AppValue
        {
            get { return ConfigurationManager.AppSettings["AppValue"]; }
        }
        public static string VendorCategorySysno
        {
            get { return ConfigurationManager.AppSettings["VendorCategorySysno"]; }
        }
        public static string QRCodeImagesAddress
        {
            get { return ConfigurationManager.AppSettings["QRCodeImagesAddress"]; }
        }
        #endregion

        /// <summary>
        /// http请求的ContentType
        /// </summary>
        public static string APIContentType
        {
            get
            {
                return ConfigurationManager.AppSettings["APIContentType"];
            }
        }

        #region 商户接口配置
        public static bool IsSendDataToGZ
        {
            get { return ConfigurationManager.AppSettings["IsSendDataToGZ"].ToString() == "1" ? true : false; }
        } 

        public static string APIAddressOfGZ
        {
            get { return ConfigurationManager.AppSettings["APIAddressOfGZ"]; }
        }

        public static string APIUserOfGZ
        {
            get { return ConfigurationManager.AppSettings["APIUserOfGZ"]; }
        }
       
        public static string APIPwdOfGZ
        {
            get { return ConfigurationManager.AppSettings["APIPwdOfGZ"]; }
        }

        public static bool IsSendDataToYF
        {
            get { return ConfigurationManager.AppSettings["IsSendDataToYF"].ToString() == "1" ? true : false; }
        } 

        public static string APIAddressOfYF
        {
            get { return ConfigurationManager.AppSettings["APIAddressOfYF"]; }
        }

        public static string APIUserOfYF
        {
            get { return ConfigurationManager.AppSettings["APIUserOfYF"]; }
        }

        public static string APIPwdOfYF
        {
            get { return ConfigurationManager.AppSettings["APIPwdOfYF"]; }
        }
        public static string WeiXinUrl
        {
            get { return ConfigurationManager.AppSettings["WeiXinUrl"]; }
        }
        //应用中心
        public static bool IsSendDataToYYZX
        {
            get { return ConfigurationManager.AppSettings["IsSendDataToYYZX"].ToString() == "1" ? true : false; }
        }

        public static string APIAddressOfYYZX
        {
            get { return ConfigurationManager.AppSettings["APIAddressOfYYZX"]; }
        }

        public static string APIUserOfYYZX
        {
            get { return ConfigurationManager.AppSettings["APIUserOfYYZX"]; }
        }

        public static string APIPwdOfYYZX
        {
            get { return ConfigurationManager.AppSettings["APIPwdOfYYZX"]; }
        }
        #endregion 

        #region 电子凭证接口
        public static bool IsCreateCardFromYF
        {
            get { return ConfigurationManager.AppSettings["IsCreateCardFromYF"].ToString() == "1" ? true : false; }
        }

        public static string CardAddressOfYF
        {
            get { return ConfigurationManager.AppSettings["CardAddressOfYF"]; }
        }

        public static string CardUserOfYF
        {
            get { return ConfigurationManager.AppSettings["CardUserOfYF"]; }
        }

        public static string CardPwdOfYF
        {
            get { return ConfigurationManager.AppSettings["CardPwdOfYF"]; }
        }

        public static string CardSmsAppID
        {
            get { return ConfigurationManager.AppSettings["CardSmsAppID"]; }
        }

        public static int CardOrganSysNo
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["CardOrganSysNo"]); }
        } 
        #endregion

        #region 电子会员卡接口配置
        /// <summary>
        /// 电子会员卡产品充值名称
        /// </summary>
        public static string ECardProductName
        {
            get
            {
                return ConfigurationManager.AppSettings["ECardProductName"];
            }
        }
        /// <summary>
        /// 电子会员卡会员所属通联机构号
        /// </summary>
        public static int ECardAllInPayOrganSysNo
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["ECardAllInPayOrganSysNo"]);
            }
        }
        /// <summary>
        /// 电子会员卡短信ID
        /// </summary>
        public static string ECardSMSID
        {
            get
            {
                return ConfigurationManager.AppSettings["ECardSMSID"];
            }
        }
        /// <summary>
        /// 电子会员卡系统接入Key
        /// </summary>
        public static string APIUserOfCard
        {
            get
            {
                return ConfigurationManager.AppSettings["APIUserOfCard"];
            }
        }

        /// <summary>
        /// 电子会员卡系统接入Key密钥
        /// </summary>
        public static string APIPwdOfCard
        {
            get
            {
                return ConfigurationManager.AppSettings["APIPwdOfCard"];
            }
        }

        /// <summary>
        /// 电子会员卡 开户机构编号
        /// </summary>
        public static string APIOpenBrh
        {
            get
            {
                return ConfigurationManager.AppSettings["APIOpenBrh"];
            }
        }

        /// <summary>
        /// 电子会员卡 持续有效期
        /// </summary>
        public static string APIECardKeepYears
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardKeepYears"];
            }
        }

        /// <summary>
        /// 电子会员卡 新增个人客户
        /// </summary>
        public static string APIECardAdd
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardAdd"];
            }
        }

        /// <summary>
        /// 电子会员卡 新增个人客户返回目录节点
        /// </summary>
        public static string APIECardAddResponse
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardAddResponse"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡充值申请
        /// </summary>
        public static string APIECardSinApply
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardSinApply"];
            }
        }

        /// <summary>
        /// 电子会员卡 充值申请返回目录节点
        /// </summary>
        public static string APIECardRechargeSubmit
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardRechargeSubmit"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡充值确认
        /// </summary>
        public static string APIECardSinAdd
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardSinAdd"];
            }
        }

        /// <summary>
        /// 电子会员卡 充值确认返回目录节点
        /// </summary>
        public static string APIECardRechargeConfirm
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardRechargeConfirm"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡密码重置
        /// </summary>
        public static string APIECardUpdatePSW
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardUpdatePSW"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡修改密码
        /// </summary>
        public static string APIECardChangePSW
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardChangePSW"];
            }
        }

        /// <summary>
        /// 电子会员卡 重置密码返回目录节点
        /// </summary>
        public static string APIECardUpdatePSWResponse
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardUpdatePSWResponse"];
            }
        }

        /// <summary>
        /// 电子会员卡 修改密码返回目录节点
        /// </summary>
        public static string APIECardChangePSWResponse
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardChangePSWResponse"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡冻结
        /// </summary>
        public static string APIECardFreez
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardFreez"];
            }
        }

        /// <summary>
        /// 电子会员卡 冻结卡返回目录节点
        /// </summary>
        public static string APIECardFreezResponse
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardFreezResponse"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡解冻
        /// </summary>
        public static string APIECardUnFreez
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardUnFreez"];
            }
        }

        /// <summary>
        /// 电子会员卡 解冻卡返回目录节点
        /// </summary>
        public static string APIECardUnFreezResponse
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardUnFreezResponse"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡信息查询
        /// </summary>
        public static string APIECardDetail
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardDetail"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡查询信息返回目录节点
        /// </summary>
        public static string APIECardDetailResponse
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardDetailResponse"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡信息查询
        /// </summary>
        public static string APIECardCancel
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardCancel"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡查询信息返回目录节点
        /// </summary>
        public static string APIECardCancelResponse
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardCancelResponse"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡查询交易流水
        /// </summary>
        public static string APIECardDealList
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardDealList"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡查询交易流水返回目录节点
        /// </summary>
        public static string APIECardDealListResponse
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardDealListResponse"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡查询充值记录
        /// </summary>
        public static string APIECardRechargelList
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardRechargelList"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡查询充值记录返回目录节点
        /// </summary>
        public static string APIECardRechargelListResponse
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardRechargelListResponse"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡消费
        /// </summary>
        public static string APIECardConsume
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardConsume"];
            }
        }

        /// <summary>
        /// 电子会员卡 单卡消费返回目录节点
        /// </summary>
        public static string APIECardConsumeResponse
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardConsumeResponse"];
            }
        }

        /// <summary>
        /// 电子会员卡 换卡
        /// </summary>
        public static string APIECardChange
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardChange"];
            }
        }

        /// <summary>
        /// 电子会员卡 换卡返回目录节点
        /// </summary>
        public static string APIECardChangeResponse
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardChangeResponse"];
            }
        }


        /// <summary>
        /// 单个用户电子会员卡列表
        /// </summary>
        public static string APIECardAccount
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardAccount"];
            }
        }

        /// <summary>
        /// 单个用户电子会员卡列表返回目录节点
        /// </summary>
        public static string APIECardAccountResponse
        {
            get
            {
                return ConfigurationManager.AppSettings["APIECardAccountResponse"];
            }
        }

        #endregion

        #region MemCached有效时间设置
        /// <summary>
        /// 前台网站基础信息有效时间
        /// </summary>
        public static string MemCachedTimeWebBasic
        {
            get { return ConfigurationManager.AppSettings["MemCachedTimeWebBasic"]; }
        }

        /// <summary>
        /// 前台网站广告位有效时间
        /// </summary>
        public static string MemCachedTimeWebAdv
        {
            get { return ConfigurationManager.AppSettings["MemCachedTimeWebAdv"]; }
        }

        /// <summary>
        /// 前台网站首页产品以及类目有效时间
        /// </summary>
        public static string MemCachedTimeWebIndexProduct
        {
            get { return ConfigurationManager.AppSettings["MemCachedTimeWebIndexProduct"]; }
        } 

        /// <summary>
        /// API基础信息缓存时间,单位分钟
        /// </summary>
        public static string MemCachedTimeAppBasicInfo
        {
            get { return ConfigurationManager.AppSettings["MemCachedTimeAppBasicInfo"]; }
        }

        /// <summary>
        /// API验证码的有效时间,单位分钟
        /// </summary>
        public static string MemCachedTimeAppVerifyCode
        {
            get { return ConfigurationManager.AppSettings["MemCachedTimeAppVerifyCode"]; }
        } 
        #endregion

        /// <summary>
        /// 商户平台的管理员角色编号
        /// </summary>
        public static int VendorRoleAdminSysNo
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["VendorRoleAdminSysNo"]); }
        }

        /// <summary>
        /// 商户平台的操作员角色编号
        /// </summary>
        public static int VendorRoleOptorSysNo
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["VendorRoleOptorSysNo"]); }
        }
         
        /// <summary>
        /// 后台上传促销图片路径
        /// </summary>
        public static string UploadPromotionPath
        {
            get { return ConfigurationManager.AppSettings["UploadPromotionPath"]; }
        }

        /// <summary>
        /// 机构编号
        /// </summary>
        public static string OrganSysno
        {
            get { return ConfigurationManager.AppSettings["OrganSysno"]; }
        }
        /// <summary>
        /// 发送订单信息限制次数
        /// </summary>
        public static string SendPwdTimes
        {
            get { return ConfigurationManager.AppSettings["SendPwdTimes"]; }
        }

        /// <summary>
        /// 产生结算单服务启动时间 单位小时 如1点
        /// </summary>
        public static string StartUpTime
        {
            get { return ConfigurationManager.AppSettings["StartUpTime"]; }
        }
        public static string CommentStartUpTime
        {
            get { return ConfigurationManager.AppSettings["CommentStartUpTime"]; }
        }

        #region 各楼层c2系统编号
        public static string Floor1C2Sysno
        {
            get { return ConfigurationManager.AppSettings["Floor1C2Sysno"]; }
        }

        public static string Floor2C2Sysno
        {
            get { return ConfigurationManager.AppSettings["Floor2C2Sysno"]; }
        }

        public static string Floor3C2Sysno
        {
            get { return ConfigurationManager.AppSettings["Floor3C2Sysno"]; }
        }

        public static string Floor4C2Sysno
        {
            get { return ConfigurationManager.AppSettings["Floor4C2Sysno"]; }
        }

        public static string Floor5C2Sysno
        {
            get { return ConfigurationManager.AppSettings["Floor5C2Sysno"]; }
        }
        #endregion

        #region 商品分类页各大类价格区间
        /// <summary>
        /// 餐饮美食价格区间
        /// </summary>
        public static string MeishiPriceRange
        {
            get { return ConfigurationManager.AppSettings["MeishiPriceRange"]; }
        }

        /// <summary>
        /// 丽人价格区间
        /// </summary>
        public static string LirenPriceRange
        {
            get { return ConfigurationManager.AppSettings["LirenPriceRange"]; }
        }

        /// <summary>
        /// 文化娱乐价格区间
        /// </summary>
        public static string WenhuaPriceRange
        {
            get { return ConfigurationManager.AppSettings["WenhuaPriceRange"]; }
        }

        /// <summary>
        /// 运动健康价格区间
        /// </summary>
        public static string YundongPriceRange
        {
            get { return ConfigurationManager.AppSettings["YundongPriceRange"]; }
        }

        /// <summary>
        /// 教育培训价格区间
        /// </summary>
        public static string JiaoyuPriceRange
        {
            get { return ConfigurationManager.AppSettings["JiaoyuPriceRange"]; }
        }



        #endregion

        #region 各大类系统编号

        /// <summary>
        /// 餐饮美食系统编号
        /// </summary>
        public static string MeishiSysno
        {
            get { return ConfigurationManager.AppSettings["MeishiSysno"]; }
        }

        /// <summary>
        /// 丽人系统编号
        /// </summary>
        public static string LirenSysno
        {
            get { return ConfigurationManager.AppSettings["LirenSysno"]; }
        }

        /// <summary>
        /// 文化娱乐系统编号
        /// </summary>
        public static string WenhuaSysno
        {
            get { return ConfigurationManager.AppSettings["WenhuaSysno"]; }
        }

        /// <summary>
        /// 运动健康系统编号
        /// </summary>
        public static string YundongSysno
        {
            get { return ConfigurationManager.AppSettings["YundongSysno"]; }
        }

        /// <summary>
        /// 教育培训系统编号
        /// </summary>
        public static string JiaoyuSysno
        {
            get { return ConfigurationManager.AppSettings["JiaoyuSysno"]; }
        }
        #endregion

        #region 机构id
        public static string OrganId
        {
            get { return ConfigurationManager.AppSettings["OrganId"]; }
        }
        #endregion

        #region 活动主图片尺寸
        public static string PromotionPicSize
        {
            get { return ConfigurationManager.AppSettings["PromotionPicSize"]; }
        }

        public static string PromotionPicBigSize
        {
            get { return ConfigurationManager.AppSettings["PromotionPicBigSize"]; }
        }

         public static string PromotionPicFLSize
        {
            get { return ConfigurationManager.AppSettings["PromotionPicFLSize"]; }
        }
        //2014-1-21 wangxl  商品详情页右边商户其他商品图片
         public static string PromotionPicRightSize
         {
             get { return ConfigurationManager.AppSettings["PromotionPicRightSize"]; }
         }

        #endregion

        #region 商户logo图片尺寸
        public static string VendorLogoPicSize
        {
            get { return ConfigurationManager.AppSettings["VendorLogoPicSize"]; }
        }
        public static string VendorLogoPicBigSize
        {
            get { return ConfigurationManager.AppSettings["VendorLogoPicBigSize"]; }
        }
        #endregion

        #region 商户图片尺寸
        public static string VendorPicBigSize
        {
            get { return ConfigurationManager.AppSettings["VendorPicBigSize"]; }
        }

        public static string VendorPicSmallSize
        {
            get { return ConfigurationManager.AppSettings["VendorPicSmallSize"]; }
        }
        #endregion

        #region 图片地址
        public static string ImageUrl
        {
            get { return ConfigurationManager.AppSettings["ImageUrl"]; }
        }
        #endregion

        #region 默认城市系统编号
        public static string DefaultCitySysNo
        {
            get { return ConfigurationManager.AppSettings["DefaultCitySysNo"]; }
        }
        #endregion

        #region 即将过期订单天数范围
        public static string ExpiringDay
        {
            get { return ConfigurationManager.AppSettings["ExpiringDay"]; }
        }
        #endregion


        #region 第三方登录
        public static string QQID
        {
            get { return ConfigurationManager.AppSettings["QQAPPID"]; }
        }
        public static string QQKey
        {
            get { return ConfigurationManager.AppSettings["QQKEY"]; }
        }
        #endregion

        #region 通联支付支付
        public static string WSTLPayGate
        {
            get { return ConfigurationManager.AppSettings["WSTLPayGate"]; }
        }
        public static string WSTLPayBackUrl
        {
            get { return ConfigurationManager.AppSettings["WSTLPayBackUrl"]; }
        }
        public static string WSTLPayBackBakUrl
        {
            get { return ConfigurationManager.AppSettings["WSTLPayBackBakUrl"]; }
        }
        public static string WSTLPayID
        {
            get { return ConfigurationManager.AppSettings["WSTLPayID"]; }
        }
        public static string WSTLPayPass
        {
            get { return ConfigurationManager.AppSettings["WSTLPayPass"]; }
        }

        public static string ECardPayBackUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ECardPayBackUrl"];
            }
        }
        public static string ECardPayBackBakUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ECardPayBackBakUrl"];
            }
        }
        public static string AllinPayType
        {
            get
            {
                return ConfigurationManager.AppSettings["AllinPayType"];
            }
        }
        #endregion

        #region 邮件短信服务

        /// <summary>
        /// 一次从表中取记录数量
        /// </summary>
        public static string SendCount
        {
            get { return ConfigurationManager.AppSettings["SendCount"]; }
        }

        /// <summary>
        /// 循环发送短信邮件时，每次停顿多长时间 单位毫秒
        /// </summary>
        public static string IntervalPause
        {
            get { return ConfigurationManager.AppSettings["IntervalPause"]; }
        }

        /// <summary>
        /// 短信异常配置,多个用逗号分隔
        /// </summary>
        public static string ExceptionPhone
        {
            get { return ConfigurationManager.AppSettings["ExceptionPhone"]; }
        }

        /// <summary>
        /// 邮件异常配置,多个用逗号分隔
        /// </summary>
        public static string ExceptionEmail
        {
            get { return ConfigurationManager.AppSettings["ExceptionEmail"]; }
        }

        /// <summary>
        /// 短信邮件允许最大的重复数量
        /// </summary>
        public static string AllowMaxRepeateCount
        {
            get { return ConfigurationManager.AppSettings["AllowMaxRepeateCount"]; }
        }

        /// <summary>
        /// 异常或重复预警时，暂停时间单位毫秒
        /// </summary>
        public static string WarningStop
        {
            get { return ConfigurationManager.AppSettings["WarningStop"]; }
        }
       
        #endregion
        #region 游娃娃
        /// <summary>
        /// 游娃娃各楼层配置
        /// </summary>
        public static string YWWFloor
        {
            get { return ConfigurationManager.AppSettings["YWWFloor"]; }
        }

        /// <summary>
        /// 导游商品c3系统编号
        /// </summary>
        public static string DYC3SysNos
        {
            get { return ConfigurationManager.AppSettings["DYC3SysNos"]; }
        }

        /// <summary>
        /// 前台上传图片路径
        /// </summary>
        public static string UploadPath
        {
            get { return ConfigurationManager.AppSettings["UploadPath"]; }
        }

         /// <summary>
        /// 邮政景点特征属性系统编号
        /// </summary>
        public static string PropertySysNo
        {
            get { return ConfigurationManager.AppSettings["PropertySysNo"]; }
        }

        /// <summary>
        /// 游娃娃头部商户配置
        /// </summary>
        public static string PageHeadVendor
        {
            get { return ConfigurationManager.AppSettings["PageHeadVendor"]; }
        }

        /// <summary>
        /// 游娃娃第一层商家信息
        /// </summary>
        public static string FirtFloorInfo
        {
            get { return ConfigurationManager.AppSettings["FirtFloorInfo"]; }
        }

        /// <summary>
        /// 2个订单最小间隔时间，以分钟为单位
        /// </summary>
        public static int MinOrderMinute
        {
            get { return int.Parse(ConfigurationManager.AppSettings["MinOrderMinute"]); }
        }
        
        #endregion

        /// <summary>
        /// PayFilePath
        /// </summary>
        public static string PayFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["PayFilePath"];
            }
        }

        /// <summary>
        /// PublicKey
        /// </summary>
        public static string PublicKey
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicKey"];
            }
        }

        /// <summary>
        /// 通联支付手机注册地址
        /// </summary>
        public static string UniteCustomerMobileUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["UniteCustomerMobileUrl"];
            }
        }

        /// <summary>
        /// 通联支付注销地址
        /// </summary>
        public static string UniteCustomerLogOutUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["UniteCustomerLogOutUrl"];
            }
        }

        /// <summary>
        /// 通联支付登录地址
        /// </summary>
        public static string UniteCustomerLoginUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["UniteCustomerLoginUrl"];
            }
        }

        /// <summary>
        /// 通联支付找回密码地址
        /// </summary>
        public static string UniteCustomerFindPassUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["UniteCustomerFindPassUrl"];
            }
        }
        /// <summary>
        /// PublicKey
        /// </summary>
        public static string UniteCustomerReturnUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["UniteCustomerReturnUrl"];
            }
        }

        /// <summary>
        /// PublicKey
        /// </summary>
        public static string UniteCustomerDesKey
        {
            get
            {
                return ConfigurationManager.AppSettings["UniteCustomerDesKey"];
            }
        }

        #region IPPView配置

        public static readonly string SSOUrl = ConfigurationManager.AppSettings["SSOUrl"] ?? string.Empty;

        public static readonly string AppKey = ConfigurationManager.AppSettings["AppKey"] ?? string.Empty;

        public static readonly string AppVersion = ConfigurationManager.AppSettings["AppVersion"] ?? string.Empty;

        public static readonly string SignKey = ConfigurationManager.AppSettings["SignKey"] ?? string.Empty;

        #endregion

        #region 快递配置
        public static string ExpressMaxRepeatCount
        {
            get
            {
                return ConfigurationManager.AppSettings["ExpressMaxRepeatCount"];
            }
        }
        public static string ExpressCallBackURL
        {
            get
            {
                return ConfigurationManager.AppSettings["ExpressCallBackURL"];
            }
        }
        public static string ExpressAPIURL
        {
            get
            {
                return ConfigurationManager.AppSettings["ExpressAPIURL"];
            }
        }
        public static string ExpressAPISearchURL
        {
            get
            {
                return ConfigurationManager.AppSettings["ExpressAPISearchURL"];
            }
        }
        public static string ExpressKey
        {
            get
            {
                return ConfigurationManager.AppSettings["ExpressKey"];
            }
        }
        public static string ExpressSearchKey
        {
            get
            {
                return ConfigurationManager.AppSettings["ExpressSearchKey"];
            }
        }
        public static string ExpressSubscribeTimeInterval
        {
            get
            {
                return ConfigurationManager.AppSettings["ExpressSubscribeTimeInterval"];
            }
        }
        public static string ExpressSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["ExpressSecret"];
            }
        }
        #endregion
        #region 京东尚娃
        public static string JDMD5APPKEY
        {
            get
            {
                return ConfigurationManager.AppSettings["JDMD5APPKEY"];
            }
        }

        public static string JDMD5SIGNKEY
        {
            get
            {
                return ConfigurationManager.AppSettings["JDMD5SIGNKEY"];
            }
        }
        public static string JDFaMaURL
        {
            get
            {
                return ConfigurationManager.AppSettings["JDFaMaURL"];
            }
        }
        public static string JDSearchURL
        {
            get
            {
                return ConfigurationManager.AppSettings["JDSearchURL"];
            }
        }
        public static string JDRefundURL
        {
            get
            {
                return ConfigurationManager.AppSettings["JDRefundURL"];
            }
        }

        #endregion
        #region 大连银行o2o
        /// <summary>
        /// 页面监控标签
        /// </summary>
        public static string PayID
        {
            get
            {
                return ConfigurationManager.AppSettings["PayID"];
            }
        }

        public static string MerName
        {
            get
            {
                return ConfigurationManager.AppSettings["MerName"];
            }
        }

        public static string PayPeriodID
        {
            get
            {
                return ConfigurationManager.AppSettings["PayPeriodID"];
            }
        }



        public static string CerPass
        {
            get
            {
                return ConfigurationManager.AppSettings["CerPass"];
            }
        }

        public static string TuanUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["TuanUrl"];
            }
        }

        public static string ShopingHomeUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ShopingHomeUrl"];
            }
        }


        public static string BoDlCity
        {
            get
            {
                return ConfigurationManager.AppSettings["BoDlCity"];
            }
        }

        public static string CasReturnUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["CasReturnUrl"];
            }
        }

        public static string CasAppKey
        {
            get
            {
                return ConfigurationManager.AppSettings["CasAppKey"];
            }
        }

        public static string CasPwd
        {
            get
            {
                return ConfigurationManager.AppSettings["CasPwd"];
            }
        }


        #endregion

        #region 民生银行
        /// <summary>
        /// 统一登陆修改密码地址
        /// </summary>
        public static string SaleChannelSysNo
        {
            get
            {
                return ConfigurationManager.AppSettings["SaleChannelSysNo"];
            }
        }

        /// <summary>
        /// 分类页价格显示
        /// </summary>
        public static string CategorySearchPriceShow
        {
            get
            {
                return ConfigurationManager.AppSettings["CategorySearchPriceShow"];
            }
        }

        /// <summary>
        /// 民生银行电子票务2级分类页面
        /// </summary>
        public static string MinShengDianziPiaoC2sysno
        {
            get
            {
                return ConfigurationManager.AppSettings["MinShengDianziPiaoC2sysno"];
            }
        }

        /// <summary>
        /// 民生银行卡券2级分类页面
        /// </summary>
        public static string MinShengKaQuanC2sysno
        {
            get
            {
                return ConfigurationManager.AppSettings["MinShengKaQuanC2sysno"];
            }
        }

        public static string ShoppingTuanUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ShoppingTuanUrl"];
            }
        }
        public static string ShoppingTripUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ShoppingTripUrl"];
            }
        }
        public static string ShoppingFavorateUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ShoppingFavorateUrl"];
            }
        }
        /// <summary>
        /// 民生银行网关直连银行列表
        /// </summary>
        public static string CmbcZhiLianBank
        {
            get
            {
                return ConfigurationManager.AppSettings["CmbcZhiLianBank"];
            }
        }

        /// <summary>
        /// 民生银行快捷支付网关地址
        /// </summary>
        public static string CmbcKuaiJiePayGate
        {
            get
            {
                return ConfigurationManager.AppSettings["CmbcKuaiJiePayGate"];
            }
        }

        /// <summary>
        /// 民生银行快捷支付商户号
        /// </summary>
        public static string CmbcKuaiJiePayID
        {
            get
            {
                return ConfigurationManager.AppSettings["CmbcKuaiJiePayID"];
            }
        }

        /// <summary>
        /// 民生银行快捷支付商户密码
        /// </summary>
        public static string CmbcKuaiJiePayPass
        {
            get
            {
                return ConfigurationManager.AppSettings["CmbcKuaiJiePayPass"];
            }
        }
        #endregion

        /// <summary>
        /// 营销管理平台地址
        /// </summary>
        public static string MMPUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["MMPUrl"];
            }
        }

        /// <summary>
        /// 登录缓存时间
        /// </summary>
        public static double LoginSessionTime
        {
            get
            {
                string LoginTimeValue = ConfigurationManager.AppSettings["LoginSessionTime"];
                if (string.IsNullOrEmpty(LoginTimeValue))
                {
                    LoginTimeValue = "20";
                }

                return Convert.ToDouble(LoginTimeValue);
            }
        }

        #region 自动取消订单配置
        public static string PromotionCancel
        {
            get { return ConfigurationManager.AppSettings["PromotionCancel"]; }
        }
        public static string DinnerCancel
        {
            get { return ConfigurationManager.AppSettings["DinnerCancel"]; }
        }
        public static string ShoppingCancel
        {
            get { return ConfigurationManager.AppSettings["ShoppingCancel"]; }
        }
        public static string CancelOrderTimeInterval
        {
            get { return ConfigurationManager.AppSettings["CancelOrderTimeInterval"]; }
        }
        #endregion

        public static string StartOffProductTime
        {
            get { return ConfigurationManager.AppSettings["StartOffProductTime"]; }
        }

        /// <summary>
        /// 部署时指定的外网端口
        /// </summary>
        public static string UrlPort
        {
            get
            {
                string temp = ConfigurationManager.AppSettings["UrlPort"];
                return string.IsNullOrEmpty(temp) ? string.Empty : temp.Trim();
            }
        }
        /// <summary>
        /// 串码的二维码链接跳转地址前缀
        /// </summary>
        public static string QRCodeURLPre
        {
            get
            {
                return ConfigurationManager.AppSettings["QRCodeURLPre"];
            }
        }

        /// <summary>
        /// 串码的二维码链接跳转地址前缀
        /// </summary>
        public static string QRCodeURLPreV2
        {
            get
            {
                return ConfigurationManager.AppSettings["QRCodeURLPreV2"];
            }
        }
 /// <summary>
        /// 串码的二维码链接跳转地址前缀
        /// </summary>
        public static string QRCodeURLPreV3
        {
            get
            {
                return ConfigurationManager.AppSettings["QRCodeURLPreV3"];
            }
        }
        /// <summary>
        /// 有娃娃系统机构号
        /// </summary>
        public static int YWWOrganSysNo
        {
            get { return int.Parse(ConfigurationManager.AppSettings["YWWOrganSysNo"]); }
        }

        /// <summary>
        /// API1.0系统地址
        /// </summary>
        public static string  OldAPIAddress
        {
            get { return  ConfigurationManager.AppSettings["OldAPIAddress"] ; }
        }

        public static string WeiXinDesKey
        {
            get { return ConfigurationManager.AppSettings["WeiXinDesKey"]; }
        }

        public static string WeidsPaymentMerId
        {
            get
            {
                return ConfigurationManager.AppSettings["WeidsPaymentMerId"];
            }
        }

        public static string WeidsPaymentKey
        {
            get
            {
                return ConfigurationManager.AppSettings["WeidsPaymentKey"];
            }
        }
        #region 查询支付网关配置
        /// <summary>
        /// 调用支付接口的地址
        /// </summary>
        public static string QueryPayAddress
        {
            get { return ConfigurationManager.AppSettings["QueryPayAddress"]; }
        }
        /// <summary>
        /// 调用支付接口的密钥
        /// </summary>
        public static string QueryPayKey
        {
            get { return ConfigurationManager.AppSettings["QueryPayKey"]; }
        }
        /// <summary>
        /// 调用支付接口的签名类型
        /// </summary>
        public static string QueryPaySignType
        {
            get { return ConfigurationManager.AppSettings["QueryPaySignType"]; }
        }
        /// <summary>
        /// 调用支付接口的版本号
        /// </summary>
        public static string QueryPayVersion
        {
            get { return ConfigurationManager.AppSettings["QueryPayVersion"]; }
        }

        #endregion

        #region 约惠一清报表相关设置
        /// <summary>
        /// FTP登录用户名
        /// </summary>
        public static string FtpUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpUserName"];
            }
        }
        /// <summary>
        /// FTP登录密码
        /// </summary>
        public static string FtpPwd
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpPwd"];
            }
        }
        /// <summary>
        /// FTP下载目录
        /// </summary>
        public static string FtpDownloadUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpDownloadUrl"];
            }
        }
        /// <summary>
        /// FTP下载的文件保存在本地的目录
        /// </summary>
        public static string CsvLocalFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["CsvLocalFilePath"];
            }
        }
        /// <summary>
        /// 启动下载Ftp服务器上的csv文件的时间
        /// </summary>
        public static string YueHuiDownStartTime
        {
            get
            {
                return ConfigurationManager.AppSettings["YueHuiDownStartTime"];
            }
        }
        /// <summary>
        /// 约惠报表文件的用户访问路径
        /// </summary>
        public static string YueHuiReportUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["YueHuiReportUrl"];
            }
        }
        /// <summary>
        /// 访问约惠报表文件的用户名
        /// </summary>
        public static string YueHuiUser
        {
            get
            {
                return ConfigurationManager.AppSettings["YueHuiUser"];
            }
        }
        /// <summary>
        /// 访问约惠报表文件的用户密码
        /// </summary>
        public static string YueHuiPwd
        {
            get
            {
                return ConfigurationManager.AppSettings["YueHuiPwd"];
            }
        }     
        /// <summary>
        /// 调用ETS自动对账接口的地址
        /// </summary>
        public static string ETSCheckUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ETSCheckUrl"];
            }
        }
        #endregion

        #region 获取卡消费流水文件的设置

        /// <summary>
        /// SFTP登录用户名
        /// </summary>
        public static string CardFtpUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["CardFtpUserName"];
            }
        }
        /// <summary>
        /// SFTP登录密码
        /// </summary>
        public static string CardFtpPwd
        {
            get
            {
                return ConfigurationManager.AppSettings["CardFtpPwd"];
            }
        }
        /// <summary>
        /// SFTP主机IP
        /// </summary>
        public static string CardFtpHost
        {
            get
            {
                return ConfigurationManager.AppSettings["CardFtpHost"];
            }
        }
        /// <summary>
        /// 卡消费流水文件所在的sftp服务器文件目录IP
        /// </summary>
        public static string CardFtpDir
        {
            get
            {
                return ConfigurationManager.AppSettings["CardFtpDir"];
            }
        }        
        /// <summary>
        /// 启动下载Ftp服务器上的csv文件的时间
        /// </summary>
        public static string CardFlowDownStartTime
        {
            get
            {
                return ConfigurationManager.AppSettings["CardFlowDownStartTime"];
            }
        }
        #endregion

        #region 获取会员绑定银行卡相关信息 2013-12-6 yf
        /// <summary>
        /// Hession接口地址
        /// </summary>
        public static string HessionUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["HessionUrl"];
            }
        }
        #endregion

        public static string KuaiJiePaytypeSysNo
        {
            get
            {
                return ConfigurationManager.AppSettings["KuaiJiePaytypeSysNo"];
            }
        }

        public static string CustomerMemcachedValidTime
        {
            get
            {
                return ConfigurationManager.AppSettings["CustomerMemcachedValidTime"];
            }
        }

        public static string DomainOrganSysno
        {
            get { return ConfigurationManager.AppSettings["DomainOrganSysno"]; }
        }
        public static string CheckMemCachedLoginUrl
        {
            get { return ConfigurationManager.AppSettings["CheckMemCachedLoginUrl"]; }
        }
        /// <summary>
        /// 短信发送机构
        /// </summary>
        public static string SMSSenderOrgan
        {
            get
            {
                return ConfigurationManager.AppSettings["SMSSenderOrgan"];
            }
        }

        #region 兰花汇App
        /// <summary>
        /// 我要团购公众号
        /// </summary>
        public static string PublicId_GroupBy
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_GroupBy"];
            }
        }

        public static string PublicId_ETicket
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_ETicket"];
            }
        }

        public static string PublicId_Card
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_Card"];
            }
        }

        public static string PublicId_Reservation
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_Reservation"];
            }
        }

        public static string PublicId_JiaoFei
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_JiaoFei"];
            }
        }

        public static string PublicId_OrderForMobileCharge
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_OrderForMobileCharge"];
            }
        }

        public static string PublicId_OrderForJiaoFei
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_OrderForJiaoFei"];
            }
        }

        public static string PublicId_Favorite
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_Favorite"];
            }
        }

        public static string PublicId_OrderList_Reservation
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_OrderList_Reservation"];
            }
        }

        public static string PublicId_OrderList_Merch
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_OrderList_Merch"];
            }
        }

        public static string PublicId_Advert
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_Advert"];
            }
        }

        public static string PublicId_ShotCut
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_ShotCut"];
            }
        }

        public static string Url_PublicTokenValidation
        {
            get
            {
                return ConfigurationManager.AppSettings["Url_PublicTokenValidation"];
            }
        }

        public static string TLNetPayVersion
        {
            get
            {
                return ConfigurationManager.AppSettings["TLNetPayVersion"];
            }
        }

        public static string LanHHOrganSysNo
        {
            get
            {
                return ConfigurationManager.AppSettings["LanHHOrganSysNo"];
            }
        }

        public static string LanHHPayCallbackWeb
        {
            get
            {
                return ConfigurationManager.AppSettings["LanHHPayCallbackWeb"];
            }
        }


        public static string LanHHDesKey
        {
            get
            {
                return ConfigurationManager.AppSettings["LanHHDesKey"];
            }
        }
        
        #endregion

        /// <summary>
        ///银行直连支付方式系统编号
        /// </summary>
        public static string ZhiLianPaySysNo
        {
            get
            {
                return ConfigurationManager.AppSettings["ZhiLianPaySysNo"];
            }
        }

        //鑫源精品专区
        public static string JingPinArea
        {
            get
            {
                return ConfigurationManager.AppSettings["JingPinArea"];
            }
        }

        /// <summary>
        ///银行直连支付方式系统编号
        /// </summary>
        public static string WDSSaleChannelSysNo
        {
            get
            {
                return ConfigurationManager.AppSettings["WDSSaleChannelSysNo"];
            }
        }

        /// <summary>
        /// 点击图文链接跳转地址
        /// </summary>
        public static string GraphicJumpUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["GraphicJumpUrl"];
            }
        }


        /// <summary>
        /// 点击图文链接跳转地址
        /// </summary>
        public static string PageSize
        {
            get
            {
                return ConfigurationManager.AppSettings["PageSize"];
            }
        }


        /// <summary>
        /// 点击图文链接跳转地址
        /// </summary>
        public static string PayPostType
        {
            get
            {
                return ConfigurationManager.AppSettings["PayPostType"];
            }
        }

        /// <summary>
        /// 监控标志
        /// </summary>
        public static string WatchFlag
        {
            get
            {
                return ConfigurationManager.AppSettings["WatchFlag"];
            }
        }

        /// <summary>
        /// 惠动民生类别排序
        /// </summary>
        public static string CmbcCategorySort
        {
            get
            {
                return ConfigurationManager.AppSettings["CmbcCategorySort"];
            }
        }

        /// <summary>
        /// 惠动民生地区排序
        /// </summary>
        public static string CmbcAreaSort
        {
            get
            {
                return ConfigurationManager.AppSettings["CmbcAreaSort"];
            }
        }

        /// <summary>
        /// 每次加载显示数据条数
        /// </summary>
        public static string OnePageShowNum
        {
            get
            {
                return ConfigurationManager.AppSettings["OnePageShowNum"];
            }
        }

        #region 掌尚达银
        /// <summary>
        /// 微商城公众号
        /// </summary>
        public static string PublicId_MicroMall
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_MicroMall"];
            }
        }

        /// <summary>
        /// 微商城订单列表公众号
        /// </summary>
        public static string PublicId_OrderList_MicroMall
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_OrderList_MicroMall"];
            }
        }
        #endregion

        public static int StartCustomerFirstPayTime {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["StartCustomerFirstPayTime"]);
            }
        }

        /// <summary>
        /// 我要旅游公众号
        /// </summary>
        public static string PublicId_Tour
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_Tour"];
            }
        }


        /// <summary>
        /// 我要旅游公众号
        /// </summary>
        public static string PublicId_OrderList_Tour
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_OrderList_Tour"];
            }
        }
        /// <summary>
        /// 特惠专区公众号
        /// </summary>
        public static string PublicId_Activity
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_Activity"];
            }
        }

        /// <summary>
        /// 地址默认区号
        /// </summary>
        public static string Default_AreaSysNo
        {
            get
            {
                return ConfigurationManager.AppSettings["Default_AreaSysNo"];
            }
        }
        /// <summary>
        /// 开始计算批次使用次数服务启动时间 单位小时 如1点
        /// </summary>
        public static string StartCouponUseTime
        {
            get { return ConfigurationManager.AppSettings["StartCouponUseTime"]; }
        }


        /// <summary>
        /// 百度坐标系转换api地址
        /// </summary>
        public static string BaiduCoordConvertUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["BaiduCoordConvertUrl"];
            }
        }
        public static string MsgOrganSysNo
        {
            get { return ConfigurationManager.AppSettings["MsgOrganSysNo"]; }
        }
        public static string MsgDays
        {
            get { return ConfigurationManager.AppSettings["MsgDays"]; }
        }
        public static string MsgStartTime
        {
            get { return ConfigurationManager.AppSettings["MsgStartTime"]; }
        }
        public static string DLYH
        {
            get { return ConfigurationManager.AppSettings["DLYH"]; }
        }
        public static string CTOrgan
        {
            get { return ConfigurationManager.AppSettings["CTOrgan"]; }
        }

        #region 360团购API配置
        /// <summary>
        /// 是否启用团购服务
        /// </summary>
        public static string IsEnableTGServer
        {
            get
            {
                return ConfigurationManager.AppSettings["IsEnableTGServer"];
            }
        }
        /// <summary>
        /// 团购服务间隔时间(分钟)
        /// </summary>
        public static string TGTimeInterval
        {
            get
            {
                return ConfigurationManager.AppSettings["TGTimeInterval"];
            }
        }
        /// <summary>
        /// FTP上传文件的地址
        /// </summary>
        public static string FtpUploadPath
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpUploadPath"];
            }
        }
        /// <summary>
        /// FTP上传文件的用户名
        /// </summary>
        public static string FtpUploadUser
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpUploadUser"];
            }
        }
        /// <summary>
        /// FTP上传文件的密码
        /// </summary>
        public static string FtpUploadPwd
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpUploadPwd"];
            }
        }
        #endregion

        public static string QdrcbId
        {
            get
            {
                return ConfigurationManager.AppSettings["QdrcbId"];
            }
        }

        public static string QdrcbCode
        {
            get
            {
                return ConfigurationManager.AppSettings["QdrcbCode"];
            }
        }

        public static string QdrcbPayID
        {
            get
            {
                return ConfigurationManager.AppSettings["QdrcbPayID"];
            }
        }

        public static string QdrcbPayPass
        {
            get
            {
                return ConfigurationManager.AppSettings["QdrcbPayPass"];
            }
        }

        public static string OrderTimeMsg
        {
            get
            {
                return ConfigurationManager.AppSettings["OrderTimeMsg"];
            }
        }


        public static string QdrcbPaytypeSysNo
        {
            get
            {
                return ConfigurationManager.AppSettings["QdrcbPaytypeSysNo"];
            }
        }

        public static string QdrcbAndroidDownloadUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["QdrcbAndroidDownloadUrl"];
            }
        }

        public static string DCYSaleChannelSysNo
        {
            get { return ConfigurationManager.AppSettings["DCYSaleChannelSysNo"]; }
        }

        public static string TicketLimit { get { return ConfigurationManager.AppSettings["TicketLimit"]; } }

        public static string TicketNoLimit { get { return ConfigurationManager.AppSettings["TicketNoLimit"]; } }
        //任我游URL
        public static string RWYAPIUrl 
        { 
            get 
            { 
                return ConfigurationManager.AppSettings["RWYAPIUrl"];
            } 
        }
        //任我游用户名
        public static string RWYUSER
        {
            get
            {
                return ConfigurationManager.AppSettings["RWYUSER"];
            }
        }
        //任我游密码
        public static string RWYPWD
        {
            get
            {
                return ConfigurationManager.AppSettings["RWYPWD"];
            }
        }
        //任我游Key
        public static string RWYMD5KEY
        {
            get
            {
                return ConfigurationManager.AppSettings["RWYMD5KEY"];
            }
        }



        /// <summary>
        /// 惠动民生省份排序
        /// </summary>
        public static string CmbcProvinceSort
        {
            get
            {
                return ConfigurationManager.AppSettings["CmbcProvinceSort"];
            }
        }

        /// <summary>
        /// 特色吉林所包含的城市
        /// </summary>
        public static string JLNLSCityes
        {
            get
            {
                return ConfigurationManager.AppSettings["JLNLSCityes"];
            }
        }

          /// <summary>
        ///积分首页地址
        /// </summary>
        public static string JifenUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["JifenUrl"];
            }
        }
        
        /// <summary>
        ///当前APP名字
        /// </summary>
        public static string APPName
        {
            get
            {
                return ConfigurationManager.AppSettings["APPName"];
            }
        }

        public static string PromotionModeCancel
        {
            get
            {
                return ConfigurationManager.AppSettings["PromotionModeCancel"];
            }
        }

        public static string PushMaxRepeat
        {
            get
            {
                return ConfigurationManager.AppSettings["PushMaxRepeat"];
            }
        }
        /// <summary>
        /// 支付审核订单取消问题邮件发送人
        /// </summary>
        public static string VerifyNetPayEmail
        {
            get { return ConfigurationManager.AppSettings["VerifyNetPayEmail"]; }
        }

        public static string AllowInsertTableUpdateLog
        {
            get { return ConfigurationManager.AppSettings["AllowInsertTableUpdateLog"]; }
        }
        /// <summary>
        /// 3天无快递信息，状态中止后，重新订阅的次数
        /// </summary>
        public static string MaxExpressSubscribeRepeat
        {
            get { return ConfigurationManager.AppSettings["MaxExpressSubscribeRepeat"]; }
        }

        public static string StatCouponServiceTime
        {
            get
            {
                return ConfigurationManager.AppSettings["StatCouponServiceTime"];
            }
        }

        public static string PublicId_Chi
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_Chi"];
            }
        }
        public static string PublicId_He
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_He"];
            }
        }
        public static string PublicId_Wan
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_Wan"];
            }
        }
        public static string PublicId_Le
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_Le"];
            }
        }
        public static string PublicId_You
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_You"];
            }
        }
        public static string PublicId_Gou
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_Gou"];
            }
        }
        public static string PublicId_Yu
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_Yu"];
            }
        }
        public static string PublicId_Zhu
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_Zhu"];
            }
        }
        public static string PublicId_Xing
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_Xing"];
            }
        }

        public static string PublicId_MomBabyShop
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicId_MomBabyShop"];
            }
        }

        public static string ImageWaterParams
        {
            get
            {
                return ConfigurationManager.AppSettings["ImageWaterParams"];
            }
        }

        public static string CEBCommitionRate
        {
            get
            {
                return ConfigurationManager.AppSettings["CEBCommitionRate"];
            }
        }

        public static string LifeAppKey
        {
            get
            {
                return ConfigurationManager.AppSettings["LifeAppKey"];
            }
        }

        public static string LifeSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["LifeSecret"];
            }
        }

        public static string LifeGate
        {
            get
            {
                return ConfigurationManager.AppSettings["LifeGate"];
            }
        }

        public static string PayCallbackBakWebForRechargeForPhone
        {
            get
            {
                return ConfigurationManager.AppSettings["PayCallbackBakWebForRechargeForPhone"];
            }
        }


        public static string LifeC2Sysno
        {
            get
            {
                return ConfigurationManager.AppSettings["LifeC2Sysno"];
            }
        }


        public static string PromotionCutDownCancel
        {
            get
            {
                return ConfigurationManager.AppSettings["PromotionCutDownCancel"];
            }
        }

        public static string ShoppingCutDownCancel
        {
            get
            {
                return ConfigurationManager.AppSettings["ShoppingCutDownCancel"];
            }
        }

        public static string CancelCutDownOrderTimeInterval
        {
            get
            {
                return ConfigurationManager.AppSettings["CancelCutDownOrderTimeInterval"];
            }
        }

        public static string ExpireCutDownTimeInterval
        {
            get
            {
                return ConfigurationManager.AppSettings["ExpireCutDownTimeInterval"];
            }
        }

        public static string CertPath
        {
            get
            {
                return ConfigurationManager.AppSettings["CertPath"];
            }
        }
        public static string OtherProductSysno
        {
            get
            {
                return ConfigurationManager.AppSettings["OtherProductSysno"];
            }
        }
        public static string CollegeZoneSysno
        {
            get
            {
                return ConfigurationManager.AppSettings["CollegeZoneSysno"];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string BankPaySysno
        {
            get
            {
                return ConfigurationManager.AppSettings["BankPaySysno"];
            }
        }
        
        
            /// <summary>
        /// 
        /// </summary>
        public static int PushMsgOpenType
        {
            get
            {
                return  Convert.ToInt32( ConfigurationManager.AppSettings["PushMsgOpenType"]);
            }
        }
            /// <summary>
        /// 
        /// </summary>
        public static int GraphicInformation
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["GraphicInformation"]);
            }
        }   
        
        /// <summary>
        /// 鑫动青岛刷卡支付方式PaymentSysNo
        /// </summary>
        public static string QingDaoShuaKa
        {
            get
            {
                return  ConfigurationManager.AppSettings["QingDaoShuaKa"]??"" ;
            }
        }

        /// <summary>
        /// 鑫动青岛刷卡url
        /// </summary>
        public static string QingDaoShuaKaURL
        {
            get
            {
                return ConfigurationManager.AppSettings["QingDaoShuaKaURL"]??"";
            }
        }

        public static string GuangDaOrganSysNo
        {
            get
            {
                return ConfigurationManager.AppSettings["GuangDaOrganSysNo"] ?? "";
            }
        }

        #region 绩效管理相关
        public static string Conn_PerformanceEvaluation
        {
            get { return ConfigurationManager.ConnectionStrings["Conn_PerformanceEvaluation"].ToString(); }
        }
        #endregion
        
    }
}
