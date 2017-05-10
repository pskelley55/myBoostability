using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redirects.Interfaces;
using System.Security.Cryptography;

namespace Redirects.Models
{
    public class RouteModel
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
                    _routeModel.RouteTable = new Dictionary<string, RouteObject>();
                }
                return _routeModel;
            }
        }

        // Key -> The hash value of the "toLoc"ation
        // Value -> The RouteObject
        public Dictionary<string, RouteObject> RouteTable;
        #endregion Properties

        #region Public Methods
        public bool IsRouteAdded(string fromLoc, string toLoc)
        {
            var routeKey = GetHashString(toLoc);
            var routeObj = new RouteObject(fromLoc, toLoc);
            return ShouldAddRoute(routeKey, routeObj);
        }

        public bool IsRouteAdded(string fromLoc)
        {
            var routeKey = GetHashString(fromLoc);
            var routeObj = new RouteObject(fromLoc, RouteObject.ToLocStub);
            return ShouldAddRoute(routeKey, routeObj);
        }

        #endregion Public Methods

        #region Private Methods
        bool ShouldAddRoute(string routeKey, RouteObject routeObj)
        {
            bool isGood = false;
            if (IsNewRoute(routeKey))
            {
                RouteTable.Add(routeKey, routeObj);
                isGood = true;
            }
            return isGood;
        }

        bool IsNewRoute(string hashValue)
        {
            return !(RouteTable.ContainsKey(hashValue));
        }

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
    }
}
