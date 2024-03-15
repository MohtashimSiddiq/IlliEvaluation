using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map.Common
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





    }
}
