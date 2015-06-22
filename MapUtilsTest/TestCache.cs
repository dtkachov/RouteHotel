using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoogleDirections;

namespace MapUtilsTest
{
    /// <summary>
    /// Test whether cache works fine
    /// Please set [, Ignore] attribute in normal mode, because cache is done to prevent account from beeing blocked by google because of number of requests
    /// So this is only to veryfy if cached data works fine. 
    /// Test to be used only if necessary to check cache work accuracy or if neede to fix something.
    /// </summary>
    [TestClass]
    public class TestCache
    {
        private Location[] Locations;

        /// <summary>
        /// Test whether cache works fine
        /// Please set [, Ignore] attribute in normal mode, because cache is done to prevent account from beeing blocked by google because of number of requests
        /// So this is only to veryfy if cached data works fine. 
        /// Test to be used only if necessary to check cache work accuracy or if neede to fix something.
        /// </summary>
        [TestMethod, Ignore]
        public void TestCacheData()
        {
            InitRoute(
                new Location("Lviv"),
                new Location("Kyiv")
                );
            TestCacheInternal();

            InitRoute(
                new Location("Lissabon"),
                new Location("Vladivostok")
                );
            TestCacheInternal();
             
        }

        private void InitRoute(params Location[] locations)
        {
            this.Locations = locations;
        }

        /// <summary>
        /// Internall execution of cache test once it initialized
        /// </summary>
        private void TestCacheInternal()
        {
            const bool OPTIMIZE_ROUTE = true;
            Route webRequestedRoute = RouteDirections.GetRoute(OPTIMIZE_ROUTE, Locations);
            CachedRoute cachedRoute = RouteDirections.GetCachedRoute(OPTIMIZE_ROUTE, Locations);

            if (!cachedRoute.Cached)
            {
                // this was a feshe copy - try obtaine it again
                cachedRoute = RouteDirections.GetCachedRoute(OPTIMIZE_ROUTE, Locations);
            }

            Assert.IsTrue(webRequestedRoute.Equals(cachedRoute));
            Assert.IsTrue(cachedRoute.Equals(webRequestedRoute));

        }


    }
}
