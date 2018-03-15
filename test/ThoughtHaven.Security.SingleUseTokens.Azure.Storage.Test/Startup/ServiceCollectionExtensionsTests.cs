using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;
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
                            storageAccountConnectionString: ConnectionString());
                    });
                }

                [Fact]
                public void NullStorageAccountConnectionString_Throws()
                {
                    Assert.Throws<ArgumentNullException>("storageAccountConnectionString", () =>
                    {
                        Services().AddSingleUseTokens(
                            storageAccountConnectionString: null);
                    });
                }

                [Fact]
                public void EmptyStorageAccountConnectionString_Throws()
                {
                    Assert.Throws<ArgumentException>("storageAccountConnectionString", () =>
                    {
                        Services().AddSingleUseTokens(
                            storageAccountConnectionString: "");
                    });
                }

                [Fact]
                public void WhiteSpaceStorageAccountConnectionString_Throws()
                {
                    Assert.Throws<ArgumentException>("storageAccountConnectionString", () =>
                    {
                        Services().AddSingleUseTokens(
                            storageAccountConnectionString: " ");
                    });
                }

                [Fact]
                public void WhenCalled_AddsCloudStorageAccount()
                {
                    var services = Services();

                    services.AddSingleUseTokens(ConnectionString(), Options());

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<CloudStorageAccount>();

                    Assert.NotNull(service);
                }

                [Fact]
                public void OptionsNull_AddsOptions()
                {
                    var services = Services();

                    services.AddSingleUseTokens(ConnectionString(), options: null);

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<TableSingleUseTokenOptions>();

                    Assert.NotNull(service);
                }

                [Fact]
                public void OptionsNotNull_AddsOptions()
                {
                    var services = Services();
                    var options = Options();

                    services.AddSingleUseTokens(ConnectionString(), options);

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<TableSingleUseTokenOptions>();

                    Assert.Equal(options, service);
                }

                [Fact]
                public void WhenCalled_AddsSystemClock()
                {
                    var services = Services();

                    services.AddSingleUseTokens(ConnectionString());

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<SystemClock>();

                    Assert.NotNull(service);
                }

                [Fact]
                public void WhenCalled_AddsSingleUseTokenService()
                {
                    var services = Services();

                    services.AddSingleUseTokens(ConnectionString());

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<ISingleUseTokenService>();

                    Assert.IsType<TableSingleUseTokenService>(service);
                }
            }
        }

        private static IServiceCollection Services() => new ServiceCollection();
        private static string ConnectionString() => "UseDevelopmentStorage=true;";
        private static TableSingleUseTokenOptions Options() => new TableSingleUseTokenOptions();
    }
}