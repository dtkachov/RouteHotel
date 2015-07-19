
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationUtils
{
    /// <summary>
    /// Default values holder
    /// </summary>
    public class CONSTS
    {
        /// <summary>
        /// DEfault accuracy to consider points equal
        /// </summary>
        public const double ACCURACY = 1; // meters

        /// <summary>
        /// Count of meters in kilometer
        /// </summary>
        public const short METERS_IN_KM = 1000;

        /// <summary>
        /// Default unit
        /// </summary>
        public const DistanceUnit DEFAULT_DISTANCE_UNIT = DistanceUnit.Meters;

        /// <summary>
        /// Earth's circumference at the equator in km
        /// </summary>
        public const double CIRCUMFERENCE_KM = 40000.0;
    }
}
