using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace SilkCMS.Core.ControllerModelConversion;

public class PrivateControllerModelConversion : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        var assembly = controller.ControllerType.Assembly.GetName();
        var policy = new AuthorizationPolicyBuilder().RequireRole("Administrator").Build();
        if (assembly.Name.Equals("SilkCMS.Administrator.Module"))
        {
            controller.Filters.Add(new AuthorizeFilter(policy));
            controller.RouteValues["area"] = "Private";
        }
    }
}