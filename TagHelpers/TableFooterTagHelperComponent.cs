using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;


namespace WebApp.TagHelpers;

[HtmlTargetElement("table")]
public class TableFooterSelector : TagHelperComponentTagHelper
{
	public TableFooterSelector(ITagHelperComponentManager mgr, ILoggerFactory log)
		: base(mgr, log) { }
}



public class TableFooterTagHelperComponent : TagHelperComponent
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (output.TagName == "table")
        {
            TagBuilder td = new TagBuilder("td");
            td.Attributes.Add("colspan", "3");
            td.Attributes.Add("class", "bg-dark text-white text-center");
            td.InnerHtml.Append("Table Footer");

            TagBuilder row = new TagBuilder("row");
            row.InnerHtml.AppendHtml(td);

            TagBuilder footer = new TagBuilder("tfoot");
            footer.InnerHtml.AppendHtml(row);

            output.PostContent.AppendHtml(footer);
        }
    }
}

