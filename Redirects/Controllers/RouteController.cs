using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redirects.Interfaces;
using Redirects.Models;

namespace Redirects.Controllers
{
    public class RouteController : IRouteAnalyzer
    {
        #region Constants
        internal const string RedirectString = "->";
        internal const int INDEX_FROMLOC = 0;
        internal const int INDEX_TOLOC = 1;
        #endregion Constants

        #region Member Variables
        RouteModel _routeModel;
        #endregion Member Variables

        #region Constructors
        public RouteController(RouteModel routeModel)
        {
            _routeModel = routeModel;
        }
        #endregion Constructors

        #region Properties
        RouteModel RouteModel
        {
            get
            {
                return _routeModel;
            }
        }
        #endregion Properties

        #region Public Methods
        #endregion Public Methods

        #region IRouteAnalyzer Implementation
        public IEnumerable<string> Process(IEnumerable<string> routes)
        {
            foreach (string route in routes)
            {
                var fromToLocArray = route.Split(new string[] { RedirectString }, StringSplitOptions.None);
                if (fromToLocArray.Length > 1)
                {
                    // We have fromLoc and toLoc
                    if (!RouteModel.IsRouteAdded(fromToLocArray[INDEX_FROMLOC].Trim(), fromToLocArray[INDEX_TOLOC].Trim()))
                    {
                        return null;
                    }
                }
                else
                {
                    // Just have a fromLoc
                    if (!RouteModel.IsRouteAdded(fromToLocArray[INDEX_FROMLOC].Trim()))
                    {
                        return null;
                    }
                }
            }
            return null;
        }
        #endregion IRouteAnalyzer Implementation
    }
}
