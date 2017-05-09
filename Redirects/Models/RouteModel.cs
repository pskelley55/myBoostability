using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redirects.Interfaces;
using System.Security.Cryptography;

namespace Redirects.Models
{
    public class RouteModel : IRouteAnalyzer
    {
        #region Member Variables
        static RouteModel _routeModel;
        #endregion Member Variables

        #region Constructors
        private RouteModel()
        {
        }
        #endregion Constructors

        #region Properties
        public static RouteModel Instance
        {
            get
            {
                if (_routeModel == null)
                {
                    _routeModel = new RouteModel();
                }
                return _routeModel;
            }
        }
        #endregion Properties

        #region Public Methods
        public bool AddRoute(string fromLoc, string toLoc)
        {
            bool isGood = true;
            return isGood;
        }
        #endregion Public Methods

        #region Private Methods
        string GetHashString(string inputString)
        {
            var sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        byte[] GetHash(string inputString)
        {
            var algorithm = MD5.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
        #endregion Private Methods

        #region IRouteAnalyzer Implementation
        public IEnumerable<string> Process(IEnumerable<string> routes)
        {
            return null;
        }
        #endregion IRouteAnalyzer Implementation
    }
}
