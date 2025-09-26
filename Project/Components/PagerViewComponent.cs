using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Models;

namespace KooliProjekt.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PagedResultBase model, string viewName = "Default")
        {
            // Safely get the current action name - fall back to route data or empty string
            var action = RouteData?.Values["action"]?.ToString();
            if (string.IsNullOrEmpty(action))
            {
                // fallback to RouteValues (robust in some MVC contexts)
                action = ViewContext?.RouteData?.Values["action"]?.ToString() ?? string.Empty;
            }

            if (model != null)
            {
                model.LinkTemplate = Url.Action(action, new { page = "{0}" }) ?? string.Empty;
            }

            return await Task.FromResult(View(viewName, model));
        }
    }
}