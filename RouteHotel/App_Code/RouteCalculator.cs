using HotelInterface.TransportObjects;
using HotelRouteCalculation;
using MapUtils;
using RouteHotel.TransportObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteHotel
{
    /// <summary>
    /// Route calculator - takes care of hotels calculation
    /// </summary>
    public class RouteCalculator
    {
        /// <summary>
        /// ID of calculatory
        /// </summary>
        public Guid ID { get { return _ID; } }
        private Guid _ID = GenerateID();

        /// <summary>
        /// Route search parameters
        /// </summary>
        private RouteParams Params;

        /// <summary>
        /// Hotels search object
        /// </summary>
        public RouteHotelSearch HotelSearch
        {
            get { return hotelSearch; }
        }
        private RouteHotelSearch hotelSearch;

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
        /// Route object
        /// </summary>
        public GoogleDirections.Route Route
        {
            get { return HotelSearch.Route; }
        }

        /// <summary>
        /// Whether search are in progress
        /// </summary>
        public bool SearchInProgress
        {
            get { return searchInProgress; }
        }
        public bool searchInProgress;

        /// <summary>
        /// Generates new ID for Route calculator
        /// </summary>
        /// <returns>ID</returns>
        private static Guid GenerateID()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="routeParams">Route search parameters</param>
        /// <param name="hotelParameters">Represents hotel search criterias</param>
        public RouteCalculator(RouteParams routeParams, HotelPreference hotelParameters)
        {
            if (null == routeParams) throw new ArgumentNullException("Argument routeParams cannot be null");
            if (null == hotelParameters) throw new ArgumentNullException("Argument hotelParameters cannot be null");

            this.Params = routeParams;
            this._hotelParameters = hotelParameters;
        }

        /// <summary>
        /// Starts route search
        /// When returns route is found, but hotel search execution is carried in background until search is marked as finished
        /// </summary>
        public void Search()
        {
            searchInProgress = true;

            GoogleDirections.Location[] locations = Location.ConvertLocations(Params.Locations);
            GoogleDirections.Route route = GoogleDirections.RouteDirections.GetRoute(Params.OptimizeRoute, locations);

            Proximity proximity = new Proximity(Params.ProximityRadius);
            hotelSearch = new RouteHotelSearch(route, proximity, HotelParameters);
            hotelSearch.Search();
        }

    }
}