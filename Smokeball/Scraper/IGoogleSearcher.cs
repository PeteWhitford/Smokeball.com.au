using System.Threading.Tasks;

namespace Scraper
{
    public interface IGoogleSearcher
    {
        Task<string> SendSearch(string url);
    }
}