using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scraper
{
    public interface IGoogleSearchResultsScraper
    {
        Task<IEnumerable<int>> GetSeoRankings(string searchString, string targetSeoUrl);
    }
}