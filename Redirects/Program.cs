using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redirects.Models;
using Redirects.Controllers;
using Redirects.Views;
using Redirects.ErrorHandlers;

/// <summary>
/// A C# application to detect circular redirects.
/// </summary>
namespace Redirects
{
    class Program
    {
        public const int GoodRoute = 1;
        public const int BadRoute = 2;
        public const int Exit = 3;

        static void Main(string[] args)
        {
            var goodRouteData = new List<string>(new string[] { "/home", "/our-ceo.html -> /about-us.html", "/about-us.html -> /about", "/product-1.html -> /seo" });
            var badRouteData = new List<string>(new string[] { "/about-us.html -> /about", "/about -> /about-us.html" });
            int userInput = 0;
            do
            {
                userInput = DisplayMenu();
                switch(userInput)
                {
                    case GoodRoute:
                        DoRoute(goodRouteData);
                        break;

                    case BadRoute:
                        DoRoute(badRouteData);
                        break;

                    case Exit:
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Oops!");
                        break;
                }
            } while (userInput != 3);
        }

        static int DisplayMenu()
        {
            Console.WriteLine("Route");
            Console.WriteLine();
            Console.WriteLine("1. Good Route");
            Console.WriteLine("2. Bad Route");
            Console.WriteLine("3. Exit");
            Console.WriteLine();
            Console.Write("Enter choice-> ");
            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }

        static void DoRoute(List<string> routeData)
        {
            var routeController = new RouteController(new RouteModel());
            var routeView = new RouteView();
            try
            {
                routeView.RouteList = routeController.Process(routeData).ToList<string>();
                routeView.ShowRoutes();
            }
            catch (RouteException re)
            {
                routeView.ShowError(re.Message);
            }
        }
    }
}
