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
    public class OrganEntity : IComparable<OrganEntity>
    {
        public OrganEntity()
        {
            Init();
        }

        #region 成员变量和公共属性
        private int _SysNo;
        private string _OrganId;
        private int _OrganType;
        private string _FunctionInfo;
        private string _OrganName;
        private int _PersonNum;
        private decimal _AGradScale;
        private decimal _BGradScale;
        private int _PersonSysNo;
        private DateTime _CreateTime;
        private DateTime _LastUpdateTime;
        private int _CreateUserSysNo;
        private int _LastUpdateUserSysNo;
        [DataMember]
        public int SysNo
        {
            set { _SysNo = value; }
            get { return _SysNo; }
        }

        [DataMember]
        public string OrganId
        {
            set { _OrganId = value; }
            get { return _OrganId; }
        }

        [DataMember]
        public int OrganType
        {
            set { _OrganType = value; }
            get { return _OrganType; }
        }

        [DataMember]
        public string FunctionInfo
        {
            set { _FunctionInfo = value; }
            get { return _FunctionInfo; }
        }

        [DataMember]
        public string OrganName
        {
            set { _OrganName = value; }
            get { return _OrganName; }
        }

        [DataMember]
        public int PersonNum
        {
            set { _PersonNum = value; }
            get { return _PersonNum; }
        }

        [DataMember]
        public decimal AGradScale
        {
            set { _AGradScale = value; }
            get { return _AGradScale; }
        }

        [DataMember]
        public decimal BGradScale
        {
            set { _BGradScale = value; }
            get { return _BGradScale; }
        }

        [DataMember]
        public int PersonSysNo
        {
            set { _PersonSysNo = value; }
            get { return _PersonSysNo; }
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


        #endregion

        [System.Runtime.Serialization.OnDeserializing]
        private void OnDeserializing(StreamingContext ctx)
        {
            Init();
        }

        public void Init()
        {
            SysNo = AppConst.IntNull;
            OrganId = AppConst.StringNull;
            OrganType = AppConst.IntNull;
            FunctionInfo = AppConst.StringNull;
            OrganName = AppConst.StringNull;
            PersonNum = AppConst.IntNull;
            AGradScale = AppConst.DecimalNull;
            BGradScale = AppConst.DecimalNull;
            PersonSysNo = AppConst.IntNull;
            CreateTime = AppConst.DateTimeNull;
            LastUpdateTime = AppConst.DateTimeNull;
            CreateUserSysNo = AppConst.IntNull;
            LastUpdateUserSysNo = AppConst.IntNull;

        }

        #region 实现IComparable<T>接口的泛型排序方法
        /// <sumary> 
        /// 根据SysNo字段实现的IComparable<T>接口的泛型排序方法 
        /// </sumary> 
        /// <param name="other"></param> 
        /// <returns></returns> 
        public int CompareTo(OrganEntity other)
        {
            return SysNo.CompareTo(other.SysNo);
        }
        #endregion
    }
}
