using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PI.Common;

namespace PI.Common.Models
{
    /// <summary>
    /// Geolocation class
    /// </summary>
    [Serializable]
    public class GeoLocation
    {
        public double lat = 0f;
        public double lng = 0f;

        public string latLng
        {
            get
            {
                return lat.ToString() + "," + lng.ToString();
            }
        }
    }
}
