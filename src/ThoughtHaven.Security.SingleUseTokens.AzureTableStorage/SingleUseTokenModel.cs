using System;
using ThoughtHaven.Security.SingleUseTokens.Internal;

namespace ThoughtHaven.Security.SingleUseTokens.AzureTableStorage
{
    public class SingleUseTokenModel
    {
        public string? Value { get; set; }
        public DateTimeOffset? Expiration { get; set; }

        public SingleUseTokenModel() { }

        public SingleUseTokenModel(SingleUseTokenRecord record)
            : this()
        {
            Guard.Null(nameof(record), record);

            this.Value = record.Value;
            this.Expiration = record.Expiration.ToOffset();
        }

        public SingleUseTokenRecord ToRecord() =>
            new SingleUseTokenRecord(this.Value!, new UtcDateTime(this.Expiration!.Value));
    }
}