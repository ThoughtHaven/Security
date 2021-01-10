using System;
using ThoughtHaven;
using ThoughtHaven.Security.SingleUseTokens;
using ThoughtHaven.Security.SingleUseTokens.AzureCosmosTable;
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
                    IServiceCollection? services = null;

                    Assert.Throws<ArgumentNullException>("services", () =>
                    {
                        services!.AddSingleUseTokens(
                            options: Options());
                    });
                }

                [Fact]
                public void NullOptions_Throws()
                {
                    Assert.Throws<ArgumentNullException>("options", () =>
                    {
                        Services().AddSingleUseTokens(options: null!);
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

                [Fact]
                public void WhenCalled_ReturnsServices()
                {
                    var services = Services();

                    var result = services.AddSingleUseTokens(Options());

                    Assert.Equal(services, result);
                }
            }
        }

        private static IServiceCollection Services() => new ServiceCollection();
        private static TableSingleUseTokenOptions Options() =>
            new TableSingleUseTokenOptions("UseDevelopmentStorage=true;");
    }
}