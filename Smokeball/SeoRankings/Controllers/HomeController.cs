using System.Threading.Tasks;
using System.Web.Mvc;
using Scraper;

namespace SeoRankings.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGoogleSearchResultsScraper _scraper;

        public HomeController(IGoogleSearchResultsScraper scraper)
        {
            _scraper = scraper;
        }
        public async Task<ActionResult> Index(string searchString = "conveyancing+software", string smokeballLink = "www.smokeball.com.au")
        {
            ViewBag.SearchString = searchString;

            var rankings = await _scraper.GetSeoRankings(searchString, smokeballLink);

            return View(rankings);
        }

    }
}