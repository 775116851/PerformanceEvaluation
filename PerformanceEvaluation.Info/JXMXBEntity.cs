﻿using PerformanceEvaluation.Cmn;
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
        private int _LowerPersonSysNo;
        private int _JXCategory;
        private int _JXSysNo;
        private int _ParentPersonSysNo;
        private string _JXCycle;
        private int _JXScore;
        private string _JXGrade;
        private int _JXMXCategory;
        private DateTime _CreateTime;
        private DateTime _LastUpdateTime;
        private int _CreateUserSysNo;
        private int _LastUpdateUserSysNo;
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
        public int JXScore
        {
            set { _JXScore = value; }
            get { return _JXScore; }
        }

        [DataMember]
        public string JXGrade
        {
            set { _JXGrade = value; }
            get { return _JXGrade; }
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


        #endregion

        [System.Runtime.Serialization.OnDeserializing]
        private void OnDeserializing(StreamingContext ctx)
        {
            Init();
        }

        public void Init()
        {
            LowerPersonSysNo = AppConst.IntNull;
            JXCategory = AppConst.IntNull;
            JXSysNo = AppConst.IntNull;
            ParentPersonSysNo = AppConst.IntNull;
            JXCycle = AppConst.StringNull;
            JXScore = AppConst.IntNull;
            JXGrade = AppConst.StringNull;
            JXMXCategory = AppConst.IntNull;
            CreateTime = AppConst.DateTimeNull;
            LastUpdateTime = AppConst.DateTimeNull;
            CreateUserSysNo = AppConst.IntNull;
            LastUpdateUserSysNo = AppConst.IntNull;

        }

        #region 实现IComparable<T>接口的泛型排序方法
        /// <sumary> 
        /// 根据LowerPersonSysNo字段实现的IComparable<T>接口的泛型排序方法 
        /// </sumary> 
        /// <param name="other"></param> 
        /// <returns></returns> 
        public int CompareTo(JXMXBEntity other)
        {
            return LowerPersonSysNo.CompareTo(other.LowerPersonSysNo);
        }
        #endregion
    }
}
