using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Type of searches available
    /// </summary>
    public enum SearchType
    {
        /// <summary>
        /// Single point search. 
        /// For each single route point there will be request to hotel provider
        /// for a list of hotels available arraound.
        /// 
        /// This is simple algorith but slow due to a big number of requests to be made
        /// </summary>
        SinglePoint,

        /// <summary>
        /// The algorithm that balance load and opmimize number of requests to hotel providers
        /// but quuering bigger region and thed filter hotels itself (those that lies on route)
        /// </summary>
        LoadBalancing
    }
}
