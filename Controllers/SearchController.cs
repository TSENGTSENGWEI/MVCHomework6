using Microsoft.AspNetCore.Mvc;
using MVCHomework6.Data;
using MVCHomework6.Data.Database;
using X.PagedList;

namespace MVCHomework6.Controllers
{
    public class SearchController : Controller
    {
        private readonly BlogDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public SearchController(ILogger<HomeController> logger, BlogDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string SearchString)
        {
            var model = from item in _context.Articles
                        select item;

            if (!String.IsNullOrEmpty(SearchString))
            {
                var result = new ViewModelBase()
                {
                    ArticlesViewModel = model.Where(s => s.Title.Contains(SearchString) || s.Body.Contains(SearchString) || s.Tags.Contains(SearchString)).ToPagedList(1, model.Count()),
                    TagClouldViewModel = _context.TagCloud.ToList(),
                };

                //return RedirectToAction("Index", "Articles", result); //不知怎麼使用
                return View(result);
            }

            return View("SearchNotFound");
        }
    }
}