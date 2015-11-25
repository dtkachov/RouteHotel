using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Route hotel search that searches by load (request) balancing approach
    /// So less request is made to hote providers in order to optimize performance.
    /// </summary>
    class LoadBalancingRouteHotelSearch : BaseRouteHotelSearch<LoadBalancingHotelSearchStrategy>
    {
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="route">Route to optimize points for</param>
        /// <param name="proximity">Required accuracy values object</param>
        /// <param name="hotelParameters">Represents hotel search criterias</param>
        public LoadBalancingRouteHotelSearch(Route route, MapUtils.Proximity proximity, HotelInterface.TO.HotelPreference hotelParameters)
            : base(route, proximity, hotelParameters)
        {
        }

    }
}
