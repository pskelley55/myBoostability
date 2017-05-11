using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redirects.ErrorHandlers
{
    public class RouteException : Exception
    {
        #region Constructors
        public RouteException()
        {
        }

        public RouteException(string errorMessage) : base (errorMessage)
        {
        }
        #endregion Constructors
    }
}
