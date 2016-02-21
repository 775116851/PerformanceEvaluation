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
    public class Custom_Sys_MenuEntity : IComparable<Custom_Sys_MenuEntity>
    {
        public Custom_Sys_MenuEntity()
        {
            Init();
        }

        #region 成员变量和公共属性
        private int _SysNo;
        private int _M1SysNo;
        private int _M2SysNo;
        private string _MenuName;
        private string _MenuLink;
        private int _Status;
        private int _Level;

        [DataMember]
        public int SysNo
        {
            set { _SysNo = value; }
            get { return _SysNo; }
        }

        [DataMember]
        public int M1SysNo
        {
            set { _M1SysNo = value; }
            get { return _M1SysNo; }
        }

        [DataMember]
        public int M2SysNo
        {
            set { _M2SysNo = value; }
            get { return _M2SysNo; }
        }

        [DataMember]
        public string MenuName
        {
            set { _MenuName = value; }
            get { return _MenuName; }
        }

        [DataMember]
        public string MenuLink
        {
            set { _MenuLink = value; }
            get { return _MenuLink; }
        }

        [DataMember]
        public int Status
        {
            set { _Status = value; }
            get { return _Status; }
        }

        [DataMember]
        public int Level
        {
            get
            {
                if (M1SysNo == AppConst.IntNull)
                    _Level = 1;
                else if (M1SysNo != AppConst.IntNull && M2SysNo == AppConst.IntNull)
                    _Level = 2;
                else if (M1SysNo != AppConst.IntNull && M2SysNo != AppConst.IntNull)
                    _Level = 3;

                return _Level;
            }
            set { _Level = value; }
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
            M1SysNo = AppConst.IntNull;
            M2SysNo = AppConst.IntNull;
            MenuName = AppConst.StringNull;
            MenuLink = AppConst.StringNull;
            Status = AppConst.IntNull;

        }

        #region 实现IComparable<T>接口的泛型排序方法
        /// <sumary> 
        /// 根据SysNo字段实现的IComparable<T>接口的泛型排序方法 
        /// </sumary> 
        /// <param name="other"></param> 
        /// <returns></returns> 
        public int CompareTo(Custom_Sys_MenuEntity other)
        {
            return SysNo.CompareTo(other.SysNo);
        }
        #endregion
    }
}
