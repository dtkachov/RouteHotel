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
            foreach (LinkedPoint start in routePoints.LegsStart)
            {
                LinkedPoint p = start;
                while (null != p) // IsLast property is not good work in this case as we still need to check the last one as well
                {
                    if (HotelSearchCancellationToken.IsCancellationRequested) break;

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
        private void IncreaseProgress()
        {
            currentProgress += 1;

            if (null != _progress)
            {
                CalculationStatusEventArgs args = new CalculationStatusEventArgs(RoutePoints.PointCount, currentProgress);
                if (args.Finished)
                {
                    OnFinished();
                }
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
