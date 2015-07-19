using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelInterface.TransportObjects
{
    /// <summary>
    /// Transport object for hotel Response data
    /// </summary>
    public class HotelList 
    {
        public HotelSummary[] Hotels { get; set; }
    }
}
