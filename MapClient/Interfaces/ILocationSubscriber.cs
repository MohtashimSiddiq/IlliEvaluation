using Map.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map.Client.Interfaces
{
    public interface ILocationProviderService
    {
        void Initialize();

        event EventHandler<List<LocationInfo>> LocationChanged;

        void UnInitialize();

    }
}
