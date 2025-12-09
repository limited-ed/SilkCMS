#pragma warning disable CS1998 
using Microsoft.AspNetCore.Mvc;
using SilkCMS.Abstractions.Navigation;

namespace SilkCMS.Administrator.Module;

public class MenuItemComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(IMenuNode menuNode, bool rootLevel = false)

    {
        if (rootLevel)
        {
            if (menuNode.MenuNodes is null || !menuNode.MenuNodes.Any())
            {
                return View("Simple", menuNode);
            }

            return View("Dropdown", menuNode);
        }
        else
        {
            if (menuNode.MenuNodes is null || !menuNode.MenuNodes.Any())
            {
                return View("DropdownItem", menuNode);
            }

            return View("DropdownItemDropdown", menuNode);        
        }



    }

}
