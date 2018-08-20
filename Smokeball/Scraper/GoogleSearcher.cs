using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scraper
{
    public class GoogleSearcher : IGoogleSearcher
    {
        private const string BaseSearchUrl = "https://www.google.com.au/search";
        private const int IncludedSearchResults = 100;

        public async Task<string> SendSearch(string searchString)
        {
            var url = new Uri(FormatSearchUrl(searchString));

            string htmlCode;

            using (var client = new HttpClient())
            {
                htmlCode = await client.GetStringAsync(url);
            }

            return htmlCode;
        }

        private static string FormatSearchUrl(string searchString)
        {
            return $"{BaseSearchUrl}?num={IncludedSearchResults}&q={searchString}";
        }
    }
}