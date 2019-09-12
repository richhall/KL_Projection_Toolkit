using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PI.Common;

namespace PI.Common.Models
{
    /// <summary>
    /// Location class
    /// </summary>
    [Serializable]
    public class Location
    {
        public string name;
        public string town;
        public string mapUrl;
        public GeoLocation geoLocation = new GeoLocation();
    }
}
