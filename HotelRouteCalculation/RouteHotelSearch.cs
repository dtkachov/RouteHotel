using GoogleDirections;
using HotelInterface.TransportObjects;
using MapUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Seeks for hotel on a route
    /// </summary>
    public class RouteHotelSearch
    {
        /// <summary>
        /// Rote points objects - optimized points list to be used for hotel search
        /// </summary>
        public RoutePoints RoutePoints
        {
            get { return routePoints; }
        }
        private RoutePoints routePoints;

        /// <summary>
        /// Route object
        /// </summary>
        public Route Route
        {
            get { return routePoints.Route; }
        }

        /// <summary>
        /// Represents hotel search criterias
        /// </summary>
        public HotelPreference HotelParameters
        {
            get
            {
                return _hotelParameters;
            }
        }
        private HotelPreference _hotelParameters;

        /// <summary>
        /// Event notifying about search progress
        /// </summary>
        public event EventHandler<CalculationStatusEventArgs> Progress
        {
            add { _progress += new EventHandler<CalculationStatusEventArgs>(value); }
            remove { _progress -= value; }
        }
        private event EventHandler<CalculationStatusEventArgs> _progress;

        /// <summary>
        /// Hotels in current search
        /// </summary>
        private List<HotelData> Hotels = new List<HotelData>();

        /// <summary>
        /// Current progress
        /// </summary>
        private int currentProgress = 0;

        /// <summary>
        /// Hotel search task canceletion token
        /// </summary>
        private CancellationToken HotelSearchCancellationToken;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="route">Route to optimize points for</param>
        /// <param name="proximity">Required accuracy values object</param>
        /// <param name="hotelParameters">Represents hotel search criterias</param>
        public RouteHotelSearch(Route route, Proximity proximity, HotelPreference hotelParameters)
        {
            if (null == hotelParameters) throw new ArgumentNullException("Argument hotelParameters cannot be null");

            _hotelParameters = hotelParameters;
            this.routePoints = new RoutePoints(route, proximity);
        }

        /// <summary>
        /// Search hotels for each point
        /// </summary>
        public void Search()
        {
            routePoints.BuildRoutePoints();

            StartSearchHotels();
        }

        /// <summary>
        /// Starts execution of internally hotel search
        /// </summary>
        private void StartSearchHotels()
        { 
            HotelSearchCancellationToken = new CancellationToken();

            Task hotelSearchTask = new Task(SearchHotelsInternalAsync);
            hotelSearchTask.Start();
        }


        /// <summary>
        /// Executes internally hotel search
        /// </summary>
        /// <param name="cancellationToken">Cancelation token</param>
        /// <returns>Task status</returns>
        private void SearchHotelsInternalAsync()
        {
            foreach (LinkedPoint start in routePoints.LegsStart)
            {
                LinkedPoint p = start;
                while (null != p) // IsLast property is not good work in this case as we still need to check the last one as well
                {
                    if (HotelSearchCancellationToken.IsCancellationRequested) break;

                    SeachHotelsForPoint(p);
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

                AddNewHotels(hotels);
            }

            IncreaseProgress();
        }

        /// <summary>
        /// Checks current hotel list and add only new ones
        /// </summary>
        /// <param name="hotels">Hotels found for some of locations</param>
        private void AddNewHotels(HotelSummary[] hotels)
        {
            // TODO - do comparision and add only new hotels
        }

        /// <summary>
        /// Notifies subsctribers about current calculation progress
        /// </summary>
        private void IncreaseProgress()
        {
            currentProgress += 1;

            if (null != _progress)
            {
                CalculationStatusEventArgs args = new CalculationStatusEventArgs(RoutePoints.PointCount, currentProgress);
                _progress(this, args);
            }
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
            hotelSearchParametersObject.SearchRadius = routePoints.Proximity.Radius;
            hotelSearchParametersObject.SearchRadiusUnit = CalculationUtils.DistanceUnit.Meters; // TODO: read from config


            return hotelSearchParametersObject;
        }
    }
}
