using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Map.Client.Helpers
{
    public static class Paths
    {
        /// <summary>
        /// Gets the current path of the executing assembly
        /// </summary>
        public static string Application
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        /// <summary>
        /// Gets the current path of the localization folder
        /// </summary>
        public static string Localization
        {
            get
            {
                string localizationFolderName = ConfigurationReader.Instance.GetAppSetting(AppSettingKeys.LocalizationFolder); 
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, localizationFolderName);
            }
        }


        /// <summary>
        /// Gets the current path of the localization folder
        /// </summary>
        public static string Themes
        {
            get
            {
                string themesFolderName = ConfigurationReader.Instance.GetAppSetting(AppSettingKeys.ThemesFolder);
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, themesFolderName);
            }
        }


        /// <summary>
        /// Gets the current path of the light theme xaml
        /// </summary>
        public static string LightTheme
        {
            get
            {
                string themesFolderName = ConfigurationReader.Instance.GetAppSetting(AppSettingKeys.ThemesFolder);
                string lightTheme = ConfigurationReader.Instance.GetAppSetting(AppSettingKeys.LightTheme);
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, themesFolderName, lightTheme);
            }
        }

        /// <summary>
        /// Gets the current path of the English Locale
        /// </summary>
        public static string EnglishLocale
        {
            get
            {
                string themesFolderName = ConfigurationReader.Instance.GetAppSetting(AppSettingKeys.LocalizationFolder);
                string englishFileName = ConfigurationReader.Instance.GetAppSetting(AppSettingKeys.EnglishLocale);
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, themesFolderName, englishFileName);
            }
        }

        /// <summary>
        /// Gets the current path of the localization folder
        /// </summary>
        public static string GermanLocale
        {
            get
            {
                string themesFolderName = ConfigurationReader.Instance.GetAppSetting(AppSettingKeys.LocalizationFolder);
                string germanFileName = ConfigurationReader.Instance.GetAppSetting(AppSettingKeys.GermanLocale);
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, themesFolderName, germanFileName);
            }
        }

        /// <summary>
        /// Gets the current path of the localization folder
        /// </summary>
        public static string DarkTheme
        {
            get
            {
                string themesFolderName = ConfigurationReader.Instance.GetAppSetting(AppSettingKeys.ThemesFolder);
                string darkTheme = ConfigurationReader.Instance.GetAppSetting(AppSettingKeys.DarkTheme);
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, themesFolderName, darkTheme);
            }
        }





    }
}
