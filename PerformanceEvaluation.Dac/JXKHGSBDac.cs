using PerformanceEvaluation.PerformanceEvaluation.Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PerformanceEvaluation.PerformanceEvaluation.Dac
{
    /// <summary>
    /// 数据访问类JXKHGSB。
    /// </summary>
    public class JXKHGSBDac
    {
        public JXKHGSBDac() { }
        private static JXKHGSBDac _instance;
        public JXKHGSBDac GetInstance()
        {
            if (_instance == null)
            { _instance = new JXKHGSBDac(); }
            return _instance;
        }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(JXKHGSBEntity model)
        {
            SqlCommand cmd = new SqlCommand();
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            strSql.Append("insert into dbo.JXKHGSB(");
            if (model.ParentPersonSysNo != AppConst.IntNull)
            {
                strSql1.Append("ParentPersonSysNo,");
                strSql2.Append("@ParentPersonSysNo,");
                SqlParameter param = new SqlParameter("@ParentPersonSysNo", SqlDbType.Int, 4);
                param.Value = model.ParentPersonSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.OrganSysNo != AppConst.IntNull)
            {
                strSql1.Append("OrganSysNo,");
                strSql2.Append("@OrganSysNo,");
                SqlParameter param = new SqlParameter("@OrganSysNo", SqlDbType.Int, 4);
                param.Value = model.OrganSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.LowerPersonSysNo != AppConst.IntNull)
            {
                strSql1.Append("LowerPersonSysNo,");
                strSql2.Append("@LowerPersonSysNo,");
                SqlParameter param = new SqlParameter("@LowerPersonSysNo", SqlDbType.Int, 4);
                param.Value = model.LowerPersonSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.LowerClassSysNo != AppConst.IntNull)
            {
                strSql1.Append("LowerClassSysNo,");
                strSql2.Append("@LowerClassSysNo,");
                SqlParameter param = new SqlParameter("@LowerClassSysNo", SqlDbType.Int, 4);
                param.Value = model.LowerClassSysNo;
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
            if (model.GradScale != AppConst.IntNull)
            {
                strSql1.Append("GradScale,");
                strSql2.Append("@GradScale,");
                SqlParameter param = new SqlParameter("@GradScale", SqlDbType.Int, 4);
                param.Value = model.GradScale;
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
            cmd.CommandText = strSql.ToString();
            SqlHelper.ExecuteNonQuery(AppConfig.Conn_Mall, cmd);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(JXKHGSBEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dbo.JXKHGSB set ");
            SqlCommand cmd = new SqlCommand();
            if (model.ParentPersonSysNo != AppConst.IntNull)
            {
                strSql.Append("ParentPersonSysNo=@ParentPersonSysNo,");
                SqlParameter param = new SqlParameter("@ParentPersonSysNo", SqlDbType.Int, 4);
                param.Value = model.ParentPersonSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.OrganSysNo != AppConst.IntNull)
            {
                strSql.Append("OrganSysNo=@OrganSysNo,");
                SqlParameter param = new SqlParameter("@OrganSysNo", SqlDbType.Int, 4);
                param.Value = model.OrganSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.LowerPersonSysNo != AppConst.IntNull)
            {
                strSql.Append("LowerPersonSysNo=@LowerPersonSysNo,");
                SqlParameter param = new SqlParameter("@LowerPersonSysNo", SqlDbType.Int, 4);
                param.Value = model.LowerPersonSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.LowerClassSysNo != AppConst.IntNull)
            {
                strSql.Append("LowerClassSysNo=@LowerClassSysNo,");
                SqlParameter param = new SqlParameter("@LowerClassSysNo", SqlDbType.Int, 4);
                param.Value = model.LowerClassSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.JXSysNo != AppConst.IntNull)
            {
                strSql.Append("JXSysNo=@JXSysNo,");
                SqlParameter param = new SqlParameter("@JXSysNo", SqlDbType.Int, 4);
                param.Value = model.JXSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.GradScale != AppConst.IntNull)
            {
                strSql.Append("GradScale=@GradScale,");
                SqlParameter param = new SqlParameter("@GradScale", SqlDbType.Int, 4);
                param.Value = model.GradScale;
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
            strSql.Append(" where ParentPersonSysNo=@ParentPersonSysNo and OrganSysNo=@OrganSysNo and LowerPersonSysNo=@LowerPersonSysNo and LowerClassSysNo=@LowerClassSysNo and JXSysNo=@JXSysNo and GradScale=@GradScale and CreateTime=@CreateTime and LastUpdateTime=@LastUpdateTime and CreateUserSysNo=@CreateUserSysNo and LastUpdateUserSysNo=@LastUpdateUserSysNo ");
            cmd.CommandText = strSql.ToString();
            return SqlHelper.ExecuteNonQuery(AppConfig.Conn_Mall, cmd);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ParentPersonSysNo, int OrganSysNo, int LowerPersonSysNo, int LowerClassSysNo, int JXSysNo, int GradScale, DateTime CreateTime, DateTime LastUpdateTime, int CreateUserSysNo, int LastUpdateUserSysNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete JXKHGSB ");
            strSql.Append("where ParentPersonSysNo=@ParentPersonSysNo and OrganSysNo=@OrganSysNo and LowerPersonSysNo=@LowerPersonSysNo and LowerClassSysNo=@LowerClassSysNo and JXSysNo=@JXSysNo and GradScale=@GradScale and CreateTime=@CreateTime and LastUpdateTime=@LastUpdateTime and CreateUserSysNo=@CreateUserSysNo and LastUpdateUserSysNo=@LastUpdateUserSysNo ");
            SqlCommand cmd = new SqlCommand(strSql.ToString());
            SqlParameter[] parameters = {
                 new SqlParameter("@ParentPersonSysNo",SqlDbType.Int,4),
                 new SqlParameter("@OrganSysNo",SqlDbType.Int,4),
                 new SqlParameter("@LowerPersonSysNo",SqlDbType.Int,4),
                 new SqlParameter("@LowerClassSysNo",SqlDbType.Int,4),
                 new SqlParameter("@JXSysNo",SqlDbType.Int,4),
                 new SqlParameter("@GradScale",SqlDbType.Int,4),
                 new SqlParameter("@CreateTime",SqlDbType.DateTime),
                 new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
                 new SqlParameter("@CreateUserSysNo",SqlDbType.Int,4),
                 new SqlParameter("@LastUpdateUserSysNo",SqlDbType.Int,4)
             };
            if (model.SysNo != AppConst.IntNull)
                parameters[0].Value = model.ParentPersonSysNo;
            else
                parameters[0].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[0]);
            if (model.SysNo != AppConst.IntNull)
                parameters[1].Value = model.OrganSysNo;
            else
                parameters[1].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[1]);
            if (model.SysNo != AppConst.IntNull)
                parameters[2].Value = model.LowerPersonSysNo;
            else
                parameters[2].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[2]);
            if (model.SysNo != AppConst.IntNull)
                parameters[3].Value = model.LowerClassSysNo;
            else
                parameters[3].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[3]);
            if (model.SysNo != AppConst.IntNull)
                parameters[4].Value = model.JXSysNo;
            else
                parameters[4].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[4]);
            if (model.SysNo != AppConst.IntNull)
                parameters[5].Value = model.GradScale;
            else
                parameters[5].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[5]);
            if (model.SysNo != AppConst.DateTimeNull)
                parameters[6].Value = model.CreateTime;
            else
                parameters[6].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[6]);
            if (model.SysNo != AppConst.DateTimeNull)
                parameters[7].Value = model.LastUpdateTime;
            else
                parameters[7].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[7]);
            if (model.SysNo != AppConst.IntNull)
                parameters[8].Value = model.CreateUserSysNo;
            else
                parameters[8].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[8]);
            if (model.SysNo != AppConst.IntNull)
                parameters[9].Value = model.LastUpdateUserSysNo;
            else
                parameters[9].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[9]);
            return SqlHelper.ExecuteNonQuery(AppConfig.Conn_Mall, cmd);
        }
        /// <summary>
        /// 将DataRow赋值到实体
        /// </summary>
        private JXKHGSBEntity SetDsToEntity(DataSet ds, JXKHGSBEntity model)
        {
            if (ds.Tables[0].Rows[0]["ParentPersonSysNo"].ToString() != "")
            {
                model.ParentPersonSysNo = int.Parse(ds.Tables[0].Rows[0]["ParentPersonSysNo"].ToString());
            }
            if (ds.Tables[0].Rows[0]["OrganSysNo"].ToString() != "")
            {
                model.OrganSysNo = int.Parse(ds.Tables[0].Rows[0]["OrganSysNo"].ToString());
            }
            if (ds.Tables[0].Rows[0]["LowerPersonSysNo"].ToString() != "")
            {
                model.LowerPersonSysNo = int.Parse(ds.Tables[0].Rows[0]["LowerPersonSysNo"].ToString());
            }
            if (ds.Tables[0].Rows[0]["LowerClassSysNo"].ToString() != "")
            {
                model.LowerClassSysNo = int.Parse(ds.Tables[0].Rows[0]["LowerClassSysNo"].ToString());
            }
            if (ds.Tables[0].Rows[0]["JXSysNo"].ToString() != "")
            {
                model.JXSysNo = int.Parse(ds.Tables[0].Rows[0]["JXSysNo"].ToString());
            }
            if (ds.Tables[0].Rows[0]["GradScale"].ToString() != "")
            {
                model.GradScale = int.Parse(ds.Tables[0].Rows[0]["GradScale"].ToString());
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
        public JXKHGSBEntity GetModel(int ParentPersonSysNo, int OrganSysNo, int LowerPersonSysNo, int LowerClassSysNo, int JXSysNo, int GradScale, DateTime CreateTime, DateTime LastUpdateTime, int CreateUserSysNo, int LastUpdateUserSysNo, int BankSysNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXSysNo,GradScale,CreateTime,LastUpdateTime,CreateUserSysNo,LastUpdateUserSysNo ");
            strSql.Append("where ParentPersonSysNo=@ParentPersonSysNo and OrganSysNo=@OrganSysNo and LowerPersonSysNo=@LowerPersonSysNo and LowerClassSysNo=@LowerClassSysNo and JXSysNo=@JXSysNo and GradScale=@GradScale and CreateTime=@CreateTime and LastUpdateTime=@LastUpdateTime and CreateUserSysNo=@CreateUserSysNo and LastUpdateUserSysNo=@LastUpdateUserSysNo ");
            SqlParameter[] parameters = {
		new SqlParameter("@ParentPersonSysNo",SqlDbType.Int,4),
		new SqlParameter("@OrganSysNo",SqlDbType.Int,4),
		new SqlParameter("@LowerPersonSysNo",SqlDbType.Int,4),
		new SqlParameter("@LowerClassSysNo",SqlDbType.Int,4),
		new SqlParameter("@JXSysNo",SqlDbType.Int,4),
		new SqlParameter("@GradScale",SqlDbType.Int,4),
		new SqlParameter("@CreateTime",SqlDbType.DateTime),
		new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
		new SqlParameter("@CreateUserSysNo",SqlDbType.Int,4),
		new SqlParameter("@LastUpdateUserSysNo",SqlDbType.Int,4)
             };
            parameters[0].Value = model.ParentPersonSysNo;
            parameters[1].Value = model.OrganSysNo;
            parameters[2].Value = model.LowerPersonSysNo;
            parameters[3].Value = model.LowerClassSysNo;
            parameters[4].Value = model.JXSysNo;
            parameters[5].Value = model.GradScale;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.LastUpdateTime;
            parameters[8].Value = model.CreateUserSysNo;
            parameters[9].Value = model.LastUpdateUserSysNo;
            JXKHGSBEntity model = new JXKHGSBEntity();
            DataSet ds = ExecuteDataSet(AppConfig.Conn_Mall, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ParentPersonSysNo"].ToString() != "")
                {
                    model.ParentPersonSysNo = int.Parse(ds.Tables[0].Rows[0]["ParentPersonSysNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrganSysNo"].ToString() != "")
                {
                    model.OrganSysNo = int.Parse(ds.Tables[0].Rows[0]["OrganSysNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LowerPersonSysNo"].ToString() != "")
                {
                    model.LowerPersonSysNo = int.Parse(ds.Tables[0].Rows[0]["LowerPersonSysNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LowerClassSysNo"].ToString() != "")
                {
                    model.LowerClassSysNo = int.Parse(ds.Tables[0].Rows[0]["LowerClassSysNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JXSysNo"].ToString() != "")
                {
                    model.JXSysNo = int.Parse(ds.Tables[0].Rows[0]["JXSysNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GradScale"].ToString() != "")
                {
                    model.GradScale = int.Parse(ds.Tables[0].Rows[0]["GradScale"].ToString());
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
            else
            {
                return null;
            }
        }
        #endregion  成员方法
    }
}
