using Microsoft.Extensions.Configuration;
using SilkCMS.Core.Modules;

namespace SilkCMS.BlogPlugin;

public class Module : IModule
{
    public string Title => "Simple Blog";

    public string Description => "Siomple blog plugin for SilkCMS";

    public string Author => "Yaros Roman";


}
