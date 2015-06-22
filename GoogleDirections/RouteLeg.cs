using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GoogleDirections
{
    /// <summary>
    /// Class representing the leg of a route
    /// </summary>
    public class RouteLeg
    {
        internal RouteLeg(XmlElement leg)
        {
            startAddress = leg.SelectSingleNode("start_address").InnerText;
            endAddress = leg.SelectSingleNode("end_address").InnerText;
            distance = int.Parse(leg.SelectSingleNode("distance/value").InnerText);
            duration = int.Parse(leg.SelectSingleNode("duration/value").InnerText);
            startLocation = new LatLng((XmlElement)leg.SelectSingleNode("start_location"));
            endLocation = new LatLng((XmlElement)leg.SelectSingleNode("end_location"));

            XmlNodeList stepsXml = leg.SelectNodes("step");
            List<RouteStep> stepsList = new List<RouteStep>();
            foreach (XmlElement step in stepsXml)
            {
                stepsList.Add(new RouteStep(step));
            }
            steps = stepsList.ToArray();
        }

        private string startAddress;
        /// <summary>
        /// Gets the start address for this leg.
        /// </summary>
        public string StartAddress
        {
            get
            {
                return startAddress;
            }
        }

        private string endAddress;
        /// <summary>
        /// Gets the end address for this leg.
        /// </summary>
        public string EndAddress
        {
            get
            {
                return endAddress;
            }
        }

        private int duration;
        /// <summary>
        /// Gets the duration of this leg in seconds.
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
        /// Gets the distance of this leg in metres.
        /// </summary>
        public int Distance
        {
            get
            {
                return distance;
            }
        }

        private RouteStep[] steps;
        /// <summary>
        /// Gets the steps for this leg of the route.
        /// </summary>
        public RouteStep[] Steps
        {
            get
            {
                return steps;
            }
        }

        private LatLng startLocation;
        /// <summary>
        /// Gets the start location of this leg of the route.
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
        /// Gets the end location of this leg of the route.
        /// </summary>
        public LatLng EndLocation
        {
            get
            {
                return endLocation;
            }
        }

        /// <summary>
        /// Method accepts points visitor. 
        /// Visitor will be invoked for each segment of the route
        /// (including all steps of the leg)
        /// </summary>
        /// <param name="visitor">Visitor object, cannot be null</param>
        public void AcceptPointVisitor(IPointVisitor visitor)
        {
            if (null == visitor) throw new ArgumentNullException("Argument 'visitor' cannot be null");
            if (null == steps) return;

            foreach (RouteStep step in this.steps)
            {
                if (step.HasPoints)
                {
                    LatLng currentPoint = step.Points[0];
                    for (int i = 1; i < step.Points.Length; ++i)
                    {
                        LatLng point = step.Points[i];
                        visitor.Visit(currentPoint, point);
                        currentPoint = point;
                    }
                }
            }
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
        /// Checks whether two route legs objects are equal 
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True, if objects are equal</returns>
        public bool Equals(RouteLeg obj)
        {
            if (null == obj) return false;

            if (this.Steps.Length != obj.Steps.Length) return false;

            if (this.StartAddress != obj.StartAddress) return false;
            if (this.EndAddress != obj.EndAddress) return false;
            if (this.Duration != obj.Duration) return false;
            if (this.Distance != obj.Distance) return false;
            if (!this.StartLocation.Equals(obj.StartLocation)) return false;
            if (!this.EndLocation.Equals(obj.EndLocation)) return false;

            if (null != this.Steps)
            {
                for (int i = 0; i < this.Steps.Length; ++i)
                {
                    if (!Steps[i].Equals(obj.Steps[i])) return false;
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
            string id = StartAddress.ToString() + EndAddress.ToString() + Duration.ToString() + Distance.ToString() + StartLocation.ToString() + EndLocation.ToString();
            if (null != this.Steps) id += this.Steps.ToString();
            return id.GetHashCode();
        }
        #endregion

    }
}
