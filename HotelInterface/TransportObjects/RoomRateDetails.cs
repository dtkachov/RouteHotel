using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelInterface.TO
{
    public class RoomRateDetails
    {
        #region dama sample
        /*
                            "maxRoomOccupancy": 4,
                            "quotedRoomOccupancy": 2,
                            "minGuestAge": 0,
                            "roomDescription": "Номер «Делюкс», 2 двоспальних ліжка, з видом на місто",
                            "propertyAvailable": true,
                            "propertyRestricted": false,
                            
        price
            currency
         */
        #endregion

        public int MaxRoomOccupancy { get; set; }
        public int QuotedRoomOccupancy { get; set; }
        public int MinGuestAge { get; set; }
        public string RoomDescription { get; set; }
        public bool PropertyAvailable { get; set; }
        public bool PropertyRestricted { get; set; }

        public double Price { get; set; }
        public string Currency { get; set; }

        public RoomRateDetails() { }
    }
}
