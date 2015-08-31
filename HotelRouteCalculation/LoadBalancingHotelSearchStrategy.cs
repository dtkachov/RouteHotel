using GoogleDirections;
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
    /// Provides possibility to search hotels optimizing number of requets to server
    /// 
    /// Typical implementation (request per point) faced performance issues since interface between 
    /// the system and hotel provider library takes a lot of time and may hotels search 
    /// invokation causes performance problems with.
    /// 
    /// So this is evolution of initial strategy.
    /// 
    /// Potential improvement would be to add threads to request/processing. 
    /// So while next request is executing processingis done in separate thread.
    /// </summary>
    class LoadBalancingHotelSearchStrategy : IHotelSearchStrategy
    {
        /// <summary>
        /// Search radius. Can be later moved to paramenters.
        /// </summary>
        const int SEARCH_RADIUS = 50; // kilometers
        const int METERS_IN_KM = 1000;

        /// <summary>
        /// Koefficient for precision of route search.
        /// While calculating key points it will be used to ensure numbe of Proximity times 
        /// to reduce radius in range calculation.
        /// Then when hotels hotels would be searched for whole radius this would ensure potential 
        /// in accuracy in key points calcualtion in ranges
        /// </summary>
        const double PRECISION_KOEFFICIENT = 2;

        /// <summary>
        /// Returns value of calculation radius in meters
        /// </summary>
        /// <returns></returns>
        public int CalculationRaduis { get { return SEARCH_RADIUS * METERS_IN_KM; } }

        /// <summary>
        /// Search object for which strategy is applied. Used to read parameters from.
        /// </summary>
        private IRouteHotelSearch SearchObject;

        /// <summary>
        /// Count of points calculation for which would be performed
        /// </summary>
        public int CalculationPointCount { get { return CalculationRegions.Count; } }

        /// <summary>
        /// Rote points objects - optimized points list to be used for hotel search
        /// </summary>
        private RoutePoints RoutePoints
        {
            get { return SearchObject.RoutePoints; }
        }

        /// <summary>
        /// Current progress
        /// </summary>
        private int CurrentProgress;

        /// <summary>
        /// Calculation points - centers of route select requests
        /// </summary>
        private List<LoadBalancingRegion> CalculationRegions;

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
        public LoadBalancingHotelSearchStrategy()
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
            CalculationRegions = new List<LoadBalancingRegion>();

            FillCalculationRegions();
            SearchHotels();
        }

        /// <summary>
        /// Fills internal structure of calculation regios for further search
        /// </summary>
        private void FillCalculationRegions()
        {
            foreach (LinkedPoint legStart in RoutePoints.LegsStart)
            {
                FillCalculationRegionsForLeg(legStart);
            }
        }

        /// <summary>
        /// Fills calculation regios for leg provided
        /// </summary>
        /// <param name="legStart">Leg startign point</param>
        private void FillCalculationRegionsForLeg(LinkedPoint legStart)
        {
            int searchRadiusMeters = GetCalculationRaduis();
            int maxDistance = (int)(searchRadiusMeters - RoutePoints.Proximity.MaxBand * PRECISION_KOEFFICIENT);
            RouteRangesCalculator rangesCalculator = new RouteRangesCalculator(legStart, maxDistance);

            /* the logic here is following. Since every keo point defined in range calculator can reach all the points to start and finish
             * we would start considering last point of each even range (or start point of each odd range - which ai sthe same) as center of circle. 
             * so two ranges work in pairs ( {0,1} - {2,3} - {4-5}, etc)
             */
            RouteRange[] ranges = rangesCalculator.Calculate();
            for (int i = 0; i < ranges.Length; i += 2) // see description above - we ensure range pairs
            {
                RouteRange evenRange = ranges[i];
                LinkedPoint center = evenRange.Finish;

                LinkedPoint endPoint = i < (ranges.Length - 1) ? ranges[i + 1].Finish : evenRange.Finish;
                LinkedPoint startPoint = evenRange.Start;

                LoadBalancingRegion region = new LoadBalancingRegion(startPoint, center, endPoint);
                CalculationRegions.Add(region);
            }
        }

        /// <summary>
        /// Executes search hotel job
        /// </summary>
        private void SearchHotels()
        {
            foreach (LoadBalancingRegion region in CalculationRegions)
            {
                HotelSummary[] hotels = null;
                try
                {
                    hotels = SearchObject.SeachHotelsForPoint(region.Center, CalculationRaduis);
                }
                catch (Exception exc)
                {
                    SignalRouteSearchError(exc, region.Center);
                }

                HotelSummary[] reachableHotels = FilterHotelsinProximity(hotels, region.Start, region.End);
                ReportNewHotels(reachableHotels);

                IncreaseProgress();

            }
        }

        /// <summary>
        /// Filters hotels in proximity range
        /// Returns those that aer in proximity reach to any of points
        /// </summary>
        /// <param name="start">Range start point</param>
        /// <param name="finish">Range finish point</param>
        /// <returns>Hotels that are in proximity distance to route's point range</returns>
        private HotelSummary[] FilterHotelsinProximity(HotelSummary[] hotels, LinkedPoint start, LinkedPoint finish)
        {
            if (null == hotels) return null;

            int proximityValue = SearchObject.RoutePoints.Proximity.Radius;

            List<HotelSummary> result = new List<HotelSummary>();
            foreach (HotelSummary currentHotel in hotels)
            {
                // iterate though all points and find if currentHotel is in proximity distance to any point
                LinkedPoint p = start;

                do
                {
                    double distanceFromHotelToPoint = CalculationUtils.DistanceUtils.Distance(p.Point.Latitude, p.Point.Longitude, currentHotel.Latitude, currentHotel.Longitude);
                    bool hotelNearPoint = proximityValue > distanceFromHotelToPoint;
                    if (hotelNearPoint)
                    {
                        result.Add(currentHotel);
                        break;
                    }

                    p = p.Next;
                }
                while (p != finish); 
            }

            return result.ToArray();
        }

        /// <summary>
        /// Reports about new hotels
        /// </summary>
        /// <param name="hotels">Hotels found for some of locations</param>
        private void ReportNewHotels(HotelSummary[] hotels)
        {
            if (null == _hotelsFound) return;
            if (null == hotels) return;
            if (0 == hotels.Length) return;

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
        /// <param name="start">Range start point</param>
        /// <param name="finish">Range finish point</param>
        private void IncreaseProgress()
        {
            ++CurrentProgress;

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
            return CalculationRaduis;
        }

#if DEBUG

        /// <summary>
        /// Returns calculation points for this search
        /// </summary>
        /// <returns>Calculation points - for which hotels would be searched</returns>
        public GoogleDirections.LatLng[] GetCalculationPoints()
        {
            List<GoogleDirections.LatLng> calculationPoints = new List<GoogleDirections.LatLng>();
            foreach (LoadBalancingRegion region in CalculationRegions)
            {
                calculationPoints.Add(region.Center.Point);
            }

            return calculationPoints.ToArray();
        }
#endif

    }
}
