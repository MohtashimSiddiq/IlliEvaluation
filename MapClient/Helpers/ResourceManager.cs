using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using Map.Client.Helpers.Enums;
using Map.Common;
using Map.Common.Enums;

namespace Map.Client.Helpers
{
    public class MapResourceManager
    {
        private static MapResourceManager _instance;
        public static MapResourceManager Instance {
            get
            {
                if (_instance == null)
                {
                    _instance = new MapResourceManager();
                }
                return _instance;
            }
        }

        public void ChangeLocale(eLocales locale, Collection<ResourceDictionary> applicationResources)
        {
            AppLogger.Instance.Log(eLogType.Debug, string.Format("BEGIN:: {0}", System.Reflection.MethodInfo.GetCurrentMethod().Name));
            int resourcesIndex = -1;
            for (int i = 0; i < applicationResources.Count; i++)
            {
                if (applicationResources[i].Contains("ApplicationLocale"))
                {
                    resourcesIndex = i;
                    break;
                }
            }
            string fileName = string.Empty;
            
            if (locale == eLocales.English)
            {
                fileName = Paths.EnglishLocale;
            }
            else
            {
                fileName = Paths.GermanLocale;
            }
            LoadResourceFile(fileName, resourcesIndex, applicationResources);
            AppLogger.Instance.Log(eLogType.Debug, string.Format("END:: {0}", System.Reflection.MethodInfo.GetCurrentMethod().Name));


        }


        public void ChangeTheme(eUIMode mode, Collection<ResourceDictionary> applicationResources)
        {
            AppLogger.Instance.Log(eLogType.Debug, string.Format("BEGIN:: {0}", System.Reflection.MethodInfo.GetCurrentMethod().Name));
            int resourcesIndex = -1;
            for (int i = 0; i < applicationResources.Count; i++)
            {
                if (applicationResources[i].Contains("Theme"))
                {
                    resourcesIndex = i;
                    break;
                }
            }
            string fileName = string.Empty;

            if (mode == eUIMode.Dark)
            {
                fileName = Paths.DarkTheme;
            }
            else
            {
                fileName = Paths.LightTheme;
            }
            LoadResourceFile(fileName, resourcesIndex, applicationResources);
            AppLogger.Instance.Log(eLogType.Debug, string.Format("END:: {0}", System.Reflection.MethodInfo.GetCurrentMethod().Name));


        }





        private void LoadResourceFile(string path, int resourcesIndex, Collection<ResourceDictionary> applicationResources)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            if (resourcesIndex != -1)
            {
                applicationResources[resourcesIndex] = (ResourceDictionary)XamlReader.Load(fs);
            }
            else
            {
                applicationResources.Add((ResourceDictionary)XamlReader.Load(fs));
            }
        }


    }
}
