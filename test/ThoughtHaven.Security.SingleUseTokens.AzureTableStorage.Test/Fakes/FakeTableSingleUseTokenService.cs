using System.Threading.Tasks;
using ThoughtHaven.Data;
using ThoughtHaven.Security.SingleUseTokens.Internal;

namespace ThoughtHaven.Security.SingleUseTokens.Fakes
{
    public class FakeTableSingleUseTokenService : TableSingleUseTokenService
    {
        new public FakeTableCrudStore Store => (FakeTableCrudStore)base.Store;
        public FakeSystemClock Clock { get; }

        public FakeTableSingleUseTokenService(FakeTableCrudStore store, FakeSystemClock clock)
            : base(store, clock)
        {
            this.Clock = clock;
        }

        new public Task<SingleUseTokenRecord> Retrieve(SingleUseToken token) =>
            base.Retrieve(token);
        new public Task Create(SingleUseTokenRecord record) => base.Create(record);
        new public Task Delete(SingleUseTokenRecord record) => base.Delete(record);

        new public static TableEntityKeys CreateKeys(SingleUseToken token) =>
            TableSingleUseTokenService.CreateKeys(token);
    }
}