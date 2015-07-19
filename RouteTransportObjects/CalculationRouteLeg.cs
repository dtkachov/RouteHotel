using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteTransportObjects
{
    /// <summary>
    /// Class representing ropute leg consisting of calculation points
    /// Point order does matter. Each next poitn is next in linked point collection.
    /// This call ise used to replace initial idea of linked list since in case if 
    /// list of linked points is long it cannot be transfered through web service to client
    /// </summary>
    public class CalculationRouteLeg
    {
        /// <summary>
        /// List of calculation posints
        /// 
        /// Point order does matter. Each next poitn is next in linked point collection.
        /// </summary>
        public CalculationPoint[] Points { get; set; }

        /// <summary>
        /// Defauls .ctor for WS
        /// </summary>
        public CalculationRouteLeg()
        { 
        }

    }
}