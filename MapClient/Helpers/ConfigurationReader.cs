using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map.Client.Helpers
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
            //ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            //fileMap.ExeConfigFilename = Path.Combine(Helpers.Paths.Application, Process.GetCurrentProcess().ProcessName, ".config");
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return config.AppSettings.Settings[key].Value;
        }

    }
}
