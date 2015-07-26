using HotelInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Represenmts a factory to instantiate hotel search providers
    /// </summary>
    internal class HotelSearchFactory
    {
        /// <summary>
        /// Factory instance
        /// </summary>
        public static HotelSearchFactory Instance { get { return instance; } }
        private static HotelSearchFactory instance = new HotelSearchFactory();

        private HotelSearchFactory()
        { 
        }

        static HotelSearchFactory()
        {
            InitEAN();
        }

        /// <summary>
        /// Creates array of search providers used to search hotels
        /// </summary>
        /// <param name="parameters">Parameter to make request to</param>
        /// <returns>Array of search providers used to search hotels</returns>
        public IHotelListRequest[] CreateSearchRequestObjects(HotelInterface.TransportObjects.HotelListParameters parameters)
        {
            List<IHotelListRequest> result = new List<IHotelListRequest>();

            result.Add(CreateEANRequest(parameters)); // add more providers here

            return result.ToArray();
        }

        /// <summary>
        /// Creates EAN request
        /// </summary>
        /// <param name="parameters">Parameter to make request to</param>
        /// <returns>EAN request object</returns>
        private IHotelListRequest CreateEANRequest(HotelInterface.TransportObjects.HotelListParameters parameters)
        {
            EANInterface.HotelListRequest result = new EANInterface.HotelListRequest(parameters);
            return result;
        }

        /// <summary>
        /// Initializes EAN library
        /// </summary>
        private static void InitEAN()
        {
            // TODO - get real data
            const string API_KEY = "cbrzfta369qwyrm9t5b8y8kf";
            const string CID = "55505";
            const string MINOR_REV = "99";
            EANInterface.EANCommon.Initialize(API_KEY, CID, MINOR_REV);
        }
    }
}
