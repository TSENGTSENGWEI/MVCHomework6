using Microsoft.AspNetCore.Mvc;
using MVCHomework6.Models;
using System.Diagnostics;
using MVCHomework6.Data;
using MVCHomework6.Data.Database;
using X.PagedList;
using System.Linq;

namespace MVCHomework6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogDbContext _context;
        private int pageSize = 5;

        public HomeController(ILogger<HomeController> logger, BlogDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int page)
        {
            page = (page == 0) ? 1 : page;
            var model = _context.Articles.ToPagedList(page, pageSize);

            return View(model);
        }

        [HttpGet]
        public IActionResult FindSearch(string SearchString, int page)
        {
            page = (page == 0) ? 1 : page;

            if (SearchString == null)
            {
                var AllArticles = _context.Articles.ToPagedList(1, pageSize); ;
                return View(AllArticles);
            }

            var Searchresult = _context.Articles.Where(x => x.Body.Contains(SearchString));
            var model = Searchresult.ToPagedList(page, pageSize);

            ViewData["SearchContext"] = $"{SearchString}";
            return View(model);
        }

        [Route("FindTag/{tag?}/{page?}")]
        public IActionResult FindTag(string tag, int page)
        {
            page = (page == 0) ? 1 : page;

            var SearchTag = from item in _context.Articles.AsEnumerable()
                            where item.Tags.Split(",").Contains(tag)
                            select item;

            var model = SearchTag.ToPagedList(page, pageSize);

            ViewData["tag"] = $"{tag}";

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}