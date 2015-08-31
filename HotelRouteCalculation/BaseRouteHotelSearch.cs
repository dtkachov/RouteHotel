using GoogleDirections;
using HotelInterface.TO;
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
    class BaseRouteHotelSearch<S> : IRouteHotelSearch  where S : IHotelSearchStrategy, new()
    {
        /// <summary>
        /// Search strategy object
        /// </summary>
        private S SearchStrategy = new S();

        /// <summary>
        /// Rote points objects - optimized points list to be used for hotel search
        /// </summary>
        public RoutePoints RoutePoints
        {
            get { return routePoints; }
        }
        private RoutePoints routePoints;

        /// <summary>
        /// Count of points calculation for which would be performed
        /// </summary>
        public int CalculationPointCount { get { return SearchStrategy.CalculationPointCount; } }

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
        /// Event notifying about error happened while seeing error for some point
        /// </summary>
        public event EventHandler<HotelSearchErrorEventArgs> HotelSearchError
        {
            add { _hotelSearchError += new EventHandler<HotelSearchErrorEventArgs>(value); }
            remove { _hotelSearchError -= value; }
        }
        private event EventHandler<HotelSearchErrorEventArgs> _hotelSearchError;

        /// <summary>
        /// Hotels in current search
        /// </summary>
        public List<HotelSummary> Hotels
        {
            get { return _hotels; }
        }
        private List<HotelSummary> _hotels;

        /// <summary>
        /// Current progress
        /// </summary>
        public int CurrentProgress { get { return currentProgress;} }
        private int currentProgress = 0;

        /// <summary>
        /// Hotel search task canceletion token
        /// </summary>
        private CancellationToken HotelSearchCancellationToken;

        /// <summary>
        /// Hotel search task.
        /// Null if no task in progress.
        /// </summary>
        private Task HotelSearchTask;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="route">Route to optimize points for</param>
        /// <param name="proximity">Required accuracy values object</param>
        /// <param name="hotelParameters">Represents hotel search criterias</param>
        public BaseRouteHotelSearch(Route route, Proximity proximity, HotelPreference hotelParameters)
        {
            if (null == hotelParameters) throw new ArgumentNullException("Argument hotelParameters cannot be null");

            _hotelParameters = hotelParameters;
            this.routePoints = new RoutePoints(route, proximity);

            SearchStrategy.HotelsFound += SearchStrategy_HotelsFound;
            SearchStrategy.Progress += SearchStrategy_Progress;
            SearchStrategy.HotelSearchError += SearchStrategy_HotelSearchError;
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
        /// If search is in progress this method will join search thread
        /// to wait until search task is finished.
        /// If no search in progress this method do nothing.
        /// </summary>
        public void WaitUntilFinished()
        {
            if (null == HotelSearchTask) return;

            HotelSearchTask.Wait();
        }

        /// <summary>
        /// Starts execution of internally hotel search
        /// </summary>
        private void StartSearchHotels()
        { 
            HotelSearchCancellationToken = new CancellationToken();

            _hotels = new List<HotelSummary>();

            HotelSearchTask = new Task(SearchHotelsInternalAsync);
            HotelSearchTask.Start();
        }


        /// <summary>
        /// Executes internally hotel search
        /// </summary>
        /// <param name="cancellationToken">Cancelation token</param>
        /// <returns>Task status</returns>
        private void SearchHotelsInternalAsync()
        {
            SearchStrategy.Search(HotelSearchCancellationToken, this);
        }

        /// <summary>
        /// Signals about exception happened when searching hotels
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">Error event args</param>
        private void SearchStrategy_HotelSearchError(object sender, HotelSearchErrorEventArgs args)
        {
            if (null != _hotelSearchError)
            {
                _hotelSearchError(this, args);
            }
        }

        private void SearchStrategy_HotelsFound(object sender, HotelsEventArgs e)
        {
            AddNewHotels(e.Hotels);
        }

        /// <summary>
        /// Checks current hotel list and add only new ones
        /// </summary>
        /// <param name="hotelsToAdd">Hotels found for some of locations</param>
        private void AddNewHotels(HotelSummary[] hotelsToAdd)
        {
            int countBeforeMerge = this.Hotels.Count;
            foreach (HotelSummary hotel in hotelsToAdd)
            {
                bool hotelInList = IsHotelInList(hotel);
                if (!hotelInList)
                {
                    // add this hotel to list
                    this.Hotels.Add(hotel);
                }
            }
            OutputDebugInfo(countBeforeMerge, hotelsToAdd.Length, this.Hotels.Count);
        }

        /// <summary>
        /// Debug time method to dump info about found hotels
        /// </summary>
        /// <param name="hotelsBefore">Hotels before adding to list</param>
        /// <param name="hotelsFound">New hotels found</param>
        /// <param name="hotelsAfter">Hotels after merge in collection</param>
        [Conditional("DEBUG")] 
        private void OutputDebugInfo(int hotelsBefore, int hotelsFound, int hotelsAfter)
        {
            string progressStr = string.Format(
                    "Found {0} hotels, added {1} new to list",
                    hotelsFound, hotelsAfter - hotelsBefore
                    );
            System.Diagnostics.Trace.WriteLine(progressStr);
        }

        /// <summary>
        /// Checks whether hotel specified is in list of hotels
        /// </summary>
        /// <param name="hotel">Hotel toc check it its in list</param>
        /// <returns>True if hotel is in current hotels collection and false otherwise</returns>
        private bool IsHotelInList(HotelSummary hotel)
        {
            foreach (HotelSummary existingHotel in Hotels)
            {
                if (existingHotel.HotelId == hotel.HotelId)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Notifies subsctribers about current calculation progress
        /// </summary>
        private void SearchStrategy_Progress(object sender, CalculationStatusEventArgs args)
        {
            currentProgress = args.Progress;

            if (args.Finished)
            {
                OnFinished();
            }
            
            if (null != _progress)
            {
                _progress(this, args);
            }
        }

        /// <summary>
        /// Is invoked when search task is finished
        /// </summary>
        private void OnFinished()
        {
            HotelSearchTask = null;
        }

        /// <summary>
        /// Search hotels for specific point
        /// </summary>
        /// <param name="point">Point to search hotels for.</param>
        /// <param name="searchRadius">Radius for hotel search.</param>
        /// <returns>Hotels matching criterias specified.</returns>
        HotelSummary[] IRouteHotelSearch.SeachHotelsForPoint(LinkedPoint point, int searchRadius)
        {
            return this.SeachHotelsForPoint(point, searchRadius);
        }

        /// <summary>
        /// Search hotels for specific point
        /// </summary>
        /// <param name="point">Point to search hotels for.</param>
        /// <param name="searchRadius">Radius for hotel search.</param>
        /// <returns>Hotels matching criterias specified.</returns>
        private HotelSummary[] SeachHotelsForPoint(LinkedPoint point, int searchRadius)
        {
            HotelListParameters hotelSearchParametersObject = CreateHotelSearhParameterObject(point);
            hotelSearchParametersObject.SearchRadius = searchRadius;

            HotelSearch search = new HotelSearch(point, hotelSearchParametersObject);
            int newHotelsFound = search.Search();

            if (newHotelsFound > 0)
            {
                HotelSummary[] hotels = search.GetHotels();
                Debug.Assert(null != hotels);

                return hotels;
            }

            return null;
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

            hotelSearchParametersObject.SearchRadiusUnit = CalculationUtils.DistanceUnit.Meters; // TODO: read from config

            return hotelSearchParametersObject;
        }

#if DEBUG
        /// <summary>
        /// Returns calculation points for this search
        /// </summary>
        /// <returns>Calculation points - for which hotels would be searched</returns>
        public GoogleDirections.LatLng[] GetCalculationPoints()
        {
            return SearchStrategy.GetCalculationPoints();
        }

        /// <summary>
        /// Returns value of calculation radius in meters
        /// </summary>
        /// <returns></returns>
        public int GetCalculationRaduis()
        {
            return SearchStrategy.GetCalculationRaduis();
        }
#endif

    }
}
