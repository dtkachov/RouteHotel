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
        /// .ctor
        /// </summary>
        /// <param name="APIKey">API key for EAN requests authorization</param>
        /// <param name="CID">Client Id fo EAN requests authorization</param>
        private EANCommon(string APIKey, string CID)
        {
            _CID = CID;
            _APIKey = APIKey;
        }

        /// <summary>
        /// Initializes sigletone.
        /// Must be invoked only once and only once during app lifecycle 
        /// </summary>
        /// <param name="APIKey">API key for EAN requests authorization</param>
        /// <param name="CID">Client Id fo EAN requests authorization</param>
        public static void Initialize(string APIKey, string CID)
        {
            if (null != _Instance)
            {
                throw new ApplicationException("EANCommon singletone can be initialized once and only once during application lifecycle!");
            }

            _Instance = new EANCommon(APIKey, CID);
        }
    }
}
