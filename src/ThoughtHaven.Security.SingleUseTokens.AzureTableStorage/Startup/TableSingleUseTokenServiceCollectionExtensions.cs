using Microsoft.Extensions.DependencyInjection.Extensions;
using ThoughtHaven;
using ThoughtHaven.Security.SingleUseTokens;
using ThoughtHaven.Security.SingleUseTokens.AzureTableStorage;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TableSingleUseTokenServiceCollectionExtensions
    {
        public static IServiceCollection AddSingleUseTokens(this IServiceCollection services,
            TableSingleUseTokenConfiguration configuration)
        {
            Guard.Null(nameof(services), services);
            Guard.Null(nameof(configuration), configuration);

            services.TryAddSingleton(configuration);
            services.TryAddSingleton<SystemClock>();
            services.TryAddTransient<ISingleUseTokenService, TableSingleUseTokenService>();

            return services;
        }
    }
}