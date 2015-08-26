using GoogleDirections;
using HotelInterface.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRouteCalculation
{
    public interface IRouteHotelSearch
    {
        /// <summary>
        /// Rote points objects - optimized points list to be used for hotel search
        /// </summary>
        RoutePoints RoutePoints { get; }

        /// <summary>
        /// Route object
        /// </summary>
        Route Route { get; }

        /// <summary>
        /// Represents hotel search criterias
        /// </summary>
        HotelPreference HotelParameters { get; }

        /// <summary>
        /// Event notifying about search progress
        /// </summary>
        event EventHandler<CalculationStatusEventArgs> Progress;

        /// <summary>
        /// Event notifying about error happened while seeing error for some point
        /// </summary>
        event EventHandler<HotelSearchErrorEventArgs> HotelSearchError;

        /// <summary>
        /// Hotels in current search
        /// </summary>
        List<HotelSummary> Hotels { get; }

        /// <summary>
        /// Current progress
        /// </summary>
        int CurrentProgress { get ; }

                /// <summary>
        /// Search hotels for each point
        /// </summary>
        void Search();

        /// <summary>
        /// If search is in progress this method will join search thread
        /// to wait until search task is finished.
        /// If no search in progress this method do nothing.
        /// </summary>
        void WaitUntilFinished();
    }
}
