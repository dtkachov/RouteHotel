using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleDirections
{
    /// <summary>
    /// Points visitor - see visitor patter
    /// </summary>
    public interface IPointVisitor
    {
        /// <summary>
        /// Visitor method, will be invoked by class accepting the visitor 
        /// and two nearest points parameters will be returned 
        /// </summary>
        /// <param name="from">Starting point</param>
        /// <param name="to">Finish point</param>
        void Visit(LatLng from, LatLng to);
    }
}
