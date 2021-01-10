using Microsoft.Extensions.DependencyInjection.Extensions;
using ThoughtHaven;
using ThoughtHaven.Security.SingleUseTokens;
using ThoughtHaven.Security.SingleUseTokens.AzureCosmosTable;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TableSingleUseTokenServiceCollectionExtensions
    {
        public static IServiceCollection AddSingleUseTokens(this IServiceCollection services,
            TableSingleUseTokenOptions options)
        {
            Guard.Null(nameof(services), services);
            Guard.Null(nameof(options), options);

            services.TryAddSingleton(options);
            services.TryAddSingleton<SystemClock>();
            services.TryAddTransient<ISingleUseTokenService, TableSingleUseTokenService>();

            return services;
        }
    }
}