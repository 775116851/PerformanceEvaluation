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
    public class JXKHGSBEntity : IComparable<JXKHGSBEntity>
    {
        public JXKHGSBEntity()
        {
            Init();
        }

        #region 成员变量和公共属性
        private int _ParentPersonSysNo;
        private int _OrganSysNo;
        private int _LowerPersonSysNo;
        private int _LowerClassSysNo;
        private int _JXSysNo;
        private int _GradScale;
        private DateTime _CreateTime;
        private DateTime _LastUpdateTime;
        private int _CreateUserSysNo;
        private int _LastUpdateUserSysNo;
        [DataMember]
        public int ParentPersonSysNo
        {
            set { _ParentPersonSysNo = value; }
            get { return _ParentPersonSysNo; }
        }

        [DataMember]
        public int OrganSysNo
        {
            set { _OrganSysNo = value; }
            get { return _OrganSysNo; }
        }

        [DataMember]
        public int LowerPersonSysNo
        {
            set { _LowerPersonSysNo = value; }
            get { return _LowerPersonSysNo; }
        }

        [DataMember]
        public int LowerClassSysNo
        {
            set { _LowerClassSysNo = value; }
            get { return _LowerClassSysNo; }
        }

        [DataMember]
        public int JXSysNo
        {
            set { _JXSysNo = value; }
            get { return _JXSysNo; }
        }

        [DataMember]
        public int GradScale
        {
            set { _GradScale = value; }
            get { return _GradScale; }
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
            ParentPersonSysNo = AppConst.IntNull;
            OrganSysNo = AppConst.IntNull;
            LowerPersonSysNo = AppConst.IntNull;
            LowerClassSysNo = AppConst.IntNull;
            JXSysNo = AppConst.IntNull;
            GradScale = AppConst.IntNull;
            CreateTime = AppConst.DateTimeNull;
            LastUpdateTime = AppConst.DateTimeNull;
            CreateUserSysNo = AppConst.IntNull;
            LastUpdateUserSysNo = AppConst.IntNull;

        }

        #region 实现IComparable<T>接口的泛型排序方法
        /// <sumary> 
        /// 根据ParentPersonSysNo字段实现的IComparable<T>接口的泛型排序方法 
        /// </sumary> 
        /// <param name="other"></param> 
        /// <returns></returns> 
        public int CompareTo(JXKHGSBEntity other)
        {
            return ParentPersonSysNo.CompareTo(other.ParentPersonSysNo);
        }
        #endregion
    }
}
