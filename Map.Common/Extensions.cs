using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map.Common
{
    public static class Extensions
    {
        public static bool In<T>(this T obj, IEnumerable<T> objList)
        {
            if (objList.Contains(obj))
            {
                return true;
            }
            else
                return false;
        }

        public static bool NotIn<T>(this T obj, IEnumerable<T> objList)
        {
            if (objList.Contains(obj))
            {
                return false;
            }
            else
                return true;
        }
    }
}
