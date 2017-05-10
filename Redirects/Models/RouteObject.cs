using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redirects.Models
{
    public class RouteObject
    {
        #region Constants
        public static string ToLocStub = "TO_LOCATION_STUB";
        #endregion Constants

        #region Constructors
        public RouteObject(string fromLoc, string toLoc)
        {
            FromLoc = fromLoc;
            ToLoc = toLoc;
        }

        // Require two parameter constructor above.
        private RouteObject()
        {
        }
        #endregion Constructors

        #region Properties
        public string FromLoc { get; set; }
        public string ToLoc { get; set; }
        #endregion Properties
    }
}
