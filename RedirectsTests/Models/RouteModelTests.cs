using Microsoft.VisualStudio.TestTools.UnitTesting;
using Redirects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redirects.Controllers;
using Redirects.ErrorHandlers;

namespace Redirects.Models.Tests
{
    [TestClass()]
    public class RouteModelTests
    {
        [TestMethod()]
        public void RouteModelTest()
        {
            var routeModel = new RouteModel();
            Assert.IsTrue(routeModel != null);
        }

        [TestMethod()]
        public void RouteModelHasPathDictionaryTest()
        {
            var routeModel = new RouteModel();
            Assert.IsTrue(routeModel.PathDictionary != null);
        }

        [TestMethod()]
        public void RouteModelHasRouteDictionaryTest()
        {
            var routeModel = new RouteModel();
            Assert.IsTrue(routeModel.RouteDictionary != null);
        }

        [TestMethod()]
        public void IsRouteAddedFromTest()
        {
            var routeModel = new RouteModel();
            string fromLoc = "/home";
            Assert.IsTrue(routeModel.IsRouteAdded(fromLoc));
        }

        [TestMethod()]
        public void IsRouteAddedFromToTest()
        {
            var routeModel = new RouteModel();
            string fromLoc = "/about-us.html";
            string toLoc = "/about";
            Assert.IsTrue(routeModel.IsRouteAdded(fromLoc, toLoc));
        }

        [TestMethod()]
        public void BuildRouteGoodListTest()
        {
            var routeModel = new RouteModel();
            var goodRouteData = new List<string>(new string[] { "/home", "/our-ceo.html -> /about-us.html", "/about-us.html -> /about", "/product-1.html -> /seo" });
            var routeController = new RouteController(routeModel);
            var routeList = routeController.Process(goodRouteData);
            Assert.IsTrue(routeList.Count() == 3);
        }

        [TestMethod()]
        [ExpectedException(typeof(RouteException))]
        public void BuildRouteBadListTest()
        {
            var routeModel = new RouteModel();
            var badRouteData = new List<string>(new string[] { "/about-us.html -> /about", "/about -> /about-us.html" });
            var routeController = new RouteController(routeModel);
            var routeList = routeController.Process(badRouteData);
        }
    }
}