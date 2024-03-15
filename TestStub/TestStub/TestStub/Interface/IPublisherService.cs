using Map.Common.Models;
using System.Collections.Generic;
using System.Xaml.Schema;

namespace TestStub.Interface
{
    public interface IPublisherService
    {
        void Initialize();

        void PublishLocation(LocationInfo locationInfo);

        void PublishMultipleLocations(List<LocationInfo> locationInfos);

        void UnInitialize();

    }
}