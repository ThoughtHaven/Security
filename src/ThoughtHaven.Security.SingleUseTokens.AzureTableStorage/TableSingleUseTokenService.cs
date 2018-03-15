using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using ThoughtHaven.Azure.Storage.Table;
using ThoughtHaven.Data;
using ThoughtHaven.Security.SingleUseTokens.Internal;

namespace ThoughtHaven.Security.SingleUseTokens
{
    public class TableSingleUseTokenService : SingleUseTokenServiceBase
    {
        protected TableCrudStore<SingleUseToken, SingleUseTokenModel> Store { get; }

        public TableSingleUseTokenService(CloudStorageAccount account,
            TableSingleUseTokenOptions options, SystemClock clock)
            : this(BuildEntityStore(account, options), clock)
        { }

        protected TableSingleUseTokenService(TableEntityStore entityStore, SystemClock clock)
            : this(new TableCrudStore<SingleUseToken, SingleUseTokenModel>(
                entityStore: Guard.Null(nameof(entityStore), entityStore),
                dataKeyToEntityKeys: token => CreateKeys(token),
                dataToEntityKeys: model => CreateKeys(model.ToRecord())),
                  clock)
        { }

        protected TableSingleUseTokenService(
            TableCrudStore<SingleUseToken, SingleUseTokenModel> store, SystemClock clock)
            : base(clock)
        {
            this.Store = Guard.Null(nameof(store), store);
        }

        protected override async Task<SingleUseTokenRecord> Retrieve(SingleUseToken token)
        {
            Guard.Null(nameof(token), token);

            var model = await this.Store.Retrieve(token).ConfigureAwait(false);

            return model?.ToRecord();
        }

        protected override Task Create(SingleUseTokenRecord record)
        {
            Guard.Null(nameof(record), record);

            return this.Store.Create(new SingleUseTokenModel(record));
        }

        protected override Task Delete(SingleUseTokenRecord record)
        {
            Guard.Null(nameof(record), record);

            return this.Store.Delete(record);
        }

        private static TableEntityStore BuildEntityStore(CloudStorageAccount account,
            TableSingleUseTokenOptions options)
        {
            Guard.Null(nameof(account), account);
            Guard.Null(nameof(options), options);

            return new TableEntityStore(
                table: account.CreateCloudTableClient().GetTableReference(options.TableName),
                options: options.TableRequest);
        }

        private static TableEntityKeys CreateKeys(SingleUseToken token) =>
            new TableEntityKeys($"PK{token.Value}", $"RK{token.Value}");
    }
}