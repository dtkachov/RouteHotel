using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GoogleDirections;

namespace MapUtilsTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class DistanceTest
    {
        public DistanceTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestDistance()
        {
            DoDistanceTest(49.839810000000007, 24.029470000000003, 49.839810000000007, 24.029470000000003, 0);
            DoDistanceTest(49.56085220619188, 23.6590576171875, 50.169861746007314, 21.9671630859375, 139220);
            DoDistanceTest(49.56085220619188, 23.6590576171875, 49.25561602883845, 23.194335736334324, 47829);
            DoDistanceTest(49.56085220619188, 23.6590576171875, 49.35733376286064, 24.194091595709324, 44855);
            DoDistanceTest(49.56085220619188, 23.6590576171875, 49.561456256934676, 23.66399717226159, 363);

            DoDistanceTest(36.104151580845866, -115.1762706041336, 34.95079155704961, -117.0329600572586, 211602);
            DoDistanceTest(36.104151580845866, -115.1762706041336, 36.0779620797358, -114.8609621822834, 28513);
            DoDistanceTest(36.104151580845866, -115.1762706041336, 36.27085020723905, -115.1740725338459, 18558);

            DoDistanceTest(-50.74931668393582, -69.47918727993965, -50.9677679797938, -70.0092776119709, 44484);
            DoDistanceTest(-50.74931668393582, -69.47918727993965, -50.682537457816636, -69.2347414791584, 18765);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <param name="expectedDistance">in Meters!</param>
        private void DoDistanceTest(double lat1, double lon1, double lat2, double lon2, double expectedDistance)
        {
            double result1 = DistanceUtils.Distance(new LatLng(lat1, lon1), new LatLng(lat2, lon2));
            double result2 = DistanceUtils.Distance(new LatLng(lat2, lon2), new LatLng(lat1, lon1));
            const double ACCURACY = 1; // 1 meter

            Assert.IsTrue(ACCURACY > Math.Abs(result1 - result2));

            if (0 == expectedDistance)
            {
                Assert.IsTrue(ACCURACY >= result1 && ACCURACY >= result2);
                return;
            }

            double persentgeVariance = Math.Abs((result1 - expectedDistance) / expectedDistance * 100);
            const double ACCURACY_PERSENTAGE = 1;
            Assert.IsTrue(ACCURACY_PERSENTAGE > persentgeVariance);
        }
    }
}
