using HotelInterface.TO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Strategy of hotel search
    /// Provides possibility to search hotels by single point.
    /// 
    /// Typical implementation faced performance issues since interface between 
    /// the system and hotel provider library takes a lot of time and may hotels search 
    /// invokation causes performance problems with.
    /// 
    /// This implementation is left as simple case and potentially might be re-used in future
    /// </summary>
    class SinglePointHotelSearchStrategy : IHotelSearchStrategy
    {
        /// <summary>
        /// Represents hotel search criterias
        /// </summary>
        private HotelPreference HotelParameters;

        /// <summary>
        /// Rote points objects - optimized points list to be used for hotel search
        /// </summary>
        private RoutePoints RoutePoints;

        /// <summary>
        /// Current progress
        /// </summary>
        private int CurrentProgress;

        /// <summary>
        /// Event that is fired when new hotels found
        /// </summary>
        public event EventHandler<HotelsEventArgs> HotelsFound
        {
            add { _hotelsFound += new EventHandler<HotelsEventArgs>(value); }
            remove { _hotelsFound -= value; }
        }
        private event EventHandler<HotelsEventArgs> _hotelsFound;

        /// <summary>
        /// Notifies about current progress
        /// </summary>
        public event EventHandler<CalculationStatusEventArgs> Progress
        {
            add { _progress += new EventHandler<CalculationStatusEventArgs>(value); }
            remove { _progress -= value; }
        }
        private event EventHandler<CalculationStatusEventArgs> _progress;

        /// <summary>
        /// Event notifying about error happened while seeing error for some point
        /// </summary>
        public event EventHandler<HotelSearchErrorEventArgs> HotelSearchError
        {
            add { _hotelSearchError += new EventHandler<HotelSearchErrorEventArgs>(value); }
            remove { _hotelSearchError -= value; }
        }
        private event EventHandler<HotelSearchErrorEventArgs> _hotelSearchError;

        /// <summary>
        /// Constructs the strategy
        /// </summary>
        public SinglePointHotelSearchStrategy()
        { 
        }

        /// <summary>
        /// Invokes search method for the strategy
        /// </summary>
        /// <param name="cancellationToken">Cancellation token - used to cancel the search if requested.</param>
        /// <param name="points">Linked pioints of the route to do search for</param>
        /// <param name="hotelParameters">Hotel search criterias</param>
        public void Search(System.Threading.CancellationToken cancellationToken, RoutePoints points, HotelPreference hotelParameters)
        {
            HotelParameters = hotelParameters;
            RoutePoints = points;

            foreach (LinkedPoint start in RoutePoints.LegsStart)
            {
                LinkedPoint p = start;
                while (null != p) // IsLast property is not good work in this case as we still need to check the last one as well
                {
                    if (cancellationToken.IsCancellationRequested) break;

                    try
                    {
                        SeachHotelsForPoint(p);
                    }
                    catch (Exception exc)
                    {
                        SignalRouteSearchError(exc, p);
                    }
                    p = p.Next;
                }

            }
        }

        /// <summary>
        /// Search hotels for specific point
        /// </summary>
        /// <param name="point">Point to search hotels for</param>
        private void SeachHotelsForPoint(LinkedPoint point)
        {
            HotelListParameters hotelSearchParametersObject = CreateHotelSearhParameterObject(point);

            HotelSearch search = new HotelSearch(point, hotelSearchParametersObject);
            int newHotelsFound = search.Search();

            if (newHotelsFound > 0)
            {
                HotelSummary[] hotels = search.GetHotels();
                Debug.Assert(null != hotels);

                ReportNewHotels(hotels);
            }

            IncreaseProgress();
        }

        /// <summary>
        /// Reports about new hotels
        /// </summary>
        /// <param name="hotels">Hotels found for some of locations</param>
        private void ReportNewHotels(HotelSummary[] hotels)
        {
            if (null == _hotelsFound) return;

            HotelsEventArgs args = new HotelsEventArgs(hotels);
            _hotelsFound(this, args);
        }

        /// <summary>
        /// Constructs hotel search parameters object based on current class data
        /// </summary>
        /// <param name="point">Point to search hotels for</param>
        /// <returns>Hotel search parameters object</returns>
        private HotelListParameters CreateHotelSearhParameterObject(LinkedPoint point)
        {
            HotelListParameters hotelSearchParametersObject = new HotelListParameters();
            hotelSearchParametersObject.HotelPreferences = HotelParameters;
            hotelSearchParametersObject.Location = new RouteTransportObjects.LatLng(point.Point.Latitude, point.Point.Longitude);
            hotelSearchParametersObject.SearchRadius = RoutePoints.Proximity.Radius;
            hotelSearchParametersObject.SearchRadiusUnit = CalculationUtils.DistanceUnit.Meters; // TODO: read from config


            return hotelSearchParametersObject;
        }

        /// <summary>
        /// Signals about exception happened when searching hotels
        /// </summary>
        /// <param name="exc">Exception occured</param>
        /// <param name="point">Point for which error occured</param>
        private void SignalRouteSearchError(Exception exc, LinkedPoint point)
        {
            if (null != _hotelSearchError)
            {
                HotelSearchErrorEventArgs args = new HotelSearchErrorEventArgs(exc, point);
                _hotelSearchError(this, args);
            }
        }

        /// <summary>
        /// Notifies subsctribers about current calculation progress
        /// </summary>
        private void IncreaseProgress()
        {
            CurrentProgress += 1;

            if (null != _progress)
            {
                CalculationStatusEventArgs args = new CalculationStatusEventArgs(RoutePoints.PointCount, CurrentProgress);
                _progress(this, args);
            }
        }

    }
}
