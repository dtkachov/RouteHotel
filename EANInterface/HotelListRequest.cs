using EANInterface.TransportObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EANInterface
{
    /// <summary>
    /// Represents hotel list request
    /// </summary>
    public class HotelListRequest : BaseRequest
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
        /// HOtel list request parameters
        /// </summary>
        private HotelInterface.TransportObjects.HotelListParameters Parameters;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="URL">URL parameter to make request to</param>
        public HotelListRequest(HotelInterface.TransportObjects.HotelListParameters parameters) : base ()
        {
            Parameters = parameters;
            Init();
        }

        /// <summary>
        /// Initializes instance
        /// </summary>
        private void Init()
        {
            HotelListRequestParametersBuilder builder = new HotelListRequestParametersBuilder(Parameters);
            Uri url = builder.Build();
            Initialize(url);
        }

        /// <summary>
        /// Performs web request and parses data
        /// </summary>
        public override void Request()
        {
            base.Request();

            response = new HotelListResponse(ResponseData);
            response.ParseData();
        }
    }
}
