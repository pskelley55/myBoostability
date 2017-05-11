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
            EndProgram();
        }

        public void ShowRoutes()
        {
            Console.WriteLine();
            RouteList?.ForEach(x => Console.WriteLine(x));
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
