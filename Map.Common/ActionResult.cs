using Map.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map.Common
{
    public class ActionResult<T>
    {
        public eReturnCode ReturnCode { get; set; }

        public string ReturnDescription { get; set; }

        public T Result { get; set; }
    }
}
