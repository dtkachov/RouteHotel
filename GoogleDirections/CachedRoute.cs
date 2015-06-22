using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace GoogleDirections
{
    /// <summary>
    /// Google has limitation for number of requests
    /// This class is done for testing purposes. 
    /// For every new route. It dumps route to local file and if file exists uses cache rather then request data from web (google)
    /// </summary>
    public class CachedRoute: Route
    {

        private const string CACHE_DIR_NAME = "RouteHotelCache";
        private string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), CACHE_DIR_NAME);

        /// <summary>
        /// if set to <c>true</c> optimize the route by re-ordering the locations to minimise the time to complete the route.
        /// </summary>
        private bool Optimize;

        /// <summary>
        /// List of locations
        /// </summary>
        private Location[] Locations;

        /// <summary>
        /// Name of cache file
        /// </summary>
        private string FileName
        {
            get
            {
                if (null == _fileName)
                {
                    _fileName = GetFileName();
                }
                return _fileName;
            }
        }
        private string _fileName;

        /// <summary>
        /// Whether this is cached copy.
        /// </summary>
        public bool Cached 
        {
            get { return _cached; }
        }
        private bool _cached = false;

        /// <summary>
        /// Checks if cache file exists
        /// </summary>
        private bool CacheFileExist
        {
            get
            {
                return File.Exists(FileName);
            }
        }
            
        internal CachedRoute(bool optimize, params Location[] locations)
        {
            this.Optimize = optimize;
            this.Locations = locations;

            Init();
        }

        /// <summary>
        /// return file Name for pathe specified
        /// </summary>
        /// <returns>File name fo local cache file</returns>
        private string GetFileName()
        {
            string requestString = RouteDirections.BuildRouteMainRequestString(Optimize, Locations);

            string encodedFileName = FilenameFromTitle(requestString);
            //string encodedFileName = Convert.ToBase64String(requestString.ToCharArray().);

            string result = Path.Combine(FilePath, encodedFileName);
            return result;
        }

        /// <summary>
        /// Escapes symbols from file name and form a name that can be saved to disk
        /// </summary>
        /// <param name="name">Unescaped file name</param>
        /// <returns>Safe name of file</returns>
        private static string FilenameFromTitle(string name)
        {
            // first trim the raw string
            string safe = name.Trim();
            // replace spaces with hyphens
            safe = safe.Replace(" ", "-").ToLower();
            // replace any 'double spaces' with singles
            if (safe.IndexOf("--") > -1)
                while (safe.IndexOf("--") > -1)
                    safe = safe.Replace("--", "-");
            // trim out illegal characters
            safe = Regex.Replace(safe, "[^a-z0-9\\-]", "");
            // trim the length
            if (safe.Length > 50)
                safe = safe.Substring(0, 49);
            // clean the beginning and end of the filename
            char[] replace = { '-', '.' };
            safe = safe.TrimStart(replace);
            safe = safe.TrimEnd(replace);
            return safe;
        }

        /// <summary>
        /// Initializes route
        /// </summary>
        private void Init()
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            if (CacheFileExist)
            {
                Load(FileName);
                _cached = true;
            }
            else
            {
                System.Xml.XmlDocument xmlDoc = RouteDirections.GetRouteXML(Optimize, Locations);
                this.Init(xmlDoc);

                this.Save(FileName);// save cache
            }
        }

        /// <summary>
        /// Deletes cache file for this route
        /// </summary>
        public void DeleteCacheFile()
        {
            if (CacheFileExist)
            {
                File.Delete(FileName);
            }
        }
    }
}
