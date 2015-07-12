using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Represents event argumets for calculation status
    /// </summary>
    public class CalculationStatusEventArgs: EventArgs
    {
        /// <summary>
        /// Count of points
        /// </summary>
        public int Count
        {
            get { return count; }
        }
        private int count;

        /// <summary>
        /// Count of processed points (current progress)
        /// </summary>
        public int Progress
        {
            get { return progress; }
        }
        private int progress;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="count">Count of all points</param>
        /// <param name="progess">Count of processed points (current progress)</param>
        public CalculationStatusEventArgs(int count, int progress)
        {
            if (progress > count)
            {
                string errMsg = string.Format("Progress ({0}) cannot be higher than total count ({1})", progress, count);
                throw new InvalidOperationException(errMsg);
            }

            this.count = count;
            this.progress = progress;
        }
    }
}
