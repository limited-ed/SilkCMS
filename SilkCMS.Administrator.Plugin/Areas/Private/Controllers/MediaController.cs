using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SilkCMS.Administrator.Plugin.Areas.Private.Controllers;

public class MediaController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ViewData["Page"] =  "Media";
        base.OnActionExecuting(context);
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }
}