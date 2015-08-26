using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Interface for hotel search strategy objects.
    /// Implemantation of this call introduce new search strategy to the system
    /// </summary>
    interface IHotelSearchStrategy
    {
        /// <summary>
        /// Event that is fired when new hotels found
        /// </summary>
        event EventHandler<HotelsEventArgs> HotelsFound;

        /// <summary>
        /// Notifies about current progress
        /// </summary>
        event EventHandler<CalculationStatusEventArgs> Progress;

        /// <summary>
        /// Event that fires when error furing search is happened
        /// </summary>
        event EventHandler<HotelSearchErrorEventArgs> HotelSearchError;     

        /// <summary>
        /// Invokes search method for the strategy
        /// </summary>
        /// <param name="cancellationToken">Cancellation token - used to cancel the search if requested.</param>
        /// <param name="points">Linked pioints of the route to do search for</param>
        void Search(System.Threading.CancellationToken cancellationToken, RoutePoints points, HotelInterface.TO.HotelPreference hotelParameters);
   
    }
}
