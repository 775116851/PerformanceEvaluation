using log4net;
using PerformanceEvaluation.Cmn;
using PerformanceEvaluation.PerformanceEvaluation.Dac;
using PerformanceEvaluation.PerformanceEvaluation.Info;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace PerformanceEvaluation.PerformanceEvaluation.Biz
{
    public class BasicManager
    {
        private ILog _log = log4net.LogManager.GetLogger(typeof(BasicManager));
        private BasicManager()
        {

        }
        private static BasicManager _instance;
        public static BasicManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new BasicManager();
            }
            return _instance;
        }

        //用户登录
        public PersonInfoEntity LoadUser(int userSysNo)
        {
            return new PersonInfoDac().GetModel(userSysNo);
        }

        //保存用户 lxf
        public int SaveUser(PersonInfoEntity model)
        {
            if (model.SysNo == AppConst.IntNull)
            {
                return new PersonInfoDac().Add(model);
            }
            else
            {
                new PersonInfoDac().Update(model);
                return model.SysNo;
            }
        }

        //评分列表
        public DataSet GetJXPFSearchDs(int PageIndex, int PageSize, Hashtable ht)
        {
            string pfCycle = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00");
            int ParentPersonSysNo = Convert.ToInt32(ht["ParentPersonSysNo"]);
            PagerData pd = PagerData.GetInstance();
            pd.Conn = AppConfig.Conn_PerformanceEvaluation;
            pd.Field = @" '" + pfCycle + "' AS JXZQ,b.SysNo,b.Name,b.EntryDate,b.SkillCategory,CASE WHEN c.SysNo IS NULL THEN 0 ELSE 1 END AS IsPF,c.JXScore,c.JXLevel,c.SysNo AS JXMXSysNo ";//c.JXLevel
            pd.Table = @" dbo.JXKHGSB a WITH (NOLOCK) INNER JOIN dbo.PersonInfo b WITH (NOLOCK) ON a.LowerPersonSysNo = b.SysNo AND a.RecordType = '" + (int)AppEnum.RecordType.SGTR + "' AND b.Status = '" + (int)AppEnum.BiStatus.Valid + @"'
LEFT JOIN dbo.JXMXB c WITH (NOLOCK) ON b.SysNo = c.LowerPersonSysNo AND a.ParentPersonSysNo = c.ParentPersonSysNo AND a.RecordType = c.RecordType AND c.Status = '" + (int)AppEnum.BiStatus.Valid + "' AND c.JXMXCategory = '" + (int)AppEnum.JXMXCategory.DGHZ + "' AND c.JXCycle = @pfCycle ";
            pd.Where = " WHERE 1=1 AND a.ParentPersonSysNo = @ParentPersonSysNo ";
            pd.SearchWhere = " 1=1 ";
            pd.Order = " ORDER BY ISNULL(c.JXScore,0) DESC,b.EntryDate DESC ";
            pd.PageSize = PageSize;
            pd.CurrentPageIndex = PageIndex;
            SqlParameterCollection spc = new SqlCommand().Parameters;
            spc.Add(new SqlParameter("@ParentPersonSysNo", ParentPersonSysNo));
            spc.Add(new SqlParameter("@pfCycle", pfCycle));
            if (ht != null && ht.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string key in ht.Keys)
                {
                    #region 搜索条件
                    if (key == "PersonName")
                    {
                        sb.Append(" AND b.Name LIKE @PersonName ");
                        spc.Add(new SqlParameter("@PersonName", "%" + ht[key].ToString() + "%"));
                    }
                    else if (key == "IsPF")
                    {
                        int mIsPF = Convert.ToInt32(ht[key]);
                        if(mIsPF == (int)AppEnum.YNStatus.Yes)
                        {
                            sb.Append(" AND b.SysNo IN (SELECT DISTINCT LowerPersonSysNo FROM dbo.JXMXB m WITH (NOLOCK) WHERE m.ParentPersonSysNo = @ParentPersonSysNo AND m.JXMXCategory = '" + (int)AppEnum.JXMXCategory.DGHZ + "' AND m.JXCycle = @pfCycle) ");
                        }
                        else
                        {
                            sb.Append(" AND b.SysNo NOT IN (SELECT DISTINCT LowerPersonSysNo FROM dbo.JXMXB m WITH (NOLOCK) WHERE m.ParentPersonSysNo = @ParentPersonSysNo AND m.JXMXCategory = '" + (int)AppEnum.JXMXCategory.DGHZ + "' AND m.JXCycle = @pfCycle) ");
                        }
                    }
                    #endregion
                }
                pd.SearchWhere += sb.ToString();
            }
            pd.Collection = spc;
            DataSet ds = pd.GetPage(PageIndex);
            return ds;
        }

        //绩效考核子项
        public DataSet GetJXKHMX(Hashtable ht)
        {
            SqlParameterCollection sp = new SqlCommand().Parameters;
            sp.Add(new SqlParameter("@ParentPersonSysNo", Convert.ToString(ht["ParentPersonSysNo"])));
            sp.Add(new SqlParameter("@LowerPersonSysNo", Convert.ToString(ht["LowerPersonSysNo"])));
            sp.Add(new SqlParameter("@pfCycle", Convert.ToString(ht["pfCycle"])));
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" SELECT b.SysNo,b.JXId,b.JXCategory,b.JXInfo,b.JXScore,b.JXGrad,a.OrganSysNo,c.JXScore AS JXMXScore,c.SysNo AS JXMXSysNo ");
            sbSQL.Append(" FROM dbo.JXKHGSB a WITH (NOLOCK) INNER JOIN dbo.JXKHYSB b WITH (NOLOCK) ON a.JXCategory = b.JXCategory AND a.RecordType = '" + (int)AppEnum.RecordType.SGTR + "' ");
            sbSQL.Append(" LEFT JOIN dbo.JXMXB c WITH (NOLOCK) ON c.ParentPersonSysNo = a.ParentPersonSysNo AND c.LowerPersonSysNo = a.LowerPersonSysNo AND b.SysNo = c.JXSysNo AND c.Status = '" + (int)AppEnum.BiStatus.Valid + "' AND c.JXMXCategory = '" + (int)AppEnum.JXMXCategory.MX + "' AND c.JXCycle = @pfCycle ");
            sbSQL.Append(" WHERE 1=1 ");
            sbSQL.Append(" AND a.ParentPersonSysNo = @ParentPersonSysNo AND a.LowerPersonSysNo = @LowerPersonSysNo ");
            return SqlHelper.ExecuteDataSet(AppConfig.Conn_PerformanceEvaluation, sbSQL.ToString(),sp);
        }

        //绩效打分
        public Tuple<bool, string> SaveJXMX(List<JXMXBEntity> list, int parentPersonSysNo,int JXMXSysNo)
        {
            try
            {
                StringBuilder sbSQL = new StringBuilder();
                SqlParameterCollection sp = new SqlCommand().Parameters;
                PersonInfoEntity pie = LoadUser(parentPersonSysNo);
                if (pie == null || pie.Status != (int)AppEnum.BiStatus.Valid)
                {
                    return Tuple.Create<bool, string>(false, "用户不存在或无效，请联系管理员！");
                }
                if (list.Count <= 0)
                {
                    return Tuple.Create<bool, string>(false, "评分子项为空，保存异常！");
                }
                int userLever = 0;//1:普通用户 2:绩效管理员 3:公司老大
                if (pie.IsAdmin == (int)AppEnum.YNStatus.Yes)
                {
                    if (pie.ParentPersonSysNo == 99999)
                    {
                        userLever = 3;
                    }
                    else
                    {
                        userLever = 2;
                    }
                }
                else
                {
                    userLever = 1;
                }
                JXMXBEntity jA = list[0];
                ////判断打分人员是否为二级部负责人(若是，则判断A等级比例上限和B等级比例上限)
                //bool isCheck = false;//是否限制比例
                //int maxA = 0;//A等级最大人数
                //int maxB = 0;//B等级最大人数
                //OrganEntity oe = new OrganDac().GetModel(parentPersonSysNo, (int)AppEnum.OrganType.EJB);
                //if (oe != null && oe.SysNo != AppConst.IntNull)
                //{
                //    isCheck = true;
                //    if (oe.PersonNum == AppConst.IntNull)
                //    {
                //        return Tuple.Create<bool, string>(false, "当前二级部人数为空，请联系系统管理员！");
                //    }
                //    if (oe.AGradScale != AppConst.DecimalNull)
                //    {
                //        maxA = Convert.ToInt32(Math.Round(oe.PersonNum * oe.AGradScale / 100, 0, MidpointRounding.AwayFromZero));
                //    }
                //    else
                //    {
                //        maxA = 99999;
                //    }
                //    if (oe.BGradScale != AppConst.DecimalNull)
                //    {
                //        maxB = Convert.ToInt32(Math.Round(oe.PersonNum * oe.BGradScale / 100, 0, MidpointRounding.AwayFromZero));
                //    }
                //    else
                //    {
                //        maxB = 99999;
                //    }

                //}
                DateTime dtTime = DateTime.Now;
                double TotalScore = 0.0;//总评分 用于等级
                int returnLevel = 0;
                TransactionOptions options = new TransactionOptions();
                options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                options.Timeout = TransactionManager.DefaultTimeout;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    foreach (JXMXBEntity jxmxbe in list)
                    {
                        if (JXMXSysNo == AppConst.IntNull)
                        {
                            if (jxmxbe.SysNo != AppConst.IntNull)
                            {
                                return Tuple.Create<bool, string>(false, "评分子项数据异常，保存异常！");
                            }
                            //判断数据是否存在
                            sbSQL.Clear();
                            sp = new SqlCommand().Parameters;
                            sp.Add(new SqlParameter("@LowerPersonSysNo", jxmxbe.LowerPersonSysNo));
                            sp.Add(new SqlParameter("@ParentPersonSysNo", jxmxbe.ParentPersonSysNo));
                            sp.Add(new SqlParameter("@JXSysNo", jxmxbe.JXSysNo));
                            sp.Add(new SqlParameter("@JXCycle", jxmxbe.JXCycle));
                            sbSQL.Append(" SELECT DISTINCT a.SysNo FROM JXMXB a WHERE a.LowerPersonSysNo = @LowerPersonSysNo AND a.ParentPersonSysNo = @ParentPersonSysNo AND a.JXSysNo = @JXSysNo AND a.JXCycle = @JXCycle AND a.JXMXCategory = '" + (int)AppEnum.JXMXCategory.MX + "' AND a.RecordType = '" + (int)AppEnum.RecordType.SGTR + "' AND a.Status = '" + (int)AppEnum.BiStatus.Valid + "' ");
                            string mSysNo = Convert.ToString(SqlHelper.ExecuteScalar(AppConfig.Conn_PerformanceEvaluation, sbSQL.ToString(), sp));
                            if (!string.IsNullOrEmpty(mSysNo))
                            {
                                jxmxbe.SysNo = Convert.ToInt32(mSysNo);
                                new JXMXBDac().Update(jxmxbe);
                            }
                            else
                            {
                                new JXMXBDac().Add(jxmxbe);
                            }
                        }
                        else
                        {
                            new JXMXBDac().Update(jxmxbe);
                        }
                        TotalScore += jxmxbe.TotalScore;
                    }
                    JXMXBEntity jeDGHZ = new JXMXBEntity();
                    returnLevel = GetScoreLevel(TotalScore);
                    //if (isCheck == true)//二级部评分 考虑A，B等级比例
                    //{
                    //    if (returnLevel == (int)AppEnum.JXLevel.A || returnLevel == (int)AppEnum.JXLevel.B)
                    //    {
                    //        //获取当前已评分人员的等级汇总
                    //        Hashtable htM = new Hashtable();
                    //        htM.Add("ParentPersonSysNo", parentPersonSysNo);
                    //        htM.Add("LowerPersonSysNo", jA.LowerPersonSysNo);
                    //        htM.Add("JXMXCategory", (int)AppEnum.JXMXCategory.DGHZ);
                    //        DataSet dsCount = GetLevelCount(htM);
                    //        if (Util.HasMoreRow(dsCount))
                    //        {
                    //            DataRow drCount = dsCount.Tables[0].Rows[0];
                    //            int drACount = Convert.ToInt32(drCount["ACount"]);//已评为A的人数
                    //            int drBCount = Convert.ToInt32(drCount["BCount"]);//已评为B的人数
                    //            if (returnLevel == (int)AppEnum.JXLevel.A)
                    //            {
                    //                if (maxA < (drACount + 1))
                    //                {
                    //                    return Tuple.Create<bool, string>(false, "评分为A等级人员已超过比例上限：" + maxA + "人，保存异常！");
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (maxB < (drBCount + 1))
                    //                {
                    //                    return Tuple.Create<bool, string>(false, "评分为B等级人员已超过比例上限：" + maxB + "人，保存异常！");
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    if (JXMXSysNo != AppConst.IntNull)
                    {
                        jeDGHZ.SysNo = JXMXSysNo;
                        jeDGHZ.JXScore = Convert.ToDecimal(TotalScore);
                        jeDGHZ.JXLevel = returnLevel;
                        jeDGHZ.LastUpdateTime = dtTime;
                        jeDGHZ.LastUpdateUserSysNo = parentPersonSysNo;
                        new JXMXBDac().Update(jeDGHZ);
                    }
                    else
                    {
                        

                        jeDGHZ.SysNo = AppConst.IntNull;
                        jeDGHZ.LowerPersonSysNo = jA.LowerPersonSysNo;
                        jeDGHZ.JXCategory = jA.JXCategory;
                        jeDGHZ.ParentPersonSysNo = jA.ParentPersonSysNo;
                        jeDGHZ.JXCycle = jA.JXCycle;
                        jeDGHZ.JXScore = Convert.ToDecimal(TotalScore);
                        jeDGHZ.JXLevel = returnLevel;
                        jeDGHZ.JXMXCategory = (int)AppEnum.JXMXCategory.DGHZ;
                        jeDGHZ.CreateTime = dtTime;
                        jeDGHZ.LastUpdateTime = dtTime;
                        jeDGHZ.CreateUserSysNo = parentPersonSysNo;
                        jeDGHZ.LastUpdateUserSysNo = parentPersonSysNo;
                        jeDGHZ.Status = (int)AppEnum.BiStatus.Valid;
                        jeDGHZ.RecordType = (int)AppEnum.RecordType.SGTR;
                        //判断数据是否存在
                        sbSQL.Clear();
                        sp = new SqlCommand().Parameters;
                        sp.Add(new SqlParameter("@LowerPersonSysNo", jeDGHZ.LowerPersonSysNo));
                        sp.Add(new SqlParameter("@ParentPersonSysNo", jeDGHZ.ParentPersonSysNo));
                        sp.Add(new SqlParameter("@JXCycle", jeDGHZ.JXCycle));
                        sbSQL.Append(" SELECT DISTINCT a.SysNo FROM JXMXB a WHERE a.LowerPersonSysNo = @LowerPersonSysNo AND a.ParentPersonSysNo = @ParentPersonSysNo AND a.JXCycle = @JXCycle AND a.JXMXCategory = '" + (int)AppEnum.JXMXCategory.DGHZ + "' AND a.RecordType = '" + (int)AppEnum.RecordType.SGTR + "' AND a.Status = '" + (int)AppEnum.BiStatus.Valid + "' ");
                        string mSysNo = Convert.ToString(SqlHelper.ExecuteScalar(AppConfig.Conn_PerformanceEvaluation, sbSQL.ToString(), sp));
                        if (!string.IsNullOrEmpty(mSysNo))
                        {
                            jeDGHZ.SysNo = Convert.ToInt32(mSysNo);
                            new JXMXBDac().Update(jeDGHZ);
                        }
                        else
                        {
                            JXMXSysNo = new JXMXBDac().Add(jeDGHZ);
                        }
                    }
                    scope.Complete();
                    return Tuple.Create<bool, string>(true, AppEnum.GetDescription(typeof(AppEnum.JXLevel), returnLevel) + "|" + JXMXSysNo);
                }
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("绩效打分出错，错误信息：{0};错误详情：{1}", ex.Message, ex));
                return Tuple.Create<bool, string>(false, "绩效打分出错：" + ex.Message);
            }
        }

        //加权汇总
        public Tuple<bool, string> SaveJQHZ(Hashtable ht)
        {
            try
            {
                DateTime dtTime = DateTime.Now;
                string iJXCycle = Convert.ToString(ht["pfCycle"]);
                int updateUserSysNo = Convert.ToInt32(ht["userSysNo"]);
                SqlParameterCollection sp = new SqlCommand().Parameters;
                sp.Add(new SqlParameter("@pfCycle", iJXCycle));
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.Append(" UPDATE JXMXB SET Status = '" + (int)AppEnum.BiStatus.Delete + "' WHERE JXMXCategory = '" + (int)AppEnum.JXMXCategory.JQHZ + "' AND JXCycle = @pfCycle;SELECT a.LowerPersonSysNo,MAX(a.JXCategory) AS JXCategory,MAX(a.ParentPersonSysNo) AS ParentPersonSysNo,MAX(a.JXCycle) AS JXCycle,SUM(a.JXScore * b.GradScale/100) AS TotalScore  ");
                sbSQL.Append(" FROM JXMXB a WITH (NOLOCK) INNER JOIN JXKHGSB b WITH (NOLOCK) ON a.ParentPersonSysNo = b.ParentPersonSysNo AND a.LowerPersonSysNo = b.LowerPersonSysNo AND a.RecordType = b.RecordType AND a.JXMXCategory = '" + (int)AppEnum.JXMXCategory.DGHZ + "' AND a.Status = '" + (int)AppEnum.BiStatus.Valid + "' AND a.JXCycle = @pfCycle AND b.RecordType = '" + (int)AppEnum.RecordType.SGTR + "' ");
                sbSQL.Append(" WHERE 1=1 AND a.LowerPersonSysNo NOT IN (SELECT DISTINCT LowerPersonSysNo FROM JXKHGSB WHERE RecordType = '" + (int)AppEnum.RecordType.XTTR + "') ");
                sbSQL.Append(" GROUP BY a.LowerPersonSysNo ");
                DataSet dsList = SqlHelper.ExecuteDataSet(AppConfig.Conn_PerformanceEvaluation, sbSQL.ToString(), sp);
                if (!Util.HasMoreRow(dsList))
                {
                    return Tuple.Create<bool, string>(false, "请先进行打分后，再进行加权汇总A！");
                }
                List<JXMXBEntity> list = new List<JXMXBEntity>();
                foreach (DataRow drOne in dsList.Tables[0].Rows)
                {
                    JXMXBEntity je = new JXMXBEntity();
                    je.SysNo = AppConst.IntNull;
                    je.LowerPersonSysNo = Convert.ToInt32(drOne["LowerPersonSysNo"]);
                    je.JXCategory = Convert.ToInt32(drOne["JXCategory"]);
                    je.ParentPersonSysNo = 9999;
                    je.JXCycle = Convert.ToString(drOne["JXCycle"]);
                    double totalScore = Convert.ToDouble(drOne["TotalScore"]);
                    totalScore = Math.Round(Convert.ToDouble(totalScore), 2, MidpointRounding.AwayFromZero);
                    je.JXScore = Convert.ToDecimal(totalScore);
                    je.JXLevel = GetScoreLevel(totalScore);
                    je.Status = (int)AppEnum.BiStatus.Valid;
                    je.JXMXCategory = (int)AppEnum.JXMXCategory.JQHZ;
                    je.CreateTime = dtTime;
                    je.LastUpdateTime = dtTime;
                    je.CreateUserSysNo = updateUserSysNo;
                    je.LastUpdateUserSysNo = updateUserSysNo;
                    je.RecordType = (int)AppEnum.RecordType.SGTR;
                    list.Add(je);
                }
                if (list.Count <= 0)
                {
                    return Tuple.Create<bool, string>(false, "请先进行打分后，再进行加权汇总B！");
                }
                TransactionOptions options = new TransactionOptions();
                options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                options.Timeout = TransactionManager.DefaultTimeout;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    foreach (JXMXBEntity jee in list)
                    {
                        new JXMXBDac().Add(jee);
                    }
                    sp = new SqlCommand().Parameters;
                    sp.Add(new SqlParameter("@pfCycle", iJXCycle));
                    sbSQL.Clear();
                    sbSQL.Append(" DELETE FROM JXMXB WHERE JXCycle = @pfCycle AND JXMXCategory = '" + (int)AppEnum.JXMXCategory.DGHZ + "' AND RecordType = '" + (int)AppEnum.RecordType.XTTR + "'; ");
                    sbSQL.Append(" SELECT b.ParentPersonSysNo,b.LowerPersonSysNo,b.JXCategory,a.JXScore,a.JXLevel");
                    sbSQL.Append(" FROM JXMXB a WITH (NOLOCK) INNER JOIN JXKHGSB b WITH (NOLOCK) ON a.LowerPersonSysNo = b.ParentPersonSysNo AND a.JXMXCategory = '" + (int)AppEnum.JXMXCategory.JQHZ + "' AND a.Status = '" + (int)AppEnum.BiStatus.Valid + "' AND a.JXCycle = @pfCycle AND b.RecordType = '" + (int)AppEnum.RecordType.XTTR + "' ");
                    DataSet dsNewList = SqlHelper.ExecuteDataSet(AppConfig.Conn_PerformanceEvaluation, sbSQL.ToString(), sp);
                    if (Util.HasMoreRow(dsNewList))//存在需系统传入数据
                    {
                        foreach(DataRow drNew in dsNewList.Tables[0].Rows)
                        {
                            JXMXBEntity jeDGHZ = new JXMXBEntity();
                            jeDGHZ.SysNo = AppConst.IntNull;
                            jeDGHZ.LowerPersonSysNo = Convert.ToInt32(drNew["LowerPersonSysNo"]);
                            jeDGHZ.JXCategory = Convert.ToInt32(drNew["JXCategory"]);
                            jeDGHZ.ParentPersonSysNo = Convert.ToInt32(drNew["ParentPersonSysNo"]);
                            jeDGHZ.JXCycle = iJXCycle;
                            jeDGHZ.JXScore = Convert.ToDecimal(drNew["JXScore"]);
                            jeDGHZ.JXLevel = Convert.ToInt32(drNew["JXLevel"]); ;
                            jeDGHZ.JXMXCategory = (int)AppEnum.JXMXCategory.DGHZ;
                            jeDGHZ.CreateTime = dtTime;
                            jeDGHZ.LastUpdateTime = dtTime;
                            jeDGHZ.CreateUserSysNo = updateUserSysNo;
                            jeDGHZ.LastUpdateUserSysNo = updateUserSysNo;
                            jeDGHZ.Status = (int)AppEnum.BiStatus.Valid;
                            jeDGHZ.RecordType = (int)AppEnum.RecordType.XTTR;
                            new JXMXBDac().Add(jeDGHZ);
                        }
                        sp = new SqlCommand().Parameters;
                        sp.Add(new SqlParameter("@pfCycle", iJXCycle));
                        sbSQL.Clear();
                        sbSQL.Append(" SELECT a.LowerPersonSysNo,MAX(a.JXCategory) AS JXCategory,MAX(a.ParentPersonSysNo) AS ParentPersonSysNo,MAX(a.JXCycle) AS JXCycle,SUM(a.JXScore * b.GradScale/100) AS TotalScore ");
                        sbSQL.Append(" FROM JXMXB a WITH (NOLOCK) INNER JOIN JXKHGSB b WITH (NOLOCK) ON a.ParentPersonSysNo = b.ParentPersonSysNo AND a.LowerPersonSysNo = b.LowerPersonSysNo AND a.RecordType = b.RecordType AND a.JXMXCategory = '" + (int)AppEnum.JXMXCategory.DGHZ + "' AND a.Status = '" + (int)AppEnum.BiStatus.Valid + "' AND a.JXCycle = @pfCycle ");
                        sbSQL.Append(" WHERE 1=1 AND a.LowerPersonSysNo IN (SELECT DISTINCT LowerPersonSysNo FROM JXKHGSB WHERE RecordType = '" + (int)AppEnum.RecordType.XTTR + "') ");
                        sbSQL.Append(" GROUP BY a.LowerPersonSysNo ");
                        DataSet dsKList = SqlHelper.ExecuteDataSet(AppConfig.Conn_PerformanceEvaluation, sbSQL.ToString(), sp);
                        if (Util.HasMoreRow(dsKList))
                        {
                            foreach(DataRow drK in dsKList.Tables[0].Rows)
                            {
                                JXMXBEntity je = new JXMXBEntity();
                                je.SysNo = AppConst.IntNull;
                                je.LowerPersonSysNo = Convert.ToInt32(drK["LowerPersonSysNo"]);
                                je.JXCategory = Convert.ToInt32(drK["JXCategory"]);
                                je.ParentPersonSysNo = 9999;
                                je.JXCycle = Convert.ToString(drK["JXCycle"]);
                                double kTotalScore = Convert.ToDouble(drK["TotalScore"]);
                                kTotalScore = Math.Round(Convert.ToDouble(kTotalScore), 2, MidpointRounding.AwayFromZero);
                                je.JXScore = Convert.ToDecimal(kTotalScore);
                                je.JXLevel = GetScoreLevel(kTotalScore);
                                je.Status = (int)AppEnum.BiStatus.Valid;
                                je.JXMXCategory = (int)AppEnum.JXMXCategory.JQHZ;
                                je.CreateTime = dtTime;
                                je.LastUpdateTime = dtTime;
                                je.CreateUserSysNo = updateUserSysNo;
                                je.LastUpdateUserSysNo = updateUserSysNo;
                                je.RecordType = (int)AppEnum.RecordType.SGTR;
                                new JXMXBDac().Add(je);
                            }
                        }
                    }
                    scope.Complete();
                }
                return Tuple.Create<bool, string>(true, "加权汇总成功;" + RetMsg(ht));
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("加权汇总出错，错误信息：{0};错误详情：{1}", ex.Message, ex));
                return Tuple.Create<bool, string>(false, "加权汇总出错：" + ex.Message);
            }
        }

        //获取二级部负责人已评的A，B等级人数(加权汇总使用)
        public string RetMsg(Hashtable ht)
        {
            try
            {
                string retuMsg = string.Empty;
                string iJXCycle = Convert.ToString(ht["pfCycle"]);
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.Append(" SELECT a.SysNo,a.PersonSysNo,a.PersonNum,a.AGradScale,a.BGradScale,b.Name FROM Organ a  WITH (NOLOCK) JOIN PersonInfo b WITH (NOLOCK) ON a.PersonSysNo = b.SysNo WHERE OrganType = '" + (int)AppEnum.OrganType.EJB + "' ");
                DataSet dsOrganList = SqlHelper.ExecuteDataSet(AppConfig.Conn_PerformanceEvaluation, sbSQL.ToString());
                if (!Util.HasMoreRow(dsOrganList))
                {
                    retuMsg = "";
                    return retuMsg;
                }
                int maxA = 0;//A等级最大人数
                int maxB = 0;//B等级最大人数
                SqlParameterCollection sp = new SqlCommand().Parameters;
                foreach (DataRow drOne in dsOrganList.Tables[0].Rows)
                {
                    int iOrganSysNo = Convert.ToInt32(drOne["SysNo"]);
                    string iName = Convert.ToString(drOne["Name"]);
                    string iPeronNum = Convert.ToString(drOne["PersonNum"]);
                    string iAGradScale = Convert.ToString(drOne["AGradScale"]);
                    string iBGradScale = Convert.ToString(drOne["BGradScale"]);
                    int mPersonNum = 0;
                    decimal mAGradScale = 0;
                    decimal mBGradScale = 0;
                    if (string.IsNullOrEmpty(iPeronNum) || !int.TryParse(iPeronNum, out mPersonNum) || mPersonNum == (int)AppConst.IntNull)
                    {
                        retuMsg += "用户：" + iName + " 未设置机构人数;";
                        continue;
                    }
                    if (!string.IsNullOrEmpty(iAGradScale) && decimal.TryParse(iAGradScale, out mAGradScale) && mAGradScale != (int)AppConst.DecimalNull)
                    {
                        maxA = Convert.ToInt32(Math.Round(mPersonNum * mAGradScale / 100, 0, MidpointRounding.AwayFromZero));
                    }
                    else
                    {
                        maxA = 99999;
                    }
                    if (!string.IsNullOrEmpty(iBGradScale) && decimal.TryParse(iBGradScale, out mBGradScale) && mBGradScale != (int)AppConst.DecimalNull)
                    {
                        maxB = Convert.ToInt32(Math.Round(mPersonNum * mBGradScale / 100, 0, MidpointRounding.AwayFromZero));
                    }
                    else
                    {
                        maxB = 99999;
                    }
                    sp = new SqlCommand().Parameters;
                    sp.Add(new SqlParameter("@OrganSysNo", iOrganSysNo));
                    sp.Add(new SqlParameter("@pfCycle", iJXCycle));
                    sbSQL.Clear();
                    sbSQL.Append(" SELECT ISNULL(SUM(CASE WHEN b.JXLevel = 1 THEN 1 ELSE 0 END),0) AS ACount,ISNULL(SUM(CASE WHEN b.JXLevel = 2 THEN 1 ELSE 0 END),0) AS BCount ");
                    sbSQL.Append(" FROM (SELECT DISTINCT LowerPersonSysNo,OrganSysNo FROM JXKHGSB) a INNER JOIN JXMXB b WITH (NOLOCK) ON a.LowerPersonSysNo = b.LowerPersonSysNo AND b.Status = '" + (int)AppEnum.BiStatus.Valid + "' AND b.JXMXCategory = '" + (int)AppEnum.JXMXCategory.JQHZ + "' AND b.JXCycle = @pfCycle ");
                    sbSQL.Append(" WHERE 1=1 AND a.OrganSysNo = @OrganSysNo ");
                    DataSet dsCount = SqlHelper.ExecuteDataSet(AppConfig.Conn_PerformanceEvaluation, sbSQL.ToString(), sp);
                    if (Util.HasMoreRow(dsCount))
                    {
                        DataRow drCount = dsCount.Tables[0].Rows[0];
                        int drACount = Convert.ToInt32(drCount["ACount"]);//已评为A的人数
                        int drBCount = Convert.ToInt32(drCount["BCount"]);//已评为B的人数
                        if (drACount > maxA)
                        {
                            retuMsg += "用户：" + iName + " 对应机构所评分为A等级人员已超过比例上限" + maxA + "人;";
                        }
                        if (drBCount > maxB)
                        {
                            retuMsg += "用户：" + iName + " 对应机构所评分为B等级人员已超过比例上限" + maxB + "人;";
                        }
                    }

                }
                return retuMsg;
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("加权汇总获取二级部负责人已评分AB人数出错，错误信息：{0};错误详情：{1}", ex.Message, ex));
                throw ex;
            }
        }

        //获取二级部负责人已评的A，B等级人数（打分使用）
        public DataSet GetLevelCount(Hashtable ht)
        {
            try
            {
                SqlParameterCollection sp = new SqlCommand().Parameters;
                sp.Add(new SqlParameter("@ParentPersonSysNo", Convert.ToString(ht["ParentPersonSysNo"])));
                sp.Add(new SqlParameter("@LowerPersonSysNo", Convert.ToString(ht["LowerPersonSysNo"])));
                sp.Add(new SqlParameter("@JXMXCategory", Convert.ToString(ht["JXMXCategory"])));
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.Append(" SELECT ISNULL(SUM(CASE WHEN JXLevel = 1 THEN 1 ELSE 0 END),0) AS ACount,ISNULL(SUM(CASE WHEN JXLevel = 2 THEN 1 ELSE 0 END),0) AS BCount ");
                sbSQL.Append(" FROM JXMXB WITH (NOLOCK) ");
                sbSQL.Append(" WHERE 1=1 ");
                sbSQL.Append(" AND ParentPersonSysNo = @ParentPersonSysNo AND LowerPersonSysNo != @LowerPersonSysNo AND Status = '" + (int)AppEnum.BiStatus.Valid + "' AND JXMXCategory = @JXMXCategory ");
                return SqlHelper.ExecuteDataSet(AppConfig.Conn_PerformanceEvaluation, sbSQL.ToString(), sp);
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("打分获取二级部负责人已评分AB人数出错，错误信息：{0};错误详情：{1}", ex.Message, ex));
                throw ex;
            }
        }

        //绩效考核历史数据
        public DataSet GetJXKHHistory(int PageIndex, int PageSize, Hashtable ht)
        {
            int mType = Convert.ToInt32(ht["MType"]);//分(普通人员)走归属表和(绩效管理员和公司老大)不走归属表 1普通员工 2管理员及老大
            PagerData pd = PagerData.GetInstance();
            pd.Conn = AppConfig.Conn_PerformanceEvaluation;
            if (mType == 1)
            {
                pd.Field = @" c.SysNo,b.OrganSysNo,b.LowerClassSysNo AS ClassSysNo,a.JXCycle AS JXZQ,ISNULL(a.JXScore,0) AS JXScore,a.JXLevel,c.Name,d.OrganName,e.FunctionInfo,c.EntryDate ";
                pd.Table = @" dbo.JXMXB a WITH (NOLOCK) INNER JOIN dbo.JXKHGSB b WITH (NOLOCK) ON a.LowerPersonSysNo = b.LowerPersonSysNo AND a.JXMXCategory = '99' AND a.Status = '0' AND b.RecordType = '1' 
INNER JOIN dbo.PersonInfo c WITH (NOLOCK) ON a.LowerPersonSysNo = c.SysNo
LEFT JOIN dbo.Organ d WITH (NOLOCK) ON b.OrganSysNo = d.SysNo
LEFT JOIN dbo.Organ e WITH (NOLOCK) ON b.LowerClassSysNo = e.SysNo ";
                
            }
            else
            {
                pd.Field = @" b.SysNo,b.OrganSysNo,b.ClassSysNo,a.JXCycle AS JXZQ,ISNULL(a.JXScore,0) AS JXScore,a.JXLevel,b.Name,d.OrganName,e.FunctionInfo,b.EntryDate ";
                pd.Table = @" dbo.JXMXB a WITH (NOLOCK) INNER JOIN dbo.PersonInfo b WITH (NOLOCK) ON a.LowerPersonSysNo = b.SysNo AND a.JXMXCategory = '99' AND a.Status = '0'
LEFT JOIN dbo.Organ d WITH (NOLOCK) ON b.OrganSysNo = d.SysNo
LEFT JOIN dbo.Organ e WITH (NOLOCK) ON b.ClassSysNo = e.SysNo ";
            }
            pd.Where = " WHERE 1=1 ";
            pd.SearchWhere = " 1=1 ";
            pd.Order = " ORDER BY ISNULL(a.JXScore,0) DESC, a.JXCycle DESC ";
            pd.PageSize = PageSize;
            pd.CurrentPageIndex = PageIndex;
            SqlParameterCollection spc = new SqlCommand().Parameters;
            if (ht != null && ht.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string key in ht.Keys)
                {
                    #region 搜索条件
                    if (key == "ParentPersonSysNo")
                    {
                        if (mType == 1)
                        {
                            sb.Append(" AND (b.ParentPersonSysNo = @ParentPersonSysNo OR b.LowerPersonSysNo=@ParentPersonSysNo) ");
                            spc.Add(new SqlParameter("@ParentPersonSysNo", ht[key].ToString()));
                        }
                    }
                    else if (key == "pfCycle")
                    {
                        sb.Append(" AND a.JXCycle = @pfCycle ");
                        spc.Add(new SqlParameter("@pfCycle", ht[key].ToString()));
                    }
                    else if (key == "JXLevel")
                    {
                        sb.Append(" AND a.JXLevel = @JXLevel ");
                        spc.Add(new SqlParameter("@JXLevel", ht[key].ToString()));
                    }
                    else if (key == "Name")
                    {
                        if (mType == 1)
                        {
                            sb.Append(" AND c.Name LIKE @Name ");
                        }
                        else
                        {
                            sb.Append(" AND b.Name LIKE @Name ");
                        }
                        spc.Add(new SqlParameter("@Name", "%" + ht[key].ToString() + "%"));
                    }
                    else if (key == "OrganSysNo")
                    {
                        sb.Append(" AND b.OrganSysNo = @OrganSysNo ");
                        spc.Add(new SqlParameter("@OrganSysNo", ht[key].ToString()));
                    }
                    else if (key == "ClassSysNo")
                    {
                        if (mType == 1)
                        {
                            sb.Append(" AND b.LowerClassSysNo = @ClassSysNo ");
                        }
                        else
                        {
                            sb.Append(" AND b.ClassSysNo = @ClassSysNo ");
                        }
                        spc.Add(new SqlParameter("@ClassSysNo", ht[key].ToString()));
                    }
                    else if (key == "pfCycleM")
                    {
                        sb.Append(" AND a.JXCycle LIKE @pfCycleM ");
                        spc.Add(new SqlParameter("@pfCycleM", ht[key].ToString() + "%"));
                    }
                    #endregion
                }
                pd.SearchWhere += sb.ToString();
            }
            pd.Collection = spc;
            DataSet ds = pd.GetPage(PageIndex);
            return ds;
        }

        //获取机构列表
        public Dictionary<int, OrganEntity> GetOrganList()
        {
            Dictionary<int, OrganEntity> organList = new Dictionary<int, OrganEntity>();
            string sSQL = " SELECT * FROM dbo.Organ WITH (NOLOCK) WHERE 1=1 AND OrganType = '" + (int)AppEnum.OrganType.EJB + "' ORDER BY OrganId ASC ";
            DataSet dsOrganList = SqlHelper.ExecuteDataSet(AppConfig.Conn_PerformanceEvaluation, sSQL);
            if(Util.HasMoreRow(dsOrganList))
            {
                foreach(DataRow drOrgan in dsOrganList.Tables[0].Rows)
                {
                    OrganEntity pie = new OrganEntity();
                    new OrganDac().SetDrToEntity(drOrgan, pie);
                    organList.Add(pie.SysNo,pie);
                }
            }
            return organList;
        }

        //获取职能室列表
        public Dictionary<int, OrganEntity> GetClassList(int organSysNo)
        {
            string OID = "";
            Dictionary<int, OrganEntity> classList = new Dictionary<int, OrganEntity>();
            OrganEntity oee = new OrganDac().GetModel(organSysNo);
            if(oee != null && oee.SysNo != AppConst.IntNull)
            {
                OID = oee.OrganId.Substring(0, 2);
            }
            SqlParameterCollection sp = new SqlCommand().Parameters;
            sp.Add(new SqlParameter("@OID", OID + "%"));
            Dictionary<int, OrganEntity> organList = new Dictionary<int, OrganEntity>();
            string sSQL = " SELECT * FROM dbo.Organ WITH (NOLOCK) WHERE OrganType = '" + (int)AppEnum.OrganType.ZNS + "' AND OrganId LIKE @OID ORDER BY OrganId ASC ";
            DataSet dsOrganList = SqlHelper.ExecuteDataSet(AppConfig.Conn_PerformanceEvaluation, sSQL, sp);
            if (Util.HasMoreRow(dsOrganList))
            {
                foreach (DataRow drOrgan in dsOrganList.Tables[0].Rows)
                {
                    OrganEntity pie = new OrganEntity();
                    new OrganDac().SetDrToEntity(drOrgan, pie);
                    organList.Add(pie.SysNo, pie);
                }
            }
            return organList;
        }

        //获取用户类型列表
        public Dictionary<int, PersonTypeEntity> GetPersonTypeList()
        {
            Dictionary<int, PersonTypeEntity> organList = new Dictionary<int, PersonTypeEntity>();
            string sSQL = " SELECT a.* FROM PersonType a WITH (NOLOCK) WHERE 1=1 AND a.Status = '" + (int)AppEnum.BiStatus.Valid + "' ORDER BY SysNo ASC ";
            DataSet dsOrganList = SqlHelper.ExecuteDataSet(AppConfig.Conn_PerformanceEvaluation, sSQL);
            if (Util.HasMoreRow(dsOrganList))
            {
                foreach (DataRow drPersonType in dsOrganList.Tables[0].Rows)
                {
                    PersonTypeEntity pie = new PersonTypeEntity();
                    new PersonTypeDac().SetDrToEntity(drPersonType, pie);
                    organList.Add(pie.SysNo, pie);
                }
            }
            return organList;
        }

        //获取人员列表
        public DataSet GetPersonList(int PageIndex, int PageSize, Hashtable ht)
        {
            PagerData pd = PagerData.GetInstance();
            pd.Conn = AppConfig.Conn_PerformanceEvaluation;
            pd.Field = @" a.SysNo,a.OrganSysNo,a.ClassSysNo,a.Name,a.EntryDate,a.Status,a.IsLogin,a.PersonTypeSysNo,c.OrganName,d.FunctionInfo,b.TypeName  ";
            pd.Table = @" PersonInfo a WITH (NOLOCK) LEFT JOIN PersonType b WITH (NOLOCK) ON a.PersonTypeSysNo = b.SysNo
LEFT JOIN Organ c WITH (NOLOCK) ON a.OrganSysNo = c.SysNo
LEFT JOIN Organ d WITH (NOLOCK) ON a.ClassSysNo = d.SysNo ";
            if (ht.ContainsKey("IsAdmin"))
            {
                pd.Where = " WHERE 1=1 AND ISNULL(a.IsAdmin,0) = 0 ";
            }
            else
            {
                pd.Where = " WHERE 1=1 ";
            }
            pd.SearchWhere = " 1=1 ";
            pd.Order = " ORDER BY a.SysNo ASC ";
            pd.PageSize = PageSize;
            pd.CurrentPageIndex = PageIndex;
            SqlParameterCollection spc = new SqlCommand().Parameters;
            if (ht != null && ht.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string key in ht.Keys)
                {
                    #region 搜索条件
                    if (key == "OrganSysNo")
                    {
                        sb.Append(" AND a.OrganSysNo = @OrganSysNo ");
                        spc.Add(new SqlParameter("@OrganSysNo", ht[key].ToString()));
                    }
                    else if (key == "ClassSysNo")
                    {
                        sb.Append(" AND a.ClassSysNo = @ClassSysNo ");
                        spc.Add(new SqlParameter("@ClassSysNo", ht[key].ToString()));
                    }
                    else if (key == "Status")
                    {
                        sb.Append(" AND a.Status = @Status ");
                        spc.Add(new SqlParameter("@Status", ht[key].ToString()));
                    }
                    else if (key == "PersonTypeSysNo")
                    {
                        sb.Append(" AND a.PersonTypeSysNo = @PersonTypeSysNo ");
                        spc.Add(new SqlParameter("@PersonTypeSysNo", ht[key].ToString()));
                    }
                    else if (key == "PersonSysNo")
                    {
                        sb.Append(" AND a.SysNo = @PersonSysNo ");
                        spc.Add(new SqlParameter("@PersonSysNo", ht[key].ToString()));
                    }
                    else if (key == "Name")
                    {
                        sb.Append(" AND a.Name LIKE @Name ");
                        spc.Add(new SqlParameter("@Name", "%" + ht[key].ToString() + "%"));
                    }
                    #endregion
                }
                pd.SearchWhere += sb.ToString();
            }
            pd.Collection = spc;
            DataSet ds = pd.GetPage(PageIndex);
            return ds;
        }

        public OrganEntity GetEJBOrgan(int OrganSysNo,int OrderType)
        {
            return new OrganDac().GetModel(OrganSysNo, OrderType);
        }

        public int GetScoreLevel(double totalScore)
        {
            try
            {
                //获取各等级分数范围
                string sValue = Convert.ToString(ConfigurationManager.AppSettings["LevelRangeScore"]);//1$90|2$80|3$70|4$60|5$0
                if (!string.IsNullOrEmpty(sValue))
                {
                    string[] mValue = sValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (mValue.Length < 4)
                    {
                        _log.Error(string.Format("未配置各等级分数范围1"));
                        throw new Exception("未配置各等级分数范围");
                    }
                    if (totalScore >= Convert.ToDouble(mValue[0].Split('$')[1]))
                    {
                        return (int)AppEnum.JXLevel.A;
                    }
                    else if (totalScore >= Convert.ToDouble(mValue[1].Split('$')[1]))
                    {
                        return (int)AppEnum.JXLevel.B;
                    }
                    else if (totalScore >= Convert.ToDouble(mValue[2].Split('$')[1]))
                    {
                        return (int)AppEnum.JXLevel.C;
                    }
                    else if (totalScore >= Convert.ToDouble(mValue[3].Split('$')[1]))
                    {
                        return (int)AppEnum.JXLevel.D;
                    }
                    else
                    {
                        return (int)AppEnum.JXLevel.E;
                    }
                }
                else
                {
                    _log.Error(string.Format("未配置各等级分数范围2"));
                    throw new Exception("未配置各等级分数范围");
                }
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("未配置各等级分数范围3,错误信息：{0};错误详情：{1}", ex.Message, ex));
                throw new Exception("未配置各等级分数范围");
            }
        }
    }
}
