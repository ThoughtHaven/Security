using System.Threading.Tasks;
using ThoughtHaven.Data;
using ThoughtHaven.Security.SingleUseTokens.AzureCosmosTable;
using ThoughtHaven.Security.SingleUseTokens.Internal;

namespace ThoughtHaven.Security.SingleUseTokens.Fakes
{
    public class FakeTableSingleUseTokenService : TableSingleUseTokenService
    {
        new public FakeTableCrudStore Store => (FakeTableCrudStore)base.Store;
        public FakeSystemClock Clock { get; }

        public FakeTableSingleUseTokenService(FakeTableCrudStore store,
            FakeSystemClock clock)
            : base(store, clock)
        {
            this.Clock = clock;
        }

        new public Task<SingleUseTokenData?> Retrieve(SingleUseToken token) =>
            base.Retrieve(token);
        new public Task Create(SingleUseTokenData data) => base.Create(data);
        new public Task Delete(SingleUseTokenData data) => base.Delete(data);

        new public static TableEntityKeys CreateKeys(SingleUseToken token) =>
            TableSingleUseTokenService.CreateKeys(token);
    }
}