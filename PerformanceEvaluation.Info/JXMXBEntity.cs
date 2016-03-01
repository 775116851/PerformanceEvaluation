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
    public class JXMXBEntity : IComparable<JXMXBEntity>
    {
        public JXMXBEntity()
        {
            Init();
        }

        #region 成员变量和公共属性
        private int _SysNo;
        private int _LowerPersonSysNo;
        private int _JXCategory;
        private int _JXSysNo;
        private int _ParentPersonSysNo;
        private string _JXCycle;
        private decimal _JXScore;
        private int _JXLevel;
        private int _JXMXCategory;
        private DateTime _CreateTime;
        private DateTime _LastUpdateTime;
        private int _CreateUserSysNo;
        private int _LastUpdateUserSysNo;
        private int _Status;
        private int _RecordType;

        private double _TotalScore;//评分总分(评分*权重/满分) 非数据库字段
        [DataMember]
        public int SysNo
        {
            set { _SysNo = value; }
            get { return _SysNo; }
        }

        [DataMember]
        public int LowerPersonSysNo
        {
            set { _LowerPersonSysNo = value; }
            get { return _LowerPersonSysNo; }
        }

        [DataMember]
        public int JXCategory
        {
            set { _JXCategory = value; }
            get { return _JXCategory; }
        }

        [DataMember]
        public int JXSysNo
        {
            set { _JXSysNo = value; }
            get { return _JXSysNo; }
        }

        [DataMember]
        public int ParentPersonSysNo
        {
            set { _ParentPersonSysNo = value; }
            get { return _ParentPersonSysNo; }
        }

        [DataMember]
        public string JXCycle
        {
            set { _JXCycle = value; }
            get { return _JXCycle; }
        }

        [DataMember]
        public decimal JXScore
        {
            set { _JXScore = value; }
            get { return _JXScore; }
        }

        [DataMember]
        public int JXLevel
        {
            set { _JXLevel = value; }
            get { return _JXLevel; }
        }

        [DataMember]
        public int JXMXCategory
        {
            set { _JXMXCategory = value; }
            get { return _JXMXCategory; }
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
        public double TotalScore
        {
            set { _TotalScore = value; }
            get { return _TotalScore; }
        }

        [DataMember]
        public int Status
        {
            set { _Status = value; }
            get { return _Status; }
        }

        [DataMember]
        public int RecordType
        {
            set { _RecordType = value; }
            get { return _RecordType; }
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
            LowerPersonSysNo = AppConst.IntNull;
            JXCategory = AppConst.IntNull;
            JXSysNo = AppConst.IntNull;
            ParentPersonSysNo = AppConst.IntNull;
            JXCycle = AppConst.StringNull;
            JXScore = AppConst.DecimalNull;
            JXLevel = AppConst.IntNull;
            JXMXCategory = AppConst.IntNull;
            CreateTime = AppConst.DateTimeNull;
            LastUpdateTime = AppConst.DateTimeNull;
            CreateUserSysNo = AppConst.IntNull;
            LastUpdateUserSysNo = AppConst.IntNull;
            Status = AppConst.IntNull;
            RecordType = AppConst.IntNull;

            TotalScore = AppConst.DoubleNull;
        }

        #region 实现IComparable<T>接口的泛型排序方法
        /// <sumary> 
        /// 根据SysNo字段实现的IComparable<T>接口的泛型排序方法 
        /// </sumary> 
        /// <param name="other"></param> 
        /// <returns></returns> 
        public int CompareTo(JXMXBEntity other)
        {
            return SysNo.CompareTo(other.SysNo);
        }
        #endregion
    }
}
