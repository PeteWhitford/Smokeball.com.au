using System.Collections.Generic;
using System.Security.Policy;

namespace Scraper
{
    public interface IGoogleSearchResultsParser
    {
        IEnumerable<Url> GetSearchResultUrls(string googleSearchResultsHtml);
    }
}