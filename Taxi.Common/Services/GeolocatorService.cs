using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Common.Services
{
    public class GeolocatorService : IGeolocatorService
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Task GetLocationAsync()
        {
            throw new NotImplementedException();
        }
    }

}
