using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;
using ThoughtHaven.Azure.Cosmos.Table;
using ThoughtHaven.Data;
using ThoughtHaven.Security.SingleUseTokens.Internal;

namespace ThoughtHaven.Security.SingleUseTokens.AzureCosmosTable
{
    public class TableSingleUseTokenService : SingleUseTokenServiceBase
    {
        protected TableCrudStore<SingleUseToken, SingleUseTokenRecord> Store { get; }

        public TableSingleUseTokenService(TableSingleUseTokenOptions options,
            SystemClock clock)
            : this(BuildEntityStore(options), clock)
        { }

        protected TableSingleUseTokenService(TableEntityStore entityStore, SystemClock clock)
            : this(new TableCrudStore<SingleUseToken, SingleUseTokenRecord>(
                entityStore: Guard.Null(nameof(entityStore), entityStore),
                dataKeyToEntityKeys: token => CreateKeys(token),
                dataToEntityKeys: model => CreateKeys(model.ToData())),
                  clock)
        { }

        protected TableSingleUseTokenService(
            TableCrudStore<SingleUseToken, SingleUseTokenRecord> store, SystemClock clock)
            : base(clock)
        {
            this.Store = Guard.Null(nameof(store), store);
        }

        protected override async Task<SingleUseTokenData?> Retrieve(SingleUseToken token)
        {
            Guard.Null(nameof(token), token);

            var model = await this.Store.Retrieve(token).ConfigureAwait(false);

            return model?.ToData();
        }

        protected override Task Create(SingleUseTokenData data)
        {
            Guard.Null(nameof(data), data);

            return this.Store.Create(new SingleUseTokenRecord(data));
        }

        protected override Task Delete(SingleUseTokenData data)
        {
            Guard.Null(nameof(data), data);

            return this.Store.Delete(data);
        }

        protected static TableEntityStore BuildEntityStore(
            TableSingleUseTokenOptions configuration)
        {
            Guard.Null(nameof(configuration), configuration);

            return new TableEntityStore(
                table: CloudStorageAccount.Parse(
                    configuration.StorageAccountConnectionString).CreateCloudTableClient()
                    .GetTableReference(configuration.TableName),
                options: configuration.TableRequest);
        }

        protected static TableEntityKeys CreateKeys(SingleUseToken token) =>
            new TableEntityKeys(Guard.Null(nameof(token), token).Value, "Token");
    }
}