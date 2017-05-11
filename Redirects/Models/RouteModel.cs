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
        #region Constants
        internal const string RedirectString = "->";
        internal const int INDEX_FROMLOC = 0;
        internal const int INDEX_TOLOC = 1;
        #endregion Constants

        #region Member Variables
        private static readonly RouteModel _routeModel = new RouteModel();
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
                if (_routeModel.RouteTable == null)
                {
                    _routeModel.RouteTable = new Dictionary<string, RouteObject>();
                    _routeModel.PathDictionary = new Dictionary<string, string>();
                }
                return _routeModel;
            }
        }

        // Key -> The hash value of the "toLoc"ation
        // Value -> The RouteObject
        public Dictionary<string, RouteObject> RouteTable;

        // Key -> A hash value (either the fromLoc or the toLoc).
        // Value -> A path under construction of the form "A->B->C..."
        Dictionary<string, string> PathDictionary;

        public List<string> RouteList
        {
            get
            {
                var routeList = new List<string>();
                BuildRouteList(routeList);
                return routeList;
            }
        }
        #endregion Properties

        #region Public Methods
        public bool IsRouteAdded(string fromLoc, string toLoc)
        {
            var routeKey = GetHashString(fromLoc);
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
        void BuildRouteList(List<string> routeList)
        {
            foreach (KeyValuePair<string, RouteObject> item in RouteTable)
            {
                var fromLoc = item.Value.FromLoc;
                var toLoc = item.Value.ToLoc;
                var valFromHash = GetHashString(fromLoc);
                string valToHash = string.Empty;
                if (!string.Equals(toLoc, RouteObject.ToLocStub))
                {
                    valToHash = GetHashString(toLoc);
                }
                if (!string.IsNullOrEmpty(valToHash))
                {
                    if (RouteTable.ContainsKey(valToHash))
                    {
                        PathDictionary.Add(valToHash, fromLoc + RedirectString + toLoc);
                        fromLoc = null;
                        toLoc = null;
                    }
                    else if (PathDictionary.ContainsKey(valFromHash))
                    {
                        string curPath = string.Empty;
                        if (PathDictionary.TryGetValue(valFromHash, out curPath))
                        {
                            curPath += RedirectString + toLoc;
                            PathDictionary[valFromHash] = curPath;
                            fromLoc = null;
                            toLoc = null;
                        }
                    }
                }
                if ((!string.IsNullOrEmpty(fromLoc))
                    && (!string.IsNullOrEmpty(toLoc))
                    && (!string.Equals(toLoc, RouteObject.ToLocStub)))
                {
                    routeList.Add(fromLoc + RedirectString + toLoc);
                }
                else if (!string.IsNullOrEmpty(fromLoc))
                {
                    routeList.Add(fromLoc);
                }
            }
            foreach(KeyValuePair<string, string> item in PathDictionary)
            {
                routeList.Add(item.Value);
            }
        }

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
