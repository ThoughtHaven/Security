using Microsoft.WindowsAzure.Storage.Table;

namespace ThoughtHaven.Security.SingleUseTokens.AzureTableStorage
{
    public class TableSingleUseTokenOptions
    {
        public string StorageAccountConnectionString { get; }

        private string _tableName = "SingleUseTokens";
        public string TableName
        {
            get { return this._tableName; }
            set { this._tableName = Guard.NullOrWhiteSpace(nameof(value), value); }
        }

        private TableRequestOptions _tableRequest = new TableRequestOptions();
        public TableRequestOptions TableRequest
        {
            get { return this._tableRequest; }
            set { this._tableRequest = Guard.Null(nameof(value), value); }
        }

        public TableSingleUseTokenOptions(string storageAccountConnectionString)
        {
            this.StorageAccountConnectionString = Guard.NullOrWhiteSpace(
                nameof(storageAccountConnectionString), storageAccountConnectionString);
        }
    }
}