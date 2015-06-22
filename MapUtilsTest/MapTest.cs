using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using MapUtils.net.bing.api;

namespace MapUtilsTest
{
    /// <summary>
    /// Summary description for MapTest
    /// </summary>
    [TestClass]
    public class MapTest
    {
        public MapTest()
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
        public void TestMethod1()
        {
            /*
            BingService service = new BingService();

            SearchRequest request = new SearchRequest();
            // use your Bing AppID
            //request.AppId = "ApGpTaYsZ1cFtPCKmfrDedCyR_3H55gawPJldTKkSHPkWWkm_ZnNbeGywW4dsLWN";
            request.AppId = "5BA839F1-12CE-4CCE-BF57-A49D98D29A44";
            request.Query = "lviv map"; // your search query

            // I want to search only web
            request.Sources = new SourceType[] { SourceType.Web };

            try
            {
                SearchResponse response = service.Search(request);

                foreach (WebResult result in response.Web.Results)
                {
                    Console.WriteLine(result.Title + " URL: " + result.Url);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                throw;
            }
            */
        }
    }
}
