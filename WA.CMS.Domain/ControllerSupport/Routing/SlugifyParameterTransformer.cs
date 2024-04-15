// https://www.hanselman.com/blog/aspnet-core-22-parameter-transformers-for-clean-url-generation-and-slugs-in-razor-pages-or-mvc
// https://github.com/dotnet/aspnetcore/issues/4578
// https://blog.verslu.is/aspnetcore/seo-friendly-urls-slug/

using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace WA.CMS.Domain.ControllerSupport.Routing;
public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object value)
    {
        if (value == null) { return null; }

        // Slugify value
        return Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
    }

    public string Format(string value)
    {
        if (value == null) { return null; }

        // Slugify value
        return Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
    }


}
