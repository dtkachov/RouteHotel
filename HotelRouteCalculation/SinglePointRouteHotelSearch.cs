using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Route hotel search that searches by navigating over single point
    /// </summary>
    class SinglePointRouteHotelSearch : BaseRouteHotelSearch<SinglePointHotelSearchStrategy>
    {
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="route">Route to optimize points for</param>
        /// <param name="proximity">Required accuracy values object</param>
        /// <param name="hotelParameters">Represents hotel search criterias</param>
        public SinglePointRouteHotelSearch(Route route, MapUtils.Proximity proximity, HotelInterface.TO.HotelPreference hotelParameters)
            : base(route, proximity, hotelParameters)
        {
            this.RoutePoints.OptimizeClosePoints = true;
        }
    }
}
