using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redirects.Interfaces;

namespace Redirects.Models
{
    public class RouteModel : IRouteAnalyzer
    {
        public bool AddRoute(string route)
        {
            bool isGood = true;
            return isGood;
        }

        #region IRouteAnalyzer Implementation
        public IEnumerable<string> Process(IEnumerable<string> routes)
        {
            return null;
        }
        #endregion IRouteAnalyzer Implementation
    }
}
