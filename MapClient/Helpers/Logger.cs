using Ruag.Client.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ruag.Client.Helpers
{
    public class AppLogger
    {
        private StackFrame curStack = new StackFrame();
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

        public void Log(eLogType logType, string message )
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

        public void LogBegin(string callingType,string callingMethod)
        {
            Log(Helpers.Enums.eLogType.Debug, string.Format("BEGIN:: Class:{0} Method:{1} ", callingType, callingMethod));
        }

        public void LogEnd(string callingType, string callingMethod)
        {
            Log(Helpers.Enums.eLogType.Debug, string.Format("BEGIN:: Class:{0} Method:{1} ", callingType, callingMethod));
        }

    }
}
