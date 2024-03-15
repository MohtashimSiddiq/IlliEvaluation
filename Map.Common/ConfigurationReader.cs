using Map.Common.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map.Common
{
    public class ConfigurationReader
    {
        private ConfigurationReader()
        {


        }

        private static ConfigurationReader _instance;
        public static ConfigurationReader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConfigurationReader();
                }
                return _instance;
            }
        }

        public string GetAppSetting(string key)
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            try
            {
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = Path.Combine(new FileInfo(Paths.Application).Directory.FullName, "web.config");
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                return config.AppSettings.Settings[key].Value;
            }
            catch (Exception ex)
            {
                AppLogger.Instance.Log(eLogType.Error, ex.ToString());
                return null;
            }
            finally
            {
                AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            
        }

    }
}
