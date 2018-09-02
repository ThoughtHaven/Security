using System;
using ThoughtHaven;
using ThoughtHaven.Security.SingleUseTokens;
using ThoughtHaven.Security.SingleUseTokens.AzureTableStorage;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class TableSingleUseTokenServiceCollectionExtensionsTests
    {
        public class AddSingleUseTokensMethod
        {
            public class ServicesAndConfigurationOverload
            {
                [Fact]
                public void NullServices_Throws()
                {
                    IServiceCollection services = null;

                    Assert.Throws<ArgumentNullException>("services", () =>
                    {
                        services.AddSingleUseTokens(
                            configuration: Configuration());
                    });
                }

                [Fact]
                public void NullOptions_Throws()
                {
                    Assert.Throws<ArgumentNullException>("configuration", () =>
                    {
                        Services().AddSingleUseTokens(configuration: null);
                    });
                }

                [Fact]
                public void WhenCalled_AddsConfiguration()
                {
                    var services = Services();
                    var options = Configuration();

                    services.AddSingleUseTokens(options);

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<TableSingleUseTokenConfiguration>();

                    Assert.Equal(options, service);
                }

                [Fact]
                public void WhenCalled_AddsSystemClock()
                {
                    var services = Services();

                    services.AddSingleUseTokens(Configuration());

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<SystemClock>();

                    Assert.NotNull(service);
                }

                [Fact]
                public void WhenCalled_AddsSingleUseTokenService()
                {
                    var services = Services();

                    services.AddSingleUseTokens(Configuration());

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<ISingleUseTokenService>();

                    Assert.IsType<TableSingleUseTokenService>(service);
                }

                [Fact]
                public void WhenCalled_ReturnsServices()
                {
                    var services = Services();

                    var result = services.AddSingleUseTokens(Configuration());

                    Assert.Equal(services, result);
                }
            }
        }

        private static IServiceCollection Services() => new ServiceCollection();
        private static TableSingleUseTokenConfiguration Configuration() =>
            new TableSingleUseTokenConfiguration("UseDevelopmentStorage=true;");
    }
}