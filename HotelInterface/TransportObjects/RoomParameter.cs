using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelInterface.TransportObjects
{
    /// <summary>
    /// Represents room parameter
    /// </summary>
    public class RoomParameter
    {
        /// <summary>
        /// Count of adults in room
        /// </summary>
        public int AdultsCount { get; set; }

        /// <summary>
        /// Array represening children.
        /// If empry or null - no childrens
        /// If not empty - each element represent a children's age
        /// </summary>
        public int[] Childrens { get; set; }
    }
}
