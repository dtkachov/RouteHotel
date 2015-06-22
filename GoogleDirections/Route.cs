using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using System.IO;

namespace GoogleDirections
{
    /// <summary>
    /// Class representing a route containing directions between various locations
    /// </summary>
    public class Route
    {
        private XmlDocument route;

        private string summary;
        /// <summary>
        /// Gets a summary of the roads used in the calculated route.
        /// </summary>
        public string Summary
        {
            get
            {
                return summary;
            }
        }

        private RouteLeg[] legs;
        /// <summary>
        /// Gets the legs of this route.
        /// </summary>
        public RouteLeg[] Legs
        {
            get
            {
                return legs;
            }
        }

        /// <summary>
        /// Gets the duration of the route in seconds.
        /// </summary>
        public int Duration
        {
            get
            {
                int duration = 0;
                for (int i = 0; i < legs.Length; i++)
                {
                    duration += legs[i].Duration;
                }
                return duration;
            }
        }

        /// <summary>
        /// Gets the distance of the route in metres.
        /// </summary>
        public int Distance
        {
            get
            {
                int distance = 0;
                for (int i = 0; i < legs.Length; i++)
                {
                    distance += legs[i].Distance;
                }
                return distance;
            }
        }

        /// <summary>
        /// Constructs new route class based on rouute document xml provided
        /// </summary>
        /// <param name="route">Route document</param>
        internal Route(XmlDocument route)
        {
            Init(route);
        }

        /// <summary>
        /// Default constructor - for overloading case ony
        /// </summary>
        protected Route()
        {
        }

        /// <summary>
        /// Initializes class from route document provided
        /// </summary>
        /// <param name="route">Route document</param>
        protected void Init(XmlDocument route)
        {
            this.route = route;
            summary = route.DocumentElement.SelectSingleNode("route/summary").InnerText;
            XmlNodeList legsXml = route.DocumentElement.SelectNodes("route/leg");
            List<RouteLeg> legsList = new List<RouteLeg>();
            foreach (XmlElement leg in legsXml)
            {
                legsList.Add(new RouteLeg(leg));
            }
            legs = legsList.ToArray();
        }

        /// <summary>
        /// Saves route in a file under path specified 
        /// Summary would be a file name
        /// </summary>
        /// <param name="fileName">File to save data to</param>
        protected void Save(string fileName)
        {
            route.Save(fileName);

        }

        /// <summary>
        /// Loads route from file specified
        /// </summary>
        /// <param name="fileName">name of route file</param>
        protected void Load(string fileName)
        {
            if (!File.Exists(fileName)) throw new ArgumentException(string.Format("Cannot file file {0}", fileName));

            XmlDocument routeDoc = new XmlDocument();
            routeDoc.Load(fileName);

            Init(routeDoc);

        }

        #region Equals
        /// <summary>
        /// Checks whether two route objects are equal 
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True, if objects are equal</returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Checks whether two route objects are equal 
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True, if objects are equal</returns>
        public bool Equals(Route obj)
        {
            if (null == obj) return false;

            if (this.Summary != obj.Summary) return false;
            if (this.Legs.Length != obj.Legs.Length) return false;

            // block below is optimization - if calculated params differs- this is not same route
            if (this.Distance != obj.Distance) return false;
            if (this.Duration != obj.Duration) return false;

            if (null != this.Legs)
            {
                for (int i = 0; i < this.Legs.Length; ++i)
                {
                    if (!Legs[i].Equals(obj.Legs[i])) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return summary.GetHashCode();
        }
        #endregion
    }
}
