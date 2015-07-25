using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using MapUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MapUtilsTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ProximityTest
    {
        public ProximityTest()
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
        public void TestInputParameters()
        {
            try
            {
                new Proximity(-1, 2);
                Assert.Fail("radius not checked on zero");
            }
            catch (ArgumentException)
            { 
            }

            try
            {
                new Proximity(3, -2);
                Assert.Fail("accuracy not checked on zero");
            }
            catch (ArgumentException)
            {
            }

            try
            {
                new Proximity(3, 101);
                Assert.Fail("accuracy can be greater than 100");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void TestCalculation()
        {
            TestSignleCalculation(2, 5, 1.788854);
            TestSignleCalculation(2, 6, 1.959592);
            TestSignleCalculation(2, 7, 2.116601);
            TestSignleCalculation(2, 4, 1.6);

            TestSignleCalculation(3, 4, 2.4);
            TestSignleCalculation(3, 5, 2.683282);
            TestSignleCalculation(3, 6, 2.939388);
            TestSignleCalculation(3, 7, 3.174902);
            TestSignleCalculation(3, 8, 3.394113);

        }

        /// <summary>
        /// Do single test for calculation
        /// </summary>
        /// <param name="radius">Radius value to pass</param>
        /// <param name="accuracy">Accuracy value to pass</param>
        /// <param name="step">Step value to be verified with what calculation returns</param>
        private void TestSignleCalculation(int radius, short accuracy, double step)
        {
            var proximity = new Proximity(radius, accuracy);
            const double DELTA_ALLOWED = 0.000001;
            Assert.AreEqual(step, proximity.Step, DELTA_ALLOWED);
        }
    }
}
