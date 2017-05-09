using System.Collections.Generic;

namespace Redirects.Interfaces
{
    public interface IRouteAnalyzer
    {
        IEnumerable<string> Process(IEnumerable<string> routes);
    }
}
