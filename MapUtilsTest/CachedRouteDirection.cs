using GoogleDirections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapUtilsTest
{
    /// <summary>
    /// Google has limitation for number of requests
    /// This class is done for testing purposes. 
    /// For every new route. It dumps route to local file and if file exists uses cache rather then request data from web (google)
    /// </summary>
    class CachedRouteDirection
    {
        public static Route GetRoute(bool optimize, params Location[] locations)
        {
            return null;
            //string fileName = optimize.ToString();
 
        }
    }
}
