using Microsoft.AspNetCore.Mvc.RazorPages;
using SilkCMS.Abstractions.Navigation;
using SilkCMS.Core;

namespace SilkCMS.Web.Areas.Private.Pages;

public class IndexModel : PageModel
{
    private readonly IMenuBuilder _menuBuilder;
    public IndexModel(IMenuBuilder menuBuilder)
    {
        _menuBuilder = menuBuilder;
    }
}
