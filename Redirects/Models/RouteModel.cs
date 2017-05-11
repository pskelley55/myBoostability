using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redirects.Interfaces;
using System.Security.Cryptography;
using Redirects.ErrorHandlers;

namespace Redirects.Models
{
    public class RouteModel
    {
        #region Constants
        internal const string RedirectString = "->";
        internal const int INDEX_FROMLOC = 0;
        internal const int INDEX_TOLOC = 1;
        #endregion Constants

        #region Constructors
        /// <summary>
        /// Constructs are RouteModel object and creates an empty
        /// RouteDictionary and PathDictionary.
        /// </summary>
        public RouteModel()
        {
            RouteDictionary = new Dictionary<string, RouteObject>();
            PathDictionary = new Dictionary<string, PathDictionaryObject>();
        }
        #endregion Constructors

        #region Properties
        // Key -> The hash value of the "toLoc"ation
        // Value -> The RouteObject
        public Dictionary<string, RouteObject> RouteDictionary { get; set; }

        // Key -> A hash value (either the fromLoc or the toLoc).
        // Value -> A path under construction of the form "A->B->C..."
        public Dictionary<string, PathDictionaryObject> PathDictionary { get; set; }
        #endregion Properties

        #region Public Methods
        /// <summary>
        /// Determines if the given route should be added to the RouteDictionary
        /// </summary>
        /// <param name="fromLoc">The route "from" location</param>
        /// <param name="toLoc">The route "to" location</param>
        /// <returns></returns>
        public bool IsRouteAdded(string fromLoc, string toLoc)
        {
            var routeKey = GetHashString(fromLoc);
            var routeObj = new RouteObject(fromLoc, toLoc);
            return ShouldAddRoute(routeKey, routeObj);
        }

        /// <summary>
        /// Determines if the given route should be added to the RouteDictionary
        /// (overloaded method used in the case where only the "from" location
        /// was given).
        /// </summary>
        /// <param name="fromLoc">The route "from" location</param>
        /// <returns></returns>
        public bool IsRouteAdded(string fromLoc)
        {
            var routeKey = GetHashString(fromLoc);
            var routeObj = new RouteObject(fromLoc, RouteObject.ToLocStub);
            return ShouldAddRoute(routeKey, routeObj);
        }

        /// <summary>
        /// Uses the RouteDictionary and PathDictionary to build routes
        /// and detect circular references.
        /// If circular reference is detected, throws a RouteException.
        /// </summary>
        /// <returns>List of routes</returns>
        public List<string> BuildRouteList()
        {
            var routeList = new List<string>();
            foreach (KeyValuePair<string, RouteObject> item in RouteDictionary)
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
                    PathDictionaryObject pathDictionaryObj = null;
                    if ((!PathDictionary.ContainsKey(valFromHash))
                        && (RouteDictionary.ContainsKey(valToHash)))
                    {
                        PathDictionary.Add(valToHash, new PathDictionaryObject(valFromHash, fromLoc + RedirectString + toLoc));
                        fromLoc = null;
                        toLoc = null;
                    }
                    else if (PathDictionary.TryGetValue(valFromHash, out pathDictionaryObj))
                    {
                        if (string.Equals(pathDictionaryObj.FromLocationHash, valToHash))
                        {
                            throw new RouteException("Oh No! " + pathDictionaryObj.CurrentPath + RedirectString + toLoc);
                        }
                        pathDictionaryObj.CurrentPath += RedirectString + toLoc;
                        PathDictionary[valFromHash] = pathDictionaryObj;
                        fromLoc = null;
                        toLoc = null;
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
            foreach (KeyValuePair<string, PathDictionaryObject> item in PathDictionary)
            {
                routeList.Add(item.Value.CurrentPath);
            }
            return routeList;
        }
        #endregion Public Methods

        #region Private Methods
        bool ShouldAddRoute(string routeKey, RouteObject routeObj)
        {
            bool isGood = false;
            if (IsNewRoute(routeKey))
            {
                RouteDictionary.Add(routeKey, routeObj);
                isGood = true;
            }
            return isGood;
        }

        bool IsNewRoute(string hashValue)
        {
            return !(RouteDictionary.ContainsKey(hashValue));
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
