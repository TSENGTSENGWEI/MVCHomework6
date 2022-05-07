using Microsoft.AspNetCore.Mvc;
using MVCHomework6.Models;
using System.Diagnostics;
using MVCHomework6.Data;
using MVCHomework6.Data.Database;
using X.PagedList;

namespace MVCHomework6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogDbContext _context;
        private int pageSize = 1;

        public HomeController(ILogger<HomeController> logger, BlogDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //這是範例，已經塞了20筆資料進去
            var model = _context.Articles.ToPagedList(1, pageSize);
            return View(model);
        }

        [Route("Index/{id?}")]
        public IActionResult Index(int id)
        {
            var model = _context.Articles.ToPagedList(id, pageSize);
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