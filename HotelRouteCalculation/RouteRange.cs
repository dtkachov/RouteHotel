using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Represents route range (start/finish point)
    /// </summary>
    class RouteRange
    {
        /// <summary>
        /// Start point of range
        /// </summary>
        public LinkedPoint Start { get { return _start; } }
        private LinkedPoint _start;

        /// <summary>
        /// Finish point of range
        /// </summary>
        public LinkedPoint Finish { get { return _finish; } }
        private LinkedPoint _finish;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="start">Start point of range</param>
        /// <param name="finish">Finish point of range</param>
        public RouteRange(LinkedPoint start, LinkedPoint finish)
        {
            if (null == start) throw new ArgumentNullException("Argument start cannot be null.");
            if (null == finish) throw new ArgumentNullException("Argument finish cannot be null.");

            this._start = start;
            this._finish = finish;

#if DEBUG
            CheckBelongsSameRoute();
#endif
        }

        /// <summary>
        /// Checks whether points belongs same route (are linked).
        /// Raises InvalidOperationException exception if not
        /// </summary>
        private void CheckBelongsSameRoute()
        {
            LinkedPoint p = Start;

            while (null != p) // IsLast property is not good work in this case as we still need to check the last one as well
            {
                if (Finish == p) 
                    return; // met finish point
                p = p.Next;
            }
            // not met finish point since we got here
            throw new InvalidOperationException("Start and finish points does not belongs same route");
        }
    }
}
