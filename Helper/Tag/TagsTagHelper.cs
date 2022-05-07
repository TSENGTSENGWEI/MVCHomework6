using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVCHomework6.Helper.Tag
{
    public class TagsTagHelper : TagHelper
    {
        private IUrlHelperFactory UrlHelperFactory { get; }

        private IActionContextAccessor Accessor { get; }

        public TagsTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor accessor)
        {
            UrlHelperFactory = urlHelperFactory ?? throw new ArgumentNullException(nameof(urlHelperFactory));
            Accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            var actionContext = Accessor.ActionContext;
            var urlHelper = UrlHelperFactory.GetUrlHelper(actionContext);
            //

            var childContent = await output.GetChildContentAsync();
            var content = childContent.GetContent();

            output.Attributes.SetAttribute("href", "http://www.google.com.tw");
            output.Content.SetContent(content);
        }
    }
}