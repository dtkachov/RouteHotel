using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GoogleDirections
{
    /// <summary>
    /// Class representing a step within a leg of a route
    /// </summary>
    public class RouteStep
    {
        internal RouteStep(XmlElement step)
        {
            distance = int.Parse(step.SelectSingleNode("distance/value").InnerText);
            duration = int.Parse(step.SelectSingleNode("duration/value").InnerText);
            startLocation = new LatLng((XmlElement)step.SelectSingleNode("start_location"));
            endLocation = new LatLng((XmlElement)step.SelectSingleNode("end_location"));
            htmlInstructions = step.SelectSingleNode("html_instructions").InnerText;

            DecodeLine(step);

        }

        private int duration;
        /// <summary>
        /// Gets the duration of this step in seconds.
        /// </summary>
        public int Duration
        {
            get
            {
                return duration;
            }
        }

        private int distance;
        /// <summary>
        /// Gets the distance in metres for this step.
        /// </summary>
        public int Distance
        {
            get
            {
                return distance;
            }
        }

        private LatLng startLocation;
        /// <summary>
        /// Gets the start location for this step.
        /// </summary>
        public LatLng StartLocation
        {
            get
            {
                return startLocation;
            }
        }

        private LatLng endLocation;
        /// <summary>
        /// Gets the end location of this step.
        /// </summary>
        public LatLng EndLocation
        {
            get
            {
                return endLocation;
            }
        }

        private string htmlInstructions;
        /// <summary>
        /// Gets the instructions for this step with HTML formatting.
        /// </summary>
        public string HtmlInstructions
        {
            get
            {
                return htmlInstructions;
            }
        }

        /// <summary>
        /// List of points
        /// </summary>
        public LatLng[] Points
        {
            get { return points; }
        }
        private LatLng[] points = null;

        /// <summary>
        /// Wether this step has points
        /// </summary>
        public bool HasPoints
        {
            get
            {
                return (null != points && points.Length > 0);
            }
        }


        /// <summary>
        /// This function is from Google's polyline utility.
        /// </summary>
        /// <param name="step">XML elemel representing step</param>
        private void DecodeLine(XmlElement step)
        {
            XmlNode node = step.SelectSingleNode("polyline/points");
            if (null == node) return;

            string encoded = node.InnerText;
            int len = encoded.Length;
            int index = 0;
            List<LatLng> array = new List<LatLng>();
            double lat = 0;
            double lng = 0;

            while (index < len)
            {
                int b;
                int shift = 0;
                int result = 0;
                do
                {
                    b = encoded[index++] - 63;
                    result |= (b & 0x1f) << shift;
                    shift += 5;
                } while (b >= 0x20);
                int dlat = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
                lat += dlat;

                shift = 0;
                result = 0;
                do
                {
                    b = encoded[index++] - 63;
                    result |= (b & 0x1f) << shift;
                    shift += 5;
                } while (b >= 0x20);
                int dlng = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
                lng += dlng;

                LatLng point = new LatLng(lat * 1e-5, lng * 1e-5);
                array.Add(point);
            }

            points = array.ToArray();
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
        /// Checks whether two route steps objects are equal 
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True, if objects are equal</returns>
        public bool Equals(RouteStep obj)
        {
            if (null == obj) return false;

            if (this.HasPoints != obj.HasPoints) return false;
            if (this.HasPoints)
                if (this.Points.Length != obj.Points.Length) return false;

            if (this.Duration != obj.Duration) return false;
            if (this.Distance != obj.Distance) return false;
            if (!this.StartLocation.Equals(obj.StartLocation)) return false;
            if (!this.EndLocation.Equals(obj.EndLocation)) return false;

            if (this.HasPoints)
            {
                for (int i = 0; i < this.Points.Length; ++i)
                {
                    if (!Points[i].Equals(obj.Points[i])) return false;
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
            string id = Duration.ToString() + Distance.ToString() + StartLocation.ToString() + EndLocation.ToString();
            if (this.HasPoints) id += this.Points.ToString();
            return id.GetHashCode();
        }
        #endregion
    }
}
