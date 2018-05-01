using System;
using Microsoft.Extensions.DependencyInjection;
using ThoughtHaven.Security.SingleUseTokens.AzureTableStorage;
using Xunit;

namespace ThoughtHaven.Security.SingleUseTokens.Startup
{
    public class ServiceCollectionExtensionsTests
    {
        public class AddSingleUseTokensMethod
        {
            public class ServicesAndStorageAccountConnectionStringAndOptionsOverload
            {
                [Fact]
                public void NullServices_Throws()
                {
                    IServiceCollection services = null;

                    Assert.Throws<ArgumentNullException>("services", () =>
                    {
                        services.AddSingleUseTokens(
                            options: Options());
                    });
                }

                [Fact]
                public void NullOptions_Throws()
                {
                    Assert.Throws<ArgumentNullException>("options", () =>
                    {
                        Services().AddSingleUseTokens(options: null);
                    });
                }

                [Fact]
                public void WhenCalled_AddsOptions()
                {
                    var services = Services();
                    var options = Options();

                    services.AddSingleUseTokens(options);

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<TableSingleUseTokenOptions>();

                    Assert.Equal(options, service);
                }

                [Fact]
                public void WhenCalled_AddsSystemClock()
                {
                    var services = Services();

                    services.AddSingleUseTokens(Options());

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<SystemClock>();

                    Assert.NotNull(service);
                }

                [Fact]
                public void WhenCalled_AddsSingleUseTokenService()
                {
                    var services = Services();

                    services.AddSingleUseTokens(Options());

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<ISingleUseTokenService>();

                    Assert.IsType<TableSingleUseTokenService>(service);
                }
            }
        }

        private static IServiceCollection Services() => new ServiceCollection();
        private static TableSingleUseTokenOptions Options() =>
            new TableSingleUseTokenOptions("UseDevelopmentStorage=true;");
    }
}