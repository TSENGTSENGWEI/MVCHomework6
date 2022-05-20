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
            output.TagName = "ul";

            var actionContext = Accessor.ActionContext;
            if (actionContext == null) throw new ArgumentException("Context=null");
            var urlHelper = UrlHelperFactory.GetUrlHelper(actionContext);
            //

            var childContent = await output.GetChildContentAsync();
            var content = childContent.GetContent();

            var TagList = content.Split(',');
            var String = string.Empty;

            String = String + $"<li>";
            foreach (var tag in TagList)
            {
                var ActionURL = $@"""{urlHelper.Action(new UrlActionContext() { Controller = "Home", Action = "FindTag", Values = new { tag = tag } })}""";
                String = String + $@"<a href={ActionURL} class=""{"d-Inline p-2"}"">{tag}</a> ";
            }

            //output.Attributes.SetAttribute("href", urlHelper.Action(new UrlActionContext() { Controller = "Home", Action = "FindTag", Values = new { tag = content } }));
            String = String + $"</li>";
            output.Content.SetHtmlContent(String);

            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}