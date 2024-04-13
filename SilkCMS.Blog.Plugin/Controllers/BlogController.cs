using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class BlogController : Controller
    {
        // GET: BlogController
        public ActionResult Index([FromQuery] string name)
        {
            return View((object)name);
        }

    }
}
