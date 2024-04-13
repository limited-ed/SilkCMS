using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SilkCMS.Core.Modules;

public interface IModule
{
        public string Title { get; }
        public string Description { get; }
        public string Author { get; }
        public void Initialize(IConfiguration configuration)
        {
           
        }

        public void ConfigureServices(IServiceCollection services)
        {
            /* Do your services configurations */
        }

        public void Configure(WebApplication app)
        {
            /* Do your application configurations  */
        }


        public void MapEntities(ModelBuilder builder)
        {

        }
}
