using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Scraper
{
    public class GoogleSearchResultsScraper : IGoogleSearchResultsScraper
    {
        private readonly IGoogleSearcher _googleSearcher;
        private readonly IGoogleSearchResultsParser _googleSearchResultsParser;

        public GoogleSearchResultsScraper(IGoogleSearcher google, IGoogleSearchResultsParser parser)
        {
            _googleSearcher = google;
            _googleSearchResultsParser = parser;
        }

        public async Task<IEnumerable<int>> GetSeoRankings(string searchString, string targetSeoUrl)
        {
            if (string.IsNullOrEmpty(searchString))
                throw new ArgumentNullException(nameof(searchString));

            if (string.IsNullOrEmpty(targetSeoUrl))
                throw new ArgumentNullException(nameof(targetSeoUrl));

            var rawHtml = await _googleSearcher.SendSearch(searchString);

            var results = _googleSearchResultsParser.GetSearchResultUrls(rawHtml).ToList();

            return FindTargetRankings(results, targetSeoUrl);
        }

        private static IEnumerable<int> FindTargetRankings(IEnumerable<Url> urls, string matchUrl)
        {
            var rank = 1;

            var hits = new List<int>();

            foreach (var url in urls)
            {
                if (url.Value.Contains(matchUrl))
                    hits.Add(rank);

                rank++;
            }
            return hits;
        }
    }
}
