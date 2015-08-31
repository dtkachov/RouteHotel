using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Represents region for load balancig search  
    /// </summary>
    class LoadBalancingRegion
    {
        /// <summary>
        /// Start of the region
        /// </summary>
        public LinkedPoint Start { get; set; }

        /// <summary>
        /// Center of the region
        /// </summary>
        public LinkedPoint Center { get; set; }

        /// <summary>
        /// End of the region
        /// </summary>
        public LinkedPoint End { get; set; }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="start">Start of the region</param>
        /// <param name="center">Center of the region</param>
        /// <param name="end">End of the region</param>
        public LoadBalancingRegion(LinkedPoint start, LinkedPoint center, LinkedPoint end)
        {
            this.Start = start;
            this.Center = center;
            this.End = end;
        }
    }
}
