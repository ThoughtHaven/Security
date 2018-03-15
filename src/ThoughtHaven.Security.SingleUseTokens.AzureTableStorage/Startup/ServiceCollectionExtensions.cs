using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.WindowsAzure.Storage;
using ThoughtHaven;
using ThoughtHaven.Security.SingleUseTokens;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSingleUseTokens(this IServiceCollection services,
            string storageAccountConnectionString, TableSingleUseTokenOptions options = null)
        {
            Guard.Null(nameof(services), services);
            Guard.NullOrWhiteSpace(nameof(storageAccountConnectionString),
                storageAccountConnectionString);

            services.TryAddSingleton(CloudStorageAccount.Parse(storageAccountConnectionString));
            services.TryAddSingleton(options ?? new TableSingleUseTokenOptions());
            services.TryAddSingleton<SystemClock>();
            services.TryAddTransient<ISingleUseTokenService, TableSingleUseTokenService>();

            return services;
        }
    }
}