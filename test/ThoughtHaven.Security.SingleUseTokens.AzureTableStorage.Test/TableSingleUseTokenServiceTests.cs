using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using ThoughtHaven.Security.SingleUseTokens.Fakes;
using ThoughtHaven.Security.SingleUseTokens.Internal;
using Xunit;

namespace ThoughtHaven.Security.SingleUseTokens.AzureTableStorage
{
    public class TableSingleUseTokenServiceTests
    {
        public class Type
        {
            [Fact]
            public void InheritsSingleUseTokenServiceBase()
            {
                Assert.True(typeof(SingleUseTokenServiceBase).IsAssignableFrom(
                    typeof(TableSingleUseTokenService)));
            }
        }

        public class Constructor
        {
            public class AccountAndOptionsAndClockOverload
            {
                [Fact]
                public void NullOptions_Throws()
                {
                    Assert.Throws<ArgumentNullException>("options", () =>
                    {
                        new TableSingleUseTokenService(
                            options: null,
                            clock: Clock());
                    });
                }

                [Fact]
                public void NullClock_Throws()
                {
                    Assert.Throws<ArgumentNullException>("clock", () =>
                    {
                        new TableSingleUseTokenService(
                            options: Options(),
                            clock: null);
                    });
                }
            }
        }

        public class RetrieveMethod
        {
            public class TokenOverload
            {
                [Fact]
                public async Task NullToken_Throws()
                {
                    await Assert.ThrowsAsync<ArgumentNullException>("token", async () =>
                    {
                        await Service().Retrieve(token: null);
                    });
                }

                [Fact]
                public async Task WhenCalled_CallsRetrieveOnStore()
                {
                    var service = Service();
                    var token = Token();

                    await service.Retrieve(token);

                    Assert.Equal(token, service.Store.Retrieve_InputToken);
                }

                [Fact]
                public async Task RetrieveOnStoreReturnsNull_ReturnsNull()
                {
                    var service = Service();
                    service.Store.Retrieve_Output = null;
                    var token = Token();

                    var result = await service.Retrieve(token);

                    Assert.Null(result);
                }

                [Fact]
                public async Task WhenCalled_ReturnsRecord()
                {
                    var service = Service();
                    var token = Token();

                    var result = await service.Retrieve(token);

                    Assert.Equal(service.Store.Retrieve_Output.Value, result.Value);
                    Assert.Equal(service.Store.Retrieve_Output.Expiration, result.Expiration);
                }
            }
        }

        public class CreateMethod
        {
            public class TokenOverload
            {
                [Fact]
                public async Task NullToken_Throws()
                {
                    await Assert.ThrowsAsync<ArgumentNullException>("record", async () =>
                    {
                        await Service().Create(record: null);
                    });
                }

                [Fact]
                public async Task WhenCalled_CallsCreateOnStore()
                {
                    var service = Service();
                    var record = Record();

                    await service.Create(record);

                    Assert.Equal(record.Value, service.Store.Create_InputModel.Value);
                    Assert.Equal(record.Expiration, service.Store.Create_InputModel.Expiration);
                }
            }
        }

        public class DeleteMethod
        {
            public class TokenOverload
            {
                [Fact]
                public async Task NullToken_Throws()
                {
                    await Assert.ThrowsAsync<ArgumentNullException>("record", async () =>
                    {
                        await Service().Delete(record: null);
                    });
                }

                [Fact]
                public async Task WhenCalled_CallsDeleteOnStore()
                {
                    var service = Service();
                    var record = Record();

                    await service.Delete(record);

                    Assert.Equal(record, service.Store.Delete_InputToken);
                }
            }
        }

        public class CreateEntityKeysMethod
        {
            public class TokenOverload
            {
                [Fact]
                public void NullToken_Throws()
                {
                    Assert.Throws<ArgumentNullException>("token", () =>
                    {
                        FakeTableSingleUseTokenService.CreateKeys(token: null);
                    });
                }

                [Fact]
                public void WhenCalled_ReturnsKeys()
                {
                    var token = new SingleUseToken("value");

                    var keys = FakeTableSingleUseTokenService.CreateKeys(token);

                    Assert.Equal(token.Value, keys.PartitionKey);
                    Assert.Equal("Token", keys.RowKey);
                }
            }
        }
        
        private static TableSingleUseTokenOptions Options() =>
            new TableSingleUseTokenOptions("UseDevelopmentStorage=true;");
        private static FakeTableCrudStore Store() => new FakeTableCrudStore();
        private static FakeSystemClock Clock() => new FakeSystemClock(DateTimeOffset.UtcNow);
        private static FakeTableSingleUseTokenService Service(
            FakeTableCrudStore store = null, FakeSystemClock clock = null) =>
            new FakeTableSingleUseTokenService(store ?? Store(), clock ?? Clock());
        private static SingleUseToken Token() => new SingleUseToken("value");
        private static SingleUseTokenRecord Record() =>
            new SingleUseTokenRecord(Token(), DateTimeOffset.UtcNow);
    }
}