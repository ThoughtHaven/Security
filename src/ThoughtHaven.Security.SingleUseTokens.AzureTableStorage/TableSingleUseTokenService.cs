using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
using ThoughtHaven.Azure.Storage.Table;
using ThoughtHaven.Data;
using ThoughtHaven.Security.SingleUseTokens.Internal;

namespace ThoughtHaven.Security.SingleUseTokens.AzureTableStorage
{
    public class TableSingleUseTokenService : SingleUseTokenServiceBase
    {
        protected TableCrudStore<SingleUseToken, SingleUseTokenModel> Store { get; }

        public TableSingleUseTokenService(TableSingleUseTokenOptions options,
            SystemClock clock)
            : this(BuildEntityStore(options), clock)
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

        protected override async Task<SingleUseTokenRecord?> Retrieve(SingleUseToken token)
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