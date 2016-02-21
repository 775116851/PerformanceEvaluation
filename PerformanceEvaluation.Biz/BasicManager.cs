using PerformanceEvaluation.PerformanceEvaluation.Dac;
using PerformanceEvaluation.PerformanceEvaluation.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceEvaluation.PerformanceEvaluation.Biz
{
    public class BasicManager
    {
        private BasicManager()
        {

        }
        private static BasicManager _instance;
        public static BasicManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new BasicManager();
            }
            return _instance;
        }

        //用户登录
        public PersonInfoEntity LoadUser(int userSysNo)
        {
            return new PersonInfoDac().GetModel(userSysNo);
        }

    }
}
