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
    /// 数据访问类JXKHYSB。
    /// </summary>
    public class JXKHYSBDac
    {
        public JXKHYSBDac() { }
        private static JXKHYSBDac _instance;
        public JXKHYSBDac GetInstance()
        {
            if (_instance == null)
            { _instance = new JXKHYSBDac(); }
            return _instance;
        }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JXKHYSBEntity model)
        {
            SqlCommand cmd = new SqlCommand();
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            strSql.Append("insert into dbo.JXKHYSB(");
            if (model.JXId != AppConst.StringNull)
            {
                strSql1.Append("JXId,");
                strSql2.Append("@JXId,");
                SqlParameter param = new SqlParameter("@JXId", SqlDbType.NVarChar, 64);
                param.Value = model.JXId;
                cmd.Parameters.Add(param);
            }
            if (model.JXCategory != AppConst.IntNull)
            {
                strSql1.Append("JXCategory,");
                strSql2.Append("@JXCategory,");
                SqlParameter param = new SqlParameter("@JXCategory", SqlDbType.SmallInt, 2);
                param.Value = model.JXCategory;
                cmd.Parameters.Add(param);
            }
            if (model.JXInfo != AppConst.StringNull)
            {
                strSql1.Append("JXInfo,");
                strSql2.Append("@JXInfo,");
                SqlParameter param = new SqlParameter("@JXInfo", SqlDbType.NVarChar, 600);
                param.Value = model.JXInfo;
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
            if (model.JXGrad != AppConst.DecimalNull)
            {
                strSql1.Append("JXGrad,");
                strSql2.Append("@JXGrad,");
                SqlParameter param = new SqlParameter("@JXGrad", SqlDbType.Decimal, 11);
                param.Value = model.JXGrad;
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
            strSql.Append(";UPDATE dbo.JXKHYSB SET JXId=CAST(JXCategory AS VARCHAR(2)) + RIGHT('00' + CAST(@@IDENTITY AS VARCHAR), 3) WHERE SysNo=@@IDENTITY;select @@IDENTITY");
            cmd.CommandText = strSql.ToString();

            return Convert.ToInt32(SqlHelper.ExecuteScalar(AppConfig.Conn_PerformanceEvaluation, cmd));
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(JXKHYSBEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dbo.JXKHYSB set ");
            SqlCommand cmd = new SqlCommand();
            if (model.SysNo != AppConst.IntNull)
            {
                SqlParameter param = new SqlParameter("@SysNo", SqlDbType.Int, 4);
                param.Value = model.SysNo;
                cmd.Parameters.Add(param);
            }
            if (model.JXId != AppConst.StringNull)
            {
                strSql.Append("JXId=@JXId,");
                SqlParameter param = new SqlParameter("@JXId", SqlDbType.NVarChar, 64);
                param.Value = model.JXId;
                cmd.Parameters.Add(param);
            }
            if (model.JXCategory != AppConst.IntNull)
            {
                strSql.Append("JXCategory=@JXCategory,");
                SqlParameter param = new SqlParameter("@JXCategory", SqlDbType.SmallInt, 2);
                param.Value = model.JXCategory;
                cmd.Parameters.Add(param);
            }
            if (model.JXInfo != AppConst.StringNull)
            {
                strSql.Append("JXInfo=@JXInfo,");
                SqlParameter param = new SqlParameter("@JXInfo", SqlDbType.NVarChar, 600);
                param.Value = model.JXInfo;
                cmd.Parameters.Add(param);
            }
            if (model.JXScore != AppConst.DecimalNull)
            {
                strSql.Append("JXScore=@JXScore,");
                SqlParameter param = new SqlParameter("@JXScore", SqlDbType.Decimal, 11);
                param.Value = model.JXScore;
                cmd.Parameters.Add(param);
            }
            if (model.JXGrad != AppConst.DecimalNull)
            {
                strSql.Append("JXGrad=@JXGrad,");
                SqlParameter param = new SqlParameter("@JXGrad", SqlDbType.Decimal, 11);
                param.Value = model.JXGrad;
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
            strSql.Append("delete JXKHYSB ");
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
        private JXKHYSBEntity SetDsToEntity(DataSet ds, JXKHYSBEntity model)
        {
            if (ds.Tables[0].Rows[0]["SysNo"].ToString() != "")
            {
                model.SysNo = int.Parse(ds.Tables[0].Rows[0]["SysNo"].ToString());
            }
            model.JXId = ds.Tables[0].Rows[0]["JXId"].ToString();
            if (ds.Tables[0].Rows[0]["JXCategory"].ToString() != "")
            {
                model.JXCategory = int.Parse(ds.Tables[0].Rows[0]["JXCategory"].ToString());
            }
            model.JXInfo = ds.Tables[0].Rows[0]["JXInfo"].ToString();
            if (ds.Tables[0].Rows[0]["JXScore"].ToString() != "")
            {
                model.JXScore = decimal.Parse(ds.Tables[0].Rows[0]["JXScore"].ToString());
            }
            if (ds.Tables[0].Rows[0]["JXGrad"].ToString() != "")
            {
                model.JXGrad = decimal.Parse(ds.Tables[0].Rows[0]["JXGrad"].ToString());
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
        public JXKHYSBEntity GetModel(int SysNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *  from  dbo.JXKHYSB WITH (NOLOCK) ");
            strSql.Append(" where SysNo=@SysNo ");
            SqlParameter[] parameters = { 
		new SqlParameter("@SysNo", SqlDbType.Int,4 )
 		};
            parameters[0].Value = SysNo;
            JXKHYSBEntity model = new JXKHYSBEntity();
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
