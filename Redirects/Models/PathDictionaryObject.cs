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
        public PathDictionaryObject(string fromLocHash, string currentPath)
        {
            FromLocationHash = fromLocHash;
            CurrentPath = currentPath;
        }
        #endregion Constructors

        #region Properties
        public string FromLocationHash { get; set; }
        public string CurrentPath { get; set; }
        #endregion Properties
    }
}
