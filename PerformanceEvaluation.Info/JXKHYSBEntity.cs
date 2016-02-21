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
    public class JXKHYSBEntity : IComparable<JXKHYSBEntity>
    {
        public JXKHYSBEntity()
        {
            Init();
        }

        #region 成员变量和公共属性
        private int _SysNo;
        private string _JXId;
        private int _JXCategory;
        private string _JXInfo;
        private decimal _JXScore;
        private decimal _JXGrad;
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
        public string JXId
        {
            set { _JXId = value; }
            get { return _JXId; }
        }

        [DataMember]
        public int JXCategory
        {
            set { _JXCategory = value; }
            get { return _JXCategory; }
        }

        [DataMember]
        public string JXInfo
        {
            set { _JXInfo = value; }
            get { return _JXInfo; }
        }

        [DataMember]
        public decimal JXScore
        {
            set { _JXScore = value; }
            get { return _JXScore; }
        }

        [DataMember]
        public decimal JXGrad
        {
            set { _JXGrad = value; }
            get { return _JXGrad; }
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
            JXId = AppConst.StringNull;
            JXCategory = AppConst.IntNull;
            JXInfo = AppConst.StringNull;
            JXScore = AppConst.DecimalNull;
            JXGrad = AppConst.DecimalNull;
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
        public int CompareTo(JXKHYSBEntity other)
        {
            return SysNo.CompareTo(other.SysNo);
        }
        #endregion
    }
}
