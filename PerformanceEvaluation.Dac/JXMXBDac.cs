using PerformanceEvaluation.Cmn;
using PerformanceEvaluation.PerformanceEvaluation.Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PerformanceEvaluation.PerformanceEvaluation.Dac
{
    /// <summary>
    /// 数据访问类JXMXB。
    /// </summary>
    public class JXMXBDac
    {
        public JXMXBDac() { }
        private static JXMXBDac _instance;
        public JXMXBDac GetInstance()
        {
            if (_instance == null)
            { _instance = new JXMXBDac(); }
            return _instance;
        }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JXMXBEntity model)
        {
            SqlCommand cmd = new SqlCommand();
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            strSql.Append("insert into dbo.JXMXB(");
            if (model.LowerPersonSysNo != AppConst.IntNull)
            {
                strSql1.Append("LowerPersonSysNo,");
                strSql2.Append("@LowerPersonSysNo,");
                SqlParameter param = new SqlParameter("@LowerPersonSysNo", SqlDbType.Int, 4);
                param.Value = model.LowerPersonSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.JXCategory != AppConst.IntNull)
            {
                strSql1.Append("JXCategory,");
                strSql2.Append("@JXCategory,");
                SqlParameter param = new SqlParameter("@JXCategory", SqlDbType.Int, 4);
                param.Value = model.JXCategory;
                cmd.Parameters.Add(param);
            }
            if (model.JXSysNo != AppConst.IntNull)
            {
                strSql1.Append("JXSysNo,");
                strSql2.Append("@JXSysNo,");
                SqlParameter param = new SqlParameter("@JXSysNo", SqlDbType.Int, 4);
                param.Value = model.JXSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.ParentPersonSysNo != AppConst.IntNull)
            {
                strSql1.Append("ParentPersonSysNo,");
                strSql2.Append("@ParentPersonSysNo,");
                SqlParameter param = new SqlParameter("@ParentPersonSysNo", SqlDbType.Int, 4);
                param.Value = model.ParentPersonSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.JXCycle != AppConst.StringNull)
            {
                strSql1.Append("JXCycle,");
                strSql2.Append("@JXCycle,");
                SqlParameter param = new SqlParameter("@JXCycle", SqlDbType.NVarChar, 64);
                param.Value = model.JXCycle;
                cmd.Parameters.Add(param);
            }
            if (model.JXScore != AppConst.DecimalNull)
            {
                strSql1.Append("JXScore,");
                strSql2.Append("@JXScore,");
                SqlParameter param = new SqlParameter("@JXScore", SqlDbType.Decimal, 11);
                param.Value = model.JXScore;
                cmd.Parameters.Add(param);
            }
            if (model.JXGrade != AppConst.IntNull)
            {
                strSql1.Append("JXGrade,");
                strSql2.Append("@JXGrade,");
                SqlParameter param = new SqlParameter("@JXGrade", SqlDbType.SmallInt, 2);
                param.Value = model.JXGrade;
                cmd.Parameters.Add(param);
            }
            if (model.JXMXCategory != AppConst.IntNull)
            {
                strSql1.Append("JXMXCategory,");
                strSql2.Append("@JXMXCategory,");
                SqlParameter param = new SqlParameter("@JXMXCategory", SqlDbType.SmallInt, 2);
                param.Value = model.JXMXCategory;
                cmd.Parameters.Add(param);
            }
            if (model.CreateTime != AppConst.DateTimeNull)
            {
                strSql1.Append("CreateTime,");
                strSql2.Append("@CreateTime,");
                SqlParameter param = new SqlParameter("@CreateTime", SqlDbType.DateTime);
                param.Value = model.CreateTime;
                cmd.Parameters.Add(param);
            }
            if (model.LastUpdateTime != AppConst.DateTimeNull)
            {
                strSql1.Append("LastUpdateTime,");
                strSql2.Append("@LastUpdateTime,");
                SqlParameter param = new SqlParameter("@LastUpdateTime", SqlDbType.DateTime);
                param.Value = model.LastUpdateTime;
                cmd.Parameters.Add(param);
            }
            if (model.CreateUserSysNo != AppConst.IntNull)
            {
                strSql1.Append("CreateUserSysNo,");
                strSql2.Append("@CreateUserSysNo,");
                SqlParameter param = new SqlParameter("@CreateUserSysNo", SqlDbType.Int, 4);
                param.Value = model.CreateUserSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.LastUpdateUserSysNo != AppConst.IntNull)
            {
                strSql1.Append("LastUpdateUserSysNo,");
                strSql2.Append("@LastUpdateUserSysNo,");
                SqlParameter param = new SqlParameter("@LastUpdateUserSysNo", SqlDbType.Int, 4);
                param.Value = model.LastUpdateUserSysNo;
                cmd.Parameters.Add(param);
            }
            strSql.Append(strSql1.Remove(strSql1.Length - 1, 1)).Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.Remove(strSql2.Length - 1, 1)).Append(")");
            strSql.Append(";select @@IDENTITY");
            cmd.CommandText = strSql.ToString();

            return Convert.ToInt32(SqlHelper.ExecuteScalar(AppConfig.Conn_PerformanceEvaluation, cmd));
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(JXMXBEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dbo.JXMXB set ");
            SqlCommand cmd = new SqlCommand();
            if (model.SysNo != AppConst.IntNull)
            {
                SqlParameter param = new SqlParameter("@SysNo", SqlDbType.Int, 4);
                param.Value = model.SysNo;
                cmd.Parameters.Add(param);
            }
            if (model.LowerPersonSysNo != AppConst.IntNull)
            {
                strSql.Append("LowerPersonSysNo=@LowerPersonSysNo,");
                SqlParameter param = new SqlParameter("@LowerPersonSysNo", SqlDbType.Int, 4);
                param.Value = model.LowerPersonSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.JXCategory != AppConst.IntNull)
            {
                strSql.Append("JXCategory=@JXCategory,");
                SqlParameter param = new SqlParameter("@JXCategory", SqlDbType.Int, 4);
                param.Value = model.JXCategory;
                cmd.Parameters.Add(param);
            }
            if (model.JXSysNo != AppConst.IntNull)
            {
                strSql.Append("JXSysNo=@JXSysNo,");
                SqlParameter param = new SqlParameter("@JXSysNo", SqlDbType.Int, 4);
                param.Value = model.JXSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.ParentPersonSysNo != AppConst.IntNull)
            {
                strSql.Append("ParentPersonSysNo=@ParentPersonSysNo,");
                SqlParameter param = new SqlParameter("@ParentPersonSysNo", SqlDbType.Int, 4);
                param.Value = model.ParentPersonSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.JXCycle != AppConst.StringNull)
            {
                strSql.Append("JXCycle=@JXCycle,");
                SqlParameter param = new SqlParameter("@JXCycle", SqlDbType.NVarChar, 64);
                param.Value = model.JXCycle;
                cmd.Parameters.Add(param);
            }
            if (model.JXScore != AppConst.DecimalNull)
            {
                strSql.Append("JXScore=@JXScore,");
                SqlParameter param = new SqlParameter("@JXScore", SqlDbType.Decimal, 11);
                param.Value = model.JXScore;
                cmd.Parameters.Add(param);
            }
            if (model.JXGrade != AppConst.IntNull)
            {
                strSql.Append("JXGrade=@JXGrade,");
                SqlParameter param = new SqlParameter("@JXGrade", SqlDbType.SmallInt, 2);
                param.Value = model.JXGrade;
                cmd.Parameters.Add(param);
            }
            if (model.JXMXCategory != AppConst.IntNull)
            {
                strSql.Append("JXMXCategory=@JXMXCategory,");
                SqlParameter param = new SqlParameter("@JXMXCategory", SqlDbType.SmallInt, 2);
                param.Value = model.JXMXCategory;
                cmd.Parameters.Add(param);
            }
            if (model.CreateTime != AppConst.DateTimeNull)
            {
                strSql.Append("CreateTime=@CreateTime,");
                SqlParameter param = new SqlParameter("@CreateTime", SqlDbType.DateTime);
                param.Value = model.CreateTime;
                cmd.Parameters.Add(param);
            }
            if (model.LastUpdateTime != AppConst.DateTimeNull)
            {
                strSql.Append("LastUpdateTime=@LastUpdateTime,");
                SqlParameter param = new SqlParameter("@LastUpdateTime", SqlDbType.DateTime);
                param.Value = model.LastUpdateTime;
                cmd.Parameters.Add(param);
            }
            if (model.CreateUserSysNo != AppConst.IntNull)
            {
                strSql.Append("CreateUserSysNo=@CreateUserSysNo,");
                SqlParameter param = new SqlParameter("@CreateUserSysNo", SqlDbType.Int, 4);
                param.Value = model.CreateUserSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.LastUpdateUserSysNo != AppConst.IntNull)
            {
                strSql.Append("LastUpdateUserSysNo=@LastUpdateUserSysNo,");
                SqlParameter param = new SqlParameter("@LastUpdateUserSysNo", SqlDbType.Int, 4);
                param.Value = model.LastUpdateUserSysNo;
                cmd.Parameters.Add(param);
            }
            strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where SysNo=@SysNo ");
            cmd.CommandText = strSql.ToString();
            return SqlHelper.ExecuteNonQuery(AppConfig.Conn_PerformanceEvaluation, cmd);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int SysNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete JXMXB ");
            strSql.Append(" where SysNo=@SysNo");
            SqlCommand cmd = new SqlCommand(strSql.ToString());
            SqlParameter[] parameters = { new SqlParameter("@SysNo", SqlDbType.Int, 4) };
            parameters[0].Value = SysNo;
            cmd.Parameters.Add(parameters[0]);
            return SqlHelper.ExecuteNonQuery(AppConfig.Conn_PerformanceEvaluation, cmd);
        }
        /// <summary>
        /// 将DataRow赋值到实体
        /// </summary>
        private JXMXBEntity SetDsToEntity(DataSet ds, JXMXBEntity model)
        {
            if (ds.Tables[0].Rows[0]["SysNo"].ToString() != "")
            {
                model.SysNo = int.Parse(ds.Tables[0].Rows[0]["SysNo"].ToString());
            }
            if (ds.Tables[0].Rows[0]["LowerPersonSysNo"].ToString() != "")
            {
                model.LowerPersonSysNo = int.Parse(ds.Tables[0].Rows[0]["LowerPersonSysNo"].ToString());
            }
            if (ds.Tables[0].Rows[0]["JXCategory"].ToString() != "")
            {
                model.JXCategory = int.Parse(ds.Tables[0].Rows[0]["JXCategory"].ToString());
            }
            if (ds.Tables[0].Rows[0]["JXSysNo"].ToString() != "")
            {
                model.JXSysNo = int.Parse(ds.Tables[0].Rows[0]["JXSysNo"].ToString());
            }
            if (ds.Tables[0].Rows[0]["ParentPersonSysNo"].ToString() != "")
            {
                model.ParentPersonSysNo = int.Parse(ds.Tables[0].Rows[0]["ParentPersonSysNo"].ToString());
            }
            model.JXCycle = ds.Tables[0].Rows[0]["JXCycle"].ToString();
            if (ds.Tables[0].Rows[0]["JXScore"].ToString() != "")
            {
                model.JXScore = decimal.Parse(ds.Tables[0].Rows[0]["JXScore"].ToString());
            }
            if (ds.Tables[0].Rows[0]["JXGrade"].ToString() != "")
            {
                model.JXGrade = int.Parse(ds.Tables[0].Rows[0]["JXGrade"].ToString());
            }
            if (ds.Tables[0].Rows[0]["JXMXCategory"].ToString() != "")
            {
                model.JXMXCategory = int.Parse(ds.Tables[0].Rows[0]["JXMXCategory"].ToString());
            }
            if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
            {
                model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
            }
            if (ds.Tables[0].Rows[0]["LastUpdateTime"].ToString() != "")
            {
                model.LastUpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString());
            }
            if (ds.Tables[0].Rows[0]["CreateUserSysNo"].ToString() != "")
            {
                model.CreateUserSysNo = int.Parse(ds.Tables[0].Rows[0]["CreateUserSysNo"].ToString());
            }
            if (ds.Tables[0].Rows[0]["LastUpdateUserSysNo"].ToString() != "")
            {
                model.LastUpdateUserSysNo = int.Parse(ds.Tables[0].Rows[0]["LastUpdateUserSysNo"].ToString());
            }
            return model;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JXMXBEntity GetModel(int SysNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *  from  dbo.JXMXB");
            strSql.Append(" where SysNo=@SysNo ");
            SqlParameter[] parameters = { 
		new SqlParameter("@SysNo", SqlDbType.Int,4 )
 		};
            parameters[0].Value = SysNo;
            JXMXBEntity model = new JXMXBEntity();
            DataSet ds = SqlHelper.ExecuteDataSet(AppConfig.Conn_PerformanceEvaluation, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                SetDsToEntity(ds, model);
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion  成员方法
    }
}
