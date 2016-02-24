using PerformanceEvaluation.Cmn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PerformanceEvaluation.PerformanceEvaluation.Info
{
    [System.Runtime.Serialization.DataContract]
    [Serializable]
    public class PersonInfoEntity : IComparable<PersonInfoEntity>
    {
        public PersonInfoEntity()
        {
            Init();
        }

        #region 成员变量和公共属性
        private int _SysNo;
        private int _OrganSysNo;
        private int _ClassSysNo;
        private string _Name;
        private DateTime _BirthDate;
        private DateTime _EntryDate;
        private DateTime _OutData;
        private int _Status;
        private string _SkillCategory;
        private int _ParentPersonSysNo;
        private string _TelPhone;
        private int _IsLogin;
        private string _LoginPwd;
        private DateTime _CreateTime;
        private DateTime _LastUpdateTime;
        private int _CreateUserSysNo;
        private int _LastUpdateUserSysNo;
        private int _IsAdmin;

        private int _UserType;//用户类型(1普通用户 2绩效管理员 3公司老大) 非数据库字段
        private int _EJBAdmin;//是否二级部管理人员(1是 0否) 非数据库字段
        [DataMember]
        public int SysNo
        {
            set { _SysNo = value; }
            get { return _SysNo; }
        }

        [DataMember]
        public int OrganSysNo
        {
            set { _OrganSysNo = value; }
            get { return _OrganSysNo; }
        }

        [DataMember]
        public int ClassSysNo
        {
            set { _ClassSysNo = value; }
            get { return _ClassSysNo; }
        }

        [DataMember]
        public string Name
        {
            set { _Name = value; }
            get { return _Name; }
        }

        [DataMember]
        public DateTime BirthDate
        {
            set { _BirthDate = value; }
            get { return _BirthDate; }
        }

        [DataMember]
        public DateTime EntryDate
        {
            set { _EntryDate = value; }
            get { return _EntryDate; }
        }

        [DataMember]
        public DateTime OutData
        {
            set { _OutData = value; }
            get { return _OutData; }
        }

        [DataMember]
        public int Status
        {
            set { _Status = value; }
            get { return _Status; }
        }

        [DataMember]
        public string SkillCategory
        {
            set { _SkillCategory = value; }
            get { return _SkillCategory; }
        }

        [DataMember]
        public int ParentPersonSysNo
        {
            set { _ParentPersonSysNo = value; }
            get { return _ParentPersonSysNo; }
        }

        [DataMember]
        public string TelPhone
        {
            set { _TelPhone = value; }
            get { return _TelPhone; }
        }

        [DataMember]
        public int IsLogin
        {
            set { _IsLogin = value; }
            get { return _IsLogin; }
        }

        [DataMember]
        public string LoginPwd
        {
            set { _LoginPwd = value; }
            get { return _LoginPwd; }
        }

        [DataMember]
        public DateTime CreateTime
        {
            set { _CreateTime = value; }
            get { return _CreateTime; }
        }

        [DataMember]
        public DateTime LastUpdateTime
        {
            set { _LastUpdateTime = value; }
            get { return _LastUpdateTime; }
        }

        [DataMember]
        public int CreateUserSysNo
        {
            set { _CreateUserSysNo = value; }
            get { return _CreateUserSysNo; }
        }

        [DataMember]
        public int LastUpdateUserSysNo
        {
            set { _LastUpdateUserSysNo = value; }
            get { return _LastUpdateUserSysNo; }
        }

        [DataMember]
        public int IsAdmin
        {
            set { _IsAdmin = value; }
            get { return _IsAdmin; }
        }

        [DataMember]
        public int UserType
        {
            set { _UserType = value; }
            get { return _UserType; }
        }

        [DataMember]
        public int EJBAdmin
        {
            set { _EJBAdmin = value; }
            get { return _EJBAdmin; }
        }


        #endregion

        [System.Runtime.Serialization.OnDeserializing]
        private void OnDeserializing(StreamingContext ctx)
        {
            Init();
        }

        public void Init()
        {
            SysNo = AppConst.IntNull;
            OrganSysNo = AppConst.IntNull;
            ClassSysNo = AppConst.IntNull;
            Name = AppConst.StringNull;
            BirthDate = AppConst.DateTimeNull;
            EntryDate = AppConst.DateTimeNull;
            OutData = AppConst.DateTimeNull;
            Status = AppConst.IntNull;
            SkillCategory = AppConst.StringNull;
            ParentPersonSysNo = AppConst.IntNull;
            TelPhone = AppConst.StringNull;
            IsLogin = AppConst.IntNull;
            LoginPwd = AppConst.StringNull;
            CreateTime = AppConst.DateTimeNull;
            LastUpdateTime = AppConst.DateTimeNull;
            CreateUserSysNo = AppConst.IntNull;
            LastUpdateUserSysNo = AppConst.IntNull;
            IsAdmin = AppConst.IntNull;

            UserType = AppConst.IntNull;
            EJBAdmin = AppConst.IntNull;
        }

        #region 实现IComparable<T>接口的泛型排序方法
        /// <sumary> 
        /// 根据SysNo字段实现的IComparable<T>接口的泛型排序方法 
        /// </sumary> 
        /// <param name="other"></param> 
        /// <returns></returns> 
        public int CompareTo(PersonInfoEntity other)
        {
            return SysNo.CompareTo(other.SysNo);
        }
        #endregion
    }
}
