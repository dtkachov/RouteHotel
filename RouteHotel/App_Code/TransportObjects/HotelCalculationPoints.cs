using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteHotel.TransportObjects
{
    /// <summary>
    /// Information about hotel calculation points (the one used to search hotels)
    /// </summary>
    public class HotelCalculationPoints
    {
        /// <summary>
        /// Calculation points
        /// </summary>
        public LatLng[] Points { get { return _points; }  set { _points = value; } }
        private LatLng[] _points;

        /// <summary>
        /// Calculation Radius in meters
        /// </summary>
        public int CalculationRadius { get; set; }

        public HotelCalculationPoints(int calculationRadius, GoogleDirections.LatLng[] points) 
        {
            this.CalculationRadius = calculationRadius;

            List<LatLng> transportPoints = new List<LatLng>();
            foreach (GoogleDirections.LatLng point in points)
            {
                LatLng transportPoint = new LatLng(point);
                transportPoints.Add(transportPoint);
            }
            _points = transportPoints.ToArray();
        }

        public HotelCalculationPoints() { }
    }
}