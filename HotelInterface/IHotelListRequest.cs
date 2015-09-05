using HotelInterface.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelInterface
{
    /// <summary>
    /// Interface for hotel list request implementations
    /// </summary>
    public interface IHotelListRequest
    {

        /// <summary>
        /// Performs web request and parses data
        /// </summary>
         HotelList Request();
    }
}
