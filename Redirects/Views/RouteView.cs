using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redirects.Views
{
    public class RouteView
    {
        #region Member Variables
        List<string> _routeList;
        #endregion Member Variables

        #region Constructors
        public RouteView(List<string> routeList)
        {
            _routeList = routeList;
        }
        #endregion Constructors

        #region Public Methods
        public void ShowRoutes()
        {
            Console.WriteLine();
            _routeList.ForEach(x => Console.WriteLine(x));
            EndProgram();
        }
        #endregion Public Methods

        #region Private Methods
        void EndProgram()
        {
            Console.WriteLine();
            Console.WriteLine("Press <Enter> to end program");
            Console.ReadLine();
        }
        #endregion Private Methods
    }
}
