using EANInterface.JsonNET;
using EANInterface.TransportObjects;
using HotelInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EANInterface
{
    /// <summary>
    /// Represents hotel list request
    /// </summary>
    public class HotelListRequest : BaseRequest, IHotelListRequest
    {
        /// <summary>
        /// Parced Response
        /// </summary>
        public HotelListResponse Response
        {
            get { return response; }
        }
        private HotelListResponse response;

        /// <summary>
        /// Raw responce object retrived
        /// </summary>
        private EANHotelList RawResponce;

        /// <summary>
        /// Hotel list request parameters
        /// </summary>
        private HotelInterface.TO.HotelListParameters Parameters { get { return _parameters; } }
        private HotelInterface.TO.HotelListParameters _parameters;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="parameters">Parameter to make request to</param>
        public HotelListRequest(HotelInterface.TO.HotelListParameters parameters) : base ()
        {
            _parameters = parameters;
            Init();
        }

        /// <summary>
        /// Initializes instance
        /// </summary>
        private void Init()
        {
            Validate();

            HotelListRequestParametersBuilder builder = new HotelListRequestParametersBuilder(Parameters);
            Uri url = builder.Build();
            Initialize(url);
        }

        /// <summary>
        /// Vaidates parameters
        /// </summary>
        private void Validate()
        {
            const int MINIMAL_ACCEPTABLE_SEARCH_RAIUS_METERS = 2000;
            Debug.Assert(Parameters.SearchRadius >= MINIMAL_ACCEPTABLE_SEARCH_RAIUS_METERS, 
                string.Format("Attempt to execut EAN hotel search request with search radius equal {0} meters, while minimal acceptable radius value {1} meters", Parameters.SearchRadius, MINIMAL_ACCEPTABLE_SEARCH_RAIUS_METERS));
        }

        /// <summary>
        /// Performs web request and parses data
        /// </summary>
        public HotelInterface.TO.HotelList Request()
        {
            MakeRequest();

            response = new HotelListResponse(RawResponce);

            return Response.Hotels;
        }

        /// <summary>
        /// Initializs response object
        /// </summary>
        /// <param name="responseStr">Downloaded responce</param>
        protected override void InitResponce(string responseStr)
        {
            RawResponce = Newtonsoft.Json.JsonConvert.DeserializeObject<EANInterface.JsonNET.EANHotelList>(responseStr);
        }
    }
}
