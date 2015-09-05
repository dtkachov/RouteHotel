using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelInterface.TO
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

        /// <summary>
        /// .ctor 
        /// </summary>
        public RoomParameter() { }

        public override string ToString()
        {
            string childrensStr = string.Empty;
            if (null != Childrens)
            {
                for (int i = 0; i < Childrens.Length; ++i)
                {
                    if (i > 0) childrensStr += ", ";
                    childrensStr += string.Format("'{0}'", Childrens[i]);
                }
            }

            return string.Format("Adults: '{0}', Childrens: '{1}'", AdultsCount, childrensStr);
        }
    }
}
