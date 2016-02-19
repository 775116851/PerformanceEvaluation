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
    /// 数据访问类Organ。
    /// </summary>
    public class OrganDac
    {
        public OrganDac() { }
        private static OrganDac _instance;
        public OrganDac GetInstance()
        {
            if (_instance == null)
            { _instance = new OrganDac(); }
            return _instance;
        }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(OrganEntity model)
        {
            SqlCommand cmd = new SqlCommand();
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            strSql.Append("insert into dbo.Organ(");
            if (model.OrganSysNo != AppConst.IntNull)
            {
                strSql1.Append("OrganSysNo,");
                strSql2.Append("@OrganSysNo,");
                SqlParameter param = new SqlParameter("@OrganSysNo", SqlDbType.Int, 4);
                param.Value = model.OrganSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.OrganType != AppConst.IntNull)
            {
                strSql1.Append("OrganType,");
                strSql2.Append("@OrganType,");
                SqlParameter param = new SqlParameter("@OrganType", SqlDbType.Int, 4);
                param.Value = model.OrganType;
                cmd.Parameters.Add(param);
            }
            if (model.FunctionInfo != AppConst.StringNull)
            {
                strSql1.Append("FunctionInfo,");
                strSql2.Append("@FunctionInfo,");
                SqlParameter param = new SqlParameter("@FunctionInfo", SqlDbType.NVarChar, 200);
                param.Value = model.FunctionInfo;
                cmd.Parameters.Add(param);
            }
            if (model.OrganName != AppConst.StringNull)
            {
                strSql1.Append("OrganName,");
                strSql2.Append("@OrganName,");
                SqlParameter param = new SqlParameter("@OrganName", SqlDbType.NVarChar, 64);
                param.Value = model.OrganName;
                cmd.Parameters.Add(param);
            }
            if (model.PersonNum != AppConst.IntNull)
            {
                strSql1.Append("PersonNum,");
                strSql2.Append("@PersonNum,");
                SqlParameter param = new SqlParameter("@PersonNum", SqlDbType.Int, 4);
                param.Value = model.PersonNum;
                cmd.Parameters.Add(param);
            }
            if (model.AGradScale != AppConst.IntNull)
            {
                strSql1.Append("AGradScale,");
                strSql2.Append("@AGradScale,");
                SqlParameter param = new SqlParameter("@AGradScale", SqlDbType.Int, 4);
                param.Value = model.AGradScale;
                cmd.Parameters.Add(param);
            }
            if (model.BGradScale != AppConst.IntNull)
            {
                strSql1.Append("BGradScale,");
                strSql2.Append("@BGradScale,");
                SqlParameter param = new SqlParameter("@BGradScale", SqlDbType.Int, 4);
                param.Value = model.BGradScale;
                cmd.Parameters.Add(param);
            }
            if (model.PersonSysNo != AppConst.IntNull)
            {
                strSql1.Append("PersonSysNo,");
                strSql2.Append("@PersonSysNo,");
                SqlParameter param = new SqlParameter("@PersonSysNo", SqlDbType.Int, 4);
                param.Value = model.PersonSysNo;
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
        public int Update(OrganEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dbo.Organ set ");
            SqlCommand cmd = new SqlCommand();
            if (model.OrganSysNo != AppConst.IntNull)
            {
                strSql.Append("OrganSysNo=@OrganSysNo,");
                SqlParameter param = new SqlParameter("@OrganSysNo", SqlDbType.Int, 4);
                param.Value = model.OrganSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.OrganType != AppConst.IntNull)
            {
                strSql.Append("OrganType=@OrganType,");
                SqlParameter param = new SqlParameter("@OrganType", SqlDbType.Int, 4);
                param.Value = model.OrganType;
                cmd.Parameters.Add(param);
            }
            if (model.FunctionInfo != AppConst.StringNull)
            {
                strSql.Append("FunctionInfo=@FunctionInfo,");
                SqlParameter param = new SqlParameter("@FunctionInfo", SqlDbType.NVarChar, 200);
                param.Value = model.FunctionInfo;
                cmd.Parameters.Add(param);
            }
            if (model.OrganName != AppConst.StringNull)
            {
                strSql.Append("OrganName=@OrganName,");
                SqlParameter param = new SqlParameter("@OrganName", SqlDbType.NVarChar, 64);
                param.Value = model.OrganName;
                cmd.Parameters.Add(param);
            }
            if (model.PersonNum != AppConst.IntNull)
            {
                strSql.Append("PersonNum=@PersonNum,");
                SqlParameter param = new SqlParameter("@PersonNum", SqlDbType.Int, 4);
                param.Value = model.PersonNum;
                cmd.Parameters.Add(param);
            }
            if (model.AGradScale != AppConst.IntNull)
            {
                strSql.Append("AGradScale=@AGradScale,");
                SqlParameter param = new SqlParameter("@AGradScale", SqlDbType.Int, 4);
                param.Value = model.AGradScale;
                cmd.Parameters.Add(param);
            }
            if (model.BGradScale != AppConst.IntNull)
            {
                strSql.Append("BGradScale=@BGradScale,");
                SqlParameter param = new SqlParameter("@BGradScale", SqlDbType.Int, 4);
                param.Value = model.BGradScale;
                cmd.Parameters.Add(param);
            }
            if (model.PersonSysNo != AppConst.IntNull)
            {
                strSql.Append("PersonSysNo=@PersonSysNo,");
                SqlParameter param = new SqlParameter("@PersonSysNo", SqlDbType.Int, 4);
                param.Value = model.PersonSysNo;
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
            strSql.Append(" where OrganSysNo=@OrganSysNo and OrganType=@OrganType and FunctionInfo=@FunctionInfo and OrganName=@OrganName and PersonNum=@PersonNum and AGradScale=@AGradScale and BGradScale=@BGradScale and PersonSysNo=@PersonSysNo and CreateTime=@CreateTime and LastUpdateTime=@LastUpdateTime and CreateUserSysNo=@CreateUserSysNo and LastUpdateUserSysNo=@LastUpdateUserSysNo ");
            cmd.CommandText = strSql.ToString();
            return SqlHelper.ExecuteNonQuery(AppConfig.Conn_Mall, cmd);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int OrganSysNo, int OrganType, string FunctionInfo, string OrganName, int PersonNum, int AGradScale, int BGradScale, int PersonSysNo, DateTime CreateTime, DateTime LastUpdateTime, int CreateUserSysNo, int LastUpdateUserSysNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete Organ ");
            strSql.Append("where OrganSysNo=@OrganSysNo and OrganType=@OrganType and FunctionInfo=@FunctionInfo and OrganName=@OrganName and PersonNum=@PersonNum and AGradScale=@AGradScale and BGradScale=@BGradScale and PersonSysNo=@PersonSysNo and CreateTime=@CreateTime and LastUpdateTime=@LastUpdateTime and CreateUserSysNo=@CreateUserSysNo and LastUpdateUserSysNo=@LastUpdateUserSysNo ");
            SqlCommand cmd = new SqlCommand(strSql.ToString());
            SqlParameter[] parameters = {
                 new SqlParameter("@OrganSysNo",SqlDbType.Int,4),
                 new SqlParameter("@OrganType",SqlDbType.Int,4),
                 new SqlParameter("@FunctionInfo",SqlDbType.NVarChar,200),
                 new SqlParameter("@OrganName",SqlDbType.NVarChar,64),
                 new SqlParameter("@PersonNum",SqlDbType.Int,4),
                 new SqlParameter("@AGradScale",SqlDbType.Int,4),
                 new SqlParameter("@BGradScale",SqlDbType.Int,4),
                 new SqlParameter("@PersonSysNo",SqlDbType.Int,4),
                 new SqlParameter("@CreateTime",SqlDbType.DateTime),
                 new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
                 new SqlParameter("@CreateUserSysNo",SqlDbType.Int,4),
                 new SqlParameter("@LastUpdateUserSysNo",SqlDbType.Int,4)
             };
            if (model.SysNo != AppConst.IntNull)
                parameters[0].Value = model.OrganSysNo;
            else
                parameters[0].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[0]);
            if (model.SysNo != AppConst.IntNull)
                parameters[1].Value = model.OrganType;
            else
                parameters[1].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[1]);
            if (model.SysNo != AppConst.StringNull)
                parameters[2].Value = model.FunctionInfo;
            else
                parameters[2].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[2]);
            if (model.SysNo != AppConst.StringNull)
                parameters[3].Value = model.OrganName;
            else
                parameters[3].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[3]);
            if (model.SysNo != AppConst.IntNull)
                parameters[4].Value = model.PersonNum;
            else
                parameters[4].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[4]);
            if (model.SysNo != AppConst.IntNull)
                parameters[5].Value = model.AGradScale;
            else
                parameters[5].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[5]);
            if (model.SysNo != AppConst.IntNull)
                parameters[6].Value = model.BGradScale;
            else
                parameters[6].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[6]);
            if (model.SysNo != AppConst.IntNull)
                parameters[7].Value = model.PersonSysNo;
            else
                parameters[7].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[7]);
            if (model.SysNo != AppConst.DateTimeNull)
                parameters[8].Value = model.CreateTime;
            else
                parameters[8].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[8]);
            if (model.SysNo != AppConst.DateTimeNull)
                parameters[9].Value = model.LastUpdateTime;
            else
                parameters[9].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[9]);
            if (model.SysNo != AppConst.IntNull)
                parameters[10].Value = model.CreateUserSysNo;
            else
                parameters[10].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[10]);
            if (model.SysNo != AppConst.IntNull)
                parameters[11].Value = model.LastUpdateUserSysNo;
            else
                parameters[11].Value = System.DBNull.Value;
            cmd.Parameters.Add(parameters[11]);
            return SqlHelper.ExecuteNonQuery(AppConfig.Conn_Mall, cmd);
        }
        /// <summary>
        /// 将DataRow赋值到实体
        /// </summary>
        private OrganEntity SetDsToEntity(DataSet ds, OrganEntity model)
        {
            if (ds.Tables[0].Rows[0]["OrganSysNo"].ToString() != "")
            {
                model.OrganSysNo = int.Parse(ds.Tables[0].Rows[0]["OrganSysNo"].ToString());
            }
            if (ds.Tables[0].Rows[0]["OrganType"].ToString() != "")
            {
                model.OrganType = int.Parse(ds.Tables[0].Rows[0]["OrganType"].ToString());
            }
            model.FunctionInfo = ds.Tables[0].Rows[0]["FunctionInfo"].ToString();
            model.OrganName = ds.Tables[0].Rows[0]["OrganName"].ToString();
            if (ds.Tables[0].Rows[0]["PersonNum"].ToString() != "")
            {
                model.PersonNum = int.Parse(ds.Tables[0].Rows[0]["PersonNum"].ToString());
            }
            if (ds.Tables[0].Rows[0]["AGradScale"].ToString() != "")
            {
                model.AGradScale = int.Parse(ds.Tables[0].Rows[0]["AGradScale"].ToString());
            }
            if (ds.Tables[0].Rows[0]["BGradScale"].ToString() != "")
            {
                model.BGradScale = int.Parse(ds.Tables[0].Rows[0]["BGradScale"].ToString());
            }
            if (ds.Tables[0].Rows[0]["PersonSysNo"].ToString() != "")
            {
                model.PersonSysNo = int.Parse(ds.Tables[0].Rows[0]["PersonSysNo"].ToString());
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
        public OrganEntity GetModel(int OrganSysNo, int OrganType, string FunctionInfo, string OrganName, int PersonNum, int AGradScale, int BGradScale, int PersonSysNo, DateTime CreateTime, DateTime LastUpdateTime, int CreateUserSysNo, int LastUpdateUserSysNo, int BankSysNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrganSysNo,OrganType,FunctionInfo,OrganName,PersonNum,AGradScale,BGradScale,PersonSysNo,CreateTime,LastUpdateTime,CreateUserSysNo,LastUpdateUserSysNo ");
            strSql.Append("where OrganSysNo=@OrganSysNo and OrganType=@OrganType and FunctionInfo=@FunctionInfo and OrganName=@OrganName and PersonNum=@PersonNum and AGradScale=@AGradScale and BGradScale=@BGradScale and PersonSysNo=@PersonSysNo and CreateTime=@CreateTime and LastUpdateTime=@LastUpdateTime and CreateUserSysNo=@CreateUserSysNo and LastUpdateUserSysNo=@LastUpdateUserSysNo ");
            SqlParameter[] parameters = {
		new SqlParameter("@OrganSysNo",SqlDbType.Int,4),
		new SqlParameter("@OrganType",SqlDbType.Int,4),
		new SqlParameter("@FunctionInfo",SqlDbType.NVarChar,200),
		new SqlParameter("@OrganName",SqlDbType.NVarChar,64),
		new SqlParameter("@PersonNum",SqlDbType.Int,4),
		new SqlParameter("@AGradScale",SqlDbType.Int,4),
		new SqlParameter("@BGradScale",SqlDbType.Int,4),
		new SqlParameter("@PersonSysNo",SqlDbType.Int,4),
		new SqlParameter("@CreateTime",SqlDbType.DateTime),
		new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
		new SqlParameter("@CreateUserSysNo",SqlDbType.Int,4),
		new SqlParameter("@LastUpdateUserSysNo",SqlDbType.Int,4)
             };
            parameters[0].Value = model.OrganSysNo;
            parameters[1].Value = model.OrganType;
            parameters[2].Value = model.FunctionInfo;
            parameters[3].Value = model.OrganName;
            parameters[4].Value = model.PersonNum;
            parameters[5].Value = model.AGradScale;
            parameters[6].Value = model.BGradScale;
            parameters[7].Value = model.PersonSysNo;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.LastUpdateTime;
            parameters[10].Value = model.CreateUserSysNo;
            parameters[11].Value = model.LastUpdateUserSysNo;
            OrganEntity model = new OrganEntity();
            DataSet ds = ExecuteDataSet(AppConfig.Conn_Mall, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["OrganSysNo"].ToString() != "")
                {
                    model.OrganSysNo = int.Parse(ds.Tables[0].Rows[0]["OrganSysNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrganType"].ToString() != "")
                {
                    model.OrganType = int.Parse(ds.Tables[0].Rows[0]["OrganType"].ToString());
                }
                model.FunctionInfo = ds.Tables[0].Rows[0]["FunctionInfo"].ToString();
                model.OrganName = ds.Tables[0].Rows[0]["OrganName"].ToString();
                if (ds.Tables[0].Rows[0]["PersonNum"].ToString() != "")
                {
                    model.PersonNum = int.Parse(ds.Tables[0].Rows[0]["PersonNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AGradScale"].ToString() != "")
                {
                    model.AGradScale = int.Parse(ds.Tables[0].Rows[0]["AGradScale"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BGradScale"].ToString() != "")
                {
                    model.BGradScale = int.Parse(ds.Tables[0].Rows[0]["BGradScale"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PersonSysNo"].ToString() != "")
                {
                    model.PersonSysNo = int.Parse(ds.Tables[0].Rows[0]["PersonSysNo"].ToString());
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
