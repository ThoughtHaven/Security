using Microsoft.WindowsAzure.Storage.Table;
using ThoughtHaven;

namespace Microsoft.Extensions.DependencyInjection
{
    public class TableSingleUseTokenOptions
    {
        private string _storageAccountConnectionString;
        public string StorageAccountConnectionString
        {
            get => this._storageAccountConnectionString;
            set
            {
                this._storageAccountConnectionString = Guard.NullOrWhiteSpace(nameof(value),
                    value);
            }
        }

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
            this._storageAccountConnectionString = Guard.NullOrWhiteSpace(
                nameof(storageAccountConnectionString), storageAccountConnectionString);
        }
    }
}