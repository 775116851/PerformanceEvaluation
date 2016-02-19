using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerformanceEvaluation.PerformanceEvaluation.Code
{
    public class ResponseResult
    {
        /// <summary>
        /// 结果代码：0=成功，非0=失败
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 结构消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 结果数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 返回的SysNo
        /// </summary>
        public int SysNo { get; set; }
        /// <summary>
        /// 返回的图片地址
        /// </summary>
        public string PhotoUrl { get; set; }

        public ResponseResult() { Code = -1; Message = "失败"; }

        public static ResponseResult CreateSuccessResponse(object o)
        {
            ResponseResult r = new ResponseResult();
            r.Code = 0;
            r.Message = "成功";
            r.Data = o;
            return r;
        }

        public static ResponseResult CreateFailureResponse(string message)
        {
            ResponseResult r = new ResponseResult();
            r.Code = 1;
            r.Message = "失败:" + message;
            return r;
        }
    }
}