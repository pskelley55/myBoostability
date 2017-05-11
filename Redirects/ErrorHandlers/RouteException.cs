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
        /// <summary>
        /// Constructs a RouteException Object.
        /// </summary>
        public RouteException()
        {
        }

        /// <summary>
        /// Constructs a RouteException and supplies an error message
        /// string to the base Exception class.
        /// </summary>
        /// <param name="errorMessage"></param>
        public RouteException(string errorMessage) : base (errorMessage)
        {
        }
        #endregion Constructors
    }
}
