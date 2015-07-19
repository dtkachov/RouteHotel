using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization.Json;

namespace EANInterface
{
    /// <summary>
    /// Represents EAN common data
    /// Singletone
    /// </summary>
    public class EANCommon
    {
        /// <summary>
        /// Instance of the class
        /// </summary>
        public static EANCommon Instance
        {
            get { return _Instance; }
        }
        private static EANCommon _Instance = null;

        /// <summary>
        /// Client Id fo EAN requests authorization
        /// </summary>
        public string CID
        {
            get { return _CID; }
        }
        private string _CID;

        /// <summary>
        /// API key for EAN requests authorization
        /// </summary>
        public string APIKey
        {
            get { return _APIKey; }
        }
        private string _APIKey;

        /// <summary>
        /// Minor Rev for EAN requests authorization
        /// </summary>
        public string MinorRev
        {
            get { return _minorRev; }
        }
        private string _minorRev;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="APIKey">API key for EAN requests authorization</param>
        /// <param name="CID">Client Id fo EAN requests authorization</param>
        /// <param name="minorRev">MinorRev fo EAN requests authorization</param>
        private EANCommon(string APIKey, string CID, string minorRev)
        {
            _CID = CID;
            _APIKey = APIKey;
            _minorRev = minorRev;
        }

        /// <summary>
        /// Initializes sigletone.
        /// Must be invoked only once and only once during app lifecycle 
        /// </summary>
        /// <param name="APIKey">API key for EAN requests authorization</param>
        /// <param name="CID">Client Id fo EAN requests authorization</param>
        /// <param name="minorRev">MinorRev fo EAN requests authorization</param>
        public static void Initialize(string APIKey, string CID, string minorRev)
        {
            if (null != _Instance)
            {
                throw new ApplicationException("EANCommon singletone can be initialized once and only once during application lifecycle!");
            }
            if (null == APIKey || APIKey.Trim().Length == 0) throw new ArgumentNullException("Parameter APIKey cannot be null or empry");
            if (null == CID || CID.Trim().Length == 0) throw new ArgumentNullException("Parameter CID cannot be null or empry");
            if (null == minorRev || minorRev.Trim().Length == 0) throw new ArgumentNullException("Parameter minorRev cannot be null or empry");

            _Instance = new EANCommon(APIKey, CID, minorRev);
        }
    }
}
