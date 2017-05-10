using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redirects.Models;
using Redirects.Controllers;
using Redirects.Views;

/// <summary>
/// A C# application to detect circular redirects.
/// </summary>
namespace Redirects
{
    class Program
    {
        static void Main(string[] args)
        {
            var goodRouteData = new List<string>(new string[] {"/home", "/our-ceo.html -> /about-us.html", "/about-us.html -> /about", "/product-1.html -> /seo" });
            var badRouteData = new List<string>(new string[] { "/about-us.html -> /about", "/about -> about-us.html" });
            var routeController = new RouteController(RouteModel.Instance);
            var routes = routeController.Process(goodRouteData);
            var routeView = new RouteView(routes.ToList());
        }
    }
}
