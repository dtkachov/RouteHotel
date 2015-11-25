using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Class representing route for calculation
    /// </summary>
    public class Route
    {
        /// <summary>
        /// List of legs represented by their start points
        /// </summary>
        public LinkedPoint[] RouteLegsStart { get { return routeLegsStart; } }
        private LinkedPoint[] routeLegsStart;

        public Route(LinkedPoint[] routeLegsStart)
        {
            if (null == routeLegsStart) throw new ArgumentNullException("Argument 'routeLegsStart' cannot be null");
            if (0 == routeLegsStart.Length) throw new ArgumentException("Argument 'routeLegsStart' must contain at least one element");

            this.routeLegsStart = routeLegsStart;
        }
    }
}
