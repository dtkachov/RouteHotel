using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapUtils
{
    /// <summary>
    /// Use to calculate value of distance from path occupied by radius approach
    /// </summary>
    public class Proximity
    {

        /// <summary>
        /// Required accuracy radius in meters
        /// </summary>
        public int Radius
        {
            get { return radius; }
        }
        private int radius;

        /// <summary>
        /// Value of minimal band (radius minimized on accuracy value)
        /// </summary>
        public double MinBand
        {
            get { return minBand; }
        }
        private double minBand;

        /// <summary>
        /// Value of maximum band (radius maximized on accuracy value)
        /// </summary>
        public double MaxBand
        {
            get { return maxBand; }
        }
        private double maxBand;

        /// <summary>
        /// Calculation accuracy (percentage value)
        /// </summary>
        public short Accuracy
        {
            get { return accuracy; }
        }
        private short accuracy;

        /// <summary>
        /// Distance between points, meters
        /// </summary>
        public double Step
        {
            get { return step; }
        }
        private double step;

        public const short RECOMMENDED_ACCURACY = 5;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="radius">Required accuracy radius in meters</param>
        public Proximity(int radius)
        {
            Init(radius, RECOMMENDED_ACCURACY);
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="radius">Required accuracy radius in meters</param>
        /// <param name="accuracy">Calculation accuracy (percentage value). Recommended accuracy value 5%</param>
        public Proximity(int radius, short accuracy)
        {
            Init(radius, accuracy);
        }

        /// <summary>
        /// Makes object initialization
        /// </summary>
        /// <param name="radius">Required accuracy radius in meters</param>
        /// <param name="accuracy">Calculation accuracy (percentage value). Recommended accuracy value 5%</param>
        private void Init(int radius, short accuracy)
        {
            if (radius < 0) throw new ArgumentException("Value of 'radius' should be bigger than zero");
            if (accuracy < 0) throw new ArgumentException("Value of 'accuracy' should be bigger than zero");
            if (accuracy > 100) throw new ArgumentException("Value of 'accuracy' should be persentage, and less than 100%");

            this.accuracy = accuracy;
            this.radius = radius;

            Calculate();
        }

        /// <summary>
        /// Calculates values
        /// </summary>
        private void Calculate()
        { 
            const short MAX_PERSENTAGE = 100;
            double accuracyValue = ((double)radius) * accuracy / MAX_PERSENTAGE;
            minBand = radius - accuracyValue;
            maxBand = radius + accuracyValue;

            /* calculation is based on two circles of same radius (maxBand) 
             * positioned on <step> distance from each other
             * and triagnle build from center of one circle
             * to circles cross and projection to line connecting 
             * two radius centers (path way).
             * Line where circle crossed is minimal distance (minBand).
             * So there is right trianle.
             * Calculation defines max step value which guarantee stil
             * min and max Band values
             */
            step = 2 * Math.Sqrt(maxBand * maxBand - minBand * minBand);
        }


    }
}
