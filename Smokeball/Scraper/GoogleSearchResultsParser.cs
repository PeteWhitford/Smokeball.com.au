using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;

namespace Scraper
{
    public class GoogleSearchResultsParser : IGoogleSearchResultsParser
    {
        private const string GoogleSearchResultElement = "<div class=\"g\">";
        private const string RegexHtmlLinkBeginCloseTags = @"(<a.*?>.*?</a>)";
        private const string RegexHtmlHrefTags = @"href=\""(.*?)\""";

        public IEnumerable<Url> GetSearchResultUrls(string googleSearchResultsHtml)
        {
            var results = ParseSearchResults(googleSearchResultsHtml);

            return GetUrls(results).ToList();
        }

        public IEnumerable<string> ParseSearchResults(string htmlCode)
        {
            var html = HttpUtility.HtmlDecode(htmlCode);

            var results = GetSearchResultsList(html, GoogleSearchResultElement);

            return results;
        }

        public IEnumerable<string> GetSearchResultsList(string input, string delimiter)
        {
            var results = Regex.Split(input, delimiter).ToList();

            // Remove doc header
            results.RemoveAt(0);

            return results;
        }

        public static IEnumerable<Url> GetUrls(IEnumerable<string> results)
        {
            return results.Select(dom => new Url(FindLinks(dom).First()));
        }

        private static IEnumerable<string> FindLinks(string html)
        {
            var linkMatches = Regex.Matches(html, RegexHtmlLinkBeginCloseTags, RegexOptions.Singleline);

            foreach (Match match in linkMatches)
            {
                var value = match.Groups[1].Value;

                var hrefMatches = Regex.Match(value, RegexHtmlHrefTags,
                    RegexOptions.IgnoreCase);

                if (hrefMatches.Success)
                {
                    yield return hrefMatches.Groups[1].Value.TrimStart("/url?q=".ToCharArray());
                }
            }
        }
    }
}