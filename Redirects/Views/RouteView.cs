using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redirects.Views
{
    public class RouteView
    {
        #region Constructors
        public RouteView()
        {
        }
        #endregion Constructors

        #region Properties
        public List<string> RouteList { get; set; }
        #endregion Properties

        #region Public Methods
        public void ShowError(string errorMsg)
        {
            Console.WriteLine();
            Console.WriteLine(errorMsg);
            Console.WriteLine();
        }

        public void ShowRoutes()
        {
            Console.WriteLine();
            RouteList?.ForEach(x => Console.WriteLine(x));
            Console.WriteLine();
        }
        #endregion Public Methods
    }
}
