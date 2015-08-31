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
        /// Search object for which strategy is applied. Used to read parameters from.
        /// </summary>
        private IRouteHotelSearch SearchObject;

        /// <summary>
        /// Rote points objects - optimized points list to be used for hotel search
        /// </summary>
        private RoutePoints RoutePoints
        {
            get { return SearchObject.RoutePoints; }
        }

        /// <summary>
        /// Count of points calculation for which would be performed
        /// </summary>
        public int CalculationPointCount { get { return RoutePoints.PointCount; } }

        /// <summary>
        /// Current progress
        /// </summary>
        private int CurrentProgress;

        #region events

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

        #endregion

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
        /// <param name="searchObject">Search object for which strategy is applied. Used to read parameters from.</param>
        public void Search(System.Threading.CancellationToken cancellationToken, IRouteHotelSearch searchObject)
        {
            if (null == searchObject) throw new ArgumentNullException("Argument searchObject cannot be null");

            this.SearchObject = searchObject;

            foreach (LinkedPoint start in RoutePoints.LegsStart)
            {
                LinkedPoint p = start;
                while (null != p) // IsLast property is not good work in this case as we still need to check the last one as well
                {
                    if (cancellationToken.IsCancellationRequested) break;

                    HotelSummary[] hotels = null;
                    try
                    {
                        hotels = SearchObject.SeachHotelsForPoint(p, RoutePoints.Proximity.Radius);
                    }
                    catch (Exception exc)
                    {
                        SignalRouteSearchError(exc, p);
                    }

                    ReportNewHotels(hotels);

                    IncreaseProgress();
                    p = p.Next;
                }

            }
        }

        /// <summary>
        /// Reports about new hotels
        /// </summary>
        /// <param name="hotels">Hotels found for some of locations</param>
        private void ReportNewHotels(HotelSummary[] hotels)
        {
            if (null == hotels) return;
            if (null == _hotelsFound) return;

            HotelsEventArgs args = new HotelsEventArgs(hotels);
            _hotelsFound(this, args);
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
                CalculationStatusEventArgs args = new CalculationStatusEventArgs(CalculationPointCount, CurrentProgress);
                _progress(this, args);
            }
        }

        /// <summary>
        /// Returns value of calculation radius in meters
        /// </summary>
        /// <returns></returns>
        public int GetCalculationRaduis()
        {
            return SearchObject.RoutePoints.Proximity.Radius;
        }

#if DEBUG

        /// <summary>
        /// Returns calculation points for this search
        /// </summary>
        /// <returns>Calculation points - for which hotels would be searched</returns>
        public GoogleDirections.LatLng[] GetCalculationPoints()
        {
            List<GoogleDirections.LatLng> result = new List<GoogleDirections.LatLng>();

            foreach (LinkedPoint start in RoutePoints.LegsStart)
            {
                LinkedPoint p = start;
                while (null != p) 
                {
                    result.Add(p.Point);
                    p = p.Next;
                }

            }

            return result.ToArray();
        }
#endif

    }
}
