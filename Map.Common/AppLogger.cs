using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Map.Common.Enums;

namespace Map.Common
{
    public class AppLogger
    {
       
        private AppLogger()
        {


        }

        private static AppLogger _instance;
        public static AppLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppLogger();
                }
                return _instance;
            }
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("");

        public void Log(eLogType logType, string message)
        {
            switch (logType)
            {
                case eLogType.Debug:
                    log.Debug(message);
                    break;
                case eLogType.Warning:
                    log.Warn(message);
                    break;
                case eLogType.Error:
                    log.Error(message);
                    break;
                case eLogType.Info:
                default:
                    log.Info(message);
                    break;
            }


        }

        public void LogBegin(string callingType, string callingMethod)
        {
            Log(eLogType.Debug, string.Format("BEGIN:: Class:{0} Method:{1} ", callingType, callingMethod));
        }

        public void LogEnd(string callingType, string callingMethod)
        {
            Log(eLogType.Debug, string.Format("BEGIN:: Class:{0} Method:{1} ", callingType, callingMethod));
        }

    }
}
