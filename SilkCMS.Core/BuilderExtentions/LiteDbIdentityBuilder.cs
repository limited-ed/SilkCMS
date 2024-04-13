using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SilkCMS.Data;
using SilkCMS.Data.Identity;

namespace SilkCMS.Core;

public static class LiteDbIdentityBuilder
{
    public static IdentityBuilder AddLiteDBIdentity(this IdentityBuilder builder, Action<LiteDbOption> configuration)
    {
        var options = new LiteDbOption();
        configuration?.Invoke(options);

        builder.Services.AddScoped<ILiteDbContext, AppDbContext>(c => new AppDbContext(options));

        builder.Services.AddScoped<IUserStore<User>, UserStore<User, Role, UserRole, UserClaim, UserLogin, UserToken>>();
        builder.Services.AddScoped<IRoleStore<Role>, RoleStore<Role, RoleClaim>>();

        return builder;
    }
}
