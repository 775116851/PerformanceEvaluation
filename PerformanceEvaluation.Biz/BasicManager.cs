using PerformanceEvaluation.Cmn;
using PerformanceEvaluation.PerformanceEvaluation.Dac;
using PerformanceEvaluation.PerformanceEvaluation.Info;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PerformanceEvaluation.PerformanceEvaluation.Biz
{
    public class BasicManager
    {
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
            pd.Table = @" dbo.JXKHGSB a WITH (NOLOCK) INNER JOIN dbo.PersonInfo b WITH (NOLOCK) ON a.LowerPersonSysNo = b.SysNo AND b.Status = '" + (int)AppEnum.BiStatus.Valid + @"'
LEFT JOIN dbo.JXMXB c WITH (NOLOCK) ON b.SysNo = c.LowerPersonSysNo AND a.ParentPersonSysNo = c.ParentPersonSysNo AND c.JXMXCategory = '" + (int)AppEnum.JXMXCategory.DGHZ + "' AND c.JXCycle = @pfCycle ";
            pd.Where = " WHERE 1=1 AND a.ParentPersonSysNo = @ParentPersonSysNo ";
            pd.SearchWhere = " 1=1 ";
            pd.Order = " ORDER BY b.EntryDate DESC ";
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
            sbSQL.Append(" FROM dbo.JXKHGSB a WITH (NOLOCK) INNER JOIN dbo.JXKHYSB b WITH (NOLOCK) ON a.JXCategory = b.JXCategory ");
            sbSQL.Append(" LEFT JOIN dbo.JXMXB c WITH (NOLOCK) ON c.ParentPersonSysNo = a.ParentPersonSysNo AND c.LowerPersonSysNo = a.LowerPersonSysNo AND c.JXMXCategory = '" + (int)AppEnum.JXMXCategory.MX + "' AND c.JXCycle = @pfCycle ");
            sbSQL.Append(" WHERE 1=1 ");
            sbSQL.Append(" AND a.ParentPersonSysNo = @ParentPersonSysNo AND a.LowerPersonSysNo = @LowerPersonSysNo ");
            return SqlHelper.ExecuteDataSet(AppConfig.Conn_PerformanceEvaluation, sbSQL.ToString(),sp);
        }

    }
}
