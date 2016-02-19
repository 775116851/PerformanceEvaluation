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
    /// 数据访问类PersonInfo。
    /// </summary>
    public class PersonInfoDac
    {
        public PersonInfoDac() { }
        private static PersonInfoDac _instance;
        public PersonInfoDac GetInstance()
        {
            if (_instance == null)
            { _instance = new PersonInfoDac(); }
            return _instance;
        }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PersonInfoEntity model)
        {
            SqlCommand cmd = new SqlCommand();
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            strSql.Append("insert into dbo.PersonInfo(");
            if (model.OrganSysNo != AppConst.IntNull)
            {
                strSql1.Append("OrganSysNo,");
                strSql2.Append("@OrganSysNo,");
                SqlParameter param = new SqlParameter("@OrganSysNo", SqlDbType.Int, 4);
                param.Value = model.OrganSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.ClassSysNo != AppConst.IntNull)
            {
                strSql1.Append("ClassSysNo,");
                strSql2.Append("@ClassSysNo,");
                SqlParameter param = new SqlParameter("@ClassSysNo", SqlDbType.Int, 4);
                param.Value = model.ClassSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.NAME != AppConst.StringNull)
            {
                strSql1.Append("NAME,");
                strSql2.Append("@NAME,");
                SqlParameter param = new SqlParameter("@NAME", SqlDbType.NVarChar, 64);
                param.Value = model.NAME;
                cmd.Parameters.Add(param);
            }
            if (model.BirthDate != AppConst.DateTimeNull)
            {
                strSql1.Append("BirthDate,");
                strSql2.Append("@BirthDate,");
                SqlParameter param = new SqlParameter("@BirthDate", SqlDbType.DateTime);
                param.Value = model.BirthDate;
                cmd.Parameters.Add(param);
            }
            if (model.EntryDate != AppConst.DateTimeNull)
            {
                strSql1.Append("EntryDate,");
                strSql2.Append("@EntryDate,");
                SqlParameter param = new SqlParameter("@EntryDate", SqlDbType.DateTime);
                param.Value = model.EntryDate;
                cmd.Parameters.Add(param);
            }
            if (model.OutData != AppConst.DateTimeNull)
            {
                strSql1.Append("OutData,");
                strSql2.Append("@OutData,");
                SqlParameter param = new SqlParameter("@OutData", SqlDbType.DateTime);
                param.Value = model.OutData;
                cmd.Parameters.Add(param);
            }
            if (model.Status != AppConst.IntNull)
            {
                strSql1.Append("Status,");
                strSql2.Append("@Status,");
                SqlParameter param = new SqlParameter("@Status", SqlDbType.SmallInt, 2);
                param.Value = model.Status;
                cmd.Parameters.Add(param);
            }
            if (model.SkillCategory != AppConst.StringNull)
            {
                strSql1.Append("SkillCategory,");
                strSql2.Append("@SkillCategory,");
                SqlParameter param = new SqlParameter("@SkillCategory", SqlDbType.NVarChar, 64);
                param.Value = model.SkillCategory;
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
            if (model.TelPhone != AppConst.StringNull)
            {
                strSql1.Append("TelPhone,");
                strSql2.Append("@TelPhone,");
                SqlParameter param = new SqlParameter("@TelPhone", SqlDbType.NVarChar, 64);
                param.Value = model.TelPhone;
                cmd.Parameters.Add(param);
            }
            if (model.IsLogin != AppConst.IntNull)
            {
                strSql1.Append("IsLogin,");
                strSql2.Append("@IsLogin,");
                SqlParameter param = new SqlParameter("@IsLogin", SqlDbType.SmallInt, 2);
                param.Value = model.IsLogin;
                cmd.Parameters.Add(param);
            }
            if (model.LoginPwd != AppConst.StringNull)
            {
                strSql1.Append("LoginPwd,");
                strSql2.Append("@LoginPwd,");
                SqlParameter param = new SqlParameter("@LoginPwd", SqlDbType.NVarChar, 64);
                param.Value = model.LoginPwd;
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
        public int Update(PersonInfoEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dbo.PersonInfo set ");
            SqlCommand cmd = new SqlCommand();
            if (model.SysNo != AppConst.IntNull)
            {
                SqlParameter param = new SqlParameter("@SysNo", SqlDbType.Int, 4);
                param.Value = model.SysNo;
                cmd.Parameters.Add(param);
            }
            if (model.OrganSysNo != AppConst.IntNull)
            {
                strSql.Append("OrganSysNo=@OrganSysNo,");
                SqlParameter param = new SqlParameter("@OrganSysNo", SqlDbType.Int, 4);
                param.Value = model.OrganSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.ClassSysNo != AppConst.IntNull)
            {
                strSql.Append("ClassSysNo=@ClassSysNo,");
                SqlParameter param = new SqlParameter("@ClassSysNo", SqlDbType.Int, 4);
                param.Value = model.ClassSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.NAME != AppConst.StringNull)
            {
                strSql.Append("NAME=@NAME,");
                SqlParameter param = new SqlParameter("@NAME", SqlDbType.NVarChar, 64);
                param.Value = model.NAME;
                cmd.Parameters.Add(param);
            }
            if (model.BirthDate != AppConst.DateTimeNull)
            {
                strSql.Append("BirthDate=@BirthDate,");
                SqlParameter param = new SqlParameter("@BirthDate", SqlDbType.DateTime);
                param.Value = model.BirthDate;
                cmd.Parameters.Add(param);
            }
            if (model.EntryDate != AppConst.DateTimeNull)
            {
                strSql.Append("EntryDate=@EntryDate,");
                SqlParameter param = new SqlParameter("@EntryDate", SqlDbType.DateTime);
                param.Value = model.EntryDate;
                cmd.Parameters.Add(param);
            }
            if (model.OutData != AppConst.DateTimeNull)
            {
                strSql.Append("OutData=@OutData,");
                SqlParameter param = new SqlParameter("@OutData", SqlDbType.DateTime);
                param.Value = model.OutData;
                cmd.Parameters.Add(param);
            }
            if (model.Status != AppConst.IntNull)
            {
                strSql.Append("Status=@Status,");
                SqlParameter param = new SqlParameter("@Status", SqlDbType.SmallInt, 2);
                param.Value = model.Status;
                cmd.Parameters.Add(param);
            }
            if (model.SkillCategory != AppConst.StringNull)
            {
                strSql.Append("SkillCategory=@SkillCategory,");
                SqlParameter param = new SqlParameter("@SkillCategory", SqlDbType.NVarChar, 64);
                param.Value = model.SkillCategory;
                cmd.Parameters.Add(param);
            }
            if (model.ParentPersonSysNo != AppConst.IntNull)
            {
                strSql.Append("ParentPersonSysNo=@ParentPersonSysNo,");
                SqlParameter param = new SqlParameter("@ParentPersonSysNo", SqlDbType.Int, 4);
                param.Value = model.ParentPersonSysNo;
                cmd.Parameters.Add(param);
            }
            if (model.TelPhone != AppConst.StringNull)
            {
                strSql.Append("TelPhone=@TelPhone,");
                SqlParameter param = new SqlParameter("@TelPhone", SqlDbType.NVarChar, 64);
                param.Value = model.TelPhone;
                cmd.Parameters.Add(param);
            }
            if (model.IsLogin != AppConst.IntNull)
            {
                strSql.Append("IsLogin=@IsLogin,");
                SqlParameter param = new SqlParameter("@IsLogin", SqlDbType.SmallInt, 2);
                param.Value = model.IsLogin;
                cmd.Parameters.Add(param);
            }
            if (model.LoginPwd != AppConst.StringNull)
            {
                strSql.Append("LoginPwd=@LoginPwd,");
                SqlParameter param = new SqlParameter("@LoginPwd", SqlDbType.NVarChar, 64);
                param.Value = model.LoginPwd;
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
            strSql.Append("delete PersonInfo ");
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
        private PersonInfoEntity SetDsToEntity(DataSet ds, PersonInfoEntity model)
        {
            if (ds.Tables[0].Rows[0]["SysNo"].ToString() != "")
            {
                model.SysNo = int.Parse(ds.Tables[0].Rows[0]["SysNo"].ToString());
            }
            if (ds.Tables[0].Rows[0]["OrganSysNo"].ToString() != "")
            {
                model.OrganSysNo = int.Parse(ds.Tables[0].Rows[0]["OrganSysNo"].ToString());
            }
            if (ds.Tables[0].Rows[0]["ClassSysNo"].ToString() != "")
            {
                model.ClassSysNo = int.Parse(ds.Tables[0].Rows[0]["ClassSysNo"].ToString());
            }
            model.NAME = ds.Tables[0].Rows[0]["NAME"].ToString();
            if (ds.Tables[0].Rows[0]["BirthDate"].ToString() != "")
            {
                model.BirthDate = DateTime.Parse(ds.Tables[0].Rows[0]["BirthDate"].ToString());
            }
            if (ds.Tables[0].Rows[0]["EntryDate"].ToString() != "")
            {
                model.EntryDate = DateTime.Parse(ds.Tables[0].Rows[0]["EntryDate"].ToString());
            }
            if (ds.Tables[0].Rows[0]["OutData"].ToString() != "")
            {
                model.OutData = DateTime.Parse(ds.Tables[0].Rows[0]["OutData"].ToString());
            }
            if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
            {
                model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
            }
            model.SkillCategory = ds.Tables[0].Rows[0]["SkillCategory"].ToString();
            if (ds.Tables[0].Rows[0]["ParentPersonSysNo"].ToString() != "")
            {
                model.ParentPersonSysNo = int.Parse(ds.Tables[0].Rows[0]["ParentPersonSysNo"].ToString());
            }
            model.TelPhone = ds.Tables[0].Rows[0]["TelPhone"].ToString();
            if (ds.Tables[0].Rows[0]["IsLogin"].ToString() != "")
            {
                model.IsLogin = int.Parse(ds.Tables[0].Rows[0]["IsLogin"].ToString());
            }
            model.LoginPwd = ds.Tables[0].Rows[0]["LoginPwd"].ToString();
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
        public PersonInfoEntity GetModel(int SysNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *  from  dbo.PersonInfo");
            strSql.Append(" where SysNo=@SysNo ");
            SqlParameter[] parameters = { 
		new SqlParameter("@SysNo", SqlDbType.Int,4 )
 		};
            parameters[0].Value = SysNo;
            PersonInfoEntity model = new PersonInfoEntity();
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
