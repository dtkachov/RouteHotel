using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Signals about error happened during error search
    /// </summary>
    public class HotelSearchErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Specific exception happened during hotels search
        /// </summary>
        public Exception ErrorSource
        {
            get { return errorSource; }
        }
        private Exception errorSource;

        /// <summary>
        /// Point for which error occured
        /// </summary>
        public LinkedPoint Point
        {
            get { return point; }
        }
        private LinkedPoint point;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="errorSource">Specific exception happened during hotels search</param>
        /// <param name="point">Point for which error occured</param>
        public HotelSearchErrorEventArgs(Exception errorSource, LinkedPoint point)
        {
            if (null == errorSource) throw new ArgumentNullException("Argument errorSource cannot be null");
            if (null == point) throw new ArgumentNullException("Argument point cannot be null");

            this.point = point;
            this.errorSource = errorSource;
        }
    }
}
