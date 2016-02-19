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
        private int _JXCategory;
        private int _JXSysNo;
        private string _JXInfo;
        private int _JXScore;
        private DateTime _CreateTime;
        private DateTime _LastUpdateTime;
        private int _CreateUserSysNo;
        private int _LastUpdateUserSysNo;
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
        public string JXInfo
        {
            set { _JXInfo = value; }
            get { return _JXInfo; }
        }

        [DataMember]
        public int JXScore
        {
            set { _JXScore = value; }
            get { return _JXScore; }
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
            JXCategory = AppConst.IntNull;
            JXSysNo = AppConst.IntNull;
            JXInfo = AppConst.StringNull;
            JXScore = AppConst.IntNull;
            CreateTime = AppConst.DateTimeNull;
            LastUpdateTime = AppConst.DateTimeNull;
            CreateUserSysNo = AppConst.IntNull;
            LastUpdateUserSysNo = AppConst.IntNull;

        }

        #region 实现IComparable<T>接口的泛型排序方法
        /// <sumary> 
        /// 根据JXCategory字段实现的IComparable<T>接口的泛型排序方法 
        /// </sumary> 
        /// <param name="other"></param> 
        /// <returns></returns> 
        public int CompareTo(JXKHYSBEntity other)
        {
            return JXCategory.CompareTo(other.JXCategory);
        }
        #endregion
    }

}
