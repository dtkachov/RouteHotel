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
        /// Generates new ID for Route calculator
        /// </summary>
        /// <returns>ID</returns>
        private static Guid GenerateID()
        {
            return new Guid();
        }
    }
}