using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Factory that instantiates hotel search objects
    /// </summary>
    public class HoteSearchFactory
    {
        /// <summary>
        /// Creates inctance of search object
        /// </summary>
        /// <param name="type">Type of search object requested</param>
        /// <param name="route">Route to optimize points for</param>
        /// <param name="proximity">Required accuracy values object</param>
        /// <param name="hotelParameters">Represents hotel search criterias</param>
        /// <returns>Search object matching parameters requested.</returns>
        public static IRouteHotelSearch CreateSearch(SearchType type, GoogleDirections.Route route, MapUtils.Proximity proximity, HotelInterface.TO.HotelPreference hotelParameters)
        {
            switch (type)
            {
                case SearchType.SinglePoint: return new SinglePointRouteHotelSearch(route, proximity, hotelParameters);

                default:
                case SearchType.LoadBalancing: return new LoadBalancingRouteHotelSearch(route, proximity, hotelParameters);
            }
        }
    }
}
