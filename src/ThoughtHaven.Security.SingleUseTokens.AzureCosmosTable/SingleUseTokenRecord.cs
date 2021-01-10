using System;
using ThoughtHaven.Security.SingleUseTokens.Internal;

namespace ThoughtHaven.Security.SingleUseTokens.AzureCosmosTable
{
    public record SingleUseTokenRecord
    {
        public string? Value { get; set; }
        public DateTimeOffset? Expiration { get; set; }

        public SingleUseTokenRecord() { }

        public SingleUseTokenRecord(SingleUseTokenData data)
            : this()
        {
            Guard.Null(nameof(data), data);

            this.Value = data.Value;
            this.Expiration = data.Expiration.ToOffset();
        }

        public SingleUseTokenData ToData() =>
            new SingleUseTokenData(this.Value!, new UtcDateTime(this.Expiration!.Value));
    }
}