using Microsoft.AspNetCore.Mvc;
using MVCHomework6.Data.Database;

namespace MVCHomework6.ViewComponets
{
    public class TagColudViewComponent : ViewComponent
    {
        private readonly BlogDbContext _context;

        public TagColudViewComponent(BlogDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var source = _context.TagCloud;
            ViewData.Model = source;
            return View();
        }
    }
}