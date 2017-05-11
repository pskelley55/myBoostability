using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redirects.Models
{
    public class PathDictionaryObject
    {
        #region Constructors
        /// <summary>
        /// Creates a PathDictionaryObject to be used during route construction.
        /// </summary>
        /// <param name="fromLocHash">The "from" location hash value.</param>
        /// <param name="currentPath">The current path/route under construction.</param>
        public PathDictionaryObject(string fromLocHash, string currentPath)
        {
            FromLocationHash = fromLocHash;
            CurrentPath = currentPath;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// The "from" location hash value.
        /// </summary>
        public string FromLocationHash { get; set; }

        /// <summary>
        /// The CurrentPath (or route) under construction.
        /// </summary>
        public string CurrentPath { get; set; }
        #endregion Properties
    }
}
