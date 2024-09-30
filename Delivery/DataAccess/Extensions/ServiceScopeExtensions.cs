using System;
using System.Threading.Tasks;
using Itmo.Dev.Platform.Postgres.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.DataAccess.Extensions;

public static class ServiceScopeExtensions
{
    public static async Task UseDataAccessAsync(this IServiceScope scope)
    {
        ArgumentNullException.ThrowIfNull(scope, nameof(scope));

        await scope
            .UsePlatformMigrationsAsync(default)
            .ConfigureAwait(false);
    }
}
