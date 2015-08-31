using HotelInterface.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteHotel.TransportObjects
{
    /// <summary>
    /// Represents responce for hotel request
    /// </summary>
    public class HotelResponse
    {
        /// <summary>
        /// Total count of calculation points
        /// </summary>
        public int CalculationPointCount { get; set; }

        /// <summary>
        /// Count of processed points
        /// </summary>
        public int ProcessedPointCount { get; set; }

        /// <summary>
        /// Whether calculation finished
        /// </summary>
        public bool IsFinished { get; set; }

        /// <summary>
        /// List of new hotels since last fetch
        /// </summary>
        public HotelInterface.TO.HotelSummary[] Hotels { get; set; }

        /// <summary>
        /// .ctor for transpoting the objkect
        /// </summary>
        public HotelResponse() {}

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="calculator">Calculator object</param>
        /// <param name="alreadyFetchedHotelsCount">Count of already fetched hotels</param>
        public HotelResponse(RouteCalculator calculator, int alreadyFetchedHotelsCount) 
        {
            if (null == calculator) throw new ArgumentNullException("Argument calculator cannot be null");

            ReadData(calculator, alreadyFetchedHotelsCount);
        }

        /// <summary>
        /// Reads data from calculator object provided.
        /// </summary>
        /// <param name="calculator">Calculator object</param>
        /// <param name="alreadyFetchedHotelsCount">Count of already fetched hotels</param>
        private void ReadData(RouteCalculator calculator, int alreadyFetchedHotelsCount) 
        {
            List<HotelSummary> allHotels = calculator.HotelSearch.Hotels;
            this.Hotels = FilterHotels(allHotels.ToArray(), alreadyFetchedHotelsCount);

            this.IsFinished = !calculator.SearchInProgress;
            this.ProcessedPointCount = calculator.CurrentProgress;
            this.CalculationPointCount = calculator.HotelSearch.CalculationPointCount;
        }

        /// <summary>
        /// Filter hotel. Return hotels since position after index specified.
        /// This is to help selection of hotels by bunches
        /// </summary>
        /// <param name="hotels">List of hotels</param>
        /// <param name="alreadyFetchedHotelsCount">Count of already fetched hotels</param>
        /// <returns>Hotel list</returns>
        private HotelSummary[] FilterHotels(HotelSummary[] hotels, int alreadyFetchedHotelsCount)
        {
            if (alreadyFetchedHotelsCount > hotels.Length)
            {
                string err = string.Format(
                    "Hotels count {0}, but requested to fetch hotels since index {1}. Logic error",
                    hotels.Length,
                    alreadyFetchedHotelsCount
                    );
                throw new ApplicationException(err);
            }

            int resultCount = hotels.Length - alreadyFetchedHotelsCount;
            HotelSummary[] result = new HotelSummary[resultCount];

            int startPosition = alreadyFetchedHotelsCount;
            for (int i = startPosition; i < hotels.Length; ++i)
            {
                int index = i - startPosition;
                result[index] = hotels[i];
            }

            return result;
        }
        
    }
}